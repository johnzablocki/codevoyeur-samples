using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace FwixPlaces.Core
{
    public class CategoryCollection
    {
        public short Success{ get; set; }
        public IList<Category> Categories { get; set; }
    }
}
