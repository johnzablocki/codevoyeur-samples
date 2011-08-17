using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Scripting.Hosting;
using System.Web.Mvc;

namespace IronPythonViewEngine.Core {
    
    public class PyRunner {

        private static Dictionary<string, PyRules> _rules = new Dictionary<string, PyRules>();
        private static ScriptRuntimeSetup _setup = null;
        private static ScriptRuntime _runtime = null;

        private string _currentView = null;
        private string _currentController = null;
        private ViewContext _viewContext = null;

        static PyRunner() {
            _setup = ScriptRuntimeSetup.ReadConfiguration();
            _runtime = new ScriptRuntime(_setup);
        }


        public PyRunner(ViewContext viewContext, string controllerName, string actionName, string codeBesideFile, string initFile = null) {

            _currentView = actionName;
            _currentController = controllerName;
            _viewContext = viewContext;

            //comment out to force refresh of rules
            if (_rules.ContainsKey(_currentController)) {
                return;
            }

            ScriptScope scope = _runtime.GetEngine("IronPython").CreateScope();
            if (initFile != null) {
                _runtime.GetEngine("IronPython").ExecuteFile(initFile, scope);
            }
            _runtime.GetEngine("IronPython").ExecuteFile(codeBesideFile, scope);

            var variables = scope.GetVariableNames().Where(s => !s.StartsWith("__") && !s.EndsWith("__"));

            PyRules pyRules = new PyRules();
            foreach (var variable in variables) {
                var rule = scope.GetVariable(variable);
                pyRules[variable] = rule;
            }
            _rules[getKey()] = pyRules;
        }

        public string RunRule(string ruleName) {

            if (_rules[getKey()].Contains(ruleName)) {
                return _runtime.Operations.Invoke(_rules[getKey()][ruleName], _viewContext) as string;
            }
            return (getValueFromBag(ruleName) ?? "").ToString();
        
        }

        private object getValueFromBag(string key) {
            return (_viewContext.ViewData[key] ?? _viewContext.TempData[key]) ?? _viewContext.HttpContext.Session[key];
        }

        private string getKey() {
            return _currentController + "::" + _currentView;
        }

    }
}