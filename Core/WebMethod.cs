using System.Collections.Generic;
using System.Reflection;

namespace Furball.Core
{
    public class WebMethod
    {
        public System.Type ReturnType { get; set; }
        public MethodInfo MethodInfo { get; set; }
        public List<Parameter> Parameters { get; set; }
        public string HttpMethod { get; set; }
        public bool HasBodyParameters { get; set; }
    }
}
