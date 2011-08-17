using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoRMRepositoryBase.Models;
using System.Web.Mvc;

namespace NoRMRepositoryBase.Areas.Admin.Controllers {
    public class AdminBaseController : Controller {

        static AdminBaseController() {
            Settings = new MongoDBSettings() {
                Server = "localhost",
                Database = "MongoPress",
                Port = 27017                
            };
        }

        protected static MongoDBSettings Settings { get; set; }
    }
}