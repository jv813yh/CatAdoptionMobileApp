using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionMobileApp.EntityFramework.Repositories
{
    public class BaseRepository<TEntity> where TEntity : CommonObject
    {
        private readonly CatAdoptionDbContext _catAdoptionDbContext;

        private readonly DbSet<TEntity> _currentDbSet;

        public BaseRepository(CatAdoptionDbContext dbContext)
        {
            _catAdoptionDbContext = dbContext;

            _currentDbSet = _catAdoptionDbContext.Set<TEntity>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<TEntity?> GetByIdAsync(uint id)
        {
            try
            {
                var result = await _currentDbSet.FindAsync(id);

                return result;
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
        public async Task<List<TEntity>?> GetAll()
        {
            try
            {
                var result = await _currentDbSet.ToListAsync();

                return result;
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
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task AddAsync(TEntity entity)
        {
            if (entity != null)
            {
                using (var transaction = _catAdoptionDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        await _currentDbSet.AddAsync(entity);
                        await _catAdoptionDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // TODO: Log database-related errors here
                        throw; // rethrowing to let higher layers handle it
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public async Task UpdateAsync(TEntity entity)
        {
            if (entity != null)
            {
                using (var transaction = _catAdoptionDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _currentDbSet.Update(entity);
                        await _catAdoptionDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // TODO: Log database-related errors here
                        throw; // rethrowing to let higher layers handle it
                    }
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(uint id)
        {
            var entity = await GetByIdAsync(id);

            if (entity != null)
            {
                using (var transaction = _catAdoptionDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        _currentDbSet.Remove(entity);
                        await _catAdoptionDbContext.SaveChangesAsync();
                        await transaction.CommitAsync();
                    }
                    catch (Exception ex)
                    {
                        await transaction.RollbackAsync();
                        // TODO: Log database-related errors here
                        throw; // rethrowing to let higher layers handle it
                    }
                }
            }
        }
    }
}
