//	HomeController.cs
//	
//		Classes:
//			public HomeController
//
//		Created By: 
//			jcz 2008-02-10 			
//
//		Modification History:
//			
//

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
using System.IO;
using System.Data.SqlClient;
using NHibernate.Expression;

namespace LocationSearchWithActiveRecord.Controllers
{
    public class HomeController : ControllerBase
    {
        public void Search(string zip, string city, string state)
        {
            Location centerPoint = null;

            if (!string.IsNullOrEmpty(zip))
                centerPoint = Location.FindByZip(zip);
            else
                centerPoint = Location.FindFirst(Expression.And(
                                                    Expression.Eq("City", city),
                                                    Expression.Eq("State", state)
                                                    ));
                        
            if (centerPoint != null)
            {
                Location[] locations = Location.FindNearbyLocations(centerPoint, 5);
                Flash["Locations"] = locations;
                Flash["CenterPoint"] = centerPoint;
            }            

            RedirectToReferrer();
            
        }
    }
}
