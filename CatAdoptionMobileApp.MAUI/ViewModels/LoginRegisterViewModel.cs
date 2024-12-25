namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    [QueryProperty(nameof(IsFirstTime), nameof(IsFirstTime))]
    public partial class LoginRegisterViewModel : BaseViewModel
    {
        [ObservableProperty]
        private bool _isRegistrationMode;

        [ObservableProperty]
        private LoginRegisterModel _loginRegisterModel;

        [ObservableProperty]
        private bool? _isFirstTime;

        public void Initialize()
        {
            if(IsFirstTime.HasValue && IsFirstTime.Value)
            {
                IsRegistrationMode = true;
            }
        }

        [RelayCommand]
        private async Task SkipForNowAsync()
        {
            try
            {
                SetTrueBoolValues();
                await Shell.Current.GoToAsync($"//{nameof(HomePage)}");

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Eror while skip for now:{ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while skip for now", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]  
        private async Task SubmitAsync()
        {
            try
            {
                if(LoginRegisterModel.Validate(IsRegistrationMode))
                {
                    await Toast.Make("All fields are mandatory").Show();

                    return;
                }


                // Make Api call to login or register user

                await SkipForNowAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Eror while submit command: {ex.Message}");
                await Shell.Current.DisplayAlert("Error", "An error occurred while submit command", "Ok");
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
