using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace IronPythonValidation.Validation
{
    public abstract class ValidationBase
    {
        public ValidationRunnerBase Runner { get; set; }
        public Dictionary<string, string> PropertiesWithErrors { get; set; }

        public bool IsValid()
        {
            PropertiesWithErrors = new Dictionary<string, string>();
            
            Type t = this.GetType();

            PropertyInfo[] properties = t.GetProperties();
            foreach (PropertyInfo prop in properties)
            {
                object[] attributes = prop.GetCustomAttributes(false);
                foreach (Attribute attr in attributes)
                {
                    if (attr is ValidationAttribute)
                    {
                        object value = prop.GetValue(this, null);
                        ValidationResult result = Runner.Run(((ValidationAttribute)attr).RuleName, value);
                        if (!result.Value)
                            PropertiesWithErrors.Add(prop.Name, result.ErrorMessage);
                    }
                }
            }

            return PropertiesWithErrors.Count == 0;
        }
    }
}
