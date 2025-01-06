namespace CatAdoptionMobileApp.MAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("Ubuntu-Bold.ttf", "UbuntuBold");
                fonts.AddFont("Ubuntu-Regular.ttf", "UbuntuRegular");
            })
            .UseMauiCommunityToolkit();
#if DEBUG
            builder.Logging.AddDebug();
#endif
            // Register app dependencies
            RegisterAppDependencies(builder.Services);

            // Configure Refit client
            ConfigureRefit(builder.Services);

            return builder.Build();
        }

        /// <summary>
        /// Register the dependencies for the app
        /// </summary>
        /// <param name="serviceCollection"></param>
        static void RegisterAppDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LoginRegisterViewModel>() // Register the LoginRegisterViewModel as transient
                             .AddTransient<LoginRegisterPage>() // Register the LoginRegisterPage as transient
                             .AddTransient<HomePage>() // Register the HomePage as transient
                             .AddTransient<HomeViewModel>() // Register the HomeViewModel as transient
                             .AddSingleton<CommonService>() // Register the CommonService as singleton
                             .AddSingleton<AllCatsViewModel>() // Register the AllCatsViewModel as singleton
                             .AddTransient<AllCatsPage>() // Register the AllCatsPage as transient
                             .AddTransient<ProfilePage>() // Register the ProfilePage as transienttransient
                             .AddSingleton<ProfileViewModel>() // Register the ProfileViewModel as singleton
                             .AddTransient<FavoritesViewModel>() // Register the FavoritesViewModel as transient
                             .AddTransient<FavoritesPage>() // Register the FavoritesPage as transient
                             .AddTransient<AdoptionSuccessPage>() // Register the FavoritesPage as transient
                             .AddTransientWithShellRoute<DetailsPage, DetailsViewModel>(nameof(DetailsPage)) // Register the DetailsPage
                                                                                                             // and DetailsViewModel as transient with shell route
                             .AddSingleton<IAuthService, AuthProvider>(); // Register the AuthProvider as IAuthService as singleton
        }

        /// <summary>
        /// Configure Refit client for the app
        /// </summary>
        /// <param name="services"></param>
        static void ConfigureRefit(IServiceCollection services)
        {
            // Register IAuthApi with BaseAddress for authentication and authorization
            services.AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(SetBaseApiAddressHttpClient);

            // Register ICatApi with BaseAddress for working with cats data
            services.AddRefitClient<ICatApi>()
                .ConfigureHttpClient(SetBaseApiAddressHttpClient);

            // Register IUserApi with authorization token and for working with user data
            services.AddRefitClient<IUserApi>(sp =>
            {
                return new RefitSettings()
                {
                    AuthorizationHeaderValueGetter = (_,__) =>
                    {
                        // Get the authorization token from the CommonService
                        return Task.FromResult(sp.GetRequiredService<CommonService>().Token ?? string.Empty);
                    }
                };
            })
                .ConfigureHttpClient(SetBaseApiAddressHttpClient);
        }

        /// <summary>
        /// Set the base URL for the API address for the HttpClient client
        /// </summary>
        /// <param name="httpClient"></param>
        static void SetBaseApiAddressHttpClient(HttpClient httpClient)
        {
            httpClient.BaseAddress = new Uri(AppConstants.BaseApiUrl);
        }
    }
}