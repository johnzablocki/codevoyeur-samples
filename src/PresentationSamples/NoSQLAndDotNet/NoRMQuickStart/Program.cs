using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Norm;
using Norm.Responses;
using System.Text.RegularExpressions;

namespace NoRMQuickStart {

    class Program {

        private const string CONN_STRING = "mongodb://localhost/AltDotNet";

        public static void Main() {

            try {

                doSetup();

                doCRUD();

                doMoreCRUD();

                doMapReduce();

                doLINQ();

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private static void doSetup() {
            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

                if (mongo.Database.GetCollection("Artists").Count() != 0) {
                    mongo.Database.DropCollection("Artists");
                }
            }
        }

        private static void doCRUD() {
            //connect to test database on localhost
            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

                var artist = new Artist() { Name = "The Decembrists" };

                //Inserting a document into a typed collection
                mongo.Database.GetCollection<Artist>("Artists").Save(artist);

                //Updating (replacing) a document in a typed collection
                artist.Name = "The Decemberists";
                mongo.Database.GetCollection<Artist>("Artists").Save(artist);

                //Updating a nested collection
                mongo.Database.GetCollection<Artist>("Artists").UpdateOne(
                    new { Name = "The Decemberists" },
                    new { Albums = M.PushAll("Castaways and Cutouts", "5 Songs EP") }
                    );

            }
        }

        private static void doMoreCRUD() {
            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

                //Find all documents in a typed collection
                var artists = mongo.GetCollection<Artist>("Artists").Find();
                Console.WriteLine("Artist name: " + artists.FirstOrDefault().Name);

                //Query with a document spec
                var artist = mongo.GetCollection<Artist>("Artists").FindOne(new { Name = "The Decemberists" });
                Console.WriteLine("Album count: " + artist.Albums.Count);

                //Count the documents in a collection
                long count = mongo.GetCollection<Artist>("Artists").Count();
                Console.WriteLine("Document count: " + count);
            }
        }

        private static void doMapReduce() {
            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

                //Add some tags
                mongo.Database.GetCollection<Artist>("Artists").UpdateOne(
                    new { Name = "The Decemberists" },
                    new { Tags = M.Set(new List<string>() { "Folk rock", "Indie" }) });


                //Create map and reduce functons
                string map = @"function() {
                            if (!this.Tags ) { return; }
                            for (index in this.Tags) { emit(this.Tags[index], 1); }
                          }";

                string reduce = @"function(previous, current) {
                                var count = 0;
                                for (index in current) { count += current[index]; }
                                return count;
                            }";


                //MapReduce class is responsible for calling mapreduce command
                MapReduce mr = mongo.Database.CreateMapReduce();

                //Represents the document passed to the db.runCommand in the shell example
                MapReduceOptions options = new MapReduceOptions("Artists") {
                    Map = map, Reduce = reduce, Permanant = false, OutputCollectionName = "Tags"
                };

                MapReduceResponse response = mr.Execute(options);
                var collection = mongo.Database.GetCollection<Tag>("Tags");
                Console.WriteLine("Tag count: " + collection.Count());

            }
        }

        private static void doLINQ() {

            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

                //LINQ provider exposed via AsQueryable method of MongoCollection
                var artists = mongo.Database.GetCollection<Artist>("Artists").AsQueryable();

                //Find items in typed collection
                var artistsStartingWithThe = from a in mongo.Database.GetCollection<Artist>("Artists").AsQueryable()
                                             where a.Name.StartsWith("The")
                                             select a;
                Console.WriteLine("First artist starting with The: " + artistsStartingWithThe.First().Name);

                //Find artists without pulling back nested collections
                var artistsWithDecInTheName =
                    from a in mongo.Database.GetCollection<Artist>("Artists").AsQueryable()
                    where Regex.IsMatch(a.Name, "dec", RegexOptions.IgnoreCase)
                    select new { Name = a.Name };
                Console.WriteLine("First artist with dec in name: " + artistsWithDecInTheName.First().Name);

                //Find artists with a given tag
                var artistsWithIndieTag = mongo.Database.GetCollection<Artist>("Artists")
                                .AsQueryable().Where(a => a.Tags.Any(s => s == "Indie"));
                Console.WriteLine("First artist with indie tag: " + artistsWithIndieTag.First().Name);
            }
        }

        private static void doSequence() {

            using (IMongo mongo = Mongo.Create(CONN_STRING)) {

            }

        }
    }
}
