using System.Net;
using Furball.Common;
using Furball.Common.Attributes;

namespace Furball.Sample.Web.Controllers
{
    [Controller]
    public class BodyParameterController
    {
        [HttpPost]
        public WebResult Post([Body] TestObject id)
        {
            return new WebResult(id, HttpStatusCode.Created);
        }
    }
}
