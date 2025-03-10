﻿namespace CatAdoptionMobileApp.MAUI.Services
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
                // Notify the subscribers that the login status has changed
                _commonService.NotifyLoginStatusChanged();
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
        public void Logout()
        {
            // Set the token to null
            _commonService.SetToken(null);
            // Remove the user info from the preferences
            RemoveUserFromPreferences();
            // Notify the subscribers that the login status has changed
            _commonService.NotifyLoginStatusChanged();
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

        /// <summary>
        /// Change the user password async
        /// </summary>
        /// <param name="emailAddres"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordAsync(ChangePasswordModel tryChangePassword)
        {
            // Create a new ChangePasswordDto object and send it to the API
            var apiResponse = await _authApi.ChangePasswordAsync(new ChangePasswordDto() 
            { 
                Email = tryChangePassword.Email,
                Password = tryChangePassword.Password,
                NewPassword = tryChangePassword.NewPassword
            });

            // Check if the response is successful or not
            if (apiResponse.IsSuccess)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
