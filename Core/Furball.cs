using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
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

            var path = pathRepository.GetMethodAsync(requestPath, requestMethod, new Dictionary<string, object>{});

            object resultObject = null;

            try
            {
                /////resultObject = path.Method.Invoke(path.Instance, new object[] {});
            }
            catch (Exception e)
            {
                environment["owin.ResponseStatusCode"] = 500;
                if (_options.HandlerErrors == HandlerErrorTypes.SendErrors)
                {
                    resultObject = e;
                }
                else
                {
                    resultObject = "An error occurred";
                }
            }
            var stream = (Stream) environment["owin.ResponseBody"];
            var writer = new StreamWriter(stream);
            writer.Write(JsonConvert.SerializeObject(resultObject));
            writer.Close();
            stream.Close();
            writer.Dispose();
            stream.Dispose();

            await _next.Invoke(environment);
        }
    }
}
