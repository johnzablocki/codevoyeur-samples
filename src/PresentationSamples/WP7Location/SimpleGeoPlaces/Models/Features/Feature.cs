using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Prohibition.SimpleGeo.Core;

namespace Prohibition.SimpleGeo.Features {
    public class Feature {

        public Geometry Geometry { get; set; }

        public string Type { get; set; }

        public string Id { get; set; }

        public Properties Properties { get; set; }

    }
}
