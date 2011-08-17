using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleGeoWebForms.Models.Core;

namespace SimpleGeoWebForms.Models.Core {
    
    public class Geometry {

        public string Point { get; set; }

        //TODO: convert to list of Coordinate 
        public IList<double> Coordinates { get; set; }        

    }
}
