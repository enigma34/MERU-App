using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public interface IRegisterUser
    {
        void Add(RegisteredUser registeruser);
        bool ValidateRegisteredUser(RegisteredUser registeruser);
        bool ValidateUsername(RegisteredUser registeruser);
        int GetLoggedUserID(RegisteredUser registeruser);
    }
}