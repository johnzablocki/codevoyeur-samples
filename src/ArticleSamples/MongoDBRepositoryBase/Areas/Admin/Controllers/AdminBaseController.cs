using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDBRepositoryBase.Models;
using System.Web.Mvc;

namespace MongoDBRepositoryBase.Areas.Admin.Controllers {
    public class AdminBaseController : Controller {

        static AdminBaseController() {
            Settings = new MongoDBSettings() {
                Server = "localhost",
                Database = "MongoDBPress",
                Port = 27017                
            };
        }

        protected static MongoDBSettings Settings { get; set; }
    }
}