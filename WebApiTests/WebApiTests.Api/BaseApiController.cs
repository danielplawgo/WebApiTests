using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using WebApiTests.Api.Mappers;
using WebApiTests.Logic;

namespace WebApiTests.Api
{
    public class BaseApiController : ApiController
    {
        protected IHttpActionResult Map<TSource, TDestination>(Result<TSource> result,
            IMapper<TSource, TDestination> mapper)
            where TSource : class
            where TDestination : class
        {
            if(result.Success == false)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Ok(Result.Ok(mapper.Map(result.Value)));
        }

        protected IHttpActionResult MapList<TSource, TDestination>(Result<IEnumerable<TSource>> result,
            IMapper<TSource, TDestination> mapper)
            where TSource : class
            where TDestination : class
        {
            if (result.Success == false)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Ok(Result.Ok(result.Value.Select(v => mapper.Map(v))));
        }

        protected IHttpActionResult Map(Result result)
        {
            if (result.Success == false)
            {
                return Content(System.Net.HttpStatusCode.BadRequest, result);
            }

            return Ok(result);
        }
    }
}