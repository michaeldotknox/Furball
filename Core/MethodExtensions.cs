using System.Reflection;
using Furball.Common.Attributes;

namespace Furball.Core
{
    internal static class MethodExtensions
    {
        internal static string GetWebMethod(this MethodInfo method)
        {
            var attribute = method.GetCustomAttribute<HttpMethodAttribute>();
            return attribute == null ? string.Empty : attribute.Name;
        }
    }
}
