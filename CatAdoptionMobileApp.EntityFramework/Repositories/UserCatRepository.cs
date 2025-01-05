using CatAdoptionMobileApp.Domain.Exceptions;
using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
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
        public async Task ToggleUserCatFavoriteAsync(int userId, int catId)
        {
            try
            {
                // Find the first favorite of the user for the cat
                var firstFavorite = await _currentUserFavoritesSet
                        .FirstOrDefaultAsync(f => f.UserId == userId && f.CatId == catId);

                // If the user has not favorited the cat, add it to the favorites
                if (firstFavorite == null)
                {
                    await AddAsync(new UserFavorites
                    {
                        UserId = userId,
                        CatId = catId
                    });
                }
                else
                {
                    // If the user has already favorited the cat, remove it from the favorites
                    await DeleteAsync(firstFavorite.Id);
                }
            }
            catch (Exception ex)
            {
                // Todo: Log database-related errors here
                throw;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Cat[]> GetUserFavoriteCatsAsync(int userId)
        {
            try
            {
                var favoriteCats = await _currentUserFavoritesSet
                        .Where(f => f.UserId == userId)
                        .Select(f => f.Cat)
                        .ToArrayAsync();

                return favoriteCats;
            }
            catch (Exception ex)
            {
                throw; // rethrowing to let higher layers handle it
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public async Task<Cat[]> GetUserAdoptionCatsAsync(int userId)
        {
            try
            {
                var userAdoptionCats = await _userAdoptionSet
                        .Where(f => f.UserId == userId)
                        .Select(f => f.Cat)
                        .ToArrayAsync();

                return userAdoptionCats;
            }
            catch (Exception ex)
            {
                throw; // rethrowing to let higher layers handle it
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="catId"></param>
        /// <returns></returns>
        public async Task<UserAdoption> AdoptCatAsync(int userId, int catId)
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
                        throw new NullReferenceException("Cat is null");
                    }

                    // Check if the cat is available for adoption
                    if (cat.AdoptionStatus == AdoptionStatus.Adopted)
                    {
                        throw new NotAvailableAdoptionCat("Cat is not available for adoption");
                    }

                    // Check if the user has already adopted the cat
                    var userAdoption = await _userAdoptionSet
                                            .FirstOrDefaultAsync(ua => ua.UserId == userId && ua.CatId == catId);

                    if (userAdoption != null)
                    {
                        throw new AlreadyAdoptedCatByUser("Cat is already adopted by the user");
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


                    return newUserAdoption;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync();
                    throw; // rethrowing to let higher layers handle it
                }
                finally
                {
                    // Release the semaphore
                    _semaphore.Release();
                }
            }
        }
    }
}
