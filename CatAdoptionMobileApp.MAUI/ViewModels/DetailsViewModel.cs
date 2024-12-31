namespace CatAdoptionMobileApp.MAUI.ViewModels
{
    [QueryProperty(nameof(CatId), nameof(CatId))]
    public partial class DetailsViewModel : BaseViewModel
    {
        private readonly ICatApi _catApi;
        [ObservableProperty]
        private int _catId;

        [ObservableProperty]
        private CatDetailDto _catDetail;

        public DetailsViewModel(ICatApi catApi)
        {
            _catApi = catApi;
            _catDetail = new CatDetailDto();
        }

        async partial void OnCatIdChanging(int value)
        {
            try
            {
                SetTrueBoolValues();

                var response = await _catApi.GetCatDetailsAsync(value);
                if (response.IsSuccess)
                {
                    CatDetail = response.Data;
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
