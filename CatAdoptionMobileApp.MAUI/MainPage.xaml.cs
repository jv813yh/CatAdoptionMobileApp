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

            //await Shell.Current.GoToAsync($"//{nameof(OnboardingPage)}");

            if (Preferences.Default.ContainsKey(UIConstants.OnBoardingShown))
            {
                await Shell.Current.GoToAsync($"//{nameof(LoginRegisterPage)}");
            }
            else
            {
                await Shell.Current.GoToAsync($"//{nameof(OnboardingPage)}");
            }
        }
    }
}
