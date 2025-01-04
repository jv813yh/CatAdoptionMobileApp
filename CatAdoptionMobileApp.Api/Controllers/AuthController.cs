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
            => await _authService.LoginAsync(loginDto);

        // POST api/auth/register
        [HttpPost("register")] 
        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync([FromBody] RegisterRequestDto registerDto)
            => await _authService.RegisterAsync(registerDto);
    }
}
