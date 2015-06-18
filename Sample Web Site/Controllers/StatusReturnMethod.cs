using System.Collections.Generic;
using System.Net;
using Furball.Common;
using Furball.Common.Attributes;

namespace Furball.Sample.Web.Controllers
{
    [Controller]
    public class StatusReturnMethod
    {
        [HttpGet]
        public WebResult Get(int id)
        {
            return new WebResult("string 1", HttpStatusCode.Accepted);
        }

        [HttpGet]
        public WebResult GetList()
        {
            return new WebResult(new List<string> {"string 1", "string 2"},
                HttpStatusCode.Accepted);
        } 
    }
}
