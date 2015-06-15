using System.Collections.Generic;
using System.Reflection;

namespace Furball.Core
{
    public class Method
    {
        internal string Name { get; set; }
        internal string WebMethod { get; set; }
        internal List<Parameter> Parameters { get; set; }
        internal MethodInfo Signature { get; set; }
    }
}
