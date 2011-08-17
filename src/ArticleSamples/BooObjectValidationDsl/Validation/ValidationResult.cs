using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Xml.Linq;
using Boo.Lang.Interpreter;
using Boo.Lang.Compiler.IO;
using System.Collections.Generic;
using Boo.Lang.Compiler;
using System.Text;
using System.Reflection;

namespace BooObjectValidationDsl.Validation {

    public enum ResultType { Pass, Fail, Warn }

    public class ValidationResult {

        public ResultType ResultType { get; set; }

        public string Message { get; set; }

        public ValidationResult(string message, ResultType resultType) {
            Message = message;
            ResultType = resultType;
        }
    }
}
