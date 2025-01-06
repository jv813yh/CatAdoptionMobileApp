namespace CatAdoptionMobileApp.MAUI
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(AdoptionSuccessPage), typeof(AdoptionSuccessPage));
        }
    }
}
