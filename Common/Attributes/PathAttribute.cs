using System;

namespace Furball.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class PathAttribute : Attribute
    {
        public PathAttribute(string path)
        {
            Path = path;
        }

        public string Path { get; }
    }
}
