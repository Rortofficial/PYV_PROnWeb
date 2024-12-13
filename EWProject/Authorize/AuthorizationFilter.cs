using System.Web.Mvc;
//using System.Web.Mvc;

namespace Client.Authorize
{
    public class AuthorizationFilter : AuthorizeAttribute, IAuthorizationFilter
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public AuthorizationFilter(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }
        void IAuthorizationFilter.OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.ActionDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true)
                || filterContext.ActionDescriptor.ControllerDescriptor.IsDefined(typeof(AllowAnonymousAttribute), true))
            {
                // Don't check for authorization as AllowAnonymous filter is applied to the action or controller
                return;
            }

            // Check for authorization
            if (httpContextAccessor.HttpContext.Session.GetString("UserIDName") == null)
            {
                filterContext.Result = new HttpUnauthorizedResult();
            }
        }
    }

}
