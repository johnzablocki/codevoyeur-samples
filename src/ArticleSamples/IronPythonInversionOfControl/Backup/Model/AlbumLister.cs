using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronPythonInversionOfControl.Model
{
    public class AlbumLister
    {
        public IAlbumFinder Finder { get; set; }

        public ILogger Logger { get; set; }

        public IEnumerable<Album> FindByArtist(string artistName)
        {
            Logger.DebugPrint("AlbumLister::FindByArtist was called...");

            IEnumerable<Album> albums = Finder.FindAll();

            return albums.Where<Album>(a => a.Artist.ToLower() == artistName.ToLower());
        }
    }
}
