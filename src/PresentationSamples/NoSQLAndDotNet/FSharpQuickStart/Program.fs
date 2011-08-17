open MongoDB.Driver
open MongoDB.Bson

type Artist = { Name : string }

//MongoServer manages access to MongoDatabase
let mongoServer = MongoServer.Create("mongodb://localhost:27017")

//MongoDatabase used to access MongoCollection instances
let mongoDatabase = mongoServer.GetDatabase("FSUG");
 
//reference the collection
let mongoCollection = mongoDatabase.GetCollection<Artist>("Artists")

//drop collection before running samples
if mongoCollection.Exists() then
    mongoCollection.Drop()  

//do setup
let artist = Artist("The Decembrists")
mongoCollection.Insert(artist) |> ignore

let updatedArtist = { artist with Name = "The Decemberists" }











    