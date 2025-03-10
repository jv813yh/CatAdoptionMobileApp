﻿namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    [QueryProperty(nameof(IsFirstTime), nameof(IsFirstTime))]
    public partial class LoginRegisterViewModel : BaseViewModel
    {
        // Injected services for authentication, logout, user info ...
        private readonly IAuthService _authService;

        [ObservableProperty]
        private bool _isRegistrationMode;

        [ObservableProperty]
        private LoginRegisterModel _loginRegisterModel;

        [ObservableProperty]
        private bool _isFirstTime;

        public LoginRegisterViewModel(IAuthService authService)
        {
            _authService = authService;
            _loginRegisterModel = new LoginRegisterModel();
        }

        partial void OnIsFirstTimeChanging(bool value)
        {
            if (value)
            {
                IsRegistrationMode = true;
            }
        }

        private bool ValidateInput()
        {
            if (IsRegistrationMode &&
                string.IsNullOrWhiteSpace(LoginRegisterModel.Name)
                || string.IsNullOrWhiteSpace(LoginRegisterModel.Email)
                || string.IsNullOrWhiteSpace(LoginRegisterModel.Password))
            {
                return false;
            }
            else if (string.IsNullOrWhiteSpace(LoginRegisterModel.Email)
                     || string.IsNullOrWhiteSpace(LoginRegisterModel.Password))
            {
                return false;
            }

            return true;
        }

        [RelayCommand]
        private async Task SkipForNowAsync()
        {
           await GoToPageAsync($"//{nameof(HomePage)}");
        }

        [RelayCommand]  
        private async Task SubmitAsync()
        {
            try
            {
                if(!ValidateInput())
                {
                    await ShowToastMessageAsync("Please fill all the fields");
                    return;
                }

                SetTrueBoolValues();

                // Make Api call to login or register user 
                var response = await _authService.LoginRegisterAsync(LoginRegisterModel, IsRegistrationMode);

                if(response)
                {
                    SetFalseBoolValues();
                    await SkipForNowAsync();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Eror while submit command: {ex.Message}");
                await ShowAlertMessageAsync("Error", "An error occured while processing your request", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]
        private void ToogleMode() 
            => IsRegistrationMode = !IsRegistrationMode;

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
