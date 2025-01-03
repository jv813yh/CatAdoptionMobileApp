namespace CatAdoptionMobileApp.Domain.Models
{
    public class UserFavorites : CommonObject
    {
        public int UserId { get; set; }
        public int CatId { get; set; }

        public virtual User User { get; set; }
        public virtual Cat Cat { get; set; }
    }
}
