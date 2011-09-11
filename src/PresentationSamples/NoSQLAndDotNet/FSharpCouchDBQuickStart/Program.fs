open System
open System.Linq
open LoveSeat
open LoveSeat.Support
open Newtonsoft.Json.Linq

//must have mutable fields for gets
type Artist(id : Guid, name : string, rev : string, albums : List<string>, tags : List<string>, tourStops : List<string>) = class
    
    member x.Name = name 
    member x.Id = id
    member x.Rev = rev 
    member x.TourStops = tourStops 
    member x.Albums = albums 
    member x.Tags = tags

end

let DESIGN_DOC = "artist"
let DATABASE = "vtcodecamp" //database names cannot have uppercase characters

let couchClient = new CouchClient("127.0.0.1", 5984, null, null)
let couchDatabase = couchClient.GetDatabase(DATABASE)
couchDatabase.SetDefaultDesignDoc(DESIGN_DOC)

let doSetup = 
    if couchClient.HasDatabase(DATABASE) then
        couchClient.DeleteDatabase(DATABASE) |> ignore
    couchClient.CreateDatabase(DATABASE) |> ignore

let doCRUD = 

    //Create POCO instance
    let artist = new Artist(Guid.NewGuid(), "The Decembrists", null, [], [], [ "Boston"; "Boston"; "Hartford"; "Burlington" ])

    //Inserting a document into a typed collection - GUID Id will be created prior insert in property, not by driver
    let result = couchDatabase.CreateDocument(new Document<Artist>(artist))

    //Updating (replacing) a document in a typed collection
    //after creating document, document revision id is in result, but POCO not updated
    let updatedArtist = new Artist(artist.Id, "The Decemberists", result.Last.Last.ToString(), artist.Albums, artist.Tags, artist.TourStops)
    let foo = couchDatabase.SaveDocument(new Document<Artist>(updatedArtist))
    DATABASE
    
let main = 
    doSetup
    doCRUD    

main    