using System.Net;
using Furball.Common;
using Furball.Common.Attributes;

namespace Furball.Sample.Web.Controllers
{
    public class HeaderController
    {
        [HttpGet]
        public WebResult GetAsync()
        {
            return new WebResult(1, HttpStatusCode.OK).AddHeader("TestHeader", "value");
        }
    }
}
