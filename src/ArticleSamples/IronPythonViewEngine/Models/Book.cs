using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronPythonViewEngine.Models {
    
    public class Book {

        public string ISBN { get; set; }
        
        public string Title { get; set; }

        public Author Author { get; set; }

    }
}