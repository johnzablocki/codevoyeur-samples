using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;
using Microsoft.Scripting.Hosting;
using IronPythonMVCControllerFactory.Controllers;
using System.Text;
using System.Reflection;
using System.Collections.Generic;
using IronPython.Runtime;

namespace IronPythonMVCControllerFactory.Core {
    public class PyControllerFactory : IControllerFactory {
        #region IControllerFactory Members

        private static Dictionary<string, object> _controllers = new Dictionary<string, object>();
        private static ScriptRuntimeSetup _setup = null;
        private static ScriptRuntime _runtime = null;

        static PyControllerFactory() {

            _setup = ScriptRuntimeSetup.ReadConfiguration(); 
            _runtime = new ScriptRuntime(_setup);
       
            _runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            
            string path = HttpContext.Current.Server.MapPath("~/App_Data/Controllers.py");
            _runtime.GetEngine("IronPython").ExecuteFile(path, _runtime.Globals);
        }

        public IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName) {

            if (! _controllers.ContainsKey(controllerName)) {
                
                _controllers[controllerName] = _runtime.Globals.GetVariable(controllerName.ToLower());                
            }

            return _runtime.Operations.Call(_controllers[controllerName]) as IController;
                     
        }

        public void ReleaseController(IController controller) {
                        
        }

        #endregion
    }
}
