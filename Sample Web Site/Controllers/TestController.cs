using System.Collections.Generic;
using Furball.Common.Attributes;

namespace Cats.Sample.Web.Controllers
{
    [Controller, DefaultController]
    public class TestController
    {
        [HttpGet]
        public List<TestObject> GetListAsync()
        {
            return new List<TestObject> {new TestObject {Property1 = "Property 1", Property2 = "Property 2"} };
        }

        [HttpGet]
        public TestObject GetAsync(int id)
        {
            return new TestObject {Property1 = "Property 1", Property2 = "Property 2"};
        }

        public TestObject PostAsync(TestObject testObject)
        {
            return testObject;
        }
    }

    public class TestObject
    {
        public string Property1 { get; set; }
        public string Property2 { get; set; }
    }
}
