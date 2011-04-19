using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text;
using Boo.Lang.Compiler;
using Boo.Lang.Interpreter;
using System.Collections.Generic;
using Boo.Lang.Compiler.IO;

namespace BooObjectValidationDsl.Validation {

    public class ValidationContext<T> {

        private static Dictionary<string, Dictionary<string, Func<T, List<ValidationResult>>>> _rules = null;        
        
        InteractiveInterpreter _interpreter = null;
        private object _instance = new object();
        private string _ruleName = "";

        private List<ValidationResult> _validationResults = new List<ValidationResult>();

        public List<ValidationResult> ValidationResults {
            get { return _validationResults; }
        }

        static ValidationContext() {
            new ValidationContext<T>().init();
        }

        public void Reset() {
            init();
        }

        public void Validate(T o) {
            Validate(o, o.GetType().Name);
        }

        public void Validate(T o, string name) {
            if (_rules.ContainsKey(name)) {
                _validationResults = _rules[name]["validate"].Invoke(o);
            } else
                throw new ApplicationException("Invalid rule name");
        }

        public bool HasErrors {

            get {
                return _validationResults.Where(r => r.ResultType == ResultType.Fail).Count() > 0;
            }
        }

        public bool HasWarnings {

            get {
                return _validationResults.Where(r => r.ResultType == ResultType.Warn).Count() > 0;
            }
        }

        public IList<string> ErrorMessages {
            get {
                return _validationResults
                    .Where(r => r.ResultType == ResultType.Fail)
                    .Select(a => a.Message).
                    ToList();
            }
        }

        public IList<string> WarningMessages {
            get {
                return _validationResults
                    .Where(r => r.ResultType == ResultType.Warn)
                    .Select(a => a.Message).
                    ToList();
            }
        }

        private void init() {

            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/Validation.boo");
            _rules = new Dictionary<string, Dictionary<string, Func<T, List<ValidationResult>>>>();            
            _interpreter = new InteractiveInterpreter();
            _interpreter.Ducky = true;

            Action<string, Action> ruleFor = delegate(string name, Action action) {

                if (!_rules.ContainsKey(name)) {
                    _rules[name] = new Dictionary<string, Func<T, List<ValidationResult>>>();
                    _ruleName = name;
                    action(); //this calls validate
                }
            };

            Action<Func<T, List<ValidationResult>>> validate = delegate(Func<T, List<ValidationResult>> func) {
                _rules[_ruleName]["validate"] = func;
            };

            _interpreter.SetValue("rule_for", ruleFor);
            _interpreter.SetValue("validate", validate);
            
            CompilerContext ctx = _interpreter.EvalCompilerInput(new FileInput(filePath));

            if (ctx.Errors.Count != 0) {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError error in ctx.Errors) {
                    sb.AppendLine(error.Message);
                }

                throw new ApplicationException(sb.ToString());
            }

        }        
    }   
}
