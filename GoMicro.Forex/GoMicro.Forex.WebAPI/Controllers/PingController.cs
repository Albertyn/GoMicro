﻿using System.Web.Http;
using GoMicro.Forex.Models;

namespace GoMicro.Forex.WebApi.Controllers
{
    [RoutePrefix("api/v1")]
    public class PingController : ApiController
    {
        private readonly IApiSettings _ApiSettings;

        public PingController(IApiSettings ApiSettings)
        {
            _ApiSettings = ApiSettings;
        }

        [HttpGet]
        [Route("Ping")]
        public CommonResult Get()
        {
            return new CommonResult(true,"Pong");
        }        
    }
}
