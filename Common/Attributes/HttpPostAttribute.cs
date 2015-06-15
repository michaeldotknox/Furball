using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Furball.Core")]

namespace Furball.Common.Attributes
{
    public class HttpPostAttribute : HttpMethodAttribute
    {
        internal override string Name => "post";
    }
}
