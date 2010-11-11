using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace MongoDBRepositoryBase.Models {
    public class Post : MongoDBModelBase {

        public string Title { get; set; }

        public string Content { get; set; }

        public ObjectId BlogId { get; set; }

    }
}