using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPythonViewEngine.Core {
    
    public class PyViewEngine : IViewEngine {
        #region IViewEngine Members

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache) {
            throw new NotImplementedException();
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache) {
            return new ViewEngineResult(new PyView(), this);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view) {
            
        }

        #endregion
    }
}