namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public partial class MyAdoptionsViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly IUserApi _userApi;

        [ObservableProperty]
        private double _totalSum;

        [ObservableProperty]
        public IEnumerable<CatListDto> _myAdoptions;

        public MyAdoptionsViewModel(IAuthService authService, IUserApi userApi)
        {
            _authService = authService;
            _userApi = userApi;

            _myAdoptions = Enumerable.Empty<CatListDto>();
        }

        public async Task InitializeAsync()
        {
            if(!_authService.IsLoggedIn)
            {
                await ShowToastMessageAsync("Please login to view your adoptions");
            }

            try
            {
                SetTrueBoolValues();
                // Get the user's adoption cats
                var apiResponse = await _userApi.GetUserAdoptionCatsAsync();
                if(apiResponse.IsSuccess)
                {
                    if(apiResponse.Data != null)
                    {
                        // Set adoptions and total sum
                        _myAdoptions = apiResponse.Data;
                        TotalSum = _myAdoptions
                                   .Select(c => c.Price)
                                   .Sum();
                    }
                }
                else
                {
                    await ShowAlertMessageAsync("Error", "Error while initializing My Adoptions from server", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InitializeAsync: {ex.Message}");
                await ShowAlertMessageAsync("Error", "Error while initializing My Adoptions", "Ok");
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
