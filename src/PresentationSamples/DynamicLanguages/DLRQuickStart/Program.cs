using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;

namespace TipsAndTechniques {
    class Program {
        static void Main(string[] args) {
            
            try {

                ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
                ScriptRuntime runtime = new ScriptRuntime(setup);
                runtime.GetEngine("Python").Execute("print \"Hello, Hosted Script World!\"");
                //runtime.ExecuteFile("scripts\\hostedscript.py");                

                //ScriptController sc = new ScriptController("scripts\\hostedscript.py");                
                //sc.SetData("name", "John Zablocki");                
                #region func
                //Func<string, string> reverse = (s) => {
                //        string reversed = "";
                //        for (int i = s.Length - 1; i >= 0; i--) {
                //            reversed += s[i];
                //        }
                //        return reversed;
                //    };                
                //sc.SetData("reverse", reverse);
                #endregion                
                //sc.RunScript();


            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        #region ScriptController
        public class ScriptController {

            private static ScriptRuntime _runtime;
            private string _fileName = "";

            static ScriptController() {
                ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
                _runtime = new ScriptRuntime(setup);
            }

            public ScriptController(string fileName) {
                _fileName = fileName;
            }

            public void RunScript() {
                _runtime.GetEngine("IronPython").ExecuteFile(_fileName, _runtime.Globals);
            }

            public void SetData(string name, object data) {
                _runtime.Globals.SetVariable(name, data);
            }

        } 
        #endregion
    }
}
