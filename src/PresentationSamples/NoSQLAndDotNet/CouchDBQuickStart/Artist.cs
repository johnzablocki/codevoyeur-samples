using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CouchDBQuickStart {
    //Class properties map to document structure
    public class Artist {

        //ObjectId property mapped to _id field in document
        private Guid _id;

        public Guid Id {
            get {
                if (_id == Guid.Empty) {
                    _id = Guid.NewGuid();
                }
                return _id; 
            }
            set { _id = value; }
        }
        

        public string Rev { get; set; }

        private string _type = "artist";
        public string Type {
            get { return _type; }
            private set { _type = value; } 
        }
        
        public string Name { get; set; }

        public int Ratings { get; set; }


        private IList<string> _tourStops = new List<string>(0);
            
        public IList<string> TourStops {
            get { return _tourStops;  }
            set { _tourStops = value; }
        }
        
        private IList<string> _albums = new List<string>(0);

        //IList property will be mapped to JavaScript Array
        public IList<string> Albums {
            get { return _albums; }
            set { _albums = value; }
        }

        //Add a Tags collection added to Artist class
        private IList<string> _tags = new List<string>(0);

        public IList<string> Tags {
            get { return _tags; }
            set { _tags = value; }
        }

    }

}
