using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronPythonInversionOfControl.Model
{
    public class Logger : ILogger
    {
        public void DebugPrint(string message)
        {
            Console.WriteLine("\tDEBUG: {0}", message);
        }
    }
}
