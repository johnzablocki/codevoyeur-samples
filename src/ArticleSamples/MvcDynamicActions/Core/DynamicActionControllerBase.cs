using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MvcDynamicActions.Core {
    public class DynamicActionControllerBase : Controller {

        private IList<DynamicAction> _dynamicActions = new List<DynamicAction>();
        public IList<DynamicAction> DynamicActions 
        {
            get { return _dynamicActions; }
            set { _dynamicActions = value; } 
        }

    }
}
