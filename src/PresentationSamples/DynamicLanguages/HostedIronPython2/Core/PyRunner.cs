using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;

namespace HostedIronPython2.Core {
    
    public class PyRunner {

        private static ScriptRuntimeSetup _scriptRuntimeSetup = null;
        private static ScriptRuntime _scriptRuntime = null;
        private static Dictionary<string, object> _rules = null;

        static PyRunner() {
            _scriptRuntimeSetup = ScriptRuntimeSetup.ReadConfiguration();
            _scriptRuntime = new ScriptRuntime(_scriptRuntimeSetup);
            _rules = new Dictionary<string, object>();
        }

        public void Load(string pyFile) {

            ScriptEngine engine = _scriptRuntime.GetEngine("Python");
            ScriptScope scope = engine.CreateScope();
            engine.ExecuteFile(pyFile, scope);

            IEnumerable<string> names = scope.GetVariableNames().Where(s => !s.StartsWith("__") && !s.EndsWith("__")); //skip private vars

            foreach (string name in names) {
                _rules.Add(name, scope.GetVariable(name));
            }

        }

        public void Execute(string name, object data) {

            if (_rules.ContainsKey(name)) {
                _scriptRuntime.Operations.Invoke(_rules[name], data);
            }
        }

    }
}
