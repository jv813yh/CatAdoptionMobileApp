using System.ComponentModel.DataAnnotations;

namespace CatAdoptionMobileApp.Shared.Dtos
{
    public class ChangePasswordDto : LoginRequestDto
    {
        [Required, MinLength(6)]
        public string NewPassword { get; set; }
    }
}
