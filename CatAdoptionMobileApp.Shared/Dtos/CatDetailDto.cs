using CatAdoptionMobileApp.Shared.Enumerations;

namespace CatAdoptionMobileApp.Shared.Dtos
{
    public class CatDetailDto : CatListDto
    {
        public bool IsFavorite { get; set; }
        public string Description { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public AdoptionStatus AdoptionStatus { get; set; }
        public string GenderDisplay
            => Gender.ToString();
        public string GenderImage
            => Gender switch
            {
                Gender.Male => "male",
                Gender.Female => "female"
            };

        public string Age
        {
            get
            {
                DateTime today = DateTime.Now;
                int age = today.Year - DateOfBirth.Year;
                if (!(DateOfBirth > today.AddYears(age)))
                {
                    ++age;
                }

                string ageString = age switch
                {
                    0 => "Less than a year",
                    1 => "1 year",
                    _ => $"{age} years"
                };

                return ageString;
            }
        }
    }
}
