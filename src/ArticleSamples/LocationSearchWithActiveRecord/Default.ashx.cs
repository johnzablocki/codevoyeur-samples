using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;

namespace LocationSearchWithActiveRecord
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Default : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.Redirect("~/Home/Default.aspx");
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
