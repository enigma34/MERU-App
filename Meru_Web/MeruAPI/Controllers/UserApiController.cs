using System;
using System.Web.Http;
using Meru_Web.Filters;
using Meru_Web.Models;
using Meru_Web.Repository;

namespace Meru_Web.Controllers
{
    public class UserApiController : ApiController
    {
        //login
        //check user (user active status, user profile updated, device registered?, token exists?, token expiered? )
        //logout
        private IUserCheck _userCheck;

        private IUserProfile _userProfile;

        public UserApiController()
        {
            _userCheck=new UserCheckConcrete();
            _userProfile=new UserProfileConcerete();
        }

        [HttpGet]
        [ActionName("UserStatus")]
        public IHttpActionResult CheckUserStatus([FromUri(Name = "UserId")]int id)
        {
            try
            {
                var result =_userCheck.UserActive(id);
                return Ok<bool>(result);
            }
            catch (Exception e)
            {
                //throw;
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("ProfileStatus")]
        public IHttpActionResult CheckProfileUdateStatus([FromUri(Name = "UserId")]int id)
        {
            try
            {
                var result = _userCheck.ProfileUpdated(id);
                return Ok<bool>(result);
            }
            catch (Exception e)
            {
                //throw;
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("UserProfile")]
        public IHttpActionResult GetUserProfile([FromUri(Name = "UserId")]int id)
        {
            try
            {
                var result = _userProfile.GetUserProfile(id);
                return Ok<UserProfile>(result);
            }
            catch (Exception e)
            {
                //throw;
                return BadRequest("User profile does not exist. Please add user profile using website.");
            }
        }

        [HttpGet]
        [ActionName("UserProfileExsist")]
        public IHttpActionResult CheckUserProfileAdded([FromUri(Name = "UserId")]int id)
        {
            try
            {
                var result = _userProfile.CheckuserProfileExists(id);
                return Ok<bool>(result);
            }
            catch (Exception e)
            {
                //throw;
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("DeviceRegistrationStatus")]
        public IHttpActionResult CheckDeviceRegistration([FromUri(Name = "UserId")]int id)
        {
            try
            {
                var result = _userCheck.DeviceRegistered(id);
                if (result)
                {
                    return Ok<Tuple<bool, int>>(new Tuple<bool, int>(result, id));
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                //throw;
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("TokenStatus")]
        public IHttpActionResult CheckTokenStatus([FromUri(Name = "DeviceId")]int id)
        {
            try
            {
                var result = _userCheck.UserActive(id);
                return Ok<bool>(result);
            }
            catch (Exception e)
            {
                //throw;
                return BadRequest();
            }
        }

        [HttpGet]
        [ActionName("TokenExpiered")]
        public IHttpActionResult CheckTokenExpiration([FromUri(Name = "DeviceId")]int id)
        {
            try
            {
                var result = _userCheck.UserActive(id);
                return Ok<bool>(result);
            }
            catch (Exception e)
            {
                //throw;
                return BadRequest();
            }
        }
    }
}
