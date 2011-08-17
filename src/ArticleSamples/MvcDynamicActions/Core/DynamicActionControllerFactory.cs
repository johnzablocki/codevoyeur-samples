using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDynamicActions.Core {
    public class DynamicActionControllerFactory : DefaultControllerFactory {

        public override IController CreateController(System.Web.Routing.RequestContext requestContext, string controllerName) {
            
            Controller controller = base.CreateController(requestContext, controllerName) as Controller;

            if (controller is DynamicActionControllerBase) {

                string action = requestContext.RouteData.Values["action"] as string;
                DynamicAction dynamicAction =  (controller as DynamicActionControllerBase)
                            .DynamicActions
                            .FirstOrDefault(d => d.ActionName.Equals(action, StringComparison.CurrentCultureIgnoreCase));

                if (dynamicAction != null) {
                    controller.ActionInvoker = new DynamicActionInvoker() { DynamicAction = dynamicAction };
                }
            }

            return controller;

        }

    }
}
