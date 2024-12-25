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

            RegisterAppDependencies(builder.Services);

            return builder.Build();
        }

        /// <summary>
        /// Register the dependencies for the app
        /// </summary>
        /// <param name="serviceCollection"></param>
        static void RegisterAppDependencies(IServiceCollection serviceCollection)
        {
            serviceCollection.AddTransient<LoginRegisterViewModel>()
                             .AddTransient<LoginRegisterPage>();
        }
    }
}