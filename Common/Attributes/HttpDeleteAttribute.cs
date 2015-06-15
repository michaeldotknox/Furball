using System;
using System.Runtime.CompilerServices;

[assembly:InternalsVisibleTo("Furball.Core")]

namespace Furball.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpDeleteAttribute : HttpMethodAttribute
    {
        internal override string Name => "delete";
    }
}
