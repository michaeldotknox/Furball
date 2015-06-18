﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Furball.Core.Exceptions;

[assembly: InternalsVisibleTo("Furball.Core.Tests")]

namespace Furball.Core
{
    /// <summary>
    /// This class holds the list of available controllers and retrieves
    /// </summary>
    internal class PathRepository : IPathRepository
    {
        private readonly List<Path> _paths;
        private readonly Dictionary<Type, object> _controllers;
         
        internal PathRepository(IEnumerable<Path> paths)
        {
            if(paths == null) throw new ArgumentNullException(nameof(paths));

            _paths = new List<Path>(paths);
            _controllers = new Dictionary<Type, object>();
        }

        public NewMethod GetTypeFromPath(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<RequestedPath> GetMethodAsync(string path, string httpMethod, Dictionary<string, object> parameters)
        {
            var foundPath = _paths.FirstOrDefault(p => p.RequestPath == path);

            //If the path is not found, then throw a PathNotFoundException
            if (foundPath == null)
            {
                throw new PathNotFoundException();
            }

            var webMethods = foundPath.WebMethods.Where(w => w.HttpMethod == httpMethod).ToList();

            //If the path is found, but no methods match the expected http method, then throw an exception
            if (!webMethods.Any())
            {
                throw new IncorrectWebMethodException();
            }

            //Find the method that matches the expected parameters
            WebMethod foundMethod = null;
            foreach (var webMethod in webMethods)
            {
                var isMethodFound = true;
                foreach (var parameter in parameters)
                {
                    if (webMethod.Parameters.All(p => p.Name != parameter.Key))
                    {
                        isMethodFound = false;
                    }
                }
                if (isMethodFound)
                {
                    foundMethod = webMethod;
                    break;
                }
            }

            object controller;
            if (_controllers.ContainsKey(foundPath.ControllerType))
            {
                controller = _controllers[foundPath.ControllerType];
            }
            else
            {
                controller = foundPath.ControllerType.Assembly.CreateInstance(foundPath.ControllerType.FullName, true);
                _controllers.Add(foundPath.ControllerType, controller);
            }

            return new RequestedPath
            {
                Parameters = (from parameter in parameters
                    join methodParameter in foundMethod.Parameters on parameter.Key equals methodParameter.Name
                    select Convert.ChangeType(parameter.Value, methodParameter.Type)).ToList(),
                Instance = controller,
                Method = foundMethod.MethodInfo
            };
        }
    }
}
