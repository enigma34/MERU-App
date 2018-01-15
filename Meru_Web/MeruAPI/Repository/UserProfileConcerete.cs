using System.Data.Entity;
using System.Linq;
using Meru_Web.DatabaseContext;
using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public class UserProfileConcerete:IUserProfile
    {
        private MeruContext _context;

        public UserProfileConcerete()
        {
            _context=new MeruContext();
        }

        public bool CheckuserProfileExists(int userId)
        {
            var userProfile = (from user in _context.Profile
                where user.UserId == userId
                select user).Count();
            return userProfile > 0;
        }

        public UserProfile GetUserProfile(int userId)
        {
            var userProfile = (from user in _context.Profile
                               where user.UserId == userId
                               select user).FirstAsync();
            return userProfile.Result;
        }

        public void AddUserProfile(UserProfile user)
        {
            _context.Profile.Add(user);
            _context.SaveChangesAsync();
        }

        public int UpdateUserProfile(UserProfile user)
        {
            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();
            return _context.SaveChanges();
        }
    }
}