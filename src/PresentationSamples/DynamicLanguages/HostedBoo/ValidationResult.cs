using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HostedBoo {

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
