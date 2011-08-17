using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostedIronPython.Validation {
    
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class ValidationAttribute : Attribute {
        readonly string _ruleName;

        public ValidationAttribute(string ruleName) {
            _ruleName = ruleName;
        }

        public string RuleName {
            get { return _ruleName; }
        }
    }
}
