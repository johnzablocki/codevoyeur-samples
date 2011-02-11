using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcIronPythonFilter.Filters;

namespace MvcIronPythonFilter.Controllers {
    
    [PyFilter("trace")]
    public class HomeController : Controller {
        
        public ActionResult Index() {
           
            return View();
        }

        [ValidateInput(false)]
        [PyFilter("injection")]
        public ActionResult Save(string name, string comments) {
            
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(comments)) {
                TempData["error"] = "Name and comments are required";
            } else {
                TempData["message"] = "Your message has been submitted to the moderator";
            }

            return RedirectToAction("Index");
        }
                
        public ActionResult InvalidPost() {
            return View();
        }
    }
}
