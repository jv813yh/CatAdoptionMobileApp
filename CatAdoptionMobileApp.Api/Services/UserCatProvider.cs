using CatAdoptionMobileApp.Api.Services.Interfaces;
using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.Repositories;
using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api.Services
{
    public class UserCatProvider : IUserCatService
    {
        private readonly UserCatRepository _userCatRepository;

        public UserCatProvider(UserCatRepository userCatRepository)
        {
            _userCatRepository = userCatRepository;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="catId"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ToggleCatFavoriteAsync(int userId, int catId)
        {
            try
            {
                var apiResult = await _userCatRepository.ToggleUserCatFavoriteAsync(userId, catId);

                return apiResult;
            }
            catch (Exception)
            {
                return ApiResponse.Fail("Something went wrong while toggling the favorite status.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatListDto[]>> GetFavoriteCatsAsync(int userId) 
        {
            try
            {
                var favoritesCatListDtos = await _userCatRepository.GetUserAdoptionCatsAsync(userId);
                return favoritesCatListDtos;
            }
            catch (Exception)
            {
                return ApiResponse<CatListDto[]>.Fail("Something went wrong while getting the favorite cats.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatListDto[]>> GetUserAdoptionCatsAsync(int userId)
        {
            try
            {
                var returnCatListDto = await _userCatRepository.GetUserAdoptionCatsAsync(userId);
                return returnCatListDto;
            }
            catch (Exception)
            {
                return ApiResponse<CatListDto[]>.Fail("Something went wrong while getting the adopted cats.");
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="catId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<UserAdoption>> AdoptCatAsync(int userId, int catId)
        {
            try
            {
                var resultUserAdoption = await  _userCatRepository.AdoptCatAsync(userId, catId);
                return resultUserAdoption;
            }
            catch (Exception)
            {
                return ApiResponse<UserAdoption>.Fail("Something went wrong while adopting the cat.");
            }
        }
    }
}
