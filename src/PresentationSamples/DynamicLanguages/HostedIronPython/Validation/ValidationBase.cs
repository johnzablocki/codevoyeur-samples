using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Xml.Linq;
using Microsoft.Scripting.Hosting;
using Microsoft.Scripting;

namespace HostedIronPython.Validation {
    public class ValidationBase {

        public Dictionary<string, string> PropertiesWithErrors { get; set; }

        public bool IsValid() {
            
            PropertiesWithErrors = new Dictionary<string, string>();

            Type t = this.GetType();

            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo prop in properties) {
                object[] attributes = prop.GetCustomAttributes(false);
                foreach (Attribute attr in attributes) {
                    if (attr is ValidationAttribute) {
                        object value = prop.GetValue(this, null);
                        ValidationResult result = test(((ValidationAttribute)attr).RuleName, value);
                        if (!result.Value)
                            PropertiesWithErrors.Add(prop.Name, result.ErrorMessage);
                    }
                }
            }

            return PropertiesWithErrors.Count == 0;
        }

        private ValidationResult test(string ruleName, object propertyValue) {

            XDocument doc = XDocument.Load("ValidationRules.xml");

            var rule = (from r in doc.Descendants("rule")
                         where r.Attribute(XName.Get("name")).Value == ruleName
                         select new {
                             Script = r.Value.Trim(),
                             Message = r.Attribute(XName.Get("message")).Value
                         }).FirstOrDefault();

            if (! eval(rule.Script, propertyValue)) {
                return new ValidationResult() { Value = false, ErrorMessage = rule.Message };
            } else {
                return new ValidationResult() { Value = true };
            }

            throw new ArgumentException("No rule found for given name.");
        }

        protected bool eval(string script, object propertyValue) {

            ScriptRuntimeSetup setup = ScriptRuntimeSetup.ReadConfiguration();
            ScriptRuntime runtime = new ScriptRuntime(setup);

            ScriptScope scope = runtime.CreateScope("IronPython");
            scope.SetVariable("property_value", propertyValue);
            scope.SetVariable("result", true);

            scope.Engine.CreateScriptSourceFromString("import clr", SourceCodeKind.SingleStatement).Execute(scope);
            scope.Engine.CreateScriptSourceFromString("clr.AddReference('System')", SourceCodeKind.SingleStatement).Execute(scope);
            scope.Engine.CreateScriptSourceFromString("from System import *", SourceCodeKind.SingleStatement).Execute(scope);
            ScriptSource source = scope.Engine.CreateScriptSourceFromString(script, SourceCodeKind.Statements);
            source.Execute(scope);
            
            bool result = (bool)scope.GetVariable("result");

            return result;
        }
    }
}
