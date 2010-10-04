using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IronPython.Hosting;

namespace IronPythonValidation.Validation
{
    public abstract class ValidationRunnerBase
    {
        private PythonEngine _engine;
        private static Dictionary<string, PythonEngine> _engineCache = new Dictionary<string,PythonEngine>();        

        public abstract ValidationResult Run(string ruleName, object propertyValue);

        protected bool Eval(string source, object propertyValue)
        {
            string typeName = this.GetType().Name;

            if (_engineCache.ContainsKey(typeName))
                _engine = _engineCache[this.GetType().Name] ;
            else
                _engine = _engineCache[typeName] = new PythonEngine();

            _engine.Execute("from System import *");

            _engine.Globals["property_value"] = propertyValue;
            _engine.Globals["result"] = true;
            _engine.Execute(source);
            
            bool result = (bool)_engine.Globals["result"];

            _engine.Shutdown();
            return result;
        }
    }
}
