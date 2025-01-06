using System.Collections.ObjectModel;

namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    public partial class FavoritesViewModel : BaseViewModel
    {
        private readonly IUserApi _userApi;
        private readonly IAuthService _authProvider;

        public ObservableCollection<Cat> FavoriteCats { get; set; }

        public FavoritesViewModel(IUserApi userApi,
                                  IAuthService authProvider)
        {
            _userApi = userApi;
            _authProvider = authProvider;

            FavoriteCats = new ObservableCollection<Cat>();

        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_authProvider.IsLoggedIn)
                {
                    var apiResponse = await _userApi.GetFavoriteCatsAsync();
                    if (apiResponse.IsSuccess &&
                            apiResponse.Data != null)
                    {
                        FavoriteCats.Clear();

                        foreach (var cat in apiResponse.Data.ToList())
                        {
                            FavoriteCats.Add(new Cat
                            {
                                Id = cat.Id,
                                Name = cat.Name,
                                ImageUrl = cat.ImageUrl,
                                Price = cat.Price,
                                Breed = cat.Breed,
                                IsFavorite = true
                            });
                        }
                    }
                    else
                    {
                        await ShowAlertMessageAsync("Error", "Error while getting favorite cats", "Ok");
                    }
                }
                else
                {
                    //Debug.WriteLine("Not Logged In - Please log in to application");
                    await ShowAlertMessageAsync("Not Logged In", "Please log in to application to load your favorite cats", "Ok");
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in InitializeAsync: {ex.Message}");
                await ShowAlertMessageAsync("Error", "Error while initializing favorites", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }

        [RelayCommand]
        private async Task ToggleFavoriteAsync(int catId)
        {
            try
            {
                Cat? currentFavoriteCat = FavoriteCats
                                         .FirstOrDefault(c => c.Id == catId);

                if (currentFavoriteCat != null)
                {
                    SetTrueBoolValues();
                    var apiResponse = await _userApi.ToggleCatFavoriteAsync(catId);
                    if (apiResponse.IsSuccess)
                    {
                        currentFavoriteCat.IsFavorite = !currentFavoriteCat.IsFavorite;
                        FavoriteCats.Remove(currentFavoriteCat);
                        await ShowAlertMessageAsync("Success", "Favorite toggled successfully", "Ok");
                        //await InitializeAsync();
                    }
                    else
                    {
                        await ShowAlertMessageAsync("Error", "Error while toggling favorite", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error in ToggleFavoriteAsync: {ex.Message}");
                await ShowAlertMessageAsync("Error", "Error while toggling favorite", "Ok");
            }
            finally
            {
                SetFalseBoolValues();
            }
        }


        protected override void SetFalseBoolValues()
        {
            IsBusy = false;
        }

        protected override void SetTrueBoolValues()
        {
            IsBusy = true;
        }
    }
}
