import tornado.web
import tornado.ioloop

import pymongo
import pymongo.objectid

from DBFactory import DBFactory

class RestaurantDeleteHandler(tornado.web.RequestHandler):
		
	def post(self):
	
		id = self.get_argument("_id", None)
		db = DBFactory.create()
		
		db.venues.remove({ "_id" : pymongo.objectid.ObjectId(id) })
		self.redirect(r"/")
	