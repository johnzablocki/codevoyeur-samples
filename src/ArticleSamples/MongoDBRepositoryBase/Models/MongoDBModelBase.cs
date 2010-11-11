using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MongoDB.Bson;

namespace MongoDBRepositoryBase.Models {
    public abstract class MongoDBModelBase {
                
        public ObjectId Id { get; set; }
    }
}
