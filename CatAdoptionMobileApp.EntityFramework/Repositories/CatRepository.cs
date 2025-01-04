using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionMobileApp.EntityFramework.Repositories
{
    public class CatRepository : BaseRepository<Cat>
    {
        private readonly CatAdoptionDbContext _dbContext;
        private readonly DbSet<Cat> _currentCatDbSet;
        public CatRepository(CatAdoptionDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
            _currentCatDbSet = _dbContext.Set<Cat>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<Cat[]> GetNewAddedCatsAsync(int count)
        {
            try
            {
                var newAddedCats = await _currentCatDbSet
                           .OrderByDescending(c => c.Id)
                           .Take(count)
                           .ToArrayAsync();

                return newAddedCats;
            }
            catch (Exception ex)
            {

                // TODO: Log database-related errors here TODO
                throw; // rethrowing to let higher layers handle it
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<Cat[]> GetPopularCatsAsync(int count)
        {
            try
            {
                var popularCats = await _currentCatDbSet
                           .OrderByDescending(c => c.Views)
                           .Take(count)
                           .ToArrayAsync();

                return popularCats;
            }
            catch (Exception ex)
            {

                // TODO: Log database-related errors here
                throw; // rethrowing to let higher layers handle it
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public async Task<Cat[]> GetRandomCatsAsync(int count)
        {
            try
            {
                var randomCats = await _currentCatDbSet
                            .OrderBy(c => Guid.NewGuid())
                            .Take(count)
                            .ToArrayAsync();

                return randomCats;
            }
            catch (Exception ex)
            {

                // TODO: Log database-related errors here
                throw; // rethrowing to let higher layers handle it
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<Cat[]> GetAllCatsAsync()
        {
            try
            {
                // Get all cats from the database
                var cats = await GetAllAsync();

                if(cats == null)
                {
                    return new Cat[1];
                }

                return cats.ToArray();
            }
            catch (Exception ex)
            {

                // TODO: Log database-related errors here
                throw; // rethrowing to let higher layers handle it
            }
        }

       /// <summary>
       /// 
       /// </summary>
       /// <param name="id"> id cat </param>
       /// <param name="idUser"> id user </param>
       /// <returns> Cat detail and is favorite cat for user </returns>
        public async Task<(Cat?, bool)> GetCatDetailsAsync(int id, int idUser = 0)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                bool isFavorite = false;
                try
                {
                    // Find the cat by id
                    var catDetail = await GetByIdAsync(id, true);   

                    if (catDetail == null)
                    {
                        return (null, isFavorite);
                    }

                    // Increment the views count and save to the database
                    catDetail.Views++;
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();

                    if (idUser > 0)
                    {
                        // Check if the user has favorited the cat
                         isFavorite = await _dbContext.UserFavorites
                            .AnyAsync(uf => uf.CatId == id && uf.UserId == idUser);
                    }

                    return (catDetail, isFavorite);

                }
                catch (Exception ex)
                {
                    // TODO: Log database-related errors here
                    await transaction.RollbackAsync();
                    throw; // rethrowing to let higher layers handle it
                }
            }
        }
    }

}
