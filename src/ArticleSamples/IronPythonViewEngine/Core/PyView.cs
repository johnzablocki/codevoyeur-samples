using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Text.RegularExpressions;

namespace IronPythonViewEngine.Core {
    
    public class PyView : IView {

        

        #region IView Members

        public void Render(ViewContext viewContext, System.IO.TextWriter writer) {

            string action = viewContext.RouteData.Values["action"] as string;
            string controller = viewContext.RouteData.Values["controller"] as string;

            string viewFile = string.Format(@"Views\{0}\{1}.html", controller, action);
            string appPath = viewContext.HttpContext.Server.MapPath(viewContext.HttpContext.Request.ApplicationPath);
            string pathToInitFile = Path.Combine(appPath, @"Views\Shared\lib.py");
            string pathToViewFile = Path.Combine(appPath, viewFile);

            PyRunner pyRunner = new PyRunner(viewContext, controller, action, pathToViewFile + ".py", pathToInitFile);

            using (StreamReader reader = new StreamReader(pathToViewFile)) {
                string contents = reader.ReadToEnd();

                MatchCollection matches = Regex.Matches(contents, @"#{(\w+)}", RegexOptions.IgnoreCase | RegexOptions.Compiled);
                foreach (Match match in matches) {
                    contents = Regex.Replace(contents, match.Value, pyRunner.RunRule(match.Groups[1].Value));
                }

                writer.Write(contents);
            }
            
        }

        #endregion
    }
}