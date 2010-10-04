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

namespace TagCloudViewComponent.Controllers
{
    public class HomeController : SmartDispatcherController
    {
        public void Default()
        {
            Dictionary<string, int> tagsAndCounts = new Dictionary<string, int>()
            {
                {"thekillers", 130},
                {"theshins", 175},
                {"radiohead", 220},
                {"chopin", 150},
                {"beethoven", 68},
                {"countingcrows", 90},
                {"ledzeppelin", 130},
                {"elviscostello", 160},
                {"nineinchnails", 103},
                {"sarabarellis", 40},
                {"aliceinchains", 94},
                {"fuddle", 134},
                {"feist", 20},
                {"reginaspektor", 55},
                {"mozart", 35},
                {"queensryche", 65},
                {"nirvana", 23},
                {"jackjohnson", 46},
                {"thebeatles", 99},
                {"rageagainstthemachine", 84},
                {"benfoldsfive", 125},
                {"weirdalyankovic", 50},
                {"weezer", 95},
                {"thirdeyeblind", 43},
                {"queen", 102},
                {"bobdylan", 44},
                {"davematthewsband", 39},
                {"davidgray", 24}
            };

            PropertyBag["TagsAndCounts"] = tagsAndCounts;
        }

        public void Artist(string a)
        {
            
        }
    }    
}
