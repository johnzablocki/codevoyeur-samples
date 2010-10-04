using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;

namespace MvcJQueryAutoComplete.Controllers {
    [HandleError]
    public class HomeController : Controller {

        private static string[] _cities = null;

        static HomeController() {

            _cities = new string[] 
            {
                "Bridgeport", "Hartford", "New Haven", "Stamford",
                "Boston", "Cambridge", "Springfield", "Worcestor",
                "Burlington", "Manchester", "Providence", "Warwick",
                "Nashua", "Montpelier", "Portland"
            };
        }

        
        public ActionResult Index() {
            
            return View();
        }

        public ActionResult Save(int? cityID, string city) {            

            if (!string.IsNullOrEmpty(city) && cityID.HasValue)
                TempData["Message"] = string.Format("You saved city {0} with ID {1}", city, cityID);
            else
                TempData["Error"] = string.Format("City not selected.", city, cityID);

            return RedirectToAction("Index");
        }

        public ActionResult Cities(string q) {

            //i is used as a pretend primary key
            for (var i = 0; i < _cities.Length; i++ ) {
                if (_cities[i].StartsWith(q, StringComparison.OrdinalIgnoreCase))
                    Response.Output.Write("{0}|{1}\r\n", _cities[i], i);
            }

            return new CancelViewResult();
            
        }

        private class CancelViewResult : ActionResult {
            public override void ExecuteResult(ControllerContext context) {
                //do nothing
            }
        }


       
    }
}
