using System;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Furball.Core")]

namespace Furball.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class HttpPutAttribute : HttpMethodAttribute
    {
        internal override string Name => "put";
    }
}
