import tornado.web
import tornado.ioloop

import pymongo

from RestaurantCreateHandler import RestaurantCreateHandler
from RestaurantUpdateHandler import RestaurantUpdateHandler
from RestaurantDeleteHandler import RestaurantDeleteHandler
from DBFactory import DBFactory

class RestaurantListHandler(tornado.web.RequestHandler):

	def get(self):
						
		db = DBFactory.create()
		venues = db.venues.find()					
		self.render("Templates/Restaurants/List.html", venues=venues)
	
application = tornado.web.Application([
	(r"/", RestaurantListHandler),
	(r"/restaurants/create/", RestaurantCreateHandler),
	(r"/restaurants/edit/", RestaurantUpdateHandler),
	(r"/restaurants/delete/", RestaurantDeleteHandler),
], debug=True)

if __name__ == "__main__":
	application.listen(8888)
	tornado.ioloop.IOLoop.instance().start()