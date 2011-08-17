using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Web.Mvc;

namespace MVCTagCloudExtensions.Controllers {
    public class ArtistsController : Controller {
        public ActionResult Index() {

            ViewData["artist"] = ((string)RouteData.Values["id"]).Replace("-", " ");
            return View();
        }
    }
}
