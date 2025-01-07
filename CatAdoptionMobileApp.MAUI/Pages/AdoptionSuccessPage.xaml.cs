namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class AdoptionSuccessPage : ContentPage
{
	public AdoptionSuccessPage()
	{
		InitializeComponent();
	}

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        await Task.Delay(100);

        // Animation 

        await VerificationImage.ScaleTo(1.2, 100);
        await VerificationImage.ScaleTo(0.6, 100);
        await VerificationImage.ScaleTo(1.2, 100);
        await VerificationImage.ScaleTo(0.6, 100);
        await VerificationImage.ScaleTo(1.2, 100);
        await VerificationImage.ScaleTo(1, 100);

        await AdoptionLabel.ScaleTo(0.4, 100);
        await AdoptionLabel.ScaleTo(0.6, 100);
        await AdoptionLabel.ScaleTo(0.8, 100);
        await AdoptionLabel.ScaleTo(1, 100);

        await ExploreMoreCatsButton.ScaleTo(1.5, 100);
        await ExploreMoreCatsButton.ScaleTo(0.5, 100);
        await ExploreMoreCatsButton.ScaleTo(1, 100);
        await ExploreMoreCatsButton.ScaleTo(1.5, 100);
        await ExploreMoreCatsButton.ScaleTo(0.5, 100);
        await ExploreMoreCatsButton.ScaleTo(1, 100);
    }

    private async void OnBackToHomeClicked(object sender, EventArgs e)
    {
		await Shell.Current.GoToAsync($"//{nameof(HomePage)}");
    }
}