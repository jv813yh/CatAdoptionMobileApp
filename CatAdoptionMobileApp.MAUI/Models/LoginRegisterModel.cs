namespace CatAdoptionMobileApp.MAUI.Models
{
    public partial class LoginRegisterModel : ObservableObject
    {
        [ObservableProperty]
        private string _name;


        [ObservableProperty]
        private string _email;

        [ObservableProperty]
        private string _password;

        public bool IsNewUser
            => string.IsNullOrWhiteSpace(_name);

        public bool Validate(bool isRegistrationMode)
        {
            if (isRegistrationMode && string.IsNullOrWhiteSpace(_name))
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(_email) || string.IsNullOrWhiteSpace(_password))
            {
                return false;
            }

            return true;
        }
    }
}
