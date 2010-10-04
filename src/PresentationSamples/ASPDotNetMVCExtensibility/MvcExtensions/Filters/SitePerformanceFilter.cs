using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace MvcExtensions.Filters {
    
    public class SitePerformanceFilter : ActionFilterAttribute {

        private string _logPath = Path.Combine(HttpContext.Current.Server.MapPath("."), "App_Data\\performancelog.txt");

        public override void OnActionExecuted(ActionExecutedContext filterContext) {
            File.AppendAllText(_logPath, constructLogMessage(filterContext));
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext) {
            File.AppendAllText(_logPath, constructLogMessage(filterContext));
        }

        public override void OnResultExecuted(ResultExecutedContext filterContext) {
            File.AppendAllText(_logPath, constructLogMessage(filterContext));
        }

        public override void OnResultExecuting(ResultExecutingContext filterContext) {
            File.AppendAllText(_logPath, constructLogMessage(filterContext));
        }

        private string constructLogMessage(ControllerContext context) {

            string messageTemplate = "Session: {0} Controller: {1} Action: {2} Filter Action: {3} Timestamp: {4}\r\n";

            return string.Format(messageTemplate, context.HttpContext.Session.SessionID,
                context.RouteData.Values["controller"], context.RouteData.Values["action"], 
                context.GetType().Name,  DateTime.Now);
                                 
        }
    }
}
