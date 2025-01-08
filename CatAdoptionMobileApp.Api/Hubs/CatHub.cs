using CatAdoptionMobileApp.Shared.Hubs.Interfaces;
using Microsoft.AspNetCore.SignalR;

namespace CatAdoptionMobileApp.Api.Hubs
{
    public class CatHub : Hub<ICatClientHub>, ICatServerHub
    {
        private readonly Dictionary<int, int> _catsBeingViewed;

        public CatHub()
        {
            _catsBeingViewed = new Dictionary<int, int>();
        }

        public Task CatIsNotAlreadyViewingAsync(int catId)
        {
            if (_catsBeingViewed.TryGetValue(catId, out int currentlyViewing))
            {
                currentlyViewing--;
            }

            if (currentlyViewing < 0)
            {
                _catsBeingViewed.Remove(catId);
            }
            else
            {
                _catsBeingViewed[catId] = currentlyViewing;
            }

            return Task.CompletedTask;
        }

        public async Task CatIsViewingAsync(int catId)
        {
            if (!_catsBeingViewed.TryGetValue(catId, out int currentlyViewing))
            {
                currentlyViewing = 0;
            }
            else
            {
                // Notify client that cat is being viewed
                await Clients.Caller.CatIsBeingViewedAsync(catId);
            }

            _catsBeingViewed[catId] = currentlyViewing++;

            // Notify others clients that cat is being viewed
            await Clients.Others.CatIsBeingViewedAsync(catId);
        }

        public async Task CatJustHasAdoptedAsync(int catId)
        {
            // Notify others clients that cat has been adopted
            await Clients.Others.CatJustHasAdoptedAsync(catId);

            // Remove cat from being viewed
            if(_catsBeingViewed.ContainsKey(catId))
            {
                _catsBeingViewed.Remove(catId);
            }
        }
    }
}
