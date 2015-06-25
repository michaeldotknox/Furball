using System.Collections.Generic;
using System.Linq;
using Furball.Common.Attributes;

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
            var newMethod = new WebMethod
            {
                HttpMethod = httpMethod,
                MethodInfo = method,
                ReturnType = method.ReturnType,
                Parameters = (from p in method.GetParameters()
                    select
                        new MethodParameter
                        {
                            Name = p.Name,
                            Type = p.ParameterType,
                            IsBodyParameter = p.GetCustomAttributes(typeof (BodyAttribute), true).Count() == 1
                        })
                    .ToList()
            };
            newMethod.HasBodyParameters = newMethod.Parameters.Count(p => p.IsBodyParameter) >= 1;

            _paths[path].WebMethods.Add(newMethod);

            return this;
        }
    }
}
