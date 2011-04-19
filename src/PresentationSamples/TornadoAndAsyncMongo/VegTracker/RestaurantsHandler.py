import tornado.web
import tornado.ioloop

import asyncmongo

class RestaurantsHandler(tornado.web.RequestHandler):

	@tornado.web.asynchronous
	def get(self):
	
		name = self.get_argument("name", None)
		
		if id != None:			
			db = asyncmongo.Client(pool_id="my_db", host="127.0.0.1", port=27017, maxcached=10, maxconnections=50, dbname="VegTracker")
			db.venues.find_one(name, callback=self._get_callback)
		else:		
			self.render("templates/Restaurants/Create.html", venue={ "name" : "Horizons", "city" : "Philadelphia", "state" : "PA" })

	def _get_callback(self, response, error):
		
		self.render("templates/Restaurants/Create.html", venue=response)		
		
	@tornado.web.asynchronous
	def post(self):
		
		doc = {}
		
		doc["name"] = self.get_argument("name")
		doc["city"] = self.get_argument("city")
		doc["state"] = self.get_argument("state")
		
		db = asyncmongo.Client(pool_id="my_db", host="127.0.0.1", port=27017, maxcached=10, maxconnections=50, dbname="VegTracker")
		db.venues.insert(doc, callback=self._post_callback)
				
		
	def _post_callback(self, response, error):
	
		self.write("Created")
		self.redirect(r"/")
		self.finish()