namespace Meru_Web.ViewModels
{
    public class UserCheck
    {
        public bool UserActive { get; set; }
        public bool ProfileUpdated { get; set; }
        public bool DeviceRegistered { get; set; }
        public bool TokenExists { get; set; }
        public bool TokenExpiered { get; set; }
    }
}