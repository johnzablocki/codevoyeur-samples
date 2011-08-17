import tornado.web
import tornado.ioloop

import pymongo
import pymongo.objectid

from DBFactory import DBFactory

class RestaurantCreateHandler(tornado.web.RequestHandler):

	def get(self):
	
		self.render("Templates/Restaurants/Create.html", venue={ "name" : "", "city" : "", "state" : "" })
	
	def post(self):
		
		doc = {}		
		
		doc["name"] = self.get_argument("name")
		doc["city"] = self.get_argument("city")
		doc["state"] = self.get_argument("state")
		
		db = DBFactory.create()		
		db.venues.insert(doc)			
		self.redirect(r"/")	