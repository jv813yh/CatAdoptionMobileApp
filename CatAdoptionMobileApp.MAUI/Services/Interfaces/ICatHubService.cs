namespace CatAdoptionMobileApp.MAUI.Services.Interfaces
{
    public interface ICatHubService
    {
        void ConnectHub();
        Task StartConnectionAsync();
        Task DisconnectHubAsync();
        Task SetOnCatIsBeingViewedAsync(int currenCattId,int catId);
        Task SetOnCatAdoptedAsync(int currentCatId, int catId);
        Task SendToServerAsync(string nameOfMethod, int currentCatId);
    }
}
