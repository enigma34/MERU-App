using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Meru_Web.Models;
using Meru_Web.Repository;

namespace Meru_Web.Controllers
{
    public class DeviceApiController : ApiController
    {
        private IClientKeys _clientKeys;
        IAuthenticate _IAuthenticate;

        public DeviceApiController()
        {
            _clientKeys=new ClientKeysConcrete();
            _IAuthenticate=new AuthenticateConcrete();
        }

        [HttpPost]
        public IHttpActionResult RegisterDevice([FromUriAttribute(Name = "UserId")]int uid, [FromUriAttribute(Name = "DeviceId")]string did)
        {
            try
            {
                ClientKey clientkeys = new ClientKey();

                // Validating ClientID and ClientSecert already Exists
                var keyExists = _clientKeys.IsUniqueKeyAlreadyGenerate(uid);

                if (keyExists)
                {
                    //// Getting Generate ClientID and ClientSecert Key By UserID
                    var ck = _clientKeys.GetGenerateUniqueKeyByUserID(uid);
                    //if (ck.UserId==cks.UserId && ck.DeviceId == cks.DeviceId)
                    //{
                    //    return Ok<ClientKey>(ck);
                    //}
                    // clientkeys = refreshClient(cks);
                    //if (ck.UserId == uid && ck.DeviceId == did)
                    //{
                    //    return Ok<ClientKey>(ck);
                    //}
                    //else
                    //{
                    string clientID = string.Empty;
                    string clientSecert = string.Empty;

                    _clientKeys.GenerateUniqueKey(out clientID, out clientSecert);

                        clientkeys.ClientKeyId = ck.ClientKeyId;
                        ck.DeviceId = did;
                        ck.Createdon = DateTime.Now;
                        ck.ClientId = clientID;
                        ck.ClientSecret = clientSecert;
                        ck.UserId = uid;
                        _clientKeys.UpdateClientIDandClientSecert(ck);
                        return Ok<ClientKey>(ck);

                   // }
                    

                    //_clientKeys.GenerateUniqueKey(out clientID, out clientSecert);
                    //return Ok<ClientKey>(clientkeys);
                }
                else
                {
                    string clientID = string.Empty;
                    string clientSecert = string.Empty;
                    int deviceId = 0;

                    //Generate Keys
                    _clientKeys.GenerateUniqueKey(out clientID, out clientSecert);

                    //Saving Keys Details in Database
                    clientkeys.ClientKeyId = 0;
                    clientkeys.DeviceId= did;
                    clientkeys.Createdon = DateTime.Now;
                    clientkeys.ClientId = clientID;
                    clientkeys.ClientSecret = clientSecert;
                    clientkeys.UserId = uid;
                    _clientKeys.SaveClientIDandClientSecert(clientkeys);
                    return Ok<ClientKey>(clientkeys);
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return BadRequest();
            }
        }


        //ClientKey refreshClient(ClientInformation ci)
        //{
        //    try
        //    {
        //        string clientID = string.Empty;
        //        string clientSecert = string.Empty;

        //        //Generate Keys
        //        _clientKeys.GenerateUniqueKey(out clientID, out clientSecert);

        //        ClientKey clientkeys = new ClientKey();

        //        //Updating ClientID and ClientSecert 
        //        clientkeys.DeviceId = ci.DeviceId;
        //        clientkeys.Createdon = DateTime.Now;
        //        clientkeys.ClientId = clientID;
        //        clientkeys.ClientSecret = clientSecert;
        //        clientkeys.UserId = ci.UserId;
        //        _clientKeys.UpdateClientIDandClientSecert(clientkeys);
        //        return clientkeys;
        //        //return Ok<ClientKey>(clientkeys);
        //    }
        //    catch (Exception e)
        //    {
        //        //Console.WriteLine(e);
        //       throw;
        //       // return BadRequest();
        //    }
        //}

        //[HttpPost]
        //public IHttpActionResult RefreshRegisteredDevice([FromBody] ClientKey clientkeys)
        //{
        //    try
        //    {
        //        string clientID = string.Empty;
        //        string clientSecert = string.Empty;

        //        //Generate Keys
        //        _clientKeys.GenerateUniqueKey(out clientID, out clientSecert);

        //        //Updating ClientID and ClientSecert 
        //        clientkeys.DeviceId = clientkeys.DeviceId;
        //        clientkeys.Createdon = DateTime.Now;
        //        clientkeys.ClientId = clientID;
        //        clientkeys.ClientSecret = clientSecert;
        //        clientkeys.UserId = clientkeys.UserId;
        //        _clientKeys.UpdateClientIDandClientSecert(clientkeys);
        //        return Ok<ClientKey>(clientkeys);
        //    }
        //    catch (Exception e)
        //    {
        //        //Console.WriteLine(e);
        //        //throw;
        //        return BadRequest();
        //    }
        //}

        [HttpPost]
        public IHttpActionResult GenerateToken([FromBody] ClientKey clientkeys)
        {
            try
            {
                if (string.IsNullOrEmpty(clientkeys.ClientId) && string.IsNullOrEmpty(clientkeys.ClientSecret))
                {
                   return BadRequest("Not Valid Request");
                }
                else
                {
                    if (_IAuthenticate.ValidateKeys(clientkeys))
                    {
                        var keys = _IAuthenticate.GetClientKeysDetailsbyCLientIDandClientSecert(clientkeys.ClientId,
                            clientkeys.ClientSecret);
                        if (keys==null)
                        {
                            return NotFound();
                        }
                        else
                        {
                            var result = _IAuthenticate.IsTokenAlreadyExists(clientkeys.DeviceId);
                            if (result)
                            {
                                _IAuthenticate.DeleteGenerateToken(clientkeys.DeviceId);
                                var token = GenerateandSaveToken(clientkeys);
                                return Ok<TokenManager>(token);
                            }
                            else
                            {
                                var token = GenerateandSaveToken(clientkeys);
                                return Ok<TokenManager>(token);
                            }
                        }
                    }
                    else
                    {
                        return NotFound();
                    }
                }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return BadRequest();
            }
        }

        [NonAction]
        private TokenManager GenerateandSaveToken(ClientKey clientkeys)
        {
            var IssuedOn = DateTime.Now;
            var newToken = _IAuthenticate.GenerateToken(clientkeys, IssuedOn);
            TokenManager token = new TokenManager();
            token.TokenId = 0;
            token.TokenKey = newToken;
            token.DeviceId = clientkeys.DeviceId;
            token.IssueOn = IssuedOn;
            token.ExpiersOn = DateTime.Now.AddMinutes(Convert.ToInt32(ConfigurationManager.AppSettings["TokenExpiry"]));
            token.CreatedOn = DateTime.Now;
            var result = _IAuthenticate.InsertToken(token);
            return token;
            //if (result == 1)
            //{
            //    HttpResponseMessage response = new HttpResponseMessage();
            //    response = Request.CreateResponse(HttpStatusCode.OK, "Authorized");
            //    response.Headers.Add("Token", newToken);
            //    response.Headers.Add("TokenExpiry", ConfigurationManager.AppSettings["TokenExpiry"]);
            //    response.Headers.Add("Access-Control-Expose-Headers", "Token,TokenExpiry");
            //    return response;
            //}
            //else
            //{
            //    var message = new HttpResponseMessage(HttpStatusCode.NotAcceptable);
            //    message.Content = new StringContent("Error in Creating Token");
            //    return message;
            //}
        }

        //[HttpPost]
        //public IHttpActionResult RefreshToken([FromBody] ClientKey clientkeys)
        //{
        //    try
        //    {

        //    }
        //    catch (Exception e)
        //    {
        //        //Console.WriteLine(e);
        //        //throw;
        //        return BadRequest();
        //    }
        //}
    }
}
