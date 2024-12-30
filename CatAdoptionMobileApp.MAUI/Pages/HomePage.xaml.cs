namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class HomePage : ContentPage
{
	private readonly HomeViewModel _viewModel;
    public HomePage(HomeViewModel homeViewModel)
	{
		InitializeComponent();
        _viewModel = homeViewModel;

        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeHomeViewModelAsync();
    }
}