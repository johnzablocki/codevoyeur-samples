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
using Castle.MonoRail.Framework;
using System.Collections.Generic;

namespace TagCloudViewComponent.Components
{       
    public class TagCloudComponent : ViewComponent
    {
        private const string TAG_HTML_TEMPLATE = "<span class=\"{0}\"><a href=\"{1}{2}/{3}\" class=\"tag\">{2}</a></span>\r\n";
        private const string TAG_CLASSNAME_TEMPLATE = "tag-{0}";

        [ViewComponentParam(Required=true)]
        public Dictionary<string, int> TagsAndCounts { get; set; }

        [ViewComponentParam(Required=true)]
        public int NumberOfStyleVariations { get; set; }

        [ViewComponentParam(Required=true)]
        public string TagLinkBaseUrl { get; set; }

        [ViewComponentParam(Required=true)]
        public string TagLinkAction { get; set; }

        public override void Render()
        {
            var sorted = TagsAndCounts.OrderBy(x => x.Key);
            double min = sorted.Min(x => x.Value);
            double max = sorted.Max(x => x.Value);
            double distribution = (double)((max - min) / NumberOfStyleVariations);
            
            foreach(var x in sorted)
            {
                for (double i = min, j = 1; i < max; i += distribution, j++)
                {
                    if (x.Value >= i && x.Value <= i+distribution)
                    {                        
                        RenderText(
                            string.Format(
                                TAG_HTML_TEMPLATE, 
                                string.Format(TAG_CLASSNAME_TEMPLATE, j), 
                                TagLinkBaseUrl, 
                                x.Key, 
                                TagLinkAction));                        
                        break;
                    }                    
                }                
            }            
        }
    }
}
