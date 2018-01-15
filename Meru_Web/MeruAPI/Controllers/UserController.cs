using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Meru_Web.AES256Encryption;
using Meru_Web.Models;
using Meru_Web.Repository;
using Microsoft.Ajax.Utilities;

namespace Meru_Web.Controllers
{
    public class UserController : Controller
    {
        private readonly IRegisterUser _registeruserRepo;

        private readonly IUserProfile _userProfileRepo;

        public UserController()
        {
            _userProfileRepo = new UserProfileConcerete();
            _registeruserRepo=new RegisterUserConcrete();
        }


        // GET: User/Register
        [HttpGet]
        public ActionResult Register() => View(new RegisteredUser());

        // POST: User/Register
        [HttpPost]
        public ActionResult Register(RegisteredUser user)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("Register", user);
                }
                if (_registeruserRepo.ValidateRegisteredUser(user))
                {
                    ModelState.AddModelError("",@"User is already registered");
                    return View("Register", user);
                }

                user.CreatedOn = DateTime.Now;

                user.Password = EncryptionLibrary.EncryptText(user.Password);

                user.ActiveStatus = true;

                user.UserType = 1;

                _registeruserRepo.Add(user);

                TempData["UserManager"] = "User registered successfully";
                ModelState.Clear();
                return RedirectToAction("Login");
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return View();
            }
        }

        //GET: User/Login
        [HttpGet]
        public ActionResult Login()
        {
            return View(new RegisteredUser());
        }

        // POST: User/Register
        [HttpPost]
        public ActionResult Login(RegisteredUser user)
        {
            try
            {
                if (string.IsNullOrEmpty(user.UserName)&&string.IsNullOrEmpty(user.Password))
                {
                    ModelState.AddModelError("", @"Enter Username and Password");
                }
                else if (string.IsNullOrEmpty(user.UserName))
                {
                    ModelState.AddModelError("", @"Enter Username");
                }
                else if (string.IsNullOrEmpty(user.Password))
                {
                    ModelState.AddModelError("", @"Enter Password");
                }
                else
                {
                    user.Password = EncryptionLibrary.EncryptText(user.Password);
                    if (_registeruserRepo.ValidateRegisteredUser(user))
                    {
                        var userId = _registeruserRepo.GetLoggedUserID(user);
                        Session["UserId"] = userId;
                        return RedirectToAction("GetProfile");
                    }
                    else
                    {
                        ModelState.AddModelError("",@"User is already registered");
                        return View("Register", user);
                    }
                }
                return View("Login", user);
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return View();
            }
        }

        public ActionResult LogOut()
        {
            Session.Abandon();
            return RedirectToAction("Login");
        }

        //Get: user/profile
        public ActionResult GetProfile()
        {
            int userId=Convert.ToInt32(Session["UserId"]);
            try
            {
                //bool result = int.TryParse(Session["UserId"], out userId);
                //if (result)
                //{
                var result = _userProfileRepo.CheckuserProfileExists(userId);
                    if (result)
                    {
                        var profile = _userProfileRepo.GetUserProfile(userId);
                        return View(profile);
                    }
                    else
                    {
                       return RedirectToAction("AddProfile");
                    }
                    
                //}
            }
            catch (Exception e)
            {
                return View();
            }
            return View();
        }

        [HttpGet]
        public ActionResult AddProfile()
        {
            return View();
        }

        //Post: user/profile
        [HttpPost]
        public ActionResult AddProfile(UserProfile profile)
        {
            int userId = Convert.ToInt32(Session["UserId"]);
            try
            {
                //bool result = int.TryParse((string)Session["UserId"], out userId);
                //if (result)
                //{
                profile.UserId = userId;
                   _userProfileRepo.AddUserProfile(profile);
                return RedirectToAction("GetProfile");
               // }
            }
            catch (Exception e)
            {
                //Console.WriteLine(e);
                //throw;
                return View();
            }
            //return View();
        }

        //Post: user/profile
        [HttpPost]
        public ActionResult UpdateProfile(UserProfile profile)
        {
            throw new NotImplementedException();
        }
    }
}