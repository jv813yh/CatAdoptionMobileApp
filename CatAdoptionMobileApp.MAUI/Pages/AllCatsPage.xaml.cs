namespace CatAdoptionMobileApp.MAUI.Pages;

public partial class AllCatsPage : ContentPage
{
	private readonly AllCatsViewModel _viewModel;
    public AllCatsPage(AllCatsViewModel allCatsViewModel)
	{
		InitializeComponent();
        _viewModel = allCatsViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.InitializeAllCatsViewModelAsync();
    }
}