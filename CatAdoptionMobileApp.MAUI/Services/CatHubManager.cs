namespace CatAdoptionMobileApp.MAUI.Services
{
    public class CatHubManager
    {
        private readonly ICatHubService _catHubService;
        public CatHubManager(ICatHubService catHubService)
        {
            _catHubService = catHubService;
        }

        public async Task ConfigureCatHubAsync(int currentCatId, int catId)
        {
            try
            {
                // Connect to the hub
                _catHubService.ConnectHub();
                // Set the events
                await _catHubService.SetOnCatIsBeingViewedAsync(currentCatId, catId);
                await _catHubService.SetOnCatAdoptedAsync(currentCatId, catId);

                // Start the connection
                await _catHubService.StartConnectionAsync();

                // Notify the hub that the cat is being viewed
                await SendToServerAsync(nameof(ICatServerHub.CatIsViewingAsync), currentCatId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // to do : log the exception
            }
        }

        public async Task SendToServerAsync(string nameOfMethod, int currentCatId)
        {
            try
            {
                await _catHubService.SendToServerAsync(nameOfMethod, currentCatId);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // to do : log the exception
            }
        }

        public async Task DisconnectCatHubAsync()
        {
            try
            {
                await _catHubService.DisconnectHubAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                // to do : log the exception
            }
        }
    }
}
