namespace CatAdoptionMobileApp.Shared.Hubs.Interfaces
{
    public interface ICatClientHub
    {
        Task CatIsBeingViewedAsync(int catId);
        Task CatJustHasAdoptedAsync(int catId);
    }
}
