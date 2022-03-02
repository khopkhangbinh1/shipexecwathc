using iMWS.Api.Db;
using iMWS.Api.Filters;
using iMWS.Api.Models.PPSPick;
using iMWS.Api.Models.Bean;
using iMWS.Api.Models.PPSUSER;
using iMWS.Api.Models.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ClientUtilsDll;

namespace iMWS.Api.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Login")]
    public class LoginController : ApiController
    {
        // GET: Login
        [HttpPost]
        [HttpGet]
        [Route("Login")]
        public IHttpActionResult Login(string userid, string password)
        {
            PPSDb p = new PPSDb();
            string username = "";
            var res = p.CheckUser(userid, password, out username);
            bool isValid = res.Contains("OK") ? true : false;

            var jsonObj = new
            {
                UserID = userid,
                UserName = username
            };

            return Ok(Result.Create(isValid, res, JsonConvert.SerializeObject(jsonObj)));
        }

        [HttpGet]
        [Route("GetPDAVersion")]
        public IHttpActionResult GetPDAVersion()
        {
            PPSDb p = new PPSDb();
            ExecutionResult res = new ExecutionResult();
            string username = "";
            var version = p.PPSGetbasicparameterBySP("PDA_VERSION");
            res.Message = version.ErrMsg;
            res.Anything = version.outparavalue;
            res.Status = !String.IsNullOrEmpty(version.outparavalue);
            return Ok(Result.Create(res.Status, res.Message, res.Anything));
        }

    }
}