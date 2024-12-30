namespace CatAdoptionMobileApp.MAUI.Services
{
    public class AuthProvider : IAuthService
    {
        // Inject the CommonService and IAuthApi for login and register.
        private readonly CommonService _commonService;
        private readonly IAuthApi _authApi;

        public AuthProvider(CommonService commonService, IAuthApi authApi)
        {
            _commonService = commonService;
            _authApi = authApi;
        }

        // Check if the user is logged in
        public bool IsLoggedIn 
            => Preferences.Default.ContainsKey(UIConstants.UserInfo);

        /// <summary>
        /// Login or register the user based on the LoginRegisterModel
        /// </summary>
        /// <param name="loginRegisterModel"></param>
        /// <returns> Return the response status </returns>
        public async Task<bool> LoginRegisterAsync(LoginRegisterModel loginRegisterModel, bool isRegistration)
        {
            Shared.Dtos.ApiResponse<AuthResponseDto> response;

            try
            {
                if (isRegistration)
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
                    await App.Current.MainPage.DisplayAlert("Login/Register alert: ", response.Message, "OK");
                    Debug.WriteLine($"Error - Login/Register:\nData: {response.Data}\nwith message: {response.Message}");
                    return response.IsSuccess;
                }

                // Create a new LoggedInUser object and save the user info to the preferences
                SaveUserToPreferences(new LoggedInUser(response.Data.UserId, response.Data.Name, response.Data.Token));
                // Save the token to the CommonService
                _commonService.SetToken(response.Data.Token);
            }
            catch (ApiException apiEx)
            {
                await App.Current.MainPage.DisplayAlert("Login/Register alert: ", apiEx.Message, "OK");
                Debug.WriteLine($"Error - Login/Register: {apiEx.Content}");
                return false;
            }

            // Return the response status
            return true;
        }

        /// <summary>
        /// Logout the user
        /// </summary>
        /// <returns></returns>
        public async Task LogoutAsync()
        {
            _commonService.SetToken(null);
            RemoveUserFromPreferences();
        }

        public LoggedInUser? GetUserInfo()
        {
            if (IsLoggedIn)
            {
                var userJson = Preferences.Default.Get(UIConstants.UserInfo, string.Empty);

                return LoggedInUser.LoadFromJson(userJson);
            }

            return null;
        }

        // Save the user info to the preferences
        private void SaveUserToPreferences(LoggedInUser loggedInUser)
            => Preferences.Default.Set(UIConstants.UserInfo, loggedInUser.ToJson());

        // Remove the user info from the preferences
        private void RemoveUserFromPreferences()
            => Preferences.Default.Remove(UIConstants.UserInfo);
    }
}
