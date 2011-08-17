using System;
using System.Collections;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Linq;
using BooObjectToRssDsl.Model;
using BooObjectToRssDsl.Core;

namespace BooObjectToRssDsl.RSS
{
    /// <summary>
    /// Summary description for $codebehindclassname$
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class ProductReviews : IHttpHandler
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
                Description = "he Juno-D is the most competitively priced and full-featured synthesizer in its class. Hundreds of radio-ready sounds are packed into the Juno-D’s jet-black metal chassis, along with a world-class array of expressive multi-effects, realtime performance controllers, and tools for groove creation and composition. ",
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

            ProductReview h2Review = new ProductReview()
            {
                Id = 10,
                Product = h2,
                Review = "After tantalizing hints and a more tantalizing shipping delay, the Zoom H2 is finally here, promising digital surround-sound recording in the palm of your hand for only $199. Was it worth the wait? Well, let me say that it certainly lives up to its moniker: this is one Handy Recorder.",
                ReviewDate = DateTime.Now,
                UserName = "Mark Nelson"
            };

            ProductReview teleReview = new ProductReview()
            {
                Id = 10,
                Product = tele,
                Review = "Not a single flaw to be found. Neck is wonderful, and paly like no guitar I have picked up before. Stock setup, well it's a Fender.",
                ReviewDate = DateTime.Now,
                UserName = "Craig Raymond"
            };

            ProductReview junoReview = new ProductReview()
            {
                Id = 10,
                Product = juno,
                Review = "The first thing you notice when turning it on is the screen. It's much bigger than it looks in the pictures on Roland's site; very bright and very orange. Similar to the look of the XP-50 screen, but bigger text and a lot more information. The contrast knob has a wide range and makes the screen viewable from any angle.",
                ReviewDate = DateTime.Now,
                UserName = "Jonathan Block"
            };

            RssWriter<ProductReview> writer = new RssWriter<ProductReview>
                (
                    "Code Voyeur Music Product Reviews",
                    "http://www.codevoyeurmusic.com/reviews",
                    "Recent Product Reviews",
                    new ProductReview[] { junoReview, teleReview, h2Review }
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
