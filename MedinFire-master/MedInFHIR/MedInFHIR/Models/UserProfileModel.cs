namespace MedInFHIR.Models
{
    public class UserProfileModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int ContactNoPrimary { get; set; }
        public int ContactNoSecondary { get; set; }
        public bool ProfileUpdated { get; set; }
        public string UserProfilePicture { get; set; }
    }
}