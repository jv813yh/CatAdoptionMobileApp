using System.ComponentModel.DataAnnotations;

namespace CatAdoptionMobileApp.Domain.Models
{
    public class User : CommonObject
    {
        [Required(ErrorMessage = "Name is required.")]
        [MaxLength(30, ErrorMessage = "Name cannot exceed 30 characters.")]
        public string Name { get; set; }

        [Required, MaxLength(100)]
        public string Email { get; set; }

        [Required, MaxLength(200)]
        public string Salt { get; set; }

        [Required, MaxLength(100)]
        public string PasswordHash { get; set; }
    }
}
