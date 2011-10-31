using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwixPlaces.Core
{
    public class Place
    {
        public string UUID { get; set; }
        public string Name { get; set; }

        public string Province { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Locality { get; set; }
        public string Postal_Code { get; set; }
        public string Phone_Number { get; set; }
        public string Country { get; set; }

        public int Price { get; set; }
        public string Link { get; set; }

        public double Lat { get; set; }
        public double Long { get; set; }

        public IList<Category> Categories { get; set; }

    }
}
