using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api.Services.Interfaces
{
    public interface IUserCatService
    {
        Task<ApiResponse> ToggleCatFavoriteAsync(int userId, int catId);
        Task<ApiResponse<CatListDto[]>> GetFavoriteCatsAsync(int userId);
        Task<ApiResponse<CatListDto[]>> GetUserAdoptionCatsAsync(int userId);
        Task<ApiResponse<UserAdoption>> AdoptCatAsync(int userId, int catId);
    }
}
