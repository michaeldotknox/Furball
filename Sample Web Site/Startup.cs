using Furball.Core;
using Furball.Sample.Web.Controllers;
using Owin;

namespace Furball.Sample.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new FurballOptions
            {
                PathSource =
                    new ManualPathSource().AddPath<TestController>("/", "GetListAsync", "get", new object[] {})
                        .AddPath<TestController>("/", "GetAsync", "get", new object[] {1})
                        .AddPath<StatusReturnMethod>("/status", "GetList", "get", new object[] {})
                        .AddPath<StatusReturnMethod>("/status", "Get", "get", new object[] {1})
            };

            app.UseFurball(options);
        }
    }
}
