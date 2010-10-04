using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections;

namespace MvcExtensions.Core {
    
    public class SimpleView : IView {
        
        #region IView Members

        public void Render(ViewContext viewContext, System.IO.TextWriter writer) {

            string viewFile = string.Format("Templates\\{0}\\{1}.txt", viewContext.RouteData.Values["controller"], viewContext.RouteData.Values["action"]);
            string pathToViewFile = Path.Combine(viewContext.HttpContext.Server.MapPath("."), viewFile);

            using (StreamReader reader = new StreamReader(pathToViewFile)) {

                string text = reader.ReadToEnd();
                processTemplate(ref text, viewContext);
                writer.Write(text.Replace(Environment.NewLine, "<br />"));
            }

        }

        #endregion

        private void processTemplate(ref string text, ViewContext viewContext) {

            processTemplate(viewContext.ViewData, ref text);
            processTemplate(viewContext.TempData, ref text);            
        }

        private string processTemplate(IDictionary<string, object> dictionary, ref string text) {
            foreach (string key in dictionary.Keys) {
                text = text.Replace(string.Format("[[{0}]]", key), dictionary[key].ToString());
            }

            return text;
        }

    }
}
