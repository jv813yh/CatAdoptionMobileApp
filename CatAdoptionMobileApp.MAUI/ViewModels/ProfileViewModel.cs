namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public partial class ProfileViewModel : BaseViewModel
    {
        private readonly IAuthService _authService;

        [ObservableProperty, NotifyPropertyChangedFor(nameof(Initials))]
        private string _userName = "Not Logged In";

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

                if(names.Length == 1)
                {
                    return names[0].ToUpper();
                }
                else
                {
                    return $"{names[0][0]}{names[1][0]}".ToUpper();
                }
            }
        }

        public ProfileViewModel(IAuthService authService)
        {
            _authService = authService;
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
