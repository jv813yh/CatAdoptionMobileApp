namespace CatAdoptionMobileApp.Domain.Models
{
    public class UserFavorites : CommonObject
    {
        public uint UserId { get; set; }
        public uint CatId { get; set; }

        public virtual User User { get; set; }
        public virtual Cat Cat { get; set; }
    }
}
