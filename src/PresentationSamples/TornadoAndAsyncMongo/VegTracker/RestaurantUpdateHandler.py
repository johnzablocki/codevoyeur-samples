import tornado.web
import tornado.ioloop

import asyncmongo
import pymongo.objectid

from DBFactory import DBFactory

class RestaurantUpdateHandler(tornado.web.RequestHandler):

	@tornado.web.asynchronous
	def get(self):
	
		id = self.get_argument("id", None)
		
		if id != None:			
			
			spec = { "_id" : pymongo.objectid.ObjectId(id) }
			
			db = DBFactory.create()
			db.venues.find_one(spec, callback=lambda response, error: self.render("templates/Restaurants/Edit.html", venue=response))			
		
	@tornado.web.asynchronous
	def post(self):
	
		id = self.get_argument("_id", None)
		
		doc = {}
		doc["_id"] = id
		doc["name"] = self.get_argument("name")
		doc["city"] = self.get_argument("city")
		doc["state"] = self.get_argument("state")
		
		db = DBFactory.create()		
		db.venues.update({ "_id" : pymongo.objectid.ObjectId(id) }, doc, callback=lambda response, error: self.redirect(r"/"))
	