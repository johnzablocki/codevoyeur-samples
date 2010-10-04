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

namespace MVCTagCloudExtensions.Models {
    
    public class TaggableArtist : ITaggable {
        #region ITaggable Members

        public string Tag { get; set; }
        public int Weight { get; set; }
        
        public string Controller 
        {
            get { return "Artists"; }            
        }

        public string Action 
        {
            get { return "Index"; }            
        }

        #endregion
    }
}
