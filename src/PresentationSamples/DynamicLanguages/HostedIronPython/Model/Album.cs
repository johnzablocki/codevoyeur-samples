using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HostedIronPython.Validation;

namespace HostedIronPython.Model {
    
    public class Album : ValidationBase{
        
        [Validation("artist_name_length")]
        //[Validation("starts_uppercase_or_digit")]
        public string Artist { get; set; }

        [Validation("title_length")]
        public string Title { get; set; }

        [Validation("non_empty_set")]
        public List<string> Tracks { get; set; }
    }
}
