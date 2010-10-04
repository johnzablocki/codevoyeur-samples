using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace HostedIronPython.Model {
    
    public class AlbumFinder {
        
        private string _fileName = null;

        public AlbumFinder() {

        }

        public AlbumFinder(string fileName) {
            _fileName = fileName;
        }

        
        public string FileName {
            get { return _fileName; }
            set { _fileName = value; }
        }


        public IEnumerable<Album> FindAll() {
            
            XDocument doc = XDocument.Load(_fileName);
            
            var allAlbums = from album in doc.Descendants("album")
                            select new Album() {
                                Artist = album.Attribute(XName.Get("artist")).Value,
                                Title = album.Attribute(XName.Get("title")).Value,
                                Tracks = album.Elements("track").Select(a => a.Value).ToList()
                            };

            return allAlbums;
        }

    }
}
