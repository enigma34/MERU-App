using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meru_Web.Filters;
using Meru_Web.Models;
using Meru_Web.Repository;

namespace Meru_Web.Controllers
{
    [ValidateSessionAttribute]
    public class KeyController : Controller
    {
        // GET: Key
        private IClientKeys _clientKeysRepo;

        public KeyController()
        {
            _clientKeysRepo = new ClientKeysConcrete();
        }

        [HttpGet]
        public ActionResult Generate()
        {
            try
            {
                ClientKey cKey = new ClientKey();
                var keyExists = _clientKeysRepo.IsUniqueKeyAlreadyGenerate(Convert.ToInt32(Session["UserId"]));
                if (keyExists)
                {
                    cKey = _clientKeysRepo.GetGenerateUniqueKeyByUserID(Convert.ToInt32(Session["UserId"]));
                }
                else
                {
                    string clientId = string.Empty;
                    string clientSecret = String.Empty;
                    int deviceId = 0;

                    _clientKeysRepo.GenerateUniqueKey(out clientId,out clientSecret);

                    cKey.ClientKeyId = 0;
                    cKey.Createdon=DateTime.Now;
                    cKey.ClientId = "100";
                    cKey.ClientSecret = clientSecret;
                    cKey.DeviceId = "0";
                    cKey.UserId = Convert.ToInt32(Session["UserId"]);
                    _clientKeysRepo.SaveClientIDandClientSecert(cKey);
                }
                return View(cKey);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return View();
            }
        }

        [HttpPost]
        public ActionResult Generate(ClientKey cKey)
        {
            try
            {
                string clientID = string.Empty;
                string clientSecert = string.Empty;

                //Generate Keys
                _clientKeysRepo.GenerateUniqueKey(out clientID, out clientSecert);

                cKey.Createdon = DateTime.Now;
                cKey.ClientId = "100";
                cKey.ClientSecret = clientSecert;
                cKey.UserId = Convert.ToInt32(Session["UserID"]);
                _clientKeysRepo.UpdateClientIDandClientSecert(cKey);
                return View(cKey);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return View();
            }
        }
    }
}