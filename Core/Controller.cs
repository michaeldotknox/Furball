using System;
using System.Collections.Generic;

namespace Furball.Core
{
    internal class Controller
    {
        internal Type Type { get; set; }
        internal string Path { get; set; }
        internal List<Method> Methods { get; set; }
        internal object Instance { get; set; }
    }
}
