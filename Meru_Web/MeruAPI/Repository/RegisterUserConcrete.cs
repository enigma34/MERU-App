using System.Data.Entity;
using System.Linq;
using Meru_Web.DatabaseContext;
using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public class RegisterUserConcrete : IRegisterUser
    {
        private readonly MeruContext _context;

        public RegisterUserConcrete()
        {
            _context=new MeruContext();
        }

        public void Add(RegisteredUser registeruser)
        {
            _context.User.Add(registeruser);
            _context.SaveChangesAsync();
        }

        public int GetLoggedUserID(RegisteredUser registeruser)
        {
            var userCount = (from user in _context.User
                where user.UserName == registeruser.UserName
                      && user.Password == registeruser.Password
                select user.UserId).FirstAsync();
            return userCount.Result;
        }

        public bool ValidateRegisteredUser(RegisteredUser registeruser)
        {
            var userCount = (from user in _context.User
                where user.UserName == registeruser.UserName
                      && user.Password == registeruser.Password
                select user).Count();
            return userCount > 0;
        }

        public bool ValidateUsername(RegisteredUser registeruser)
        {
            var userCount = (from user in _context.User
                where user.UserName == registeruser.UserName
                select user).Count();
            return userCount > 0;
        }
    }
}