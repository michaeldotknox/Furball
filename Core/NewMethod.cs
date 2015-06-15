using System.Reflection;

namespace Furball.Core
{
    public class NewMethod
    {
        internal object Object { get; set; }
        internal MethodInfo MethodInfo { get; set; }
        internal object[] Parameters { get; set; }
    }
}
