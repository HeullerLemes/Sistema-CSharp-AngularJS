
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace aulaWebApi.Authorization
{
    public class MyAuthorizationAttribute : ActionFilterAttribute
    {
        private IAuthProvider authoProvider = new JWTSimple();
        private List<string> claims = new List<string>();

        public MyAuthorizationAttribute(string[] strClaims)
        {
            claims.AddRange(strClaims);
        }

        public override void OnActionExecuting(HttpActionContext actionContext)
        {

           if (actionContext.Request.Headers.Authorization != null)
            {
                bool valid = false;
                string auth = actionContext.Request.Headers.Authorization.Parameter;
                foreach (string claim in claims)
                {
                    if (authoProvider.HasClaim(auth, claim))
                    {
                        valid = true;
                        break;
                    }
                }
                if (!valid)
                {
                    actionContext.Response = new System.Net.Http.HttpResponseMessage() { StatusCode = HttpStatusCode.Forbidden };
                }
            }
            else
            {
                actionContext.Response = new System.Net.Http.HttpResponseMessage() { StatusCode = HttpStatusCode.Unauthorized };
            }

        }
    }
}
