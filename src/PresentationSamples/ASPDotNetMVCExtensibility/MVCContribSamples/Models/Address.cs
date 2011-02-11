using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCContribSamples.Models {
    public class Address {
        public int ID { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string ZipCode { get; set; }
    }
}
