using CatAdoptionMobileApp.Api.Services.Interfaces;
using CatAdoptionMobileApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CatAdoptionMobileApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        // POST api/auth/login
        [HttpPost("login")] 
        public async Task<ApiResponse<AuthResponseDto>> LoginAsync([FromBody] LoginRequestDto loginDto)
        {
            var apiResponse = await _authService.LoginAsync(loginDto);

            return apiResponse;
        }

        // POST api/auth/register
        [HttpPost("register")] 
        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync([FromBody] RegisterRequestDto registerDto)
        {
            var apiResponse = await _authService.RegisterAsync(registerDto);

            return apiResponse;
        }
    }
}
