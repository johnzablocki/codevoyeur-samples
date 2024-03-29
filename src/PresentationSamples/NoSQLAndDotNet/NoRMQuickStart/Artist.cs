﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;

namespace NoRMQuickStart {
    //Class properties map to document structure
    public class Artist {

        //ObjectId property mapped to _id field in document
        public ObjectId Id { get; set; }

        public string Name { get; set; }

        private IList<string> _albums = new List<string>(0);

        //IList property will be mapped to JavaScript Array
        public IList<string> Albums {
            get { return _albums; }
            set { _albums = value; }
        }

        //Add a Tags collection added to Artist class
        private IList<string> _tags;
        
        public IList<string> Tags {
            get { return _tags; }
            set { _tags = value; }
        }
        
    }

}
