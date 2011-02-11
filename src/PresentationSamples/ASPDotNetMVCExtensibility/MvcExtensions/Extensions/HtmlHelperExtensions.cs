using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcExtensions.Extensions {
    public static class HtmlHelperExtensions {

        public static string Alert(this HtmlHelper html, string message) {
            return string.Format("<script>alert('{0}');</script>", message);
        }

        public static string Prompt(this HtmlHelper html, string message) {
            return string.Format("<script>var result = prompt('{0}');</script>", message);
        }

        public static string Confirm(this HtmlHelper html, string message) {            
            return string.Format("<script>var result = confirm('{0}');</script>", message);
        }

    }
}
