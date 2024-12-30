namespace CatAdoptionMobileApp.MAUI.Services.Queries
{
    /// <summary>
    /// Cat API interface for Refit to generate API calls
    /// </summary>
    public interface ICatApi
    {
        // GET api/cat
        [Get("/api/cat/")]
        Task<Shared.Dtos.ApiResponse<CatListDto[]>> GetAllCatsAsync();

        // GET api/cat/new/5
        [Get("/api/cat/new/{count}")]
        Task<Shared.Dtos.ApiResponse<CatListDto[]>> GetNewAddedCatsAsync(int count);

        // GET api/cat/random/5
        [Get("/api/cat/random/{count}")]
        Task<Shared.Dtos.ApiResponse<CatListDto[]>> GetRandomCatsAsync(int count);

        // GET api/cat/1
        [Get("/api/cat/{id}")]
        Task<Shared.Dtos.ApiResponse<CatDetailDto>> GetCatDetailsAsync(int id);

        // GET api/cat/popular/10
        [Get("/api/cat/popular/{count}")]
        Task<Shared.Dtos.ApiResponse<CatListDto[]>> GetPopularCatsAsync(int count);
    }
}
