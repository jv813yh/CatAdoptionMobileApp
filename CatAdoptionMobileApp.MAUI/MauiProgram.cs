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
            serviceCollection.AddTransient<LoginRegisterViewModel>()
                             .AddTransient<LoginRegisterPage>()
                             .AddSingleton<CommonService>()
                             .AddSingleton<IAuthService, AuthProvider>()
                             .AddSingleton<AuthProvider>();
        }

        /// <summary>
        /// Configure Refit client for the app
        /// </summary>
        /// <param name="services"></param>
        static void ConfigureRefit(IServiceCollection services)
        {
            // Register IAuthApi with BaseAddress
            services.AddRefitClient<IAuthApi>()
                .ConfigureHttpClient(SetBaseApiAddressHttpClient);

            // Register ICatApi with BaseAddress
            services.AddRefitClient<ICatApi>()
                .ConfigureHttpClient(SetBaseApiAddressHttpClient);

            // Register IUserApi with authorization token
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