namespace CatAdoptionMobileApp.Domain.Exceptions
{
    public class NotAvailableAdoptionCat : Exception
    {
        public NotAvailableAdoptionCat(string message) : base(message)
        {
        }
        public NotAvailableAdoptionCat(string message, Exception innerException): base(message, innerException) 
        {
        }
    }
}
