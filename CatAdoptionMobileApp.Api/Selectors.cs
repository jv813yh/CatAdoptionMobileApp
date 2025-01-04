using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.Shared.Dtos;
using System.Linq.Expressions;

namespace CatAdoptionMobileApp.Api
{
    public static class Selectors
    {
        /// <summary>
        /// Selects a Cat entity and maps it to a CatDto
        /// </summary>
        public static Expression<Func<Cat, CatListDto>> CatToCatListDto =>
            cat => new CatListDto
            {
                Id = cat.Id,
                Name = cat.Name,
                Price = cat.Price,
                Breed = cat.Breed,
                ImageUrl = cat.ImageUrl
            };
    }
}
