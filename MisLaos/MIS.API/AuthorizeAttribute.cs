using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;

namespace MIS.API
{
    public class AuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(System.Web.Http.Controllers.HttpActionContext actionContext)
        {
            if (!HttpContext.Current.User.Identity.IsAuthenticated)
            {
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        Message = "Unauthorized",
                        StatusCode = HttpStatusCode.Unauthorized
                    })),

                };
            }
            else
            {
                //Setting error message and status fode 403 for unauthorized user
                actionContext.Response = new HttpResponseMessage(System.Net.HttpStatusCode.Forbidden)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(new
                    {
                        Message = "Access Denied",
                        StatusCode = HttpStatusCode.Forbidden
                    })),

                };
            }

        }
    }
}