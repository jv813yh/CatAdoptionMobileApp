namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public partial class AllCatsViewModel : BaseViewModel
    {
        private readonly ICatApi _catApi;
        private bool _isInitialized;

        [ObservableProperty]
        private bool _isRefreshing;

        [ObservableProperty]
        private IEnumerable<CatListDto> _allCats;

        public AllCatsViewModel(ICatApi catApi)
        {
            _catApi = catApi;
            _allCats = Enumerable.Empty<CatListDto>();
            _isInitialized = false;
        }

        public async Task InitializeAllCatsViewModelAsync()
        {
            if(_isInitialized)
            {
                return;
            }

            await LoadAllCatsAsync(true);
        }

        [RelayCommand]
        private async Task RefreshAllCatsViewModelAsync()
        {
            await LoadAllCatsAsync(false);
        }

        private async Task LoadAllCatsAsync(bool initialLoad)
        {
            if (initialLoad)
            {
                SetTrueBoolValues();
            }
            else 
            {
                _isRefreshing = true;
            }

            try
            {
                var response = await _catApi.GetAllCatsAsync();
                if (response.IsSuccess)
                {
                    AllCats = response.Data;
                    _isInitialized = true;
                }
                else
                {
                    await ShowAlertMessageAsync("Problem", "Problem while loading all cats", "Ok");
                }
            }
            catch (ApiException apiEx)
            {
                await ShowAlertMessageAsync("Error", "Error while loading all cats", "Ok");
                Debug.WriteLine($"ApiException from AllCatsViewModel: {apiEx.Message}");
            }
            finally
            {
                if (initialLoad)
                {
                    SetFalseBoolValues();
                }
                else
                {
                    _isRefreshing = false;
                }
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
