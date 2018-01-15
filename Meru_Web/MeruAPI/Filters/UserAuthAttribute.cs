using System;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Controllers;

namespace Meru_Web.Filters
{
    public class UserAuthAttribute: AuthorizeAttribute
    {
        public override Task OnAuthorizationAsync(HttpActionContext actionContext, CancellationToken cancellationToken)
        {
            //if (Authorize(actionContext))
            //{
                return base.OnAuthorizationAsync(actionContext, cancellationToken);
            //}
            //HandleUnauthorizedRequest(actionContext);
        }

        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            base.HandleUnauthorizedRequest(actionContext);
        }

        //private bool Authorize(HttpActionContext actionContext)
        //{
            
        //}
    }
}