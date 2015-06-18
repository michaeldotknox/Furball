using System.Collections.Generic;
using System.Linq;

namespace Furball.Core
{
    public class ManualPathSource : IPathSource
    {
        private readonly Dictionary<string, Path> _paths;

        public ManualPathSource()
        {
            _paths = new Dictionary<string, Path>();
        }

        public List<Path> GetPaths()
        {
            return (from p in _paths select p.Value).ToList();
        }

        public ManualPathSource AddPath<TController>(string path, string methodName, string httpMethod, object[] parameters)
        {
            if (!_paths.ContainsKey(path))
            {
                _paths.Add(path, new Path
                {
                    ControllerType = typeof(TController),
                    RequestPath = path,
                    WebMethods = new List<WebMethod>()
                });
            }

            var method = typeof (TController).GetMethod(methodName);

            _paths[path].WebMethods.Add(
                new WebMethod
                {
                    HttpMethod = httpMethod,
                    MethodInfo = typeof (TController).GetMethod(methodName),
                    ReturnType = typeof (TController).GetMethod(methodName).ReturnType,
                    Parameters = (from p in typeof (TController).GetMethod(methodName).GetParameters()
                        select new Parameter {Name = p.Name, Type = p.ParameterType}).ToList()
                });

            return this;
        }
    }
}
