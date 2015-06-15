using Furball.Core;
using Owin;

namespace Cats.Sample.Web
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            var options = new FurballOptions();

            app.UseCats(options);
        }
    }
}
