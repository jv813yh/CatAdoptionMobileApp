namespace CatAdoptionMobileApp.MAUI.Services
{
    public class CatHubProvider : ICatHubService
    {
        private HubConnection _hubConnection;

        /// <summary>
        /// Connects to the hub
        /// </summary>
        public void ConnectHub()
        {
            _hubConnection = new HubConnectionBuilder()
                .WithUrl(AppConstants.HubFullBaseUrl)
                .Build();
        }

        /// <summary>
        /// Disconnects from the hub
        /// </summary>
        public async Task DisconnectHubAsync()
        {
            if(_hubConnection == null)
            {
                return;
            }

            await _hubConnection.DisposeAsync();
        }

        /// <summary>
        /// Sets the event when the cat is adopted
        /// </summary>
        /// <param name="currentCatId"></param>
        /// <param name="catId"></param>
        /// <returns></returns>
        public Task SetOnCatAdoptedAsync(int currentCatId, int catId)
        {
            if (_hubConnection == null)
            {
                return Task.CompletedTask;
            }

            _hubConnection.On<int>(nameof(ICatClientHub.CatJustHasAdoptedAsync), async catId =>
            {
                if (currentCatId == catId)
                {
                    await App.Current.Dispatcher.DispatchAsync(() => Toast.Make("Cat has just adopted"));
                }
            });

            return Task.CompletedTask;
        }

        /// <summary>
        /// Sets the event when the cat is being viewed
        /// </summary>
        /// <param name="currentCatId"></param>
        /// <param name="catId"></param>
        /// <returns></returns>
        public Task SetOnCatIsBeingViewedAsync(int currentCatId, int catId)
        {
            if (_hubConnection == null)
            {
                return Task.CompletedTask;
            }

            _hubConnection.On<int>(nameof(ICatClientHub.CatIsBeingViewedAsync), async catId =>
            {
                if(currentCatId == catId)
                {
                    await App.Current.Dispatcher.DispatchAsync(() => Toast.Make("Someone else is also viewing this cat"));
                }
            });

            return Task.CompletedTask;
        }

        /// <summary>
        /// Starts the connection to the hub
        /// </summary>
        /// <returns></returns>
        public async Task StartConnectionAsync()
        {
            await _hubConnection.StartAsync();
        }

        /// <summary>
        /// Sends the message to the server
        /// </summary>
        /// <param name="nameOfMethod"></param>
        /// <param name="currentCatId"></param>
        /// <returns></returns>
        public async Task SendToServerAsync(string nameOfMethod, int currentCatId)
        {
            if (_hubConnection == null)
            {
                return;
            }

            await _hubConnection.SendAsync(nameOfMethod, currentCatId);
        }
    }
}
