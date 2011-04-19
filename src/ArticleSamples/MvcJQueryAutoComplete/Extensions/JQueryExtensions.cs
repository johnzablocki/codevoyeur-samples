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
using System.Web.Mvc.Html;
using System.Text;
using System.Reflection;

namespace MvcJQueryAutoComplete.Extensions {
    
    public static class JQueryExtensions {

        public static string AutoCompleteTextBox(this HtmlHelper html, string textBoxName, string fieldName, string selectedText, object selectedValue, object htmlAttributes) {
            
            return string.Format("{0} {1}", html.TextBox(textBoxName, selectedText, htmlAttributes), html.Hidden(fieldName, selectedValue));
        }

        public static string AutoCompleteTextBox(this HtmlHelper html, string textBoxName, string fieldName, object htmlAttributes) {

            return string.Format("{0} {1}", html.TextBox(textBoxName, null, htmlAttributes), html.Hidden(fieldName));
        }

        public static string InitializeAutoComplete(this HtmlHelper html, string textBoxName, string fieldName, string url, object options, bool wrapInReady) {

            StringBuilder sb = new StringBuilder();
            if (wrapInReady) sb.AppendLineFormat("<script language='javascript'>");

            if (wrapInReady) sb.AppendLineFormat("$().ready(function() {{");
            sb.AppendLine();
            sb.AppendLineFormat("   $('#{0}').autocomplete('{1}', {{", textBoxName.Replace(".", "\\\\."), url);

            PropertyInfo[] properties = options.GetType().GetProperties();

            for (int i = 0; i < properties.Length; i++) {
                sb.AppendLineFormat("   {0} : {1}{2}",
                                        properties[i].Name,
                                        properties[i].GetValue(options, null),
                                        i != properties.Length - 1 ? "," : "");
            }
            sb.AppendLineFormat("   }});");
            sb.AppendLine();
            sb.AppendLineFormat("   $('#{0}').result(function(e, d, f) {{", textBoxName.Replace(".", "\\\\."));
            sb.AppendLineFormat("       $('#{0}').val(d[1]);", fieldName);
            sb.AppendLineFormat("    }});");
            sb.AppendLine();
            if (wrapInReady) sb.AppendLineFormat("}});");
            if (wrapInReady) sb.AppendLineFormat("</script>");
            return sb.ToString();

        }

        public static string InitializeAutoComplete(this HtmlHelper html, string textBoxName, string fieldName, string url, object options) {
            return InitializeAutoComplete(html, textBoxName, fieldName, url, options, false);
        } 
        
    }
}
