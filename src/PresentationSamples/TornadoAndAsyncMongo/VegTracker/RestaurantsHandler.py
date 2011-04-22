import tornado.web
import tornado.ioloop

import asyncmongo
import pymongo.objectid

from DBFactory import DBFactory

class RestaurantsHandler(tornado.web.RequestHandler):

	@tornado.web.asynchronous
	def get(self):
	
		self.render("Templates/Restaurants/Create.html", venue={ "name" : "", "city" : "", "state" : "" })
	
	@tornado.web.asynchronous
	def post(self):
		
		doc = {}		
		
		doc["name"] = self.get_argument("name")
		doc["city"] = self.get_argument("city")
		doc["state"] = self.get_argument("state")
		
		db = DBFactory.create()		
		db.venues.insert(doc, callback=self._post_callback)			
		
	def _post_callback(self, response, error):
	
		self.write("Created")
		self.redirect(r"/")
		self.finish()