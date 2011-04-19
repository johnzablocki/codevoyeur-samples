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
using System.Text;
using System.Reflection;

namespace MvcJWFlvPlayerExtensions.Extensions {
    public static class FlashExtensions {

        public static string FlashPlayer(this HtmlHelper html, string pathToPlayer, string containerID, int height, int width, string version, object flashVars, object parameters) {
            return FlashPlayer(html, null, pathToPlayer, containerID, height, width, version, flashVars, parameters);
        }

        public static string FlashPlayer(this HtmlHelper html, string pathToSWFObject, string pathToPlayer, string containerID, int height, int width, string version, object flashVars, object parameters) {

            StringBuilder sb = new StringBuilder();

            if (! string.IsNullOrEmpty(pathToSWFObject))
                sb.AppendFormat("<script type=\"text/javascript\" src=\"{0}\"></script>", pathToPlayer);
            
            sb.Append("<script type='text/javascript'>");

            string parametersHash = getJSObject(parameters);
            string flashVarsHash = getJSObject(flashVars);

            sb.AppendFormat("swfobject.embedSWF(\"{0}\", \"{1}\", \"{2}\", \"{3}\", \"{4}\", {{{5}}}, {{{6}}});", pathToPlayer, containerID, height, width, version, parametersHash, flashVarsHash);
            
            sb.AppendFormat("</script>");

            
            return sb.ToString();
        }

        public static string FlashPlayerEmbed(this HtmlHelper html, string pathToPlayer, string pathToMedia, int width, int height, object parameters) {

            StringBuilder sb = new StringBuilder();

            sb.AppendFormat(@"<embed src=""{0}"", width=""{1}"", height=""{2}"", {3} />", pathToPlayer, width, height, getTagAttributes(parameters));


            return sb.ToString();

        }
              
   
        private static string getJSObject(object source) {

            PropertyInfo[] properties = source.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < properties.Length; i++) {
                sb.AppendFormat("{0}:\"{1}\"{2}",
                                properties[i].Name,
                                properties[i].GetValue(source, null),
                                i != properties.Length - 1 ? "," : "");
            }

            return sb.ToString();
        }

        private static string getTagAttributes(object source) {
           
            PropertyInfo[] properties = source.GetType().GetProperties();

            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < properties.Length; i++) {
                sb.AppendFormat("{0}=\"{1}\"",
                                properties[i].Name,
                                properties[i].GetValue(source, null));
            }

            return sb.ToString();
        }

    }
        
}
