﻿using CatAdoptionMobileApp.Shared.Dtos;

namespace CatAdoptionMobileApp.Api.Services.Interfaces
{
    public interface ICatService
    {
        Task<ApiResponse<CatListDto[]>> GetAllCatsAsync();
        Task<ApiResponse<CatListDto[]>> GetNewAddedCatsAsync(int count);
        Task<ApiResponse<CatListDto[]>> GetRandomCatsAsync(int count);
        Task<ApiResponse<CatDetailDto>> GetCatDetailsAsync(int catId, int userId = 0);
        Task<ApiResponse<CatListDto[]>> GetPopularCatsAsync(int count);
    }
}
