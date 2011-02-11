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
using System.Text;
using System.Xml;
using System.IO;
using System.Collections.Generic;

namespace BooObjectToRssDsl.Core
{
    public class RssWriter<T>
    {
        private StringWriter _stringWriter = new StringWriter();        
        
        public RssWriter(string title, string link, string description, T[] items)
        {
            RssDslEngine engine = new RssDslEngine();

            XmlTextWriter writer = new XmlTextWriter(_stringWriter);            
            writer.WriteStartElement("rss");
                writer.WriteAttributeString("version", "2.0");
                writer.WriteStartElement("channel");
                writer.WriteElementString("title", title);
                writer.WriteElementString("link", link);
                writer.WriteElementString("description", description);
                writer.WriteElementString("language", "en-us");
                    
            foreach (T item in items)
            {
                writer.WriteStartElement("item");                
                List<string> fields = RssFieldFactory.Create();
                fields.ForEach(x => writer.WriteElementString(x, engine.Execute(item, x)));
                writer.WriteEndElement();
            }

            writer.WriteEndElement();//channel
            writer.WriteEndElement();//rss
            writer.Close();
        }

        public string GetFeed()
        {            
            return _stringWriter.ToString();            
        }
        

    }
}
