namespace CatAdoptionMobileApp.Domain.Exceptions
{
    public class AlreadyAdoptedCatByUser : Exception
    {
        public AlreadyAdoptedCatByUser(string message) : base(message)
        {
        }
        public AlreadyAdoptedCatByUser(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
