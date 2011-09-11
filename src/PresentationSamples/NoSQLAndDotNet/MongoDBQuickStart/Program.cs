using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using MongoDBQuickStart;
using MongoDB.Driver;
using MongoDB.Bson;
using MongoDB.Driver.Builders;

namespace MongoDBQuickStart {

    class Program {

        private const string COLLECTION = "Artists";
                
        private static MongoDatabase _mongoDatabase = null;

        static Program() {

            //MongoServer manages access to MongoDatabase
            MongoServer mongoServer = MongoServer.Create("mongodb://localhost:27017");
            
            //MongoDatabase used to access MongoCollection instances
            _mongoDatabase = mongoServer.GetDatabase("nyccodecamp");
        }

        public static void Main() {
            
            try {

                doSetup();

                doCRUD();

                doMoreCRUD();

                doMapReduce();

                doQueries();

                doGroup();

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
                Update.PushAll("Albums", "Castaways and Cutouts", "Picaresque", "Hazards of Love", "The Crane Wife")
            );
        }

        private static void doMoreCRUD() {

            //Find all documents in a typed collection
            var artists = _mongoDatabase.GetCollection<Artist>(COLLECTION).FindAll();
            Console.WriteLine("Artist name: " + artists.FirstOrDefault().Name);

            //Query with a document spec
            var artist = _mongoDatabase.GetCollection<Artist>(COLLECTION).FindOne(Query.EQ("Name", "The Decemberists"));
            Console.WriteLine("Album count: " + artist.Albums.Count);

            //Count the documents in a collection
            long count = _mongoDatabase.GetCollection<Artist>(COLLECTION).Count();
            Console.WriteLine("Document count: " + count);

        }

        private static void doMapReduce() {

            //Add some tags
            _mongoDatabase.GetCollection<Artist>(COLLECTION).Update(
                Query.EQ("Name", "The Decemberists"),
                    Update.PushAll("Tags", "Folk rock", "Indie")
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

            var result = _mongoDatabase.GetCollection<Artist>(COLLECTION)
                    .MapReduce(map, reduce, MapReduceOptions.SetKeepTemp(true).SetOutput("Tags"));

            var collection = _mongoDatabase.GetCollection<Tag>(result.CollectionName);
            Console.WriteLine("Tag count: " + collection.Count());

        }

        private static void doQueries() {
            
            var artists = _mongoDatabase.GetCollection<Artist>(COLLECTION);
            
            //Find items in typed collection
            var artistsStartingWithThe = artists.Find(Query.Matches("Name", new Regex("the", RegexOptions.IgnoreCase)));
            Console.WriteLine("First artist starting with The: " + artistsStartingWithThe.First().Name);

            //Find artists without pulling back nested collections
            var artistsWithDecInTheName = artists.Find(Query.Matches("Name", "Dec")).SetFields("Name");
            Console.WriteLine("First artist with dec in name: " + artistsWithDecInTheName.First().Name);

            ////Find artists with a given tag
            var artistsWithIndieTag = artists.Find(Query.In("Tags", "Indie"));
            Console.WriteLine("First artist with indie tag: " + artistsWithIndieTag.First().Name);
        }

        private static void doGroup() {

            //add one more artist for good measure
            var artists = _mongoDatabase.GetCollection<Artist>(COLLECTION);
            artists.Insert(new Artist() { Name = "Blind Pilot", Albums = new List<string>() { "3 Rounds and a Sound" } });

            BsonJavaScript reduce = @"function(obj, out) { out.count += obj.Albums.length; }";
            var groupBy = _mongoDatabase.GetCollection<Artist>(COLLECTION).Group(Query.Null, GroupBy.Keys("Name"), new BsonDocument("count", 0), reduce, null);

            foreach (var item in groupBy) {
                Console.WriteLine("{0}: {1} Album(s)", item.GetValue(0), item.GetValue(1));
            }            
        }

    }

}
