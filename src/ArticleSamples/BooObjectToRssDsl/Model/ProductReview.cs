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

namespace BooObjectToRssDsl.Model
{
    public class ProductReview
    {
        public int Id { get; set; }

        public Product Product { get; set; }

        public string UserName { get; set; }

        public string Review { get; set; }

        public DateTime ReviewDate { get; set; }
    }
}
