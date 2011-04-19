using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronPythonViewEngine.Core {
    public class PyRules {

        private Dictionary<string, object> _codeBlocks = new Dictionary<string, object>(0);

        public Object this[string ruleName] {
            get { return _codeBlocks[ruleName]; }
            set { _codeBlocks[ruleName] = value; }
        }

        public int Count {
            get { return _codeBlocks.Count; }
        }

        public bool Contains(string ruleName) {
            return _codeBlocks.ContainsKey(ruleName);
        }       
    }
}