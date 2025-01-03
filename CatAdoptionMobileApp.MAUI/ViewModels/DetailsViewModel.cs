﻿namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    [QueryProperty(nameof(CatId), nameof(CatId))]
    public partial class DetailsViewModel : BaseViewModel
    {
        private readonly ICatApi _catApi;
        private readonly IAuthService _authProvider;
        private readonly IUserApi _userApi;

        [ObservableProperty]
        private int _catId;

        [ObservableProperty]
        private Cat _catDetail;

        public DetailsViewModel(ICatApi catApi,
                                IAuthService authProvider,
                                IUserApi userApi)
        {
            _catApi = catApi;
            _authProvider = authProvider;
            _userApi = userApi;

            _catDetail = new Cat();
        }

        async partial void OnCatIdChanging(int value)
        {
            try
            {
                SetTrueBoolValues();

                // Check if the user is logged in or not to get the cat details
                var response = _authProvider.IsLoggedIn
                    ? await _userApi.GetCatDetailsAsync(value)
                    : await _catApi.GetCatDetailsAsync(value);

                if (response.IsSuccess)
                {
                    var CatDto = response.Data;

                    if (CatDto != null)
                    {
                        // Map CatDto to Cat
                        CatDetail = MapCatDtoToCat(CatDto);
                    }
                }
                else
                {
                    await ShowAlertMessageAsync("Problem", "Problem while loading cat details", "Ok");
                }
            }
            catch (ApiException apiEx)
            {
                await ShowAlertMessageAsync("Error", "Error while loading cat details", "Ok");
                Debug.WriteLine($"ApiException from DetailsViewModel: {apiEx.Message}");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]
        private async Task ToggleFavoriteCatAsync()
        {
            try
            {
                if(!_authProvider.IsLoggedIn)
                {
                    await ShowAlertMessageAsync("Not Logged In User Error", "Please login", "Ok");
                    return;
                }

                if(CatDetail == null)
                {
                    await ShowAlertMessageAsync("Problem", "Problem while loading cat details", "Ok");
                    return;
                }

                SetTrueBoolValues();
                // Toggle favorite status
                CatDetail.IsFavorite = !CatDetail.IsFavorite;
                // Call the API to toggle favorite status
                await _userApi.ToggleCatFavoriteAsync(CatDetail.Id);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ToggleFavoriteCat: {ex.Message}");

                // Revert back the favorite status
                CatDetail.IsFavorite = !CatDetail.IsFavorite;
                await ShowAlertMessageAsync("Error", "Error while toggling favorite cat", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]
        private async Task GoBackPageAsync()
            => await GoToPageAsync("..");
        protected override void SetFalseBoolValues()
        {
            IsBusy = false;
        }

        protected override void SetTrueBoolValues()
        {
            IsBusy = true;
        }

        private Cat MapCatDtoToCat(CatDetailDto catDetailDto)
            => new Cat
            {
                Id = catDetailDto.Id,
                IsFavorite = catDetailDto.IsFavorite,
                Name = catDetailDto.Name,
                Description = catDetailDto.Description,
                ImageUrl = catDetailDto.ImageUrl,
                Age = catDetailDto.Age,
                Breed = catDetailDto.Breed,
                GenderDisplay = catDetailDto.GenderDisplay,
                GenderImage = catDetailDto.GenderImage,
                Price = catDetailDto.Price,
                AdoptionStatus = catDetailDto.AdoptionStatus
            };
    }
}
