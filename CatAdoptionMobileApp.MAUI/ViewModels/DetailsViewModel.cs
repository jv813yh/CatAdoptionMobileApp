namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    [QueryProperty(nameof(CatId), nameof(CatId))]
    public partial class DetailsViewModel : BaseViewModel, IAsyncDisposable
    {
        private readonly ICatApi _catApi;
        private readonly IAuthService _authProvider;
        private readonly IUserApi _userApi;

        private readonly CatHubManager _catHubManager;

        [ObservableProperty]
        private int _catId;

        [ObservableProperty]
        private CatExtended _catDetail;

        public DetailsViewModel(ICatApi catApi,
                                IAuthService authProvider,
                                IUserApi userApi,
                                CatHubManager catHubManager)
        {
            _catApi = catApi;
            _authProvider = authProvider;
            _userApi = userApi;
            _catHubManager = catHubManager;

            _catDetail = new CatExtended();
        }

        async partial void OnCatIdChanging(int value)
        {
            try
            {
                SetTrueBoolValues();

                await _catHubManager.ConfigureCatHubAsync(CatDetail.Id, value);

                //Check if the user is logged in or not to get the cat details
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
                // Call the API to toggle favorite cat
                var resultApi = await _userApi.ToggleCatFavoriteAsync(CatDetail.Id);

                if (resultApi.IsSuccess)
                {
                    // Toggle favorite status
                    CatDetail.IsFavorite = !CatDetail.IsFavorite;
                    await ShowToastMessageAsync("Favorite status updated");
                }
                else
                {
                    await ShowAlertMessageAsync("Error", "Error while toggling favorite cat", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ToggleFavoriteCat: {ex.Message}");
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

        [RelayCommand]
        private async Task AdoptCatNowAsync(int catId)
        {
            if (!_authProvider.IsLoggedIn)
            {
               bool result = await ShowConfirmMessageAsync("Not Logged In User Error", "Please login !\nDo you want to go to login page ?", 
                   "Yes", "No");
                if (result)
                {
                    await GoToPageAsync($"//{nameof(LoginRegisterPage)}");
                }
            }
            try
            {
                SetTrueBoolValues();
                // Call the API to adopt cat
                var apiResponse = await _userApi.AdoptCatAsync(catId);
                if (apiResponse.IsSuccess)
                {
                    // Notify the hub that the cat has been adopted
                    await _catHubManager.SendToServerAsync(nameof(ICatServerHub.CatJustHasAdoptedAsync), catId);
                    // Navigate to Adoption Success Page if the adoption is successful
                    await GoToPageAsync(nameof(AdoptionSuccessPage));
                }
                else
                {
                    await ShowToastMessageAsync("Error while adopting cat");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in AdoptCatNowAsync: {ex.Message}");
                await ShowAlertMessageAsync("Error", "Error while adopting cat", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        private CatExtended MapCatDtoToCat(CatDetailDto catDetailDto)
            => new CatExtended
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

        public async ValueTask DisposeAsync()
        {
            // Notify the hub that the cat is not being viewed
            await _catHubManager.SendToServerAsync(nameof(ICatServerHub.CatIsNotAlreadyViewingAsync), CatDetail.Id);
            // Disconnect the hub
            await _catHubManager.DisconnectCatHubAsync();
        }
    }
}
