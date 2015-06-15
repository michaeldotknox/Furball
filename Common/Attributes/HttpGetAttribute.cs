using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Furball.Core")]

namespace Furball.Common.Attributes
{
    public class HttpGetAttribute : HttpMethodAttribute
    {
        internal override string Name => "get";
    }
}
