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
            //Querystring parameters
            var requestParameters = new List<RequestParameter>();
            requestParameters.AddRange(GetParameters(queryString, "QueryString"));

            //Body parameters
            requestParameters.AddRange(GetParameters((Stream)environment["owin.RequestBody"], "Body"));

            var pathRepository = _options.PathRepository;

            RequestedPath path = null;
            try
            {
                path = await pathRepository.GetMethodAsync(requestPath, requestMethod, requestParameters);
            }
            catch (Exception e)
            {
                environment["owin.ResponseStatusCode"] = 500;
                string message;
                if (_options.HandlerErrors == HandlerErrorTypes.SendErrors)
                {
                    message = e.ToString();
                }
                else
                {
                    message = "An error occurred";
                }
                var errorStream = (Stream)environment["owin.ResponseBody"];
                var errorWriter = new StreamWriter(errorStream);
                errorWriter.Write(message);
                errorWriter.Close();
                errorStream.Close();
                errorWriter.Dispose();
                errorStream.Dispose();
                return;
            }

            WebResult resultObject = null;

            try
            {
                if (path.ReturnType == typeof (WebResult))
                {
                    resultObject = (WebResult)path.Method.Invoke(path.Instance, (from p in path.Parameters select p).ToArray());
                }
                else
                {
                    var result = path.Method.Invoke(path.Instance, (from p in path.Parameters select p).ToArray());
                    resultObject = new WebResult(result, HttpStatusCode.OK);
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
            var headers = (IDictionary<string, string[]>)environment["owin.ResponseHeaders"];
            foreach (var header in resultObject.Headers)
            {
                KeyValuePair<string, string[]> outgoingHeader;
                if (!headers.ContainsKey(header.Key))
                {
                    outgoingHeader = new KeyValuePair<string, string[]>(header.Key, new string[] {header.Value});
                }
                else
                {
                    outgoingHeader = headers.First(h => h.Key == header.Key);
                    headers.Remove(outgoingHeader);
                    var headerList = outgoingHeader.Value.ToList();
                    headerList.AddRange(new List<string> {header.Value});
                    outgoingHeader = new KeyValuePair<string, string[]>(header.Key, headerList.ToArray());
                }
                headers.Add(outgoingHeader);
            }

            //Remove requested headers
            foreach (var header in _options.HeadersToRemove)
            {
                headers.Remove(header);
            }
            var stream = (Stream) environment["owin.ResponseBody"];
            var writer = new StreamWriter(stream);
            writer.Write(JsonConvert.SerializeObject(resultObject.Result));
            writer.Close();
            stream.Close();
            writer.Dispose();
            stream.Dispose();

            await _next.Invoke(environment);
        }

        private List<RequestParameter> GetParameters(string parameters, string source)
        {
            var requestParametersList = parameters.Split('&');
            var requestParameters = new List<RequestParameter>();
            if (parameters.Length > 0)
            {
                foreach (var parameter in requestParametersList)
                {
                    var keyValue = parameter.Split('=');
                    var name = keyValue[0];
                    var value = (object)keyValue[1];
                    requestParameters.Add(new RequestParameter {Name = name, Source = source, Value = value});
                }
            }

            return requestParameters;
        }

        private List<RequestParameter> GetParameters(Stream stream, string source)
        {
            var reader = new StreamReader(stream);
            var parameters = reader.ReadToEnd();
            reader.Close();
            stream.Close();

            return GetParameters(parameters, source);
        }
    }
}
