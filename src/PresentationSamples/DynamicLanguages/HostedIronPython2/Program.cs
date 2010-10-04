using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Scripting.Hosting;
using System.Reflection;

namespace HostedIronPython2 {

    public class Foo {
        public string Size { get; set; }
        public string Bar { get; set; }
    }

    class Program {

        static void Main(string[] args) {

            try {

                var items = new List<Foo>() {
                    new Foo() { Bar = "connecticut", Size = "Small" },
                    new Foo() { Bar = "new york", Size = "Large" },
                    new Foo() { Bar = "new jersey", Size = "Medium" }
                };

                items.ForEach((i) => { getPythonFunc(i); Console.WriteLine(i.Bar); });

            } catch (Exception ex) {

                Console.WriteLine(ex.Message);
            }

        }

        static void getPythonFunc(Foo foo) {
            
            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            ScriptRuntime runtime = new ScriptRuntime(setup);
            runtime.LoadAssembly(Assembly.GetExecutingAssembly());
            ScriptEngine engine = runtime.GetEngine("IronPython");
            ScriptScope scope = engine.CreateScope();

            engine.ExecuteFile("filter.py", scope);

            var filterFunc = scope.GetVariable("filter_" + foo.Size.ToLower());
            scope.Engine.Operations.Invoke(filterFunc, foo);
        }
    }
}
