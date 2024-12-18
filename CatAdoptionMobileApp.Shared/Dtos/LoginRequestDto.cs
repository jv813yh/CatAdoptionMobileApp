using System.ComponentModel.DataAnnotations;

namespace CatAdoptionMobileApp.Shared.Dtos
{
    public class LoginRequestDto
    {
        [Required, EmailAddress]
        public string Email { get; set; }

        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
