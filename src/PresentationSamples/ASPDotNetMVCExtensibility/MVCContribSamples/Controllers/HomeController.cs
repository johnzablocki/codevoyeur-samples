using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcContrib.Filters;

namespace MVCContribSamples.Controllers {
    [HandleError]
    public class HomeController : Controller {
        
        public ActionResult Index() {
            ViewData["Message"] = "Welcome to ASP.NET MVC!";
            ViewData["SomeObject"] = new { FirstName = "John", LastName = "Smith" };

            return View("Index", "Site");
        }

        [PredicatePreconditionFilter("b", PreconditionFilter.ParamType.Request, "isNotZero", typeof(ArgumentException))]
        public ActionResult Division(int a, int b) {

            Response.Write(a / b);
            return new EmptyResult();
        }

        [Layout("Alternate")]
        public ActionResult AlternateIndex() {
            return View();
        }

        private bool isNotZero(object value) {
            return Convert.ToInt32(value) != 0;
        }
    }
}
