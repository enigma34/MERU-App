using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Meru_Web.Models
{
    public class UserProfile
    {
        [Key]
        public int ProfileId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public int ContactNoPrimary { get; set; }
        public int ContactNoSecondary { get; set; }
        public bool ProfileUpdated { get; set; }
        public string UserProfilePicture { get; set; }
        public int UserId { get; set; }
    }
}