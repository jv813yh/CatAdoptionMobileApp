using CatAdoptionMobileApp.MAUI.Pages;

namespace CatAdoptionMobileApp.MAUI
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
        }


        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if(Preferences.Default.ContainsKey(UIConstants.OnBoardingShown))
            {
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(OnboardingPage)}");
            }
        }
    }
}
