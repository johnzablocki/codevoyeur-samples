using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using System.Xml.Linq;
using Castle.ActiveRecord;
using System.Reflection;
using Castle.ActiveRecord.Framework.Config;

namespace LocationSearchWithActiveRecord
{
    public class Global : System.Web.HttpApplication
    {

        protected void Application_Start(object sender, EventArgs e)
        {
            ActiveRecordStarter.Initialize(
                Assembly.Load("LocationSearchWithActiveRecord"),
                ActiveRecordSectionHandler.Instance);
        }
        
    }
}