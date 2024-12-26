namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class LoginRegisterPage : ContentPage
{
	private readonly LoginRegisterViewModel _loginRegisterModel;
    public LoginRegisterPage(LoginRegisterViewModel loginRegisterModel)
	{
		InitializeComponent();
        _loginRegisterModel = loginRegisterModel;
        // Set the BindingContext
        BindingContext = _loginRegisterModel;
    }
}