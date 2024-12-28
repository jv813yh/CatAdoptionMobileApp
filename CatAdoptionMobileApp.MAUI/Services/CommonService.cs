namespace CatAdoptionMobileApp.MAUI.Services
{
    public class CommonService
    {
        public string? Token { get; private set; }

        public void SetToken(string token)
        {
            Token = token;
        }
    }
}
