using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FwixPlaces.Core
{
    public class PlaceCollection
    {
        public short Success{ get; set; }
        public IList<Place> Places { get; set; }
    }
}
