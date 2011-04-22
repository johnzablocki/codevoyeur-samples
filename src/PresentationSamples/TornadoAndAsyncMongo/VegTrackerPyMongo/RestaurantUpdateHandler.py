import tornado.web
import tornado.ioloop

import pymongo
import pymongo.objectid

from DBFactory import DBFactory

class RestaurantUpdateHandler(tornado.web.RequestHandler):

	def get(self):
	
		id = self.get_argument("id", None)
		
		if id != None:			
			
			spec = { "_id" : pymongo.objectid.ObjectId(id) }
			
			db = DBFactory.create()
			venue = db.venues.find_one(spec)
			self.render("Templates/Restaurants/Edit.html", venue=venue)
		
	def post(self):
	
		id = self.get_argument("_id", None)
		
		doc = {}
		doc["_id"] = pymongo.objectid.ObjectId(id)
		doc["name"] = self.get_argument("name")
		doc["city"] = self.get_argument("city")
		doc["state"] = self.get_argument("state")
		
		db = DBFactory.create()		
		db.venues.update({ "_id" : pymongo.objectid.ObjectId(id) }, doc)
		self.redirect(r"/")
	
