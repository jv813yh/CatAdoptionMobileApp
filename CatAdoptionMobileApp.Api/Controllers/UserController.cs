﻿using CatAdoptionMobileApp.Api.Services.Interfaces;
using CatAdoptionMobileApp.Domain.Models;
using CatAdoptionMobileApp.Shared.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace CatAdoptionMobileApp.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserCatService _userCatService;

        public UserController(IUserCatService userCatService)
        {
            _userCatService = userCatService;
        }

        private uint UserId 
            => Convert.ToUInt32(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value);

        // POST api/user/favorite/1
        [HttpPost("favorite/{catId:uint}")]
        public async Task<ApiResponse> ToggleCatFavoriteAsync(uint catId)
            => await _userCatService.ToggleCatFavoriteAsync(UserId, catId);

        // GET api/user/favorite
        [HttpGet("favorite")]
        public async Task<ApiResponse<CatListDto[]>> GetFavoriteCatsAsync()
            => await _userCatService.GetFavoriteCatsAsync(UserId);

        // GET api/user/adopt
        [HttpGet("adopt")]
        public async Task<ApiResponse<CatListDto[]>> GetUserAdoptionCatsAsync()
            => await _userCatService.GetUserAdoptionCatsAsync(UserId);

        // POST api/user/adopt/1
        [HttpPost("adopt/{catId:uint}")]
        public async Task<ApiResponse<UserAdoption>> AdoptCatAsync(uint catId)
            => await _userCatService.AdoptCatAsync(UserId, catId);
    }
}
