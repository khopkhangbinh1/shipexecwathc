using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace iMWS.Api.Filters
{
    public class JwtAuth : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<SkipAuthAttribute>(false).Any())
            {
                return;
            }

            var auth = actionContext.Request.Headers.Authorization;
            if (auth == null || auth.Scheme != "Bearer")
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                return;
            }

            try
            {
                // 取得 Token 之後，帶回 Auth 驗證。

                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings["Auth2Url"]);

                var verifyResult = client.PostAsJsonAsync("api/Authentication/VerifyToken",
                        new { Token = auth.Parameter }).Result;

                VerifyTokenResult result = verifyResult.Content.ReadAsAsync<VerifyTokenResult>().Result;

                if (!result.IsSuccess)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                }

                if (result.ReNew)
                {
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
                    actionContext.Response.Headers.Add("Access-Control-Expose-Headers", "*");
                    actionContext.Response.Headers.Add("renew-token", result.Token);
                }
                // 如果.. 第一次驗證成功，可以將 Token 存在 AP DB中，避免頻繁呼叫 Auth Server
            }
            catch (Exception ex)
            {
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
            }
        }
    }

    public class VerifyTokenResult
    {
        public bool IsSuccess { get; set; }
        public string Token { get; set; }
        public bool ReNew { get; set; }
    }
}