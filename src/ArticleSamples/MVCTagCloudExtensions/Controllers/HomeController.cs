using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using MVCTagCloudExtensions.Models;

namespace MVCTagCloudExtensions.Controllers {
    [HandleError]
    public class HomeController : Controller {
        
        public ActionResult Index() {
            
            IList<ITaggable> artists = new List<ITaggable>()
            {
                new TaggableArtist() { Tag = "Radiohead", Weight = 100 },
                new TaggableArtist() { Tag = "The Shins", Weight = 90 },
                new TaggableArtist() { Tag = "The Killers", Weight = 85 },
                new TaggableArtist() { Tag = "JayMay", Weight = 30 },
                new TaggableArtist() { Tag = "Sara Barellis", Weight = 40 },
                new TaggableArtist() { Tag = "Missy Higgins", Weight = 20 },
                new TaggableArtist() { Tag = "Ben Folds", Weight = 50 },
                new TaggableArtist() { Tag = "Counting Crows", Weight = 30 },
                new TaggableArtist() { Tag = "Franz Ferdinand", Weight = 10},
                new TaggableArtist() { Tag = "Chairlift", Weight = 15 },
                new TaggableArtist() { Tag = "Ok Go", Weight = 30},
                new TaggableArtist() { Tag = "Regina Spektor", Weight = 60},
                new TaggableArtist() { Tag = "Weezer", Weight = 70},
                new TaggableArtist() { Tag = "Nine Inch Nails", Weight = 80},
                new TaggableArtist() { Tag = "Third Eye Blind", Weight = 65},
                new TaggableArtist() { Tag = "Led Zeppelin", Weight = 80},
                new TaggableArtist() { Tag = "The Beatles", Weight = 40},
                new TaggableArtist() { Tag = "Elvis Costello", Weight = 90},
                new TaggableArtist() { Tag = "Audioslave", Weight = 30}
            };

            ViewData.Model = artists;
            return View();
        }

    }
}
