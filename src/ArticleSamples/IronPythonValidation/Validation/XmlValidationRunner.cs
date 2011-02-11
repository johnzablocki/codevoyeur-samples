using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace IronPythonValidation.Validation
{
    public class XmlValidationRunner : ValidationRunnerBase
    {
        XDocument _ruleDoc = null;        

        public XmlValidationRunner(string ruleSource)
        {            
            _ruleDoc = XDocument.Load(ruleSource);
        }

        public override ValidationResult Run(string ruleName, object propertyValue)
        {
            var rules = from rule in _ruleDoc.Descendants("rule")
                        where rule.Attribute(XName.Get("name")).Value == ruleName
                        select new 
                        { 
                            Script = rule.Value.Trim() , 
                            Message = rule.Attribute(XName.Get("message")).Value 
                        } ;            

            foreach(var rule in rules)
            {
                if (! Eval(rule.Script, propertyValue))
                {
                    return new ValidationResult() { Value = false, ErrorMessage = rule.Message };
                }

                return new ValidationResult() { Value = true };
            }            
                
            throw new ArgumentException("No rule found for given name.");
        }
    }
}
