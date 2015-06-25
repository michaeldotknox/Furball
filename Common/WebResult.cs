using System.Collections.Generic;
using System.Net;

namespace Furball.Common
{
    public class WebResult
    {
        public WebResult()
        {
            Headers = new Dictionary<string, string>();
        }

        public WebResult(object result, HttpStatusCode status) : this()
        {
            Result= result;
            Status = status;
        }

        public object Result { get; set; }
        public HttpStatusCode Status { get; set; }

        public WebResult AddHeader(string key, string value)
        {
            Headers.Add(key, value);

            return this;
        }

        internal Dictionary<string, string> Headers { get; }
    }
}
