using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using NoRMQuickStart;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MongoDBQuickStart {

    class Program {

        private const string COLLECTION = "Artists";
        private static MongoDatabase _mongoDatabase = null;

        static Program() {

            MongoConnectionSettings settings = new MongoConnectionSettings();
            settings.Address = new MongoServerAddress("localhost", 27017);
            MongoServer mongoServer = new MongoServer(settings);
            _mongoDatabase = mongoServer.GetDatabase("AltNet");
        }

        public static void Main() {

            try {

                doSetup();

                doCRUD();

                doMoreCRUD();

                doMapReduce();

                //doLINQ();

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }



        private static void doSetup() {

            if (_mongoDatabase.CollectionExists(COLLECTION)) {
                _mongoDatabase.DropCollection(COLLECTION);
            }

        }

        private static void doCRUD() {

            var artist = new Artist() { Name = "The Decembrists" };

            //Inserting a document into a typed collection
            _mongoDatabase.GetCollection<Artist>(COLLECTION).Insert(artist);

            //Updating (replacing) a document in a typed collection
            artist.Name = "The Decemberists";
            _mongoDatabase.GetCollection<Artist>(COLLECTION).Save(artist);

            //Updating a nested collection
            _mongoDatabase.GetCollection<Artist>(COLLECTION).Update(
                Query.EQ("Name", "The Decemberists"),
                new BsonDocument("$pushAll",
                    new BsonDocument("Albums", new BsonArray() { "Castaways and Cutouts", "Picaresque", "Hazards of Love", "The Crane Wife" }))
            );

        }

        private static void doMoreCRUD() {

            //Find all documents in a typed collection
            var artists = _mongoDatabase.GetCollection<Artist>(COLLECTION).FindAll();
            Console.WriteLine("Artist name: " + artists.FirstOrDefault().Name);

            //Query with a document spec
            var artist = _mongoDatabase.GetCollection<Artist>(COLLECTION).FindOne(new BsonDocument { { "Name", "The Decemberists" } });
            Console.WriteLine("Album count: " + artist.Albums.Count);

            //Count the documents in a collection
            long count = _mongoDatabase.GetCollection<Artist>(COLLECTION).Count();
            Console.WriteLine("Document count: " + count);

        }

        private static void doMapReduce() {

            //Add some tags
            _mongoDatabase.GetCollection<Artist>(COLLECTION).Update(
                Query.EQ("Name", "The Decemberists"),
                    new BsonDocument("$pushAll",
                        new BsonDocument("Tags", new BsonArray() { "Folk rock", "Indie" }))
             );

            var artist = new Artist() {
                Name = "Sunny Day Real Estate",
                Albums = new List<string>() { "How it Feels to be Something On", "Diary" },
                Tags = new List<string>() { "Indie", "Emo" }
            };
            _mongoDatabase.GetCollection<Artist>(COLLECTION).Save(artist);

            //Create map and reduce functons
            BsonJavaScript map = @"function() {
                            if (!this.Tags ) { return; }
                            for (index in this.Tags) { emit(this.Tags[index], 1); }
                            }";

            BsonJavaScript reduce = @"function(previous, current) {
                                var count = 0;
                                for (index in current) { count += current[index]; }
                                return count;
                            }";

            //MapReduce class is responsible for calling mapreduce command                                   
            var result = _mongoDatabase.GetCollection<Artist>(COLLECTION).MapReduce(map, reduce, MapReduceOptions.SetKeepTemp(true).SetOutput("Tags"));
            
            var collection = _mongoDatabase.GetCollection<Tag>(result.ResultCollectionName);
            Console.WriteLine("Tag count: " + collection.Count());

            //}
        }

        //        private static void doLINQ() {

        //            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

        //                //LINQ provider exposed via AsQueryable method of MongoCollection
        //                var artists = mongo.Database.GetCollection<Artist>("Artists").AsQueryable();

        //                //Find items in typed collection
        //                var artistsStartingWithThe = from a in mongo.Database.GetCollection<Artist>("Artists").AsQueryable()
        //                                             where a.Name.StartsWith("The")
        //                                             select a;
        //                Console.WriteLine("First artist starting with The: " + artistsStartingWithThe.First().Name);

        //                //Find artists without pulling back nested collections
        //                var artistsWithDecInTheName =
        //                    from a in mongo.Database.GetCollection<Artist>("Artists").AsQueryable()
        //                    where Regex.IsMatch(a.Name, "dec", RegexOptions.IgnoreCase)
        //                    select new { Name = a.Name };
        //                Console.WriteLine("First artist with dec in name: " + artistsWithDecInTheName.First().Name);

        //                //Find artists with a given tag
        //                var artistsWithIndieTag = mongo.Database.GetCollection<Artist>("Artists")
        //                                .AsQueryable().Where(a => a.Tags.Any(s => s == "Indie"));
        //                Console.WriteLine("First artist with indie tag: " + artistsWithIndieTag.First().Name);
        //            }
        //        }

        //        private static void doSequence() {

        //            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

        //            }

        //        }
    }

}
