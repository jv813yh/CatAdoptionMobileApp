using CatAdoptionMobileApp.Shared.Enumerations;
using System.ComponentModel.DataAnnotations;

namespace CatAdoptionMobileApp.Domain.Models
{
    public class Cat : CommonObject
    {
        [Required, MaxLength(30)]
        public string Name { get; set; } 

        [Required, MaxLength(50)]
        public string? Breed { get; set; }

        [Required, MaxLength(250)]
        public string Description { get; set; }

        [Required]
        public string ImageUrl { get; set; }

        [Required]
        public Gender Gender { get; set; }

        [Required, Range(0, double.MaxValue)]
        public double Price { get; set; }

        public DateTime DateOfBirth { get; set; }

        public int Views { get; set; }

        public AdoptionStatus AdoptionStatus { get; set; } = AdoptionStatus.Available;

        public bool IsActive { get; set; }
    }
}
