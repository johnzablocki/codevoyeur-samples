using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleGeoPlaces.Models.Core;

namespace SimpleGeoPlaces.Models.Core {
    
    public class Geometry {

        public string Point { get; set; }

        //TODO: convert to list of Coordinate 
        public IList<decimal> Coordinates { get; set; }        

    }
}
