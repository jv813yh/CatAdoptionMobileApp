using CatAdoptionMobileApp.Api.Services.Interfaces;
using CatAdoptionMobileApp.Shared.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace CatAdoptionMobileApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatController : ControllerBase
    {
        private readonly ICatService _catService;

        public CatController(ICatService catService)
        {
            _catService = catService;
        }

        // GET api/cat
        [HttpGet("")]
        public async Task<ApiResponse<CatListDto[]>> GetAllCatsAsync()
            => await _catService.GetAllCatsAsync();

        // GET api/cat/new/5
        [HttpGet("new/{count:int}")]
        public async Task<ApiResponse<CatListDto[]>> GetNewAddedCatsAsync(int count)
            => await _catService.GetNewAddedCatsAsync(count);

        // GET api/cat/random/5
        [HttpGet("random/{count:int}")]
        public async Task<ApiResponse<CatListDto[]>> GetRandomCatsAsync(int count)
            => await _catService.GetRandomCatsAsync(count);

        // GET api/cat/1
        [HttpGet("{catId:int}")]
        public async Task<ApiResponse<CatDetailDto>> GetCatDetailsAsync(int catId)
            => await _catService.GetCatDetailsAsync(catId);

        // GET api/cat/popular/10
        [HttpGet("popular/{count:int}")]
        public async Task<ApiResponse<CatListDto[]>> GetPopularCatsAsync(int count)
            => await _catService.GetPopularCatsAsync(count);
    }
}
