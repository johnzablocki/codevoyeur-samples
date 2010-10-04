using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IronPythonRouteRegistration.Controllers {
    public class PostsController : Controller {

        public ActionResult Index(int year, int month, int day, string title) {

            return View();
        }

    }
}