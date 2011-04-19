import tornado.web
import tornado.ioloop

import asyncmongo

class RestaurantsHandler(tornado.web.RequestHandler):

	def get(self):
	
		self.render("templates/Restaurants/Create.html")

	@tornado.web.asynchronous
	def post(self):
		
		doc = {}
		
		doc["name"] = self.get_argument("name")
		doc["city"] = self.get_argument("city")
		doc["state"] = self.get_argument("state")
		
		db = asyncmongo.Client(pool_id="my_db", host="127.0.0.1", port=27017, maxcached=10, maxconnections=50, dbname="VegTracker")
		db.venues.insert(doc, callback=self._post_callback)
		
		self.finish()
		
	def _post_callback(self):
	
		self.write("Created")
		self.finish()