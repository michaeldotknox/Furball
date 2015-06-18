using System.Collections.Generic;

namespace Furball.Core
{
    public class Path
    {
        public string RequestPath { get; set; }
        public System.Type ControllerType { get; set; }
        public List<WebMethod> WebMethods { get; set; } 
    }
}
