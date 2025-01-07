namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class AdoptionPage : ContentPage
{
	private readonly MyAdoptionsViewModel _viewModel;
    public AdoptionPage(MyAdoptionsViewModel myAdoptionsViewModel)
	{
		InitializeComponent();
        _viewModel = myAdoptionsViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAsync();
    }
}