namespace CatAdoptionMobileApp.MAUI.Services.Interfaces
{
    /// <summary>
    /// Service for handling login, registration, logout and get user info
    /// </summary>
    public interface IAuthService
    {
        Task<bool> LoginRegisterAsync(LoginRegisterModel loginRegisterModel, bool isRegistration);
        void Logout();
        LoggedInUser? GetUserInfo();
        bool IsLoggedIn { get; }
    }
}
