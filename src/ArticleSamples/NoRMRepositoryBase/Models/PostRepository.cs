using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Norm.Linq;
using Norm;

namespace NoRMRepositoryBase.Models {
    public class PostRepository : NoRMRepositoryBase<Post> {
       
        protected override string _Collection {
            get { return "Posts"; }
        }
    }
}