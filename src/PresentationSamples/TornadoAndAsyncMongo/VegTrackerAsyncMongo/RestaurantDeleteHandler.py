import tornado.web
import tornado.ioloop

import asyncmongo
import pymongo.objectid

from DBFactory import DBFactory

class RestaurantDeleteHandler(tornado.web.RequestHandler):
		
	@tornado.web.asynchronous
	def post(self):
	
		id = self.get_argument("_id", None)
		db = DBFactory.create()
		
		db.venues.remove({ "_id" : pymongo.objectid.ObjectId(id) }, callback=lambda response, error: self.redirect(r"/"))
	