using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using System.IO;

namespace MvcIronPythonFilter.Filters {
    
    /// <summary>
    /// PyFilter extends ActionFilterAttribute allowing arbitrary 
    /// IronPython code to be associated with standard ActionFilter methods
    /// </summary>
    public class PyFilter : ActionFilterAttribute {
        
        private static ScriptRuntimeSetup _setup = null;
        private static ScriptRuntime _runtime = null;
        private static Dictionary<string, object> _filters = new Dictionary<string, object>(); //will hold function references
        private string _name = null;

        public static string RootPath { get; set; } // will default to app data path

        public static string ScriptFile { get; set; }

        /// <summary>
        /// Set the name of the filter to be executed
        /// </summary>
        /// <param name="name"></param>
        public PyFilter(string name) {            
            _name = name;
        }

        /// <summary>
        /// Initialize static scripting API objects
        /// </summary>
        static PyFilter() {
            
            _setup = ScriptRuntimeSetup.ReadConfiguration();
            _runtime = new ScriptRuntime(_setup);

            //make System and the current assembly availible to the scripts
            _runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            _runtime.LoadAssembly(Assembly.GetAssembly(typeof(DateTime)));

            //assume default of App_Data/filters.py, but allow for overwriting
            string path = RootPath ?? HttpContext.Current.Server.MapPath("~/App_Data/");
            ScriptEngine engine = _runtime.GetEngine("IronPython");            
            engine.SetSearchPaths(new string[] { path });
            engine.ExecuteFile(Path.Combine(path, ScriptFile ?? "filters.py"), _runtime.Globals);
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            execute("action_executed", filterContext);
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            execute("action_executing", filterContext);
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext) {
            execute("result_executed", filterContext);
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext) {
            execute("result_executing", filterContext);
        }

        private void execute(string suffix, ControllerContext context) {

            //required naming pattern for methods
            //when name is trace and suffix is result_executing, a python
            //function named trace_is_executing will be invoked
            string key = string.Format("{0}_{1}", _name, suffix);

            //the first time a filter method is called, it's cached
            if (!_filters.ContainsKey(key)) {
                object method;
                _runtime.Globals.TryGetVariable(key.ToLower(), out method);
                _filters[key] = method;
            }

            //if the filter wasn't defined in Python, just return
            if (_filters[key] == null) 
                return;
            
            _runtime.Operations.Call(_filters[key], context);                        
        }
     
    }
}
