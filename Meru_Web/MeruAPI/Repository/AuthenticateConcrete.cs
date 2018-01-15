using System;
using System.Data.Entity;
using System.Linq;
using Meru_Web.AES256Encryption;
using Meru_Web.DatabaseContext;
using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public class AuthenticateConcrete:IAuthenticate
    {
        private MeruContext _context;

        public AuthenticateConcrete()
        {
            _context=new MeruContext();
        }

        public ClientKey GetClientKeysDetailsbyCLientIDandClientSecert(string clientID, string clientSecert)
        {
            try
            {
                var result = (from keys in _context.Key
                    where keys.ClientId == clientID
                          && keys.ClientSecret == clientSecert
                    select keys).FirstAsync().Result;
                return result;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw;
            }
        }

        public bool ValidateKeys(ClientKey ClientKeys)
        {
            try
            {
                var result = (from keys in _context.Key
                    where keys.ClientId == ClientKeys.ClientId
                          && keys.ClientSecret == ClientKeys.ClientSecret
                    select keys).Count();
                return result > 0;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw;
            }
        }

        public bool IsTokenAlreadyExists(string deviceId)
        {
            try
            {
                var result = (from keys in _context.Token
                    where keys.DeviceId == deviceId
                    select keys).Count();
                return result > 0;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw;
            }
        }

        public int DeleteGenerateToken(string deviceId)
        {
            try
            {
                var token = _context.Token.SingleOrDefault(x => x.DeviceId == deviceId);
                _context.Token.Remove(token);
                return _context.SaveChanges();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int InsertToken(TokenManager token)
        {
            try
            {
                _context.Token.Add(token);
                return _context.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public string GenerateToken(ClientKey ClientKeys, DateTime IssuedOn)
        {
            try
            {
                string randomnumber =
                    string.Join(":", new string[]
                    {   Convert.ToString(ClientKeys.UserId),
                        EncryptionLibrary.KeyGenerator.GetUniqueKey(),
                        Convert.ToString(ClientKeys.DeviceId),
                        Convert.ToString(IssuedOn.Ticks),
                        ClientKeys.ClientId
                    });

                return EncryptionLibrary.EncryptText(randomnumber);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}