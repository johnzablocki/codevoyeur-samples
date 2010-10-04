//	ViewComponentBase.cs
//	
//		Classes:
//			public ViewComponentBase
//
//		Created By: 
//			jcz 2008-01-03 			
//
//		Modification History:
//			
//

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

namespace MapstractionViewComponentWeb.Components
{
    public class ViewComponentBase : ViewComponent
    {
        public void RenderTextLine(string textToRender, params object[] args)
        {
            RenderText(string.Format(string.Concat(textToRender, "\n"), args));
        }

        public void RenderTextLine(string textToRender)
        {
            RenderText(string.Concat(textToRender, "\n"));
        }
    }
}
