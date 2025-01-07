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
            if(string.IsNullOrEmpty(email))
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

        /// <summary>
        /// Verifies the password of the user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="tryPassword"></param>
        /// <returns></returns>
        public async Task<bool> VerifyPasswordAsync(string email, string tryPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(tryPassword))
            {
                return false;
            }
            try
            {
                // Find the user by email
                var currentUser = await GetUserByEmailAsync(email);

                if (currentUser == null)
                {
                    return false;
                }

                // Verify the password 
                var result = PasswordHasher.VerifyPasswordHash(tryPassword, currentUser.PasswordHash, currentUser.Salt);
                
                return result;
            }
            catch (Exception ex)
            {
                // Log database-related errors here ...
                throw; // rethrowing to let higher layers handle it
            }
        }

        /// <summary>
        /// Try to change the password of the user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="newPassword"></param>
        /// <returns></returns>
        public async Task<bool> ChangePasswordAsync(string email, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                return false;
            }
            try
            {
                // Find the user by email
                var currentUser = await _dbContext.Set<User>()
                                  .AsTracking()
                                  .FirstOrDefaultAsync(u => u.Email == email);
                if (currentUser == null)
                {
                    return false;
                }

                // Hash the password
                PasswordHasher.CreatePasswordHash(newPassword, out string passwordHash, out string salt);
                // Update the user's password
                currentUser.PasswordHash = passwordHash;
                currentUser.Salt = salt;

                // Save udpated data to db
                await UpdateAsync(currentUser);

                return true;
            }
            catch (Exception ex)
            {
                // Log database-related errors here ...
                throw; // rethrowing to let higher layers handle it
            }
        }
    }
}
