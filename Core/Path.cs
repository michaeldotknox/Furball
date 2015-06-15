using System.Collections.Generic;
using System.Reflection;

namespace Furball.Core
{
    public class Path
    {
        public string RequestPath { get; set; }
        public System.Type Type { get; set; }
        public object Instance { get; set; }
        public MethodInfo Method { get; set; }
        public List<Parameter> Parameters { get; set; } 
        public string WebMethod { get; set; }
    }
}
