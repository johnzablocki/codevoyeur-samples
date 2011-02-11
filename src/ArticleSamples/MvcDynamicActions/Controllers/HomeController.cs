using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MvcDynamicActions.Core;

namespace MvcDynamicActions.Controllers
{
    public class HomeController : DynamicActionControllerBase
    {
        public HomeController() {
            DynamicActions.Add(new DynamicAction() { ActionName = "About", Result = View });
            DynamicActions.Add(new DynamicAction() { ActionName = "Contact", Result = View });
            DynamicActions.Add(new DynamicAction() { 
                ActionName = "Ping", Result = () => { Response.Write("OK"); return new EmptyResult(); } 
            });
            DynamicActions.Add(new DynamicAction() { 
                ActionName = "API", Result = () => Json(new { FirstName = "John", LastName = "Zablocki" }) 
            });
        }                

        public ActionResult Index()
        {
            return View();
        }

    }
}
