using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Furball.Core.Tests")]

namespace Furball.Common
{
    public class WebResult
    {
        public WebResult()
        {
            Headers = new Dictionary<string, string>();
            Status = HttpStatusCode.NoContent;
        }

        public WebResult(object result, HttpStatusCode status) : this()
        {
            Result= result;
            Status = status;
        }

        public WebResult(object result) : this()
        {
            Result = result;
            Status = result == null ? Status = HttpStatusCode.NoContent : HttpStatusCode.OK;
        }

        public WebResult(HttpStatusCode status) : this()
        {
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
