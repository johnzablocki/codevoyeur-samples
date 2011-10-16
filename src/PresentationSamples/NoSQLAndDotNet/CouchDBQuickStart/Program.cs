using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using LoveSeat;
using LoveSeat.Support;
using Newtonsoft.Json.Linq;

namespace CouchDBQuickStart {

    class Program {

        private const string COLLECTION = "artists";
        private const string DESIGN_DOC = "artist";
        private const string DATABASE = "phillycodecamp"; //database names cannot have uppercase characters

        private static CouchClient _couchClient = null;
        private static CouchDatabase _couchDatabase = null;

        static Program() {

            //create the reusable client and database, set the default design doc to "artist"
            _couchClient = new CouchClient("127.0.0.1", 5984, null, null);
            _couchDatabase = _couchClient.GetDatabase(DATABASE);
            _couchDatabase.SetDefaultDesignDoc(DESIGN_DOC);
                        
        }

        public static void Main() {

            try {

                doSetup();

                doDesignDoc();

                doCRUD();

                doMoreCRUD();

                doMapReduce();

                doQueries();

                doGroup();

                doValidation();

                doListAndShow();

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
        }

        private static void doSetup() {

            //drop and recreate database for demo purposes only (start clean each run)
            if (_couchClient.HasDatabase(DATABASE)) {
                _couchClient.DeleteDatabase(DATABASE);
            }
            
            _couchClient.CreateDatabase(DATABASE);
        }
       
        private static void doDesignDoc() {

            //Create map and reduce functons for tag counts
            var tagMap = @"function(doc) { if (!doc.tags ) { return; } for (index in doc.tags) { emit(doc.tags[index], 1); } }";            
            var tagReduce = @"function(keys, values) { return sum(values); }";
            
            //create map and reduce functions for group by artist and tour stop"
            var tourStopMap = @"function(doc) { for(i in doc.tourStops) { emit([doc.tourStops[i], doc.name], 1) } }";
            var tourStopReduce = @"function(keys, values) { return sum(values) }";

            //basic design document
            var design = string.Format(
                            @"{{ ""_id"": ""_design/artist"", 
                            ""validate_doc_update"" : ""function(newDoc, oldDoc, userCtx) {{ if (!newDoc.name) {{ throw({{ forbidden : 'Name is required'}}); }} }}"",
                            ""shows"" :
                            {{
                                ""csv"" : ""function(doc, req) {{ return doc._id + ',' + doc.name }}""
                            }},
                            ""views"": 
                             {{
                                ""all"" : {{
                                    ""map"" : ""function(doc) {{ emit(null, doc) }}""
                                }},
                                ""by_name"" : {{
                                    ""map"" : ""function(doc) {{ emit(doc.name, doc) }}"" 
                                }},
                                ""by_name_starts_with"" : {{ 
                                    ""map"" : ""function(doc) {{ var match = doc.name.match(/^.{{0,3}}/i)[0]; if (match) {{ emit(match, doc) }} }}""
                                }},
                               ""by_tag"" : {{ 
                                    ""map"" : ""function(doc) {{ for(i in doc.tags) {{ emit(doc.tags[i], doc) }} }}""
                               }},
                               ""by_tag_count"" : {{
                                    ""map"" : ""{0}"", ""reduce"" : ""{1}""
                                }},
                               ""by_tour_stop"" : {{
                                    ""map"" : ""{2}"", ""reduce"" : ""{3}""
                                }}
                             }},
                             ""lists"" : 
                             {{
                                ""all_csv"" : ""function(head, row, req, row_info) {{ while(row = getRow()) {{ send(row.value._id + ',' + row.value.name + '\\r\\n'); }} }}""
                             }}
                           }}", tagMap, tagReduce, tourStopMap, tourStopReduce);

            var request = new CouchRequest("http://127.0.0.1:5984/" + DATABASE + "/_design/" + DESIGN_DOC);
            var response = request.Put().Form().ContentType("multipart/form-data").Data(JObject.Parse(design)).GetResponse();
        }

        private static void doCRUD() {

            //Create POCO instance
            var artist = new Artist() { Name = "The Decembrists", TourStops = { "Boston", "Boston", "Hartford", "Burlington" } };

            //Inserting a document into a typed collection - GUID Id will be created prior insert in property, not by driver
            var result = _couchDatabase.CreateDocument(new Document<Artist>(artist));

            //Updating (replacing) a document in a typed collection
            artist.Rev = result["rev"].ToString(); //after creating document, document revision id is in result, but POCO not updated
            artist.Name = "The Decemberists";
            result = _couchDatabase.SaveDocument(new Document<Artist>(artist));

            //Updating a nested collection
            artist.Rev = result.Rev;
            artist.Albums = new List<string>() { "Castaways and Cutouts", "Picaresque", "Hazards of Love", "The Crane Wife" };
            result = _couchDatabase.SaveDocument(new Document<Artist>(artist));
        }

        private static void doMoreCRUD() {

            //Find all documents in a typed view
            var artists = _couchDatabase.View<Artist>("all");
            Console.WriteLine("Artist name: " + artists.Items.FirstOrDefault().Name);

            //Find a single document by name
            var options = new ViewOptions() { Key = new KeyOptions("The Decemberists") };
            var artist = _couchDatabase.View<Artist>("by_name", options).Items.First();            
            Console.WriteLine("Album count: " + artist.Albums.Count);

            //Count the documents in a view
            long count = _couchDatabase.View<Artist>("all").Items.Count();
            Console.WriteLine("Document count: " + count);

        }

        private static void doMapReduce() {

            //Add some tags
            var artist = _couchDatabase.View<Artist>("all").Items.First();
            artist.Tags = new List<string> { "Folk rock", "Indie" };
            _couchDatabase.SaveDocument(new Document<Artist>(artist));

            //add a new artist with some tags
            var newArtist = new Artist() {
                Name = "Sunny Day Real Estate",
                Albums =  { "How it Feels to be Something On", "Diary" },
                Tags = { "Indie", "Emo" },
                TourStops = { "Boston", "Philadelphia", "Philadelphia", "Philadelphia", "New York", "New York", "Hartford" }
            };
            _couchDatabase.SaveDocument(new Document<Artist>(newArtist));           
            
            var options = new ViewOptions() { Key = new KeyOptions("Indie"), Group = true };
            var tagCounts = _couchDatabase.View("by_tag_count", options);
            Console.WriteLine("Indie tag count: " + tagCounts.Rows.First()["value"]);

        }

        private static void doQueries() {
            
            //Find items in typed collection
            var options = new ViewOptions() { Key = new KeyOptions("The") };
            var artistsStartingWithThe = _couchDatabase.View<Artist>("by_name_starts_with", options);
            Console.WriteLine("First artist starting with The: " + artistsStartingWithThe.Items.First().Name);
            
            //Find artists with a given tag
            options = new ViewOptions() { Key = new KeyOptions("Indie") };
            var artistsWithIndieTag = _couchDatabase.View<Artist>("by_tag", options);
            foreach (var artist in artistsWithIndieTag.Items) {
                Console.WriteLine("Found artist with indie tag: " + artist.Name);                       
            }
        }

        private static void doGroup() {

            //add one more artist for good measure
            _couchDatabase.CreateDocument(new Document<Artist>(
                    new Artist() { Name = "Blind Pilot", Albums = { "3 Rounds and a Sound" }, TourStops = { "Philadelphia", "Providence", "Boston" } }));

            var tourStopGroupBy = _couchDatabase.View("by_tour_stop", new ViewOptions() { Group = true });

            Func<JToken, string> stripQuotes = (j) => j.ToString().Replace("\"", "");
            foreach (var row in tourStopGroupBy.Rows) {                
                Console.WriteLine("{0} played {1} {2} time(s)", stripQuotes(row["key"][1]), stripQuotes(row["key"][0]), row["value"]);            
            }
        }

        private static void doValidation() {
            var result = _couchDatabase.CreateDocument(new Document<Artist>(new Artist()));
            Console.WriteLine("Error message: {0}", result["reason"]);
        }

        private static void doListAndShow() {
            
            var artist = _couchDatabase.View<Artist>("all").Items.First();
            var csv = _couchDatabase.Show("csv", artist.Id.ToString());
            Console.WriteLine("Show: {0}", csv);

            var csvList = _couchDatabase.List("all_csv", "by_tag", new ViewOptions() { Key = new KeyOptions("Indie") });
            Console.WriteLine("List: {0}", csvList.RawString.Split(Environment.NewLine.ToCharArray(), StringSplitOptions.RemoveEmptyEntries).First());
        }

    }

}
