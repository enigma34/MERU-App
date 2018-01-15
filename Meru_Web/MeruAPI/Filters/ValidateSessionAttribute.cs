using System;
using System.Web.Mvc;

namespace Meru_Web.Filters
{
    public class ValidateSessionAttribute:FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (string.IsNullOrEmpty(Convert.ToString(filterContext.HttpContext.Session["UserId"])))
            {
                ViewResult result = new ViewResult();
                result.ViewName = "Error";
                result.ViewBag.ErrorMessage = "Cannot access pages without login";
                filterContext.Result = result;
            }
        }
    }
}