using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sample_Web_Api_Site.Controllers
{
    public class TestController : ApiController
    {
        [HttpGet]
        public async Task<IHttpActionResult> GetListAsync()
        {
            return Ok(new List<TestObject> {new TestObject {Property1 = "Property 1", Property2 = "Property 2"}});
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get(int id)
        {
            return Ok(new TestObject { Property1 = "Property 1", Property2 = "Property 2" });
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(TestObject testObject)
        {
            return Ok(testObject);
        }
    }
    public class TestObject
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
