using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using Microsoft.Scripting.Hosting;
using System.Reflection;
using Microsoft.Scripting;

namespace HostedIronPython.Container {
    public class PyCrust {

        private static Dictionary<string, object> _staticObjects = new Dictionary<string, object>();
        private static Dictionary<string, ContainedObject> _containedObjects = new Dictionary<string, ContainedObject>();

        private string _filePath = null;

        public PyCrust(string filePath) {
            _filePath = filePath;
        }

        public object GetFilling(string objectName) {

            XDocument doc = XDocument.Load(_filePath);

            if (_containedObjects.ContainsKey(objectName)) {
                return ConstructObject(_containedObjects[objectName]);
            } else {

                var contained = (from obj in doc.Descendants("object")
                                 where obj.Attribute("name").Value == objectName
                                 select new ContainedObject {
                                     Name = objectName,
                                     Script = obj.Value.Trim(),
                                     IsStatic = bool.Parse(obj.Attribute("static").Value)
                                 }).FirstOrDefault();

                if (contained != null)
                    return ConstructObject(contained);
            }

            throw new ApplicationException("No object found for given name.");
        }


        public object GetObject(string objectName) {

            Dictionary<string, object> instances = new Dictionary<string, object>();

            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            ScriptRuntime runtime = new ScriptRuntime(setup);
            runtime.LoadAssembly(Assembly.GetExecutingAssembly());

            ScriptEngine engine = runtime.GetEngine("IronPython");
            runtime.Globals.SetVariable("instances", instances);
            engine.ExecuteFile("Objects.py", runtime.Globals);

            return instances[objectName];

        }

        protected object ConstructObject(ContainedObject co) {

            if (_staticObjects.ContainsKey(co.Name)) {
                return _staticObjects[co.Name];
            } else {

                //in the full CodeVoyeur version, this is cached!
                ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
                ScriptRuntime runtime = new ScriptRuntime(setup);
                runtime.LoadAssembly(Assembly.GetExecutingAssembly());

                ScriptScope scope = runtime.CreateScope("IronPython");
                ScriptSource source = scope.Engine.CreateScriptSourceFromString("from HostedIronPython.Model import *", SourceCodeKind.SingleStatement);
                source.Execute(scope);

                scope.SetVariable("instance", new object());

                scope.SetVariable("reference", new Func<string, object>
                    (
                        delegate(string refName) {
                            if (_staticObjects.ContainsKey(refName))
                                return _staticObjects[refName];
                            else
                                return GetFilling(refName);
                        }
                    ));

                ScriptSource configSource = scope.Engine.CreateScriptSourceFromString(co.Script, SourceCodeKind.Statements);
                configSource.Execute(scope);

                if (co.IsStatic)
                    return _staticObjects[co.Name] = scope.GetVariable("instance");
                else
                    return scope.GetVariable("instance");
            }
        }
    }
}
