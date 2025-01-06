using CatAdoptionMobileApp.Api.Services.Interfaces;
using CatAdoptionMobileApp.Domain.Exceptions;
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
                // Toggle the favorite cat
                await _userCatRepository.ToggleUserCatFavoriteAsync(userId, catId);

                return ApiResponse.Success();
            }
            catch (Exception)
            {
                return ApiResponse.Fail("Something went wrong while toggling the favorite cat.");
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
                // Fetching favorite cats from the database
                var favoriteCatsArray = await _userCatRepository.GetUserFavoriteCatsAsync(userId);

                // Mapping the Cat[] to CatListDto[]
                var favoritesCatListDtos = favoriteCatsArray
                                          .Select(c => c.MapToCatListDto())
                                          .ToArray();

                return ApiResponse<CatListDto[]>.Success(favoritesCatListDtos);
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
                // Fetching adopted cats from the database
                var returnCats = await _userCatRepository.GetUserAdoptionCatsAsync(userId);

                // Mapping the Cat[] to CatListDto[]
                var returnCatListDto = returnCats
                                      .Select(c => c.MapToCatListDto())
                                      .ToArray();

                return ApiResponse<CatListDto[]>.Success(returnCatListDto);
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
                var userAdoption = await  _userCatRepository.AdoptCatAsync(userId, catId);

                return ApiResponse<UserAdoption>.Success(userAdoption);
            }
            catch (NotAvailableAdoptionCat)
            {
                return ApiResponse<UserAdoption>.Fail("Cat is not available for adoption");
            }
            catch (AlreadyAdoptedCatByUser)
            {
                return ApiResponse<UserAdoption>.Fail("Cat is already adopted by the user");
            }
            catch (Exception)
            {
                return ApiResponse<UserAdoption>.Fail("Something went wrong while adopting the cat.");
            }
        }
    }
}
