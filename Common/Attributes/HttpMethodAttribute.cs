using System;

namespace Furball.Common.Attributes
{
    public abstract class HttpMethodAttribute : Attribute
    {
        internal abstract string Name { get; }
    }
}
