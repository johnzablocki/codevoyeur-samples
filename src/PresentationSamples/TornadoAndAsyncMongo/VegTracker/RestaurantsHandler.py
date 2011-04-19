import tornado.web
import tornado.ioloop

import asyncmongo
import pymongo.objectid

class RestaurantsHandler(tornado.web.RequestHandler):

	@tornado.web.asynchronous
	def get(self):
	
		id = self.get_argument("id", None)
		
		if id != None:			
			
			spec = { "_id" : pymongo.objectid.ObjectId(id) }
			
			db = asyncmongo.Client(pool_id="my_db", host="127.0.0.1", port=27017, maxcached=10, maxconnections=50, dbname="VegTracker")
			db.venues.find_one(spec, callback=lambda response, error: self.render("templates/Restaurants/Create.html", venue=response[0]))
		else:		
			self.render("templates/Restaurants/Create.html", venue={ "name" : "Horizons", "city" : "Philadelphia", "state" : "PA", "_id" : "" })
	
	@tornado.web.asynchronous
	def post(self):
		
		doc = {}
		
		id = self.get_argument("_id", None)
		
		if id != None:
			doc["_id"] = id
		
		doc["name"] = self.get_argument("name")
		doc["city"] = self.get_argument("city")
		doc["state"] = self.get_argument("state")
		
		db = asyncmongo.Client(pool_id="my_db", host="127.0.0.1", port=27017, maxcached=10, maxconnections=50, dbname="VegTracker")
		
		if id == None:
			db.venues.insert(doc, callback=self._post_callback)
		else:
			db.venues.update({ "_id" : pymongo.objectid.ObjectId(id) }, doc, callback=self._post_callback)
				
		
	def _post_callback(self, response, error):
	
		self.write("Created")
		self.redirect(r"/")
		self.finish()