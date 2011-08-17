using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MongoDBRepositoryBase.Models {
    public class PostRepository : MongoDBRepositoryBase<Post> {
       
        public PostRepository(MongoDBSettings settings) : base(settings) {}

        protected override string _Collection {
            get { return "Posts"; }
        }
    }
}