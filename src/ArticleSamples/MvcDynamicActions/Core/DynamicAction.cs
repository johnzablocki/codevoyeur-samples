using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDynamicActions.Core {
    public class DynamicAction {

        public string ActionName { get; set; }
        public Func<ActionResult> Result { get; set; }
       
    }
}
