using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Norm;

namespace NoRMRepositoryBase.Models {
    public class Post : NoRMModelBase {

        public string Title { get; set; }

        public string Content { get; set; }

        public ObjectId BlogId { get; set; }

    }
}