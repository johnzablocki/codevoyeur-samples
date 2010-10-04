using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Collections.Specialized;
using BooObjectValidationDsl.Validation;
using BooObjectValidationDsl.Models;

namespace BooObjectValidationDsl.Controllers {
    [HandleError]
    public class HomeController : Controller {
        
        public ActionResult Index() {
            
            return View();
        }

        public ActionResult ValidateForm() {

            ValidationContext<NameValueCollection> ctx = new ValidationContext<NameValueCollection>();
            ctx.Reset();
            ctx.Validate(Request.Form, "LoginForm");

            if (ctx.HasErrors) {
                TempData["Errors"] = ctx.ErrorMessages;
            } else {
                TempData["Message"] = "You have successfully passed validation";
            }

            return RedirectToAction("Index");
        }

        public ActionResult ValidateBusinessObj(string username, string password) {

            User user = new User() { Username = username, Password = password }; 

            ValidationContext<User> ctx = new ValidationContext<User>();
            ctx.Reset();
            ctx.Validate(user);

            if (ctx.HasErrors) {
                TempData["Errors"] = ctx.ErrorMessages;
            } else {
                TempData["Message"] = "You have successfully passed validation";
            }

            return RedirectToAction("Index");
        }

        
    }
}
