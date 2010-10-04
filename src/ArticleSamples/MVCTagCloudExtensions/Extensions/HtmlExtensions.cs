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
using System.Collections;
using System.Collections.Generic;
using MVCTagCloudExtensions.Models;
using System.Text;
using System.Web.Mvc.Html;

namespace MVCTagCloudExtensions.Extensions {
    public static class HtmlExtensions {

        private const string TAG_HTML_TEMPLATE = "<span class=\"tag-{0}\">{1}</span>\r\n";

        public static string TagCloud(this HtmlHelper html, IList<ITaggable> taggables, int numberOfStyleVariations) {

            var sorted = taggables.OrderBy(x => x.Tag);
            double min = sorted.Min(x => x.Weight);
            double max = sorted.Max(x => x.Weight);
            double distribution = (double)((max - min) / numberOfStyleVariations);

            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("<div class=\"tag-cloud\">");
            foreach (var x in sorted) {
                for (double i = min, j = 1; i < max; i += distribution, j++) {
                    if (x.Weight >= i && x.Weight <= i + distribution) {
                        string link = html.RouteLink(x.Tag, new { ID = x.Tag.Replace(" ", "-"), Controller = x.Controller, Action = x.Action }, new { @class = "tag" }).ToString();
                        sb.AppendFormat(TAG_HTML_TEMPLATE, j, link);
                        break;
                    }
                }
            }
            sb.Append("</div>");
            return sb.ToString();
        }
    }
}
