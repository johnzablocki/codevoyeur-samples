using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Prohibition.SimpleGeo.Core {
    
    public class Properties {

        public string Province { get; set; }

        public string City { get; set; }

        public string Name { get; set; }

        public IList<string> Tags { get; set; }

        public string Country { get; set; }

        public string Phone { get; set; }

        public string Href { get; set; }

        public string Address { get; set; }

        public string Owner { get; set; }

        public string Postcode { get; set; }

        public IList<Classifier> Classifiers { get; set; }
    }
}
