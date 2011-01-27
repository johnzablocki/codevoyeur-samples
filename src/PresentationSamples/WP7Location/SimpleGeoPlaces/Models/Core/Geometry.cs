using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prohibition.SimpleGeo.Core;

namespace Prohibition.SimpleGeo.Core {
    
    public class Geometry {

        public string Point { get; set; }

        //TODO: convert to list of Coordinate 
        public IList<decimal> Coordinates { get; set; }        

    }
}
