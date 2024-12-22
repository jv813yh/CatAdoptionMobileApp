using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api.Services.Interfaces
{
    public interface IAuthService
    {
        Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto loginDto);
        Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto registerDto);
    }
}
