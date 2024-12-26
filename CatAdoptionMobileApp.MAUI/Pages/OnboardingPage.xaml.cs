namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class OnboardingPage : ContentPage
{
	public OnboardingPage()
	{
		InitializeComponent();

        // Set the onboarding shown flag string.empty
        Preferences.Default.Set(UIConstants.OnBoardingShown, string.Empty);	
    }

    /// <summary>
    /// Navigate to login / register page
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private async void ButtonClicked(object sender, EventArgs e)
    {
        var parameters = new Dictionary<string, object>
        {
            [nameof(LoginRegisterViewModel.IsFirstTime)] = true
        };

        await Shell.Current.GoToAsync($"//{nameof(LoginRegisterPage)}", true, parameters);
    }
}