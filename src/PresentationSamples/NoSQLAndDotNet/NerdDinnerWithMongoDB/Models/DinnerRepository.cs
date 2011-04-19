using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Objects.DataClasses;
using System.Data;
using Norm;

namespace NerdDinner.Models
{

    public class DinnerRepository : NerdDinner.Models.IDinnerRepository
    {

        private const string _connectionString = "mongodb://localhost:27017/NerdDinner";

        //
        // Query Methods

        public IQueryable<Dinner> FindDinnersByText(string q)
        {

            using (Mongo mongo = Mongo.Create(_connectionString)) {

                return mongo.Database.GetCollection<Dinner>().AsQueryable()
                            .Where(d => d.Title.Contains(q)
                            || d.Description.Contains(q)
                            || d.HostedBy.Contains(q));
            }

            
        }

        public IQueryable<Dinner> FindAllDinners()
        {
            using (Mongo mongo = Mongo.Create(_connectionString)) {
                return mongo.Database.GetCollection<Dinner>().AsQueryable();
            }
        }

        public IQueryable<Dinner> FindUpcomingDinners()
        {
            using (Mongo mongo = Mongo.Create(_connectionString)) {
                
                return from dinner in FindAllDinners()
                       where dinner.EventDate >= DateTime.Now
                       orderby dinner.EventDate
                       select dinner;    
            }
            
        }

        public IQueryable<Dinner> FindByLocation(float latitude, float longitude)
        {
            using (Mongo mongo = Mongo.Create(_connectionString)) {
                var dinners = from dinner in FindUpcomingDinners()
                              join i in NearestDinners(latitude, longitude)
                              on dinner.DinnerID equals i.DinnerID
                              select dinner;

                return dinners;    
            }            
        }

        public Dinner GetDinner(string id)
        {
            using (Mongo mongo = Mongo.Create(_connectionString)) {
                return mongo.Database.GetCollection<Dinner>("Dinners").FindOne(new { _id = id });
            }
        }

        //
        // Insert/Delete Methods

        public void Add(Dinner dinner)
        {
            using (Mongo mongo = Mongo.Create(_connectionString)) {
                mongo.GetCollection<Dinner>("Dinners").Insert(dinner);
            }
        }

        public void Delete(Dinner dinner)
        {
            using (Mongo mongo = Mongo.Create(_connectionString)) {
                mongo.GetCollection<Dinner>("Dinners").Delete(dinner);
            }
        }

        //
        // Persistence 

        public void Save()
        {
            //todo
        }


        // Helper Methods

        [EdmFunction("NerdDinnerModel.Store", "DistanceBetween")]
        public static double DistanceBetween(double lat1, double long1, double lat2, double long2)
        {
            throw new NotImplementedException("Only call through LINQ expression");
        }

        public IQueryable<Dinner> NearestDinners(double latitude, double longitude)
        {
            using (Mongo mongo = Mongo.Create(_connectionString)) {
                var x = from d in FindAllDinners()
                       where DistanceBetween(latitude, longitude, d.Latitude, d.Longitude) < 100
                       select d;

                return x;
            }            
        }
    }
}
