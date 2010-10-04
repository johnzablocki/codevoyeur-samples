using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoRMRepositoryBase.Models;

namespace NoRMRepositoryBase.Models {
    public class BlogRepository : NoRMRepositoryBase<Blog> {

        protected override string _Collection {
            get { return "Blogs"; }
        }
    }
}