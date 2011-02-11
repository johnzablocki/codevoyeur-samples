using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronPythonInversionOfControl.Model
{
    public class Album
    {
        public string Artist { get; set; }

        public string Title { get; set; }

        public List<string> Tracks { get; set; }
    }
}
