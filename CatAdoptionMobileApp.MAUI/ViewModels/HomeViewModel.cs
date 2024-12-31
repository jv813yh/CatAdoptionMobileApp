namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ICatApi _catApi;
        [ObservableProperty]
        private IEnumerable<CatListDto> _newAddedCats;
        [ObservableProperty]
        private IEnumerable<CatListDto> _popularCats;
        [ObservableProperty]
        private IEnumerable<CatListDto> _randomCats;

        [ObservableProperty]
        private string _userName = "Jozko123";


        public HomeViewModel(ICatApi catApi)
        {
            _catApi = catApi;
            _newAddedCats = Enumerable.Empty<CatListDto>();
            _popularCats = Enumerable.Empty<CatListDto>();
            _randomCats = Enumerable.Empty<CatListDto>();
            _isInitialized = false;
        }

        private bool _isInitialized;
        public async Task InitializeHomeViewModelAsync()
        {
            if (_isInitialized)
            {
                return;
            }

            try
            {
                SetTrueBoolValues();

                var responsePopularCats = await _catApi.GetPopularCatsAsync(5);
                if (responsePopularCats.IsSuccess)
                {
                    PopularCats = responsePopularCats.Data;
                }

                var responseNewAddedCats = await _catApi.GetNewAddedCatsAsync(5);
                if (responseNewAddedCats.IsSuccess)
                {
                    NewAddedCats = responseNewAddedCats.Data;
                }

                var responseRandomCats = await _catApi.GetRandomCatsAsync(5);
                if (responseRandomCats.IsSuccess)
                {
                    RandomCats = responseRandomCats.Data;
                }

                _isInitialized = true;
            }
            catch (ApiException apiEx)
            {
                await ShowAlertMessageAsync("Error while loading home page", apiEx.Message, "Ok");
                Debug.WriteLine($"Error in InitializeHomeViewModelAsync: {apiEx.Content}"); 
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]
        private async Task GoToDetailsPageAsync(int catId)
        {
            try
            {
                SetTrueBoolValues();
                await GoToPageAsync($"{nameof(DetailsPage)}?{nameof(DetailsViewModel.CatId)}={catId}");
            }
            catch (Exception ex)
            {
                await ShowAlertMessageAsync("Error", "Error while navigating to details page", "Ok");
                Debug.WriteLine($"Error in GoToDetailsPageAsync: {ex.Message}");
                return;
            }
            finally
            {
                SetFalseBoolValues();
            }
        }


        protected override void SetFalseBoolValues()
        {
            IsBusy = false;
        }

        protected override void SetTrueBoolValues()
        {
            IsBusy = true;
        }
    }
}
