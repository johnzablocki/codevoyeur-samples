using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronPythonViewEngine.Models {
    public class Author {

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string FullName {
            get { return FirstName + " " + LastName; }
        }
    }
}