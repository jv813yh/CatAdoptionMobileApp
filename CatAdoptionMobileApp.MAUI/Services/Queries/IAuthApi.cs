namespace CatAdoptionMobileApp.MAUI.Services.Queries
{
    /// <summary>
    /// Interface for the Auth API with the login and register endpoints.
    /// </summary>
    public interface IAuthApi
    {
        // POST api/auth/login
        [Post("/api/auth/login")]
        Task<Shared.Dtos.ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto loginDto);

        // POST api/auth/register
        [Post("/api/auth/register")]
        Task<Shared.Dtos.ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto registerDto);

        // POST api/auth/change-password
        [Post("/api/auth/change-password")]
        Task<Shared.Dtos.ApiResponse<AuthResponseDto>> ChangePasswordAsync(ChangePasswordDto loginDto);
    }
}
