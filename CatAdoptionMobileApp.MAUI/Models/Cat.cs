namespace CatAdoptionMobileApp.MAUI.Models
{
    public partial class Cat : ObservableObject
    {
        [ObservableProperty]
        private bool _isFavorite;
        [ObservableProperty]
        private AdoptionStatus _adoptionStatus;

        public int Id { get; set; }
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public double Price { get; set; }
        public string Breed { get; set; }
        public string Description { get; set; }
        public string GenderDisplay { get; set; }
        public string GenderImage { get; set; }
        public string Age { get; set; }
    }
}
