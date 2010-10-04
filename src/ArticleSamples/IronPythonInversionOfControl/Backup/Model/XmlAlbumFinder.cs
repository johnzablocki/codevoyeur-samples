using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IronPythonInversionOfControl.Model
{
    public class XmlAlbumFinder : IAlbumFinder
    {
        private string _fileName = null;

        public XmlAlbumFinder()
        {

        }

        public XmlAlbumFinder(string fileName)
        {
            _fileName = fileName;
        }

        public ILogger Logger { get; set; }

        public string FileName 
        {
            get { return _fileName; }
            set { _fileName = value; }
        }

        #region IAlbumFinder Members

        public IEnumerable<Album> FindAll()
        {
            XDocument doc = XDocument.Load(_fileName);

            Logger.DebugPrint("XmlAlbumFinder::FindAll was called.");

            var allAlbums = from artist in doc.Descendants("album")
                            select new Album()
                            {
                                Artist = artist.Attribute(XName.Get("artist")).Value,
                                Title = artist.Attribute(XName.Get("title")).Value                                
                            };

            return allAlbums;
        }

        #endregion
    }
}
