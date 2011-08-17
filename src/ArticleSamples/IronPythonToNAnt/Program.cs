using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;

namespace IronPythonToNAnt {
    class Program {
        static void Main(string[] args) {

            try {

                ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
                ScriptRuntime runtime = new ScriptRuntime(setup);

                ScriptEngine engine = runtime.GetEngine("Python");
                ScriptScope scope = engine.CreateScope();

                engine.ExecuteFile("Scripts\\BuildProject.py", scope);

                IronPython.Runtime.py

                var constructor = scope.GetVariable("__init__");

                Console.WriteLine(constructor);


            } catch (Exception ex) {
                Console.WriteLine(ex.GetBaseException().Message);
            }

        }
    }
}
