using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDynamicActions.Core {
    
    public class DynamicActionInvoker : ControllerActionInvoker {

        public DynamicAction DynamicAction { get; set; }        
        
        public override bool InvokeAction(ControllerContext controllerContext, string actionName) {
                       
            base.InvokeActionResult(controllerContext, DynamicAction.Result());
            return true;

        }
        
    }
}
