using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDBRepositoryBase.Models;

namespace MongoDBRepositoryBase.Models {
    public class BlogRepository : MongoDBRepositoryBase<Blog> {

        public BlogRepository(MongoDBSettings settings) : base(settings) {}

        protected override string _Collection {
            get { return "Blogs"; }
        }
    }
}