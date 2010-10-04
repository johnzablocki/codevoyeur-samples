using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NoRMRepositoryBase.Controllers {
    [HandleError]
    public class HomeController : Controller {
        public ActionResult Index() {
            return RedirectToAction("Index", new { area = "Admin", controller = "Blogs" });
        }

        public ActionResult About() {
            return View();
        }
    }
}
