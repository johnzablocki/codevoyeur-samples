using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;

namespace TipsAndTechniques {
    class Program {
        
        private const string PYTHON_FILE = "scripts\\hostedscript.py";
        
        static void Main(string[] args) {
            
            try {
                

                ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
                ScriptRuntime runtime = new ScriptRuntime(setup);
                runtime.GetEngine("Python").Execute("print \"Hello, Hosted Script World!\"");
                //runtime.ExecuteFile(PYTHON_FILE);                

                //ScriptScope scope = runtime.GetEngine("Python").CreateScope();
                //scope.SetVariable("name", "John Zablocki");
                #region func
                //Func<string, string> reverse = (s) => {
                //        string reversed = "";
                //        for (int i = s.Length - 1; i >= 0; i--) {
                //            reversed += s[i];
                //        }
                //        return reversed;
                //    };                
                //scope.SetVariable("reverse", reverse);
                #endregion                
                //runtime.GetEngine("Python").ExecuteFile(PYTHON_FILE, scope);                

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }       
    }
}
