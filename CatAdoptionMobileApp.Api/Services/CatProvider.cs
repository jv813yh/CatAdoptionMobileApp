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
                // Fetching all cats from the database
                var result = await _catRepository.GetAllCatsAsync();

                // Mapping the Cat[] to CatListDto[]
                CatListDto[] returnCats = result
                                          .Select(c => c.MapToCatListDto())
                                          .ToArray();

                return ApiResponse<CatListDto[]>.Success(returnCats);

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
                // Fetching new added cats from the database
                var newAddedCats = await _catRepository.GetNewAddedCatsAsync(count);

                // Mapping the Cat[] to CatListDto[]
                CatListDto[] returnCats = newAddedCats
                                          .Select(c => c.MapToCatListDto())
                                          .ToArray();

                return ApiResponse<CatListDto[]>.Success(returnCats);
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
                // Fetching popular cats from the database
                var popularCats = await _catRepository.GetPopularCatsAsync(count);

                // Mapping the Cat[] to CatListDto[]
                CatListDto[] returnCats = popularCats
                                         .Select(c => c.MapToCatListDto())
                                         .ToArray();

                return ApiResponse<CatListDto[]>.Success(returnCats);
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
                // Fetching random cats from the database
                var randomCats = await _catRepository.GetRandomCatsAsync(count);

                // Mapping the Cat[] to CatListDto[]
                CatListDto[] returnCats = randomCats
                                        .Select(c => c.MapToCatListDto())
                                        .ToArray();

                return ApiResponse<CatListDto[]>.Success(returnCats);
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
        public async Task<ApiResponse<CatDetailDto>> GetCatDetailsAsync(int id, int idUser = 0)
        {
            try
            {
                var catDetails = await _catRepository.GetCatDetailsAsync(id, idUser);

                if (catDetails.Item1 == null)
                {
                    return ApiResponse<CatDetailDto>.Fail("Cat not found.");
                }

                // Mapping the Cat to CatDetailDto
                CatDetailDto returnCat = catDetails.Item1.MapToCatDetailsDto();

                // Setting the IsFavorite property
                returnCat.IsFavorite = catDetails.Item2;

                return ApiResponse<CatDetailDto>.Success(returnCat);
            }
            catch (Exception ex)
            {
                return ApiResponse<CatDetailDto>.Fail("Something went wrong while fetching data.");
            }
        }
    }
}
