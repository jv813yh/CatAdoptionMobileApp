namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public abstract partial class BaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title = string.Empty;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        private bool _isBusy;

        public bool IsNotBusy 
            => !_isBusy;

        [ObservableProperty]
        private LoginRegisterModel _loginRegisterModel;

        #region Show Message Methods
        protected async Task ShowToastMessageAsync(string message)
            => await Toast.Make(message).Show();
        protected async Task ShowAlertMessageAsync(string title, string message, string buttonText)
            => await Shell.Current.DisplayAlert(title, message, buttonText);
        protected async Task<bool> ShowConfirmMessageAsync(string title, string message, string okButtonText, string cancelButtonText)
          => await Shell.Current.DisplayAlert(title, message, okButtonText, cancelButtonText);
        #endregion

        #region Global Navigation Methods
        protected async Task GoToPageAsync(ShellNavigationState shellNavigationState)
         => await Shell.Current.GoToAsync(shellNavigationState);
        protected async Task GoToPageAsync(ShellNavigationState shellNavigationState, bool animate)
         => await Shell.Current.GoToAsync(shellNavigationState, animate);

        protected async Task GoToPageAsync(ShellNavigationState shellNavigationState, IDictionary<string, object> parameters)
            => await Shell.Current.GoToAsync(shellNavigationState, parameters);

        protected async Task GoToPageAsync(ShellNavigationState shellNavigationState, bool animate, IDictionary<string, object> parameters)
            => await Shell.Current.GoToAsync(shellNavigationState, animate, parameters);
        #endregion

        #region Abstract methods for setting boolean values for current view model

        /// <summary>
        /// Abstract method to set the values of the boolean properties to true
        /// according the implementation
        /// </summary>
        protected abstract void SetTrueBoolValues();

        /// <summary>
        /// Abstract method to set the values of the boolean properties to false
        /// according the implementation
        /// </summary>
        protected abstract void SetFalseBoolValues();
        #endregion
    }
}
