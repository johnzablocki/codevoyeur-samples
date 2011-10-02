using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Glimpse.Models;
using System.Configuration;

namespace Glimpse.Controllers {
    public class AdminController : Controller {
        
        public ActionResult Index() {
            return View();
        }

        [HttpPost]
        public ActionResult Save(GlimpseSettingsPart part) {
                        

        }

    }
}