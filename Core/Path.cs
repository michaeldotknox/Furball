using System.Collections.Generic;

namespace Furball.Core
{
    public class Path
    {
        public string RequestPath { get; set; }
        public object Instance { get; set; }
        public List<WebMethod> WebMethods { get; set; } 
    }
}
