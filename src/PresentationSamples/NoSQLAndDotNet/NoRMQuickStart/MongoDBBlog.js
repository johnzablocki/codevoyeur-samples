/*
    This script file demonstrates the basics of using MongoDB from the interactive shell.
    It's not intended to be a best-practive example for anything!
*/

//create a document
var post = { Title: "On Installing MongoDB as a Service on Windows" }

//insert a document, if the collection doesn't exist it's created
db.Posts.insert(post);

//verify that the document was created
db.Posts.find();

//write a query to find a post with a valid title
var query = { Title: { $ne: null} }

//use that query to find the post
var post = db.Posts.findOne(query);

//this line will actually set the content after pressing enter
post.Content = "When installing MongoDB as a service on Windows..."

//update the content to include an author using collection update method
db.Posts.update( { Title : "On Installing MongoDB as a Service on Windows" }, { Author : "John Zablocki" } )

//check that the post was updated
db.Posts.findOne()

//where'd my document go?  updates are in place, replacing entire documents!
//need to use the $set operator to update partial documents - let's start over
//first remove the new document.  Notice how remove takes a function as an argument.
//find and findOne also accept functions as arguments
db.Posts.remove(function (e) { return this.Author == "John Zablocki" })

//rerun the first statements up to but not including the db.Posts.update(...
db.Posts.update({ Title: "On Installing MongoDB as a Service on Windows" },
                { $set: { Author: "John Zablocki" } }) 

//verify that the update worked
db.Posts.findOne()

//add a tag to the post
db.Posts.update({ Title: "On Installing MongoDB as a Service on Windows" },
                { $push: { Tags: "mongodb" } })

//look for that tag
db.Posts.findOne()

//add two more tags
db.Posts.update({ Title: "On Installing MongoDB as a Service on Windows" },
                { $pushAll: { Tags: ["windows", "nosql"] } })

//add another post
db.Posts.insert( { Author : "John Zablocki", Title : "On MapReduce in MongoDB",
    Tags: ["mongodb", "nosql"]
})

//verify that last insert worked
db.Posts.findOne(function (e) { return this.Title.indexOf("MapReduce") != -1; })

//add a "like" counter to the post.  The booleans arguments tell update
//not to insert if the document doesn't exist and to update all documents, 
//not just one respectively
db.Posts.update({ Author: "John Zablocki" }, { $set: { Likes: 0} }, false, true)

//increment the likes counter for the mapreduce article
db.Posts.update({ Title: /mapreduce/i }, { $inc: { Likes: 1} })

//check that the counter was incremented
db.Posts.findOne({ Title: /mapreduce/i }).Likes

//use MapReduce to get counts of the tags
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


