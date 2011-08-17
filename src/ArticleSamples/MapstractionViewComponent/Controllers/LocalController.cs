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
using Castle.MonoRail.Framework;
using MapstractionViewComponentWeb.Model;

namespace MapstractionViewComponentWeb.Controllers
{
    public class LocalController : Controller
    {
        public void Default()
        {
            PropertyBag["CenterPoint"] = new Location() 
                { 
                    Id = 1, 
                    City = "Fairfield", 
                    State = "Conn", 
                    Latitude = 41.16f,
                    Longitude = -73.26f
                };

            PropertyBag["NearbyPoints"] = new Location[]
            {
                new Location() 
                {
                    Id = 1,
                    City = "Fairfield",
                    State = "Conn",
                    Latitude = 41.05f,
                    Longitude = -73.54f
                },

                new Location() 
                {
                    Id = 1,
                    City = "New Haven",
                    State = "Conn",
                    Latitude = 41.3f,
                    Longitude = -72.94f
                }
            };
        }
    }
}
