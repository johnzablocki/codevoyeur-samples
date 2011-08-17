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
using System.Collections.Generic;

namespace BooObjectToRssDsl.Core
{
    public abstract class RssFieldFactory
    {
        public static List<string> Create()
        {
            return new List<string>() { "title", "description", "link", "pubDate"};
        }
    }
}
