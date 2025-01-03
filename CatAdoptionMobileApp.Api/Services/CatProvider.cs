using CatAdoptionMobileApp.Api.Services.Interfaces;
using CatAdoptionMobileApp.EntityFramework.Repositories;
using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api.Services
{
    public class CatProvider : ICatService
    {
        private readonly CatRepository _catRepository;

        public CatProvider(CatRepository catRepository)
        {
            _catRepository = catRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<ApiResponse<CatListDto[]>> GetAllCatsAsync()
        {
            try
            {
                var result = await _catRepository.GetAllCatsAsync();

                return result;
            }
            catch (Exception ex)
            {
                return ApiResponse<CatListDto[]>.Fail("Something went wrong while fetching data.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatListDto[]>> GetNewAddedCatsAsync(int count)
        {
            try
            {
                var newAddedCats = await _catRepository.GetNewAddedCatsAsync(count);

                return newAddedCats;
            }
            catch (Exception ex)
            {
                return ApiResponse<CatListDto[]>.Fail("Something went wrong while fetching data.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatListDto[]>> GetPopularCatsAsync(int count)
        {
            try
            {
                var popularCats = await _catRepository.GetPopularCatsAsync(count);
                return popularCats;
            }
            catch (Exception)
            {
                return ApiResponse<CatListDto[]>.Fail("Something went wrong while fetching data.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatListDto[]>> GetRandomCatsAsync(int count)
        {
            try
            {
                var randomCats = await _catRepository.GetRandomCatsAsync(count);
                return randomCats;
            }
            catch (Exception ex)
            {
                return ApiResponse<CatListDto[]>.Fail("Something went wrong while fetching data.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatDetailDto>> GetCatDetailsAsync(int id, int idUser = -1)
        {
            try
            {
                var catDetails = await _catRepository.GetCatDetailsAsync(id, idUser);

                return catDetails;
            }
            catch (Exception ex)
            {
                return ApiResponse<CatDetailDto>.Fail("Something went wrong while fetching data.");
            }
        }
    }
}
