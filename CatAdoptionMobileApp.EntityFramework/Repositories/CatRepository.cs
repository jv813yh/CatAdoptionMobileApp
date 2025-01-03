﻿using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.EntityFramework.DbContexts;
using CatAdoptionMobileApp.Shared.Dtos;
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
        public async Task<ApiResponse<CatListDto[]>> GetNewAddedCatsAsync(int count)
        {
            try
            {
                var newAddedCats = await _currentCatDbSet
                           .Select(Selectors.CatToCatListDto)
                           .OrderByDescending(c => c.Id)
                           .Take(count)
                           .ToArrayAsync();

                return ApiResponse<CatListDto[]>.Success(newAddedCats);
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
        public async Task<ApiResponse<CatListDto[]>> GetPopularCatsAsync(int count)
        {
            try
            {
                var popularCats = await _currentCatDbSet
                           .OrderByDescending(c => c.Views)
                           .Select(Selectors.CatToCatListDto)
                           .Take(count)
                           .ToArrayAsync();

                return ApiResponse<CatListDto[]>.Success(popularCats);
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
        public async Task<ApiResponse<CatListDto[]>> GetRandomCatsAsync(int count)
        {
            try
            {
                var randomCats = await _currentCatDbSet
                            .OrderBy(c => Guid.NewGuid())
                            .Select(Selectors.CatToCatListDto)
                            .Take(count)
                            .ToArrayAsync();

                return ApiResponse<CatListDto[]>.Success(randomCats);
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
        public async Task<ApiResponse<CatListDto[]>> GetAllCatsAsync()
        {
            try
            {
                // Get all cats from the database
                var cats = await GetAllAsync();
                // Map the cats to CatListDto
                var catsListDto = cats
                                  .Select(c => c.MapToCatDetailsDto())
                                        .ToArray();

                return ApiResponse<CatListDto[]>.Success(catsListDto);
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
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ApiResponse<CatDetailDto>> GetCatDetailsAsync(int id, int idUser = -1)
        {

            using (var transaction = _dbContext.Database.BeginTransaction())
            {
                try
                {
                    // Find the cat by id
                    var catDetail = await GetByIdAsync(id, true);   

                    if (catDetail == null)
                    {
                        return ApiResponse<CatDetailDto>.Fail("Cat not found");
                    }

                    // Increment the views count and save to the database
                    catDetail.Views++;
                    await _dbContext.SaveChangesAsync();
                    await transaction.CommitAsync();


                    // Map the Cat to CatDetailDto
                    var catDetailDto = catDetail.MapToCatDetailsDto();

                    if(idUser >= 0)
                    {
                        // Check if the user has favorited the cat
                        var isFavorite = await _dbContext.UserFavorites
                            .AnyAsync(uf => uf.CatId == id && uf.UserId == idUser);
                        catDetailDto.IsFavorite = isFavorite;
                    }

                    return ApiResponse<CatDetailDto>.Success(catDetailDto);

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
