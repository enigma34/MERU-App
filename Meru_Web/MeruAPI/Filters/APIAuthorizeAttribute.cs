using System;
using System.Data.Entity;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Meru_Web.AES256Encryption;
using Meru_Web.DatabaseContext;

namespace Meru_Web.Filters
{
    public class APIAuthorizeAttribute:AuthorizeAttribute
    {
        MeruContext _context=new MeruContext();

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (Authorize(actionContext))
            {
                return;
            }
            HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        private bool Authorize(HttpActionContext actionContext)
        {
            try
            {
                var encodeString = actionContext.Request.Headers.GetValues("Token").First();
                bool validFlag = false;
                if (!string.IsNullOrEmpty(encodeString))
                {
                    var key = EncryptionLibrary.DecryptText(encodeString);
                    string[] parameters = key.Split(new char[] {':'});
                    var UserId = Convert.ToInt32(parameters[0]);
                    var RandomKey = parameters[1];
                    var Deviceid = parameters[2];
                    long ticks = long.Parse(parameters[3]);
                    DateTime issuedOn=new DateTime(ticks);
                    var ClientId = parameters[4];

                    var registerModel = (from register in _context.Key
                        where register.DeviceId == Deviceid
                              && register.UserId == UserId
                              && register.ClientId == ClientId
                        select register).FirstAsync().Result;
                    if (registerModel!=null)
                    {
                        var ExpiersOn = (from token in _context.Token
                            where token.DeviceId == Deviceid
                            select token.ExpiersOn).FirstAsync().Result;
                        validFlag = DateTime.Now <= ExpiersOn;
                    }
                    else
                    {
                        validFlag = false;
                    }
                }
                return validFlag;
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                throw;
            }
        }
    }
}