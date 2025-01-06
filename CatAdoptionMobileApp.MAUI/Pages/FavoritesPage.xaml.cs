namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class FavoritesPage : ContentPage
{
	private readonly FavoritesViewModel _viewModel;
    public FavoritesPage(FavoritesViewModel favoritesViewModel)
	{
		InitializeComponent();
        BindingContext = _viewModel = favoritesViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
    }
}