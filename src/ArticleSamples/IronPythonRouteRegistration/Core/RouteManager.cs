using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using System.Web.Routing;
using System.Web.Mvc;
using IronPython.Runtime;

namespace IronPythonRouteRegistration.Core {
    
    public class RouteManager {

        private const short EXPECTED_TUPLE_LENGTH = 3;

        private static Dictionary<string, object> _routes = new Dictionary<string, object>();
        private static ScriptRuntimeSetup _setup = null;
        private static ScriptRuntime _runtime = null;

        static RouteManager() {
            _setup = ScriptRuntimeSetup.ReadConfiguration();
            _runtime = new ScriptRuntime(_setup);
            _runtime.LoadAssembly(Assembly.GetExecutingAssembly());
        }

        public static void RegisterRoutes(string path = null) {

            path = path ?? HttpContext.Current.Server.MapPath("~/App_Data/routes.py");
            _runtime.GetEngine("IronPython").ExecuteFile(path, _runtime.Globals);

            var variables = _runtime.Globals.GetVariableNames().Where(s => ! s.StartsWith("__") && ! s.EndsWith("__"));

            foreach (var variable in variables) {                

                var route = _runtime.Globals.GetVariable(variable);
                var routeInfo = _runtime.Operations.Invoke(route) as PythonTuple;

                validateRouteInfo(routeInfo);

                var url = routeInfo[0] as string;
                var defaults = routeInfo[1] as PythonDictionary;
                var constraints = routeInfo[2] as PythonDictionary;                

                Func<PythonDictionary, RouteValueDictionary> routeDictFromPyDict = delegate(PythonDictionary pythonDict) { 
                    var routeValueDict = new RouteValueDictionary();
                    foreach (string key in pythonDict.Keys) {
                        routeValueDict[key] = pythonDict[key];
                    }
                    return routeValueDict;
                };

                RouteTable.Routes.Add(variable, new Route(url, routeDictFromPyDict(defaults), routeDictFromPyDict(constraints), new MvcRouteHandler()));
             
            }

        }

        private static void validateRouteInfo(PythonTuple routeInfo) {
            
            if (routeInfo.Count != EXPECTED_TUPLE_LENGTH) {
                throw new RouteMappingException();
            }

            if (routeInfo.Contains(null)) {
                throw new RouteMappingException();
            }
        }


    }
}