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

        public Task SetOnCatAdoptedAsync(int currentCatId, int catId)
        {
            if (_hubConnection == null)
            {
                return Task.CompletedTask;
            }

            _hubConnection.On<int>(nameof(ICatClientHub.CatIsBeingViewedAsync), async catId =>
            {
                if (currentCatId == catId)
                {
                    await App.Current.Dispatcher.DispatchAsync(() => Toast.Make("Someone else is also viewing this cat"));
                }
            });

            return Task.CompletedTask;
        }

        public Task SetOnCatIsBeingViewedAsync(int currentCatId, int catId)
        {
            if (_hubConnection == null)
            {
                return Task.CompletedTask;
            }

            _hubConnection.On<int>(nameof(ICatClientHub.CatJustHasAdoptedAsync), async catId =>
            {
                if(currentCatId == catId)
                {
                    await App.Current.Dispatcher.DispatchAsync(() => Toast.Make("This cat has already been adopted"));
                }
            });

            return Task.CompletedTask;
        }

        public async Task StartConnectionAsync()
        {
            await _hubConnection.StartAsync();
        }

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
