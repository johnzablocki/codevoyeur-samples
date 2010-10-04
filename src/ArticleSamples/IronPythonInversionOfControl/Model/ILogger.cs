using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IronPythonInversionOfControl.Model
{
    public interface ILogger
    {
        void DebugPrint(string message);
    }
}
