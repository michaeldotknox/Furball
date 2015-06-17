using System.Collections.Generic;
using System.Reflection;

namespace Furball.Core
{
    public class RequestedPath
    {
        public object Instance { get; set; }
        public MethodInfo Method { get; set; }
        public List<object> Parameters { get; set; } 
    }
}
