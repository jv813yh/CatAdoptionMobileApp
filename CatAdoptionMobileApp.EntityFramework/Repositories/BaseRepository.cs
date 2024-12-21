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

        public async Task<TEntity?> GetByIdAsync(uint id)
        {
            try
            {
                var result = await _currentDbSet.FindAsync(id);

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<List<TEntity>?> GetAll()
        {
            try
            {
                var result = await _currentDbSet.ToListAsync();

                return result;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

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
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

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
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }

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
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
            }
        }
    }
}
