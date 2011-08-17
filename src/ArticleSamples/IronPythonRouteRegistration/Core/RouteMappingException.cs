using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IronPythonRouteRegistration.Core {
    public class RouteMappingException : Exception {

        private const string DEFAULT_EXCEPTION_MESSAGE = "Expected non-null values for url, defaults, and constraints in return tuple for each mapping function.";
        
        public RouteMappingException(string message = DEFAULT_EXCEPTION_MESSAGE) : 
            base(message) {
        }        
    }
}