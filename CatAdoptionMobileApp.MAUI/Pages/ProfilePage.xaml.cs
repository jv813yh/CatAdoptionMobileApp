namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class ProfilePage : ContentPage
{
	private readonly ProfileViewModel _viewModel;
    public ProfilePage(ProfileViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;

        BindingContext = _viewModel;
    }

    private async void OnProfilGrid_Tapped(object sender, string tappedOption)
    {
        switch(tappedOption)
        {
            case "My Adoptions":
                await _viewModel.ShowToastMessageAsync("My Adoptions");
                break;
            case "Change Password":
                await _viewModel.ShowToastMessageAsync("Change Password");
                break;
            default:
                break;
        }
    }
}