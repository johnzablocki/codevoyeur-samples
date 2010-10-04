using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NoRMRepositoryBase.Models;
using Norm;
using Norm.Linq;
using Norm.Responses;

namespace NoRMRepositoryBase.Models {
    public abstract class NoRMRepositoryBase<T> where T : NoRMModelBase {

        protected abstract string _Collection { get; }

        public virtual MongoDBSettings Settings { get; set; }

        public virtual void Create(T instance) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<T>(_Collection).Insert(instance);
            }
        }

        public virtual void Create<Y>(Y instance, string collection) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<Y>(collection).Insert(instance);
            }
        }

        public virtual void Save<Y>(Y instance, string collection) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<Y>(collection).Save(instance);
            }
        }

        public virtual void Save(T instance) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<T>(_Collection).Save(instance);
            }
        }        

        public void Update(object spec, object newValues) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<T>(_Collection).UpdateOne(spec, newValues);
            }
        }

        public void UpdateAll(object spec, object newValues, bool upsert = false) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<T>(_Collection).Update(spec, newValues, true, upsert);
            }
        }        

        public virtual T FindById(ObjectId id) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<T>(_Collection).FindOne(new { _id = id });
            }
        }

        public virtual T FindById(string id) {
            return FindById(new ObjectId(id));
        }

        public virtual T FindOne(object spec) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<T>(_Collection).FindOne(spec);
            }
        }

        public virtual T FindOne(Func<T, bool> func) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<T>(_Collection).AsQueryable().Where(func).FirstOrDefault();
            }
        }

        public virtual Y FindOne<Y>(Func<Y, bool> func, string collection) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<Y>(collection).AsQueryable().Where(func).FirstOrDefault();
            }
        }

        public virtual IEnumerable<T> Find(object spec) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<T>(_Collection).Find(spec);
            }
        }

        public virtual IEnumerable<T> Find(Func<T, bool> func) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<T>(_Collection).AsQueryable().Where(func);
            }
        }

        public virtual IEnumerable<Y> Find<Y>(Func<Y, bool> func, string collection) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<Y>(collection).AsQueryable().Where(func);
            }
        }


        public virtual IQueryable<T> FindAll() {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<T>(_Collection).AsQueryable();
            }
        }

        public virtual IQueryable<Y> FindAll<Y>(string collection) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                return mongo.GetCollection<Y>(_Collection).AsQueryable();
            }
        }

        public virtual void Remove(object spec) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<T>(_Collection).Delete(spec);
            }
        }

        public virtual void Remove(T instance) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<T>(_Collection).Delete(instance);
            }
        }

        public virtual void Remove(string id) {
            Remove(new ObjectId(id));
        }

        public virtual void Remove<Y>(Y instance, string collection) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<Y>(collection).Delete(instance);
            }
        }

        public virtual void Remove(ObjectId id) {
            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {
                mongo.GetCollection<T>(_Collection).Delete(new { _id = id });
            }
        }

        public IEnumerable<YMappedType> MapReduce<YMappedType>(string map, string reduce, string outputCollectioName) {

            using (Mongo mongo = Mongo.Create(Settings.ConnectionString)) {

                MapReduce mr = mongo.Database.CreateMapReduce();
                MapReduceResponse response = mr.Execute(new MapReduceOptions(_Collection) {
                    Map = map, Reduce = reduce, Permanant = false, OutputCollectionName = outputCollectioName
                });

                var collection = mongo.Database.GetCollection<YMappedType>(outputCollectioName).AsQueryable();

                return collection;
            }

        }
    }
}