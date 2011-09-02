open System
open System.Linq
open MongoDB.Driver
open MongoDB.Bson
open MongoDB.Driver.Builders

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

let doSetup  = 
    //drop collection before running samples    
    if mongoCollection.Exists() then
        mongoCollection.Drop()  

let doCRUD = 
    //Insert a document into a typed collection 
    let artist = { Name = "The Decembrists"; Genre = "Folk"; Id = ObjectId.GenerateNewId(); Albums = []; Tags = [] } 
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
    Console.WriteLine("Album count: {0}", artist.Albums.Count())

    //Count the documents in a collection
    let count = mongoDatabase.GetCollection<Artist>(COLLECTION).Count()
    Console.WriteLine("Document count: {0}", count)

let doMapReduce = 

    //Add some tags
    mongoDatabase.GetCollection<Artist>(COLLECTION).Update(
        Query.EQ("Name", BsonValue.Create("The Decemberists")), 
        Update.PushAll("Tags", BsonArray.Create(["Folk rock"; "Indie"]))) |> ignore

    let artist = { Name = "Sunny Day Real Estate"; Genre = "Emo"; Albums = [ "How it Feels to be Something On"; "Diary" ];  Tags = ["Indie"; "Emo" ]; Id = ObjectId.Empty }
    
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

let main = 
    doSetup
    doCRUD
    doMoreCRUD
    doMapReduce

main


    