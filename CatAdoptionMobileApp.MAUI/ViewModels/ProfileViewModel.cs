namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;
        private readonly CommonService _commonService;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initials))]
        private string _userName = AppConstants.NotLoggedInUserMessage;

        [ObservableProperty]
        private bool _isLoggedIn = false;

        public string Initials
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_userName))
                {
                    return string.Empty;
                }

                var names = _userName.Split(' ');

                if(UserName.Length == 1)
                {
                    return names[0].ToUpper();
                }

                if(names.Length == 1)
                {
                    return names[0].Substring(0, 2).ToUpper();
                }
                else
                {
                    return $"{names[0][0]}{names[1][0]}".ToUpper();
                }
            }
        }

        public ProfileViewModel(IAuthService authService,
                                CommonService commonService)
        {
            _authService = authService;
            _commonService = commonService;


            // Subscribe to the LoginStatusChanged event
            _commonService.LoginStatusChanged += OnLoginStatusChanged;
            // Set the user info
            SetUserInfo();
        }


        private void OnLoginStatusChanged(object? sender, EventArgs e)
        {
            SetUserInfo();
        }

        private void SetUserInfo()
        {
            if (_authService.IsLoggedIn)
            {
                var loggedInUser = _authService.GetUserInfo();
                if (loggedInUser != null)
                {
                    UserName = loggedInUser.Name.ToUpper();
                    IsLoggedIn = true;
                    // Set the token
                    _commonService.SetToken(loggedInUser.Token);
                }
            }
            else
            {
                UserName = AppConstants.NotLoggedInUserMessage;
                IsLoggedIn = false;
            }
        }

        [RelayCommand]  
        private async Task LoginLogoutAsync()
        {
            try
            {
                SetTrueBoolValues();

                if (IsLoggedIn)
                {
                    _authService.Logout();
                    IsLoggedIn = false;
                    //_commonService.LoginStatusChanged -= OnLoginStatusChanged;
                    await GoToPageAsync($"//{nameof(HomePage)}");
                }
                else
                {
                    //_commonService.LoginStatusChanged -= OnLoginStatusChanged;
                    await GoToPageAsync($"//{nameof(LoginRegisterPage)}");
                }
            }
            catch (Exception ex)
            {

                Debug.WriteLine($"Error in LoginLogoutAsync: {ex.Message}");
                await ShowAlertMessageAsync("Error", "Error while logging in or out", "Ok");
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
