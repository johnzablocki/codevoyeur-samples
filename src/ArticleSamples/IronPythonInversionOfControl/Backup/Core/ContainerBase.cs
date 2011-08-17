using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;
using System.Reflection;

namespace IronPythonInversionOfControl.Core
{
    public abstract class ContainerBase
    {
        protected static Dictionary<string, object> _StaticObjects = new Dictionary<string, object>();        

        public abstract T GetObject<T>(string objectName);
            
        public abstract object GetObject(string objectName);
        
        protected object ConstructObject(ContainedObject co)
        {
            if (_StaticObjects.ContainsKey(co.Name))
            {
                return _StaticObjects[co.Name];
            }            
            else
            {
                PythonEngine engine = new PythonEngine();

                engine.LoadAssembly(Assembly.GetExecutingAssembly());
                engine.Execute("from IronPythonInversionOfControl import *");
                engine.Execute("from IronPythonInversionOfControl.Model import *");

                engine.Globals["instance"] = null;
                engine.Globals["reference"] = new Func<string, object>
                    (
                        delegate(string refName)
                        {
                            if (_StaticObjects.ContainsKey(refName))
                                return _StaticObjects[refName];
                            else
                                return GetObject(refName);
                        }
                    );
                
                engine.Execute(co.Script);

                engine.Shutdown();

                if (co.IsStatic)
                    return _StaticObjects[co.Name] = engine.Globals["instance"];
                else
                    return engine.Globals["instance"];
            }            

        }        
    }
}
