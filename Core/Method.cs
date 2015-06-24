using System.Reflection;

namespace Furball.Core
{
    public class Method
    {
        internal object Object { get; set; }
        internal MethodInfo MethodInfo { get; set; }
        internal object[] Parameters { get; set; }
    }
}
