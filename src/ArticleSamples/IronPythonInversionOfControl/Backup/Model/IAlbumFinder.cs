using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronPythonInversionOfControl.Model
{
    public interface IAlbumFinder
    {
        IEnumerable<Album> FindAll();
        ILogger Logger { get; set; }

        string FileName { get; set; }
    }
}
