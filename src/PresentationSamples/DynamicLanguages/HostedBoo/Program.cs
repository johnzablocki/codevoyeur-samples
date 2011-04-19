using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Boo.Lang.Interpreter;
using Boo.Lang.Compiler.IO;
using Boo.Lang.Compiler;

namespace HostedBoo {
    class Program {
        static void Main(string[] args) {

            try {

                if (checkEnvironment()) {
                    Console.WriteLine("The environment is ok, continuing on...");
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.GetBaseException().Message);
            }
        }

        private static bool checkEnvironment() {
            
            List<ValidationResult> results = runBooScript("checkenv.boo");

            foreach (ValidationResult result in results) {
                Console.WriteLine("Result type {0}: {1}", Enum.GetName(typeof(ResultType), result.ResultType), result.Message);
            }

            return results.Where(r => r.ResultType == ResultType.Fail).Count() == 0;
        }
       
    
        private static List<ValidationResult> runBooScript(string fileName) {

            InteractiveInterpreter interpreter = new InteractiveInterpreter();
            interpreter.Ducky = true;
            
            Func<string, ValidationResult> pass = (m => new ValidationResult(m, ResultType.Pass));
            Func<string, ValidationResult> fail = (m => new ValidationResult(m, ResultType.Fail));
            Func<string, ValidationResult> warn = (m => new ValidationResult(m, ResultType.Warn));

            interpreter.SetValue("results", new List<ValidationResult>());
            interpreter.SetValue("pass", pass);
            interpreter.SetValue("fail", fail);
            interpreter.SetValue("warn", warn);
                        
            CompilerContext ctx = interpreter.EvalCompilerInput(new FileInput(fileName));

            if (ctx.Errors.Count != 0) {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError error in ctx.Errors) {
                    sb.AppendLine(error.Message);
                }

                throw new ApplicationException(sb.ToString());
            }

            List<ValidationResult> results = interpreter.GetValue("results") as List<ValidationResult>;
            
            return results;

        }
        
    }
}
