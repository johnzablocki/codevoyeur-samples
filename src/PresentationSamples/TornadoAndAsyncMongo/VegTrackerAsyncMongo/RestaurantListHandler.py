import tornado.web
import tornado.ioloop

import asyncmongo

from RestaurantCreateHandler import RestaurantCreateHandler
from RestaurantUpdateHandler import RestaurantUpdateHandler
from RestaurantDeleteHandler import RestaurantDeleteHandler
from DBFactory import DBFactory

class RestaurantListHandler(tornado.web.RequestHandler):

	@tornado.web.asynchronous
	def get(self):
						
		db = DBFactory.create()
		db.venues.find(limit=10, callback=self._get_callback)					
	
	def _get_callback(self, response, error):
			self.render("Templates/Restaurants/List.html", venues=response)
	
application = tornado.web.Application([
	(r"/", RestaurantListHandler),
	(r"/restaurants/create/", RestaurantCreateHandler),
	(r"/restaurants/edit/", RestaurantUpdateHandler),
	(r"/restaurants/delete/", RestaurantDeleteHandler),
], debug=True)

if __name__ == "__main__":
	application.listen(8888)
	tornado.ioloop.IOLoop.instance().start()