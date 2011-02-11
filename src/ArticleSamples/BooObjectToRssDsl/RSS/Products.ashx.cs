using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BooObjectToRssDsl.Core;
using BooObjectToRssDsl.Model;

namespace BooObjectToRssDsl.RSS
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class Products : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/xml";

            Manufacturer roland = new Manufacturer() { Id = 1, Name = "Roland" };
            Manufacturer fender = new Manufacturer() { Id = 2, Name = "Fender" };
            Manufacturer zoom = new Manufacturer() { Id = 3, Name = "Zoom" }; 
              
            Product juno = new Product()
            {
                Id = 1000,
                Description = "The Juno-D is the most competitively priced and full-featured synthesizer in its class. Hundreds of radio-ready sounds are packed into the Juno-D’s jet-black metal chassis, along with a world-class array of expressive multi-effects, realtime performance controllers, and tools for groove creation and composition. ",
                Name = "Juno D 61 Key Pro Keyboard",
                Manufacturer = roland,
                CreateDate = DateTime.Now   
            };

            Product tele = new Product()
            {
                Id = 1001,
                Name = "American Series Telecaster Electric Guitar",
                Description = "Over the past five-plus decades, the Fender American Telecaster electric guitar has been through numerous design variations, but its heart and soul has remained the same. For the American Series Telecaster, the original body radius and new parchment-colored pickguard give you the classic feel with a distinctive look.",
                Manufacturer = fender,
                CreateDate = DateTime.Now.AddDays(-3)                
            };

            Product h2 = new Product()
            {
                Id = 1003,
                Name = "H2 Handy Portable Digital Recorder",
                Description = "Who Needs the H2 Handy Recorder from Zoom? Everyone who craves brilliant stereo recording. Simplicity is a beautiful thing. It's a simple idea: provide brilliant stereo recording in an easy-to-use, ultra-portable device. Now everyone can record pristine audio in an infinite variety of applications. From seminars and conferences, to electronic news gathering (ENG) and podcasting, to musical performances, songwriting sessions and rehearsals, the H2 provides amazing recording quality. And no matter what kind of music you perform or the instrument you play, the H2 can effortlessly record it in high-quality stereo.",
                Manufacturer = zoom,
                CreateDate = DateTime.Now.AddDays(-1)
            };

            RssWriter<Product> writer = new RssWriter<Product>
                (
                    "Code Voyeur Music Product Listing",
                    "http://www.codevoyeurmusic.com",
                    "New Products",
                    new Product[] {juno, tele, h2}
                );            
            context.Response.Write(writer.GetFeed());
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}
