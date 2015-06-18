using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Furball.Common;
using Newtonsoft.Json;

namespace Furball.Core
{
    using AppFunc = Func<IDictionary<string, object>, Task>;
    public class Furball
    {
        private readonly AppFunc _next;
        private readonly FurballOptions _options;

        public Furball(AppFunc next, FurballOptions options = null)
        {
            if (next == null) throw new ArgumentNullException(nameof(next));

            _next = next;
            _options = options;

            if (options == null)
            {
                _options = new FurballOptions();
            }
        }

        public async Task Invoke(IDictionary<string, object> environment)
        {
            var requestPath = environment["owin.RequestPath"].ToString().ToLower();
            var requestMethod = environment["owin.RequestMethod"].ToString().ToLower();
            var queryString = environment["owin.RequestQueryString"].ToString();
            var requestParametersList = queryString.Split('&');
            var requestParameters = new Dictionary<string, object>();
            if(queryString.Length > 0)
            {
                foreach (var parameter in requestParametersList)
                {
                    var keyValue = parameter.Split('=');
                    var key = keyValue[0];
                    var value = (object)keyValue[1];
                    requestParameters.Add(key, value);
                }
            }

            var pathRepository = _options.PathRepository;

            var path = await pathRepository.GetMethodAsync(requestPath, requestMethod, requestParameters);

            WebResult resultObject = null;

            try
            {
                object result = path.Method.Invoke(path.Instance, (from p in path.Parameters select p).ToArray());

                var type = result.GetType();
                if (type != typeof(WebResult))
                {
                    resultObject = new WebResult(result, HttpStatusCode.Accepted);
                }
                else
                {
                    resultObject = (WebResult)result;
                }
            }
            catch (Exception e)
            {
                environment["owin.ResponseStatusCode"] = 500;
                if (_options.HandlerErrors == HandlerErrorTypes.SendErrors)
                {
                    resultObject = new WebResult(e, HttpStatusCode.InternalServerError);
                }
                else
                {
                    resultObject = new WebResult("An error occurred", HttpStatusCode.InternalServerError);
                }
            }
            environment["owin.ResponseStatusCode"] = resultObject.Status;
            var stream = (Stream) environment["owin.ResponseBody"];
            var writer = new StreamWriter(stream);
            writer.Write(JsonConvert.SerializeObject(resultObject.Result));
            writer.Close();
            stream.Close();
            writer.Dispose();
            stream.Dispose();

            await _next.Invoke(environment);
        }
    }
}
