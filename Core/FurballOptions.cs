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
        }

        public HandlerErrorTypes HandlerErrors { get; set; }
        public IPathRepository PathRepository { get; set; }
    }
}
