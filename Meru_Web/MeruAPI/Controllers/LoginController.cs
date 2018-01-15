using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Controllers;
using Meru_Web.AES256Encryption;
using Meru_Web.Models;
using Meru_Web.Repository;

namespace Meru_Web.Controllers
{
    public class LoginController : ApiController
    {
        private IRegisterUser _registeruserRepo;

        public LoginController()
        {
            _registeruserRepo=new RegisterUserConcrete();
        }

        [HttpPost]
        [ActionName("Login")]
        public IHttpActionResult Login(RegisteredUser user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName) && string.IsNullOrEmpty(user.Password))
                {
                    //ModelState.AddModelError("", @"Enter Username and Password");
                    return BadRequest(@"Enter Username and Password");
                }
                else if (string.IsNullOrEmpty(user.UserName))
                {
                    //ModelState.AddModelError("", @"Enter Username");
                    return BadRequest(@"Enter Username");
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    //ModelState.AddModelError("", @"Enter Password");
                    return BadRequest(@"Enter Password");
                }
                else
                {
                    user.Password = EncryptionLibrary.EncryptText(user.Password);
                    if (_registeruserRepo.ValidateRegisteredUser(user))
                    {
                        var userId = _registeruserRepo.GetLoggedUserID(user);
                        //Session["UserId"] = userId;
                        //return RedirectToAction("GetProfile");
                        return Ok<int>(userId);
                    }
                    else
                    {
                        //ModelState.AddModelError("", @"User is already registered");
                        return Ok<string>(@"User is already loged in");
                    }
                }
                //return View("Login", user);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return BadRequest();
            }
        }

        //[System.Web.Http.HttpPost]
        //public IHttpActionResult LogOut(RegisteredUser user)
        //{
        //    try
        //    {
               
                    
        //    }
        //    catch (Exception e)
        //    {
        //        return BadRequest(e.Message);
        //    }
        //}
    }
}
