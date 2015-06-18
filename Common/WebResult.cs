using System.Collections.Generic;
using System.Net;

namespace Furball.Common
{
    public class WebResult
    {
        private readonly Dictionary<string, string> _headers;

        public WebResult()
        {
            _headers = new Dictionary<string, string>();
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
            _headers.Add(key, value);

            return this;
        }
    }
}
