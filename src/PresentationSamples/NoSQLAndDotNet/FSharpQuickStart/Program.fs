open System
open System.Linq
open MongoDB.Driver
open MongoDB.Bson
open MongoDB.Driver.Builders
open System.Collections.Generic
open System.Text.RegularExpressions

let COLLECTION = "Artists"

//must have mutable fields for gets
type Artist = { mutable Name : string; mutable Genre : string; mutable Id : ObjectId; mutable Albums : List<string>; mutable Tags : List<string> }

type Tag = { mutable Id : string; mutable Value : int }   

//MongoServer manages access to MongoDatabase
let mongoServer = MongoServer.Create("mongodb://localhost:27017")

//MongoDatabase used to access MongoCollection instances
let mongoDatabase = mongoServer.GetDatabase("FSUG");
 
//reference the collection
let mongoCollection = mongoDatabase.GetCollection<Artist>(COLLECTION)

let mutableListAddRange items = 
    let mutableList = new List<string>()
    mutableList.AddRange(items)
    mutableList

let doSetup  = 
    //drop collection before running samples    
    if mongoCollection.Exists() then
        mongoCollection.Drop()  

let doCRUD = 
    //Insert a document into a typed collection     
    let artist = { Name = "The Decembrists"; Genre = "Folk"; Id = ObjectId.GenerateNewId(); Albums = new List<string>(); Tags = new List<string>()} 
    mongoCollection.Insert(artist) |> ignore

    //Updating (replacing) a document in a typed collection
    let updatedArtist = { artist with Name = "The Decemberists" }
    mongoCollection.Save(updatedArtist) |> ignore

    //Updating a nested collection
    mongoDatabase.GetCollection<Artist>(COLLECTION).Update(
        Query.EQ("Name", BsonValue.Create("The Decemberists")), 
        Update.PushAll("Albums", BsonArray.Create(["Picaresque"; "Hazards of Love"; "The Crane Wife"]))) |> ignore

let doMoreCRUD = 
    
    //Find all documents in a typed collection
    let artists = mongoDatabase.GetCollection<Artist>(COLLECTION).FindAll()
    Console.WriteLine("Artist name: " + artists.FirstOrDefault().Name)

    //Query with a document spec
    let artist = mongoDatabase.GetCollection<Artist>(COLLECTION).FindOne(Query.EQ("Name", BsonValue.Create("The Decemberists")))
    Console.WriteLine("Album count: {0}", artist.Albums.Count)

    //Count the documents in a collection
    let count = mongoDatabase.GetCollection<Artist>(COLLECTION).Count()
    Console.WriteLine("Document count: {0}", count)

let doMapReduce = 

    //Add some tags
    mongoDatabase.GetCollection<Artist>(COLLECTION).Update(
        Query.EQ("Name", BsonValue.Create("The Decemberists")), 
        Update.PushAll("Tags", BsonArray.Create(["Folk rock"; "Indie"]))) |> ignore

 
    let artist = { Name = "Foo Fighters"; 
                   Genre = "Hard rock"; 
                   Albums =  mutableListAddRange(["The Colour and the Shape"; "Wasted Light"]);  
                   Tags = mutableListAddRange(["Hard Rock"; "Grunge" ]); Id = ObjectId.Empty }
    
    mongoDatabase.GetCollection<Artist>(COLLECTION).Save(artist) |> ignore

    //Create map and reduce functons
    let map = @"function() {
                    if (!this.Tags ) { return; }
                    for (index in this.Tags) { emit(this.Tags[index], 1); }
                    }";

    let reduce = @"function(previous, current) {
                        var count = 0;
                        for (index in current) { count += current[index]; }
                        return count;
                    }";
    
    let result = mongoDatabase.GetCollection<Artist>(COLLECTION).MapReduce(BsonJavaScript.Create(map), 
                 BsonJavaScript.Create(reduce), MapReduceOptions.SetKeepTemp(true).SetOutput(MapReduceOutput.op_Implicit("Tags"))) 

    let collection = mongoDatabase.GetCollection<Tag>(result.CollectionName);
    Console.WriteLine("Tag count: {0}", collection.Count()) 

let doQueries =             
    
    let artists = mongoDatabase.GetCollection<Artist>(COLLECTION)
            
    //Find items in typed collection
    let artistsStartingWithFoo = artists.Find(Query.Matches("Name", BsonRegularExpression.Create(new Regex("foo", RegexOptions.IgnoreCase))))
    Console.WriteLine("First artist starting with Foo: {0}", artistsStartingWithFoo.First().Name);

    //Find artists without pulling back nested collections
    let artistsWithDecInTheName = artists.Find(Query.Matches("Name", BsonRegularExpression.Create("Dec"))).SetFields("Name");
    Console.WriteLine("First artist with dec in name: {0}", artistsWithDecInTheName.First().Name);

    ////Find artists with a given tag
    let artistsWithIndieTag = artists.Find(Query.In("Tags", BsonArray.Create(["Indie"])))
    Console.WriteLine("First artist with indie tag: " + artistsWithIndieTag.First().Name);

let doGroup =

    //add one more artist for good measure
    let artists = mongoDatabase.GetCollection<Artist>(COLLECTION)
    let artist = { Name = "The Fratellis"; Genre = "Rock"; Id = ObjectId.GenerateNewId(); Albums = mutableListAddRange(["Costello Music"]); Tags = new List<string>() } 
    artists.Insert(artist) |> ignore

    let reduce = BsonJavaScript.Create("function(obj, out) { out.count += obj.Albums.length; }")
    let groupBy = mongoDatabase.GetCollection<Artist>(COLLECTION).Group(Query.Null, GroupBy.Keys("Name"), new BsonDocument("count", BsonInt32.Create(0)), reduce, null)

    for item in groupBy do
        Console.WriteLine("{0}: {1} Album(s)", item.GetValue(0), item.GetValue(1));

let main = 
    doSetup
    doCRUD
    doMoreCRUD
    doMapReduce
    doQueries

main


    