namespace CatAdoptionMobileApp.MAUI.Services.Queries
{
    /// <summary>
    /// Interface for the User API with the favorite, 
    /// adopt, and get favorite cats endpoints with authorization token
    /// </summary>
    [Headers("Authorization: Bearer")]
    public interface IUserApi
    {
        // POST api/user/favorite/1
        [Post("/api/user/favorite/{catId}")]
        Task<ApiResponse> ToggleCatFavoriteAsync(int catId);

        // GET api/user/favorite
        [Get("/api/user/favorite")]
        Task<Shared.Dtos.ApiResponse<CatListDto[]>> GetFavoriteCatsAsync();

        // GET api/user/adopt
        [Get("/api/user/adopt")]
        Task<Shared.Dtos.ApiResponse<CatListDto[]>> GetUserAdoptionCatsAsync();

        // POST api/user/adopt/1
        [Post("/api/user/adopt/{catId}")]
        Task<Shared.Dtos.ApiResponse<UserAdoption>> AdoptCatAsync(int catId);
    }
}
