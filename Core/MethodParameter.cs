using System;

namespace Furball.Core
{
    public class MethodParameter
    {
        internal string Name { get; set; }
        internal Type Type { get; set; }
        internal bool IsBodyParameter { get; set; }
    }
}
