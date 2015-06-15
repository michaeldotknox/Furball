using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Furball.Common.Attributes;

[assembly: InternalsVisibleTo("Furball.Core.Tests")]

namespace Furball.Core
{
    /// <summary>
    /// This class holds the list of available controllers and retrieves
    /// </summary>
    internal class PathRepository : IPathRepository
    {
        private readonly List<Path> _paths; 
         
        ///// <summary>
        ///// This constructor will build the constructors based on the currently loaded assemblies
        ///// </summary>
        //internal PathRepository()
        //{
        //    _controllers = BuildControllers(from a in AppDomain.CurrentDomain.GetAssemblies()
        //        from t in a.GetTypes()
        //        where t.IsDefined(typeof (ControllerAttribute), true)
        //        select t).ToList();
        //}

        ///// <summary>
        ///// This constructors will build the constructors from the specified assembly
        ///// </summary>
        ///// <param name="assembly"></param>
        //internal PathRepository(Assembly assembly)
        //{
        //    _controllers = BuildControllers(from t in assembly.GetTypes()
        //        where t.IsDefined(typeof (ControllerAttribute), true)
        //        select t).ToList();
        //}

        /// <summary>
        /// This constructor will build the constructors from the list of types
        /// </summary>
        /// <param name="controllers"></param>
        internal PathRepository(IEnumerable<Type> controllers)
        {
            var controllerList = controllers.ToList();
            _paths = (from t in controllerList
                      where t.IsDefined(typeof (ControllerAttribute), true)
                from m in t.GetMethods()
                where m.IsDefined(typeof (HttpMethodAttribute), true)
                select
                    new Path
                    {
                        Type = t,
                        Method = m,
                        WebMethod = m.GetWebMethod(),
                        RequestPath = "/" + t.Name,
                        Parameters =
                            (from p in m.GetParameters() select new Parameter {Name = p.Name, Type = p.ParameterType})
                                .ToList()
                    }).ToList();
            var defaultController = (from t in controllerList
                            where t.IsDefined(typeof (DefaultControllerAttribute), true)
                from m in t.GetMethods()
                where m.IsDefined(typeof (HttpMethodAttribute), true)
                select
                    new Path
                    {
                        Method = m,
                        RequestPath = "/",
                        Parameters = new List<Parameter>(),
                        Type = t,
                        WebMethod = m.GetWebMethod()
                    }).FirstOrDefault();
            if (_paths.All(p => p.RequestPath != "/"))
            {
                _paths.Add(defaultController);
            }
        }

        private IEnumerable<Controller> BuildControllers(IEnumerable<Type> types)
        {
            return from t in types
                select
                    new Controller
                    {
                        Path = "/" + t.Name,
                        Type = t,
                        Methods =
                            (from m in t.GetMethods(BindingFlags.Public) select new Method {Name = m.Name}).ToList()
                    };
        }

        public NewMethod GetTypeFromPath(string path)
        {
            throw new NotImplementedException();
        }

        public Path GetMethod(string path, string webMethod, object[] parameters)
        {
            var returnPath = (from p in _paths
                where p.RequestPath == path && p.WebMethod == webMethod && p.Parameters.Count == parameters.Count()
                select p).FirstOrDefault();

            var o = Activator.CreateInstance(returnPath.Type);// returnPath.Type.Assembly.CreateInstance(returnPath.Type.Name);

            if (returnPath.Instance == null)
            {
                returnPath.Instance = Activator.CreateInstance(returnPath.Type);// returnPath.Type.Assembly.CreateInstance(returnPath.Type.AssemblyQualifiedName);
            }

            return returnPath;
        }
    }
}
