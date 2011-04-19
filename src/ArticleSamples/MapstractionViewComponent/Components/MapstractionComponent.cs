//	MapstractionComponent.cs
//	
//		Classes:
//			public MapstractionComponent
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
using MapstractionViewComponentWeb.Model;

namespace MapstractionViewComponentWeb.Components
{
    [ViewComponentDetails("Mapstraction", Sections = "CenterPointBubbleHtml,NearbyPointBubbleHtml")]
    public class MapstractionComponent : ViewComponentBase
    {
        private const int DEFAULT_ZOOM_LEVEL = 9;

        [ViewComponentParam]
        public string Name { get; set; }

        [ViewComponentParam]
        public string Container { get; set; }

        [ViewComponentParam]
        public string Provider { get; set; }

        [ViewComponentParam]
        public Location CenterPoint { get; set; }

        [ViewComponentParam]
        public Location[] NearbyPoints { get; set; }

        public override void Render()
        {
            //default names
            if (string.IsNullOrEmpty(Name)) Name = "mapstraction";
            if (string.IsNullOrEmpty(Container)) Container = "mapstraction";

            RenderTextLine("<script type=\"text/javascript\">");
            RenderTextLine("var {0} = new Mapstraction('{1}','{2}');", Name, Container, Provider);
            RenderTextLine("var centerPoint = new LatLonPoint({0}, {1});", CenterPoint.Latitude, CenterPoint.Longitude);
            RenderTextLine("{0}.setCenterAndZoom(centerPoint, {1});", Name, DEFAULT_ZOOM_LEVEL);
            RenderTextLine("{0}.addControls({{pan: true, zoom: 'large', map_type: true}});", Name);
            RenderTextLine("var marker = new Marker(centerPoint);");
            RenderTextLine("{0}.addMarker(marker);", Name);

            if (Context.HasSection("CenterPointBubbleHtml"))
            {
                PropertyBag["CenterPoint"] = CenterPoint;
                RenderSection("CenterPointBubbleHtml");
                RenderTextLine("marker.setInfoBubble(centerPointBubbleHtml);");
            }


            if (Context.HasSection("NearbyPointBubbleHtml")) 
            {
                foreach (Location location in NearbyPoints)
                {
                    PropertyBag["NearbyPoint"] = location;

                    RenderTextLine("var point{0} = new LatLonPoint({1},{2});", location.Id, location.Latitude, location.Longitude);
                    RenderTextLine("var marker{0} = new Marker(point{0});", location.Id);                    

                    RenderSection("NearbyPointBubbleHtml");
                    RenderTextLine("marker{0}.setInfoBubble(nearbyPointBubbleHtml);", location.Id);
                    RenderTextLine("{0}.addMarker(marker{1});", Name, location.Id);

                }
            }

            RenderTextLine("marker.openBubble();");
            RenderTextLine("</script>");
        }
    }
}
