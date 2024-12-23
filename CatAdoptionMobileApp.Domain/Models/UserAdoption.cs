namespace CatAdoptionMobileApp.Domain.Models
{
    public class UserAdoption : CommonObject
    {
        public uint UserId { get; set; }
        public uint CatId { get; set; }
        public DateTime AdoptedOn { get; set; }

        public virtual User User { get; set; }
        public virtual Cat Cat { get; set; }
    }
}
