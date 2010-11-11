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

        public virtual void Create<Y>(Y instance, string collection) {

            _mongoDatabase.GetCollection<Y>(collection).Insert(instance);
        }

        public virtual void Save<Y>(Y instance, string collection) {

            _mongoDatabase.GetCollection<Y>(collection).Save(instance);
        }

        public virtual void Save(T instance) {
            _mongoDatabase.GetCollection<T>(_Collection).Save(instance);
        }        

        public void Update(object spec, object newValues) {
            _mongoDatabase.GetCollection<T>(_Collection).Update(spec, newValues);
        }

        public void UpdateAll(object spec, object newValues, bool upsert = false) {
            //using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
            //    mongo.GetCollection<T>(_Collection).Update(spec, newValues, true, upsert);
            //}
        }        

        public virtual T FindById(ObjectId id) {            
            var query = Query.EQ("_id", id);
            return _mongoDatabase.GetCollection<T>(_Collection).FindOne(query);
        }

        public virtual T FindById(string id) {
            return FindById(new ObjectId(id));
        }

        public virtual T FindOne(object spec) {
            
            return _mongoDatabase.GetCollection<T>(_Collection).FindOne(spec);
        }

        public virtual T FindOne(Func<T, bool> func) {

            throw new NotImplementedException();
        }

        public virtual Y FindOne<Y>(Func<Y, bool> func, string collection) {
            throw new NotImplementedException();            
        }

        public virtual IEnumerable<T> Find(object spec) {
            
            return _mongoDatabase.GetCollection<T>(_Collection).Find(spec);
        }

        public virtual IEnumerable<T> Find(Func<T, bool> func) {

            return _mongoDatabase.GetCollection<T>(_Collection).FindAll().Where(func);
        }

        public virtual IEnumerable<Y> Find<Y>(Func<Y, bool> func, string collection) {

            return _mongoDatabase.GetCollection<Y>(collection).FindAll().Where(func);
        }


        public virtual IEnumerable<T> FindAll() {
            return _mongoDatabase.GetCollection<T>(_Collection).FindAll();
        }

        public virtual IEnumerable<Y> FindAll<Y>(string collection) {
            return _mongoDatabase.GetCollection<Y>(collection).FindAll();

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

        public virtual void Remove<Y>(Y instance, string collection) {
            throw new NotImplementedException();
        }

        public virtual void Remove(ObjectId id) {
            _mongoDatabase.GetCollection<T>(_Collection).Remove(Query.EQ("_id", id));
        }

        public IEnumerable<YMappedType> MapReduce<YMappedType>(string map, string reduce, string outputCollectioName) {


            throw new NotImplementedException();

        }
    }
}