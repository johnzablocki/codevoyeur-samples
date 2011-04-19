using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcExtensions.Filters;

namespace MvcExtensions.Controllers {
    
    [HandleError]
    //[SitePerformanceFilter]
    public class HomeController : Controller {
        
        public ActionResult Index() {
            
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            TempData["AnotherMessage"] = "Some temp data";
            return View();
        }

        public ActionResult About() {
            return View();
        }
    }
}
