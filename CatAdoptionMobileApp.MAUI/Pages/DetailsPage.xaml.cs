namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class DetailsPage : ContentPage
{
	private readonly DetailsViewModel _viewModel;
    public DetailsPage(DetailsViewModel detailsViewModel)
	{
		InitializeComponent();
        _viewModel = detailsViewModel;
        BindingContext = _viewModel;
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.DisposeAsync();
    }
}