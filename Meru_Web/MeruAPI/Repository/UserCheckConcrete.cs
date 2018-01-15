using System;
using System.Data.Entity;
using System.Linq;
using Meru_Web.DatabaseContext;

namespace Meru_Web.Repository
{
    public class UserCheckConcrete:IUserCheck
    {
        private MeruContext _context;

        public UserCheckConcrete()
        {
            _context=new MeruContext();
        }

        public bool UserActive(int UserId)
        {
            var result = (from user in _context.User
                where user.UserId == UserId
                select user.ActiveStatus).FirstAsync().Result;
            return result;
        }

        public bool ProfileUpdated(int UserId)
        {
            var result = (from user in _context.Profile
                where user.UserId == UserId
                select user.ProfileUpdated).FirstAsync().Result;
            return result;
        }

        public bool DeviceRegistered(int UserId)
        {
            var result = (from device in _context.Key
                where device.UserId == UserId
                select device).CountAsync().Result;
            return result > 0;
        }

        public bool TokenExists(string DeviceId)
        {
            var result = (from token in _context.Token
                where token.DeviceId == DeviceId
                          select token).CountAsync().Result;
            return result > 0;
        }

        public bool TokenExpiered(string DeviceId)
        {
            var result = (from token in _context.Token
                where token.DeviceId == DeviceId
                select token.ExpiersOn).FirstAsync().Result;
            return DateTime.Now>result;
        }
    }
}