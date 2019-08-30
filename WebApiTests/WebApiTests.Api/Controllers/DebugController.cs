using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApiTests.DataAccess;
using WebApiTests.Logic.Interfaces;

namespace WebApiTests.Api.Controllers
{
    public class DebugController : ApiController
    {
        private Lazy<IDatabaseRestoreService> _databaseRestoreService;

        protected IDatabaseRestoreService DatabaseRestoreService => _databaseRestoreService.Value;

        public DebugController(Lazy<IDatabaseRestoreService> databaseRestoreService)
        {
            _databaseRestoreService = databaseRestoreService;
        }

        [Route("api/debug/resettestdata")]
        public IHttpActionResult ResetTestData()
        {
            var result = DatabaseRestoreService.Restore();

            if (result.Success == false)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Ok();
        }
    }
}
