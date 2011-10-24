using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CassandraQuickStart_CassandraSharp
{
    public class Artist {

        public string Name { get; set; }

        public int Ratings { get; set; }

        private IList<double> _nextShowLocation = new List<double>(2);

        public IList<double> NextShowLocation {
            get { return _nextShowLocation; }
            set { _nextShowLocation = value; }
        }
        

        private IList<string> _albums = new List<string>(0);

        public IList<string> Albums {
            get { return _albums; }
            set { _albums = value; }
        }

        private IList<string> _tags = new List<string>(0);
        
        public IList<string> Tags {
            get { return _tags; }
            set { _tags = value; }
        }        
        
    }
}
