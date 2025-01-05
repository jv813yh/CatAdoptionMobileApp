
namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public partial class HomeViewModel : BaseViewModel
    {
        private readonly ICatApi _catApi;
        private readonly CommonService _commonService;
        private readonly IAuthService _authService;

        [ObservableProperty]
        private IEnumerable<CatListDto> _newAddedCats;
        [ObservableProperty]
        private IEnumerable<CatListDto> _popularCats;
        [ObservableProperty]
        private IEnumerable<CatListDto> _randomCats;

        [ObservableProperty]
        private string _userName = AppConstants.NotLoggedInUserMessage;


        public HomeViewModel(ICatApi catApi, 
                             CommonService commonService,
                             IAuthService authService)
        {
            _catApi = catApi;
            _commonService = commonService;
            _authService = authService;
            _isInitialized = false;

            _newAddedCats = Enumerable.Empty<CatListDto>();
            _popularCats = Enumerable.Empty<CatListDto>();
            _randomCats = Enumerable.Empty<CatListDto>();

            // Subscribe to the LoginStatusChanged event
            _commonService.LoginStatusChanged += OnLoginStatusChanged;
            // Set the user info
            SetUserInfo();
        }

        private void OnLoginStatusChanged(object? sender, EventArgs e)
         => SetUserInfo();

        private void SetUserInfo()
        {
            if (_authService.IsLoggedIn)
            {
                var loggedInUser = _authService.GetUserInfo();
                if (loggedInUser != null)
                {
                    UserName = loggedInUser.Name;
                    // Set the token
                    _commonService.SetToken(loggedInUser.Token);
                }
            }
            else
            {
                UserName = AppConstants.NotLoggedInUserMessage;
            }
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
