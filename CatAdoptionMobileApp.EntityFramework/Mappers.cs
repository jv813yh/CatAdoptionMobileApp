using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.EntityFramework
{
    public static class Mappers
    {
        public static CatDetailDto MapToCatDetailsDto(this Cat cat)
        {
            return new CatDetailDto
            {
                Id = cat.Id,
                AdoptionStatus = cat.AdoptionStatus,
                DateOfBirth = cat.DateOfBirth,
                Gender = cat.Gender,
                Name = cat.Name,
                Price = cat.Price,
                Breed = cat.Breed,
                Description = cat.Description,
                ImageUrl = cat.ImageUrl,
            };
        }
    }
}
