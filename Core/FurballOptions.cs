using System;
using System.Linq;
using Furball.Common.Attributes;

namespace Furball.Core
{
    public class FurballOptions
    {
        public FurballOptions()
        {
            HandlerErrors = HandlerErrorTypes.DontSendErrors;
            PathRepository = new PathRepository(from a in AppDomain.CurrentDomain.GetAssemblies()
                from t in a.GetTypes()
                where t.IsDefined(typeof (ControllerAttribute), true)
                select t);
        }

        public HandlerErrorTypes HandlerErrors { get; set; }
        public IPathRepository PathRepository { get; set; }
    }
}
