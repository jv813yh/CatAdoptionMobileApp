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
                await Shell.Current.GoToAsync(nameof(AdoptionPage));
                break;
            case "Change Password":
                await _viewModel.ChangePasswordCommand.ExecuteAsync(null);
                break;
            default:
                break;
        }
    }
}