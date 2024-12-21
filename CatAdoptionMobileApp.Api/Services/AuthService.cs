using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.Repositories;
using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api.Services
{
    public class AuthService
    {
        private readonly UserRepository _userRepository;
        private readonly TokenService _tokenService;

        public AuthService(UserRepository userRepository, TokenService tokenService)
        {
            _userRepository = userRepository;
            _tokenService = tokenService;
        }

        public async Task<ApiResponse<AuthResponseDto>> LoginAsync(LoginRequestDto loginDto)
        {
            // Find the user by email
            var user = await _userRepository.GetUserByEmailAsync(loginDto.Email);

            if (user == null)
            {
                return ApiResponse<AuthResponseDto>.Fail("User does not exist");
            }

            if (!PasswordHasher.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.Salt))
            {
                return ApiResponse<AuthResponseDto>.Fail("Incorrect password");
            }

            // If the user exists and the password is correct,
            // generate JWT token
            var token = _tokenService.GenerateJWT(user);

            // Return the user id, name and token in the response as success
            return ApiResponse<AuthResponseDto>.Success(new AuthResponseDto(user.Id, user.Name, token));
        }

        public async Task<ApiResponse<AuthResponseDto>> RegisterAsync(RegisterRequestDto registerDto)
        {
            // Check if the Email already exists
            var checkUser = await _userRepository.GetUserByEmailAsync(registerDto.Email);
            if (checkUser != null)
            {
                return ApiResponse<AuthResponseDto>.Fail("Email already exists");
            }

            // Hash the password
            PasswordHasher.CreatePasswordHash(registerDto.Password, out string passwordHash, out string salt);

            // Create a new user
            var newUser = new User
            {
                Name = registerDto.Name,
                Email = registerDto.Email,
                PasswordHash = passwordHash,
                Salt = salt
            };

            // Add the user to the database
            await _userRepository.UpdateAsync(newUser);
            // Generate JWT token
            var token = _tokenService.GenerateJWT(newUser);
            // Return the user id, name and token in the response as success
            return ApiResponse<AuthResponseDto>.Success(new AuthResponseDto(newUser.Id, newUser.Name, token));
        }
    }
}
