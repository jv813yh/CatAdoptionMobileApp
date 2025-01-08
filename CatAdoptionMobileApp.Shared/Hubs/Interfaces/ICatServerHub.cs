namespace CatAdoptionMobileApp.Shared.Hubs.Interfaces
{
    public interface ICatServerHub
    {
        Task CatIsViewingAsync(int catId);
        Task CatIsNotAlreadyViewingAsync(int catId);
        Task CatJustHasAdoptedAsync(int catId);
    }
}
