using System.ComponentModel.DataAnnotations;

namespace CatAdoptionMobileApp.Shared.Dtos
{
    public class RegisterRequestDto : LoginRequestDto
    {
        [Required]
        public string Name { get; set; }
    }
}
