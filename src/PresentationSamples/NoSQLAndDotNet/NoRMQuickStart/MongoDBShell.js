//JSon document
var post = {
    Author : { Name : "John Zablocki", _id : "497ce96f395f2f052a494fd4" },
    Title : "On Running NerdDinner on MongoDB with NoRM - Part 1",
    Tags : ["mvc", "mongodb", "norm"],    
    CreatedDate : new Date('6-14-2010'),
    Text : "As part of a presentation I’ll be giving at the Hartford Code Camp..."
}


/*Connect to a server:port/database 
(defaults are localhost:27017/test ):*/
mongo.exe localhost:27017/AltNetGroup

//Switch database:
use CodeCamp

//View collections in a database:
show collections

//create an index on Name field
db.Artists.ensureIndex({ Name : 1 });

//copy one database to another
db.copyDatabase("CodeCamp", "AltNetGroup")


//Insert an item into a collection
db.Artists.insert({ Name : “The Shins” });

//Find an item in a collection:
db.Artists.findOne({ Name: “Radiohead”});

//Find items in a collection:
db.Artists.find({ Name : /The/i});

//Count items in a collection
db.Artists.count();


//Update an item in a collection
db.Artists.update({ Name : “JackJohnson” }, $set : { Name : “Jack Johnson” } });

//Update items in a collection
db.Artists.update({ Name : { $ne : null } }, { $set : { Category : “Rock” } }, false, true);

//$ denotes special operators and operations
//$push, $pop, $pull, etc.


//Connections managed with IDisposable pattern
using (Mongo mongo = Mongo.Create(“connstring”)) {
  //CRUD goes here
}
/*Mongo instance has MongoDatabase property 
MongoDatabase has GetCollection<T> methods for accessing MongoCollection
CRUD operations performed on collections*/


//MapReduce

//Create some data
db.Posts.insert({ Name : "On Installing MongoDB as a Service On Windows", Tags : ["mongodb"] });
db.Posts.insert({ Name : "On Running NerdDinner on MongoDB with NoRM", Tags : ["mongodb", "norm", "mvc"] });
db.Posts.insert({ Name : "On A Simple IronPython Route Mapper for ASP.NET MVC", Tags : ["mvc", "ironpthon"] });

//create the map and reduce functions
 var map = function() {
                if (!this.Tags) { return; } 
                for (var index in this.Tags) { 
                    emit(this.Tags[index], 1); 
                }
            };

//conceptually, reduce gets called like:
//reduce("mvc", [1, 1]);
//reduce("norm", [1]
var reduce = function(key, vals) {
                var count = 0;
                for (var index in vals) { 
                    count += vals[index]; 
                }
                return count;
             };

/*
run the mapreduce command on the Posts collection
using the map and reduce functions defined above
store the results in a collection named Tags
*/
var result = db.runCommand(
    {
        mapreduce : "Posts",
        map : map,
        reduce : reduce,
        out : "Tags"
   });

db.Tags.find()