namespace CatAdoptionMobileApp.Domain.Models
{
    public class UserAdoption : CommonObject
    {
        public int UserId { get; set; }
        public int CatId { get; set; }
        public DateTime AdoptedOn { get; set; }

        public virtual User User { get; set; }
        public virtual Cat Cat { get; set; }
    }
}
