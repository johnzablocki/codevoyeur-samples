using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MongoDBRepositoryBase.Models;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MongoDBRepositoryBase.Models {
    public abstract class MongoDBRepositoryBase<T> where T : MongoDBModelBase {

        protected abstract string _Collection { get; }

        private readonly MongoServer _mongoServer = null;
        private readonly MongoDatabase _mongoDatabase = null;

        public MongoDBRepositoryBase(MongoDBSettings settings) {
            _mongoServer = MongoServer.Create(settings.ConnectionString);
            _mongoDatabase = _mongoServer.GetDatabase(settings.Database);
        }

        public virtual void Create(T instance) {
            _mongoDatabase.GetCollection<T>(_Collection).Insert(instance);                        
        }

        public virtual void Save(T instance) {
            _mongoDatabase.GetCollection<T>(_Collection).Save(instance);
        }

        public void Update(BsonDocument spec, BsonDocument newValues, bool upsert = false, UpdateFlags flags = UpdateFlags.None) {

            if (upsert) {
                _mongoDatabase.GetCollection<T>(_Collection).Update(spec, newValues, UpdateFlags.Upsert | flags);
            } else {
                _mongoDatabase.GetCollection<T>(_Collection).Update(spec, newValues, flags);
            }
        }

        public void UpdateAll(BsonDocument spec, BsonDocument newValues, bool upsert = false) {
            
            if (upsert) {
                Update(spec, newValues, true, UpdateFlags.Multi);
            } else {
                Update(spec, newValues, false, UpdateFlags.Multi);
            }
            
        }        

        public virtual T FindById(ObjectId id) {
            return _mongoDatabase.GetCollection<T>(_Collection).FindOne(Query.EQ("_id", id));            
        }

        public virtual T FindById(string id) {
            return FindById(new ObjectId(id));
        }

        public virtual T FindOne(BsonDocument spec) {            
            return _mongoDatabase.GetCollection<T>(_Collection).FindOne(spec);
        }
                
        public virtual IEnumerable<T> Find(object spec) {
            
            return _mongoDatabase.GetCollection<T>(_Collection).Find(spec);
        }

        public virtual IEnumerable<T> Find(Func<T, bool> func) {

            return _mongoDatabase.GetCollection<T>(_Collection).FindAll().Where(func);
        }
          
        
        public virtual IEnumerable<T> FindAll() {
            return _mongoDatabase.GetCollection<T>(_Collection).FindAll();
        }

        public virtual void Remove(object spec) {
            _mongoDatabase.GetCollection<T>(_Collection).Remove(spec);
        }

        public virtual void Remove(T instance) {
            throw new NotImplementedException();
        }

        public virtual void Remove(string id) {
            Remove(new ObjectId(id));
        }

        public virtual void Remove(ObjectId id) {
            _mongoDatabase.GetCollection<T>(_Collection).Remove(Query.EQ("_id", id));
        }

        public IEnumerable<TMapReduceType> MapReduce<TMapReduceType>(BsonJavaScript map, BsonJavaScript reduce, string outputCollectioName) {
                              
            var result = _mongoDatabase.GetCollection<T>(_Collection)
                .MapReduce(map, reduce, MapReduceOptions.SetKeepTemp(true).SetOutput(outputCollectioName));
            var collection = _mongoDatabase.GetCollection<TMapReduceType>(result.ResultCollectionName);

            return collection.FindAll();

        }
    }
}