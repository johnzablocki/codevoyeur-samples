using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FwixPlaces.Core
{
    public class Category
    {
        public int Category_Id { get; set; }
        public int? Parent_Id { get; set; }
        public string Name { get; set; }

        public IList<Category> Categories { get; set; }

    }
}
