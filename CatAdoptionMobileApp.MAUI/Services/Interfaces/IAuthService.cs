namespace CatAdoptionMobileApp.MAUI.Services.Interfaces
{
    /// <summary>
    /// Service for handling login, registration, logout and get user info
    /// </summary>
    public interface IAuthService
    {
        Task<bool> LoginRegisterAsync(LoginRegisterModel loginRegisterModel);
        Task LogoutAsync();
        LoggedInUser? GetUserInfo();
        bool IsLoggedIn { get; }
    }
}
