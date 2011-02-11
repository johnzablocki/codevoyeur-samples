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
using System.Text;

namespace MvcJQueryAutoComplete.Extensions {
    public static class StringBuilderExtensions {

        public static void AppendLineFormat(this StringBuilder stringBuilder, string source, params object[] args) {

            stringBuilder.AppendFormat(source + "\r\n", args);
        }
    }
}
