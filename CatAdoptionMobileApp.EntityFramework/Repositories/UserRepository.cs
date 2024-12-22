using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;

namespace CatAdoptionMobileApp.EntityFramework.Repositories
{
    public class UserRepository : BaseRepository<User> 
    {
        private readonly CatAdoptionDbContext _dbContext;
        public UserRepository(CatAdoptionDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Find the user by email
        public async Task<User?> GetUserByEmailAsync(string email)
        {
            if(!string.IsNullOrEmpty(email))
            {
                return null;
            }

            try
            {
                var result = await _dbContext.Set<User>()
                                  .FirstOrDefaultAsync(u => u.Email == email);

                return result;
            }
            catch (Exception ex)
            {
                // Log database-related errors here ...
                throw; // rethrowing to let higher layers handle it
            }

        }
    }
}
