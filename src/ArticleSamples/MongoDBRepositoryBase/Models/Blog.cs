using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDB.Bson;

namespace MongoDBRepositoryBase.Models {
    public class Blog : MongoDBModelBase {

        public ObjectId Id { get; set; }


        public string Name { get; set; }

        public string Author { get; set; }

    }
}