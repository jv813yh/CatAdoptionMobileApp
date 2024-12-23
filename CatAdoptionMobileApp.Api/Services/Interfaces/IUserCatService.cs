using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api.Services.Interfaces
{
    public interface IUserCatService
    {
        Task<ApiResponse> ToggleCatFavoriteAsync(uint userId, uint catId);
        Task<ApiResponse<CatListDto[]>> GetFavoriteCatsAsync(uint userId);
        Task<ApiResponse<CatListDto[]>> GetUserAdoptionCatsAsync(uint userId);
        Task<ApiResponse<UserAdoption>> AdoptCatAsync(uint userId, uint catId);
    }
}
