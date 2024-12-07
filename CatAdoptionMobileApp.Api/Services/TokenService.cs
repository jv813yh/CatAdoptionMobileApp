using CatAdoptionMobileApp.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CatAdoptionMobileApp.Api.Services
{
    public class TokenService
    {
        private readonly IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// For the generation of parameters for validatin of the JWT token
        /// </summary>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static TokenValidationParameters GetTokenValidationParameters(IConfiguration configuration)
        {
            return new TokenValidationParameters()
            {
                ValidateIssuer = true, // Validate the server that created that token
                ValidateAudience = false, // Validate the user that is using that token
                ValidateLifetime = true, // Validate if the token is expired
                ValidateIssuerSigningKey = true, // Validate the security key
                ValidIssuer = configuration["Jwt:Issuer"], // The server that created that token
                IssuerSigningKey = GetSecurityKey(configuration) // The security key

            };
        }
        public string GenerateJWT(IEnumerable<Claim>? additionalClaims = null)
        {
            // Get the security key
            var securityKey = GetSecurityKey(_configuration);
            // Set the credentials
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            // Set expiration time
            var expires = Convert.ToInt32(_configuration["Jwt:Expires"] ?? "60");

            // 
            var claims = new List<Claim>
            {
                // token has a unique identifier
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            };

            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            // Create the token
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: "*", // The audience is any
                claims: claims,
                expires: DateTime.Now.AddMinutes(expires),
                signingCredentials: credentials
            );

            // Return the token
            return new JwtSecurityTokenHandler().WriteToken(token);

        }

        public string GenerateJWT(User user, IEnumerable<Claim>? additionalClaims = null)
        {
            var claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
            };

            if (additionalClaims != null)
            {
                claims.AddRange(additionalClaims);
            }

            return GenerateJWT(claims);
        }


        private static SecurityKey GetSecurityKey(IConfiguration configuration)
         => new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "empty"));
    }
}
