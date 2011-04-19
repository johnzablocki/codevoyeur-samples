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
using Boo.Lang.Interpreter;
using Boo.Lang.Compiler.IO;
using System.Collections.Generic;

namespace BooObjectToRssDsl.Core
{
    public class RssDslEngine
    {
        private static Dictionary<string, Dictionary<string, Func<object, string>>> _rules = new Dictionary<string, Dictionary<string, Func<object, string>>>();
        private string _ruleName = null;
        InteractiveInterpreter _interpreter = null;        
        
        static RssDslEngine()
        {
            new RssDslEngine().init();             
        }

        public void Reset()
        {
            init();
        }

        public string Execute(object o, string field)
        {
            string ruleName = o.GetType().Name;
            if (_rules.ContainsKey(ruleName))
                return _rules[ruleName][field].Invoke(o);
            else
                throw new ApplicationException("Invalid rule name");            
        }        

        private void init()
        {
            string filePath = HttpContext.Current.Server.MapPath("~/App_Data/rules.boo");
            _interpreter = new InteractiveInterpreter();
            _interpreter.Ducky = true;

            Action<string, Action> ruleFor = delegate(string name, Action action)
            {
                if (!_rules.ContainsKey(name))
                    _rules[name] = new Dictionary<string, Func<object, string>>();
                _ruleName = name;
                action();
            };


            List<string> fields = RssFieldFactory.Create();
            fields.ForEach(x => getRuleBody(x));

            _interpreter.SetValue("rule_for", ruleFor);
            _interpreter.EvalCompilerInput(new FileInput(filePath));
        }

        private void getRuleBody(string ruleName)
        {
            Action<Func<object, string>> action = (x) => _rules[_ruleName][ruleName] = x;
            _interpreter.SetValue(ruleName, action);
        }
    }
}
