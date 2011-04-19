using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;
using MongoDB.Bson.DefaultSerializer;

namespace MongoDBRepositoryBase.Models {
    public class Post : MongoDBModelBase {

        public string Title { get; set; }

        public string Content { get; set; }

        public ObjectId BlogId { get; set; }

    }
}