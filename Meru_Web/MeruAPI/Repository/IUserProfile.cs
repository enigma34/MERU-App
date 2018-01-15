using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public interface IUserProfile
    {
        bool CheckuserProfileExists(int userId);
        UserProfile GetUserProfile(int userId);
        void AddUserProfile(UserProfile user);
        int UpdateUserProfile(UserProfile user);
    }
}