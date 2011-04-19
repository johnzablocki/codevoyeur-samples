using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExtensions.Core {
    public class SimpleViewEngine : IViewEngine {
        
        #region IViewEngine Members

        public ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache) {
            return new ViewEngineResult(new SimpleView(), this);
        }

        public ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache) {


            return new ViewEngineResult(new SimpleView(), this);
        }

        public void ReleaseView(ControllerContext controllerContext, IView view) {
            
        }

        #endregion
    }
}
