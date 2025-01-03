namespace CatAdoptionMobileApp.MAUI.Services
{
    public class CommonService
    {
        public string? Token { get; private set; }

        public void SetToken(string token)
        {
            Token = token;
        }

        public event EventHandler LoginStatusChanged;

        public void NotifyLoginStatusChanged()
         => LoginStatusChanged?.Invoke(this, EventArgs.Empty);
        
    }
}
