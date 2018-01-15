namespace Meru_Web.Repository
{
    public interface IUserCheck
    {
        bool UserActive(int UserId);
        bool ProfileUpdated(int UserId);
        bool DeviceRegistered(int UserId);
        bool TokenExists(string DeviceId);
        bool TokenExpiered(string DeviceId);
    }
}