using Owin;

namespace Furball.Core
{
    public static class OwinExtensions
    {
        public static IAppBuilder UseFurball(this IAppBuilder app, FurballOptions options)
        {
            app.Use(typeof (Furball), options);

            return app;
        }
    }
}
