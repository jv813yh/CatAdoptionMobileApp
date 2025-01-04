using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api
{
    public static class Mapper
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

        public static CatListDto MapToCatListDto(this Cat cat)
        {
            return new CatListDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Price = cat.Price,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };
        }
    }
}
