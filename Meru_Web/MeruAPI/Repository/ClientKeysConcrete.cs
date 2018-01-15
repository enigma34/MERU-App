using System;
using System.Data;
using System.Data.Entity;
using System.Linq;
using Meru_Web.AES256Encryption;
using Meru_Web.DatabaseContext;
using Meru_Web.Models;

namespace Meru_Web.Repository
{
    public class ClientKeysConcrete:IClientKeys
    {
        private MeruContext _context;

        public ClientKeysConcrete()
        {
            _context=new MeruContext();
        }

        public bool IsUniqueKeyAlreadyGenerate(int UserID)
        {
            bool KeyExsists = _context.Key.AnyAsync(clientkey => clientkey.UserId.Equals(UserID)).Result;
            return KeyExsists;
        }

        public void GenerateUniqueKey(out string ClientID, out string ClientSecert)
        {
            //Change client Id whith device id
            ClientID = EncryptionLibrary.KeyGenerator.GetUniqueKey();
            ClientSecert = EncryptionLibrary.KeyGenerator.GetUniqueKey();
        }

        public int SaveClientIDandClientSecert(ClientKey ClientKeys)
        {
            _context.Key.Add(ClientKeys);
            return _context.SaveChangesAsync().Result;
        }

        public int UpdateClientIDandClientSecert(ClientKey ClientKeys)
        {
            try
            {
                var userId = ClientKeys.UserId;
                var dpt = GetGenerateUniqueKeyByUserID(userId);
                _context.Entry(dpt).CurrentValues.SetValues(ClientKeys);
                return _context.SaveChanges();
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw;
            }
          
            //_context.Entry(ClientKeys).State=EntityState.Modified;
            //return _context.SaveChangesAsync().Result;
        }

        public ClientKey GetGenerateUniqueKeyByUserID(int UserID)
        {
            var clientKey = (from key in _context.Key
                where key.UserId == UserID
                select key).FirstAsync();
            return clientKey.Result;
        }
    }
}