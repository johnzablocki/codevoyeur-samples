using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SimpleGeoPlaces.Models.Features {
    public class FeatureCollection {

        public int Total { get; set; }
        
        public string Type { get; set; }

        public IList<Feature> Features { get; set; }

    }
}
