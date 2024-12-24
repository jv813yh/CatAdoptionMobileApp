using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
using CatAdoptionMobileApp.Shared.Dtos;
using CatAdoptionMobileApp.Shared.Enumerations;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionMobileApp.EntityFramework.Repositories
{
    public class UserCatRepository  : BaseRepository<UserFavorites>
    {
        private static readonly SemaphoreSlim _semaphore = new SemaphoreSlim(1, 1);

        private readonly CatAdoptionDbContext _dbContext;
        private readonly DbSet<UserFavorites> _currentUserFavoritesSet;
        private readonly DbSet<UserAdoption> _userAdoptionSet;

        public UserCatRepository(CatAdoptionDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _currentUserFavoritesSet = _dbContext.Set<UserFavorites>();
            _userAdoptionSet = _dbContext.Set<UserAdoption>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="catId"></param>
        /// <returns></returns>
        public async Task<ApiResponse> ToggleUserCatFavoriteAsync(int userId, int catId)
        {
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Find the first favorite of the user for the cat
                    var firstFavorite = await _currentUserFavoritesSet
                            .FirstOrDefaultAsync(f => f.UserId == userId && f.CatId == catId);

                    // If the user has not favorited the cat, add it to the favorites
                    if (firstFavorite == null)
                    {
                        await _currentUserFavoritesSet.AddAsync(new UserFavorites
                        {
                            UserId = userId,
                            CatId = catId
                        });
                    }
                    else
                    {
                        // If the user has already favorited the cat, remove it from the favorites
                        _currentUserFavoritesSet.Remove(firstFavorite);
                    }

                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    return ApiResponse.Success();
                }
                catch (Exception ex)
                {
                    // Todo: Log database-related errors here
                    await transaction.RollbackAsync();

                    return ApiResponse.Fail("An error occurred while toggling the favorite status.");
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatListDto[]>> GetUserFavoriteCatsAsync(int userId)
        {
            try
            {
                var favoriteCats = await _currentUserFavoritesSet
                        .Where(f => f.UserId == userId)
                        .Select(f => f.Cat)
                        .Select(Selectors.CatToCatListDto)
                        .ToArrayAsync();

                return ApiResponse<CatListDto[]>.Success(favoriteCats);
            }
            catch (Exception ex)
            {
                return ApiResponse<CatListDto[]>.Fail("An error occurred while fetching the favorite cats.");
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
                var userAdoptionCats = await _userAdoptionSet
                        .Where(f => f.UserId == userId)
                        .Select(f => f.Cat)
                        .Select(Selectors.CatToCatListDto)
                        .ToArrayAsync();

                return ApiResponse<CatListDto[]>.Success(userAdoptionCats);
            }
            catch (Exception ex)
            {
                return ApiResponse<CatListDto[]>.Fail("An error occurred while fetching the adopted cats.");
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
            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Semaphore can be used to adapt the code to be thread-safe
                    await _semaphore.WaitAsync();

                    // Get the cat
                    var cat = await _dbContext
                                    .Cats
                                    .AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == catId);

                    // Check if the cat exists
                    if (cat == null)
                    {
                        return ApiResponse<UserAdoption>.Fail("The cat does not exist.");
                    }

                    // Check if the cat is available for adoption
                    if (cat.AdoptionStatus == AdoptionStatus.Adopted)
                    {
                        return ApiResponse<UserAdoption>.Fail("The cat has already been adopted.");
                    }

                    // Check if the user has already adopted the cat
                    var userAdoption = await _userAdoptionSet
                                            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.CatId == catId);

                    if (userAdoption != null)
                    {
                        return ApiResponse<UserAdoption>.Fail("The user has already adopted the cat.");
                    }

                    // Update the cat's adoption status
                    cat.AdoptionStatus = AdoptionStatus.Adopted;

                    // Add the user adoption
                    var newUserAdoption = new UserAdoption
                    {
                        UserId = userId,
                        CatId = catId,
                        AdoptedOn = DateTime.Now
                    };

                    // Save the user adoption to the database
                    await _userAdoptionSet.AddAsync(newUserAdoption);
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    // Release the semaphore
                    _semaphore.Release();

                    return ApiResponse<UserAdoption>.Success(newUserAdoption);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    // Release the semaphore
                    _semaphore.Release();
                    return ApiResponse<UserAdoption>.Fail("An error occurred while adopting the cat.");
                }
            }
        }
    }
}
