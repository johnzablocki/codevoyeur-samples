using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostedIronPython.Container {
    
    public class ContainedObject {
        public string Name { get; set; }

        public string Script { get; set; }

        public bool IsStatic { get; set; }
    }
}
