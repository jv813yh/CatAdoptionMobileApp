namespace CatAdoptionMobileApp.MAUI.Services
{
    public class AuthProvider
    {
        // Inject the CommonService and IAuthApi for login and register.
        private readonly CommonService _commonService;
        private readonly IAuthApi _authApi;

        public AuthProvider(CommonService commonService, IAuthApi authApi)
        {
            _commonService = commonService;
            _authApi = authApi;
        }

        public async Task LoginRegisterAsync(LoginRegisterModel loginRegisterModel)
        {
            Shared.Dtos.ApiResponse<AuthResponseDto> response;

            if (loginRegisterModel.IsNewUser)
            {
                // Try to register the user
                response = await _authApi.RegisterAsync(new RegisterRequestDto
                {
                    Name = loginRegisterModel.Name,
                    Email = loginRegisterModel.Email,
                    Password = loginRegisterModel.Password
                });
            }
            else
            {
                // Try to login the user
                response = await _authApi.LoginAsync(new LoginRequestDto
                {
                    Email = loginRegisterModel.Email,
                    Password = loginRegisterModel.Password
                });
            }

            // Check if the response is not successful
            if (!response.IsSuccess)
            {
                // Handle the error
                await App.Current.MainPage.DisplayAlert("Error - Api", response.Message, "OK");
                return;
            }

            // Create a new LoggedInUser object
            var LoggedInUser = new LoggedInUser(response.Data.UserId, response.Data.Name, response.Data.Token);
            // Save the user info to the preferences
            SaveUserToPreferences(LoggedInUser);
            // Save the token to the CommonService
            _commonService.SetToken(response.Data.Token);
        }

        public async Task LogoutAsync()
        {
        }

        public void GetUserInfo()
        {
        }

        // Save the user info to the preferences
        private void SaveUserToPreferences(LoggedInUser loggedInUser)
            => Preferences.Default.Set(UIConstants.UserInfo, loggedInUser.ToJson());
    }
}
