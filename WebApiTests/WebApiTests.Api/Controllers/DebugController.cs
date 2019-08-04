using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTests.DataAccess;

namespace WebApiTests.Api.Controllers
{
    public class DebugController : ApiController
    {
        [Route("api/debug/resettestdata/{source}")]
        public IHttpActionResult ResetTestData(string source)
        {
            if(source == "product")
            {
                ProductRepository.ResetTestData();
            }

            return Ok();
        }
    }
}
