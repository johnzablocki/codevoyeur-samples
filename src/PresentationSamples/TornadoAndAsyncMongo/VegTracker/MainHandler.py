import tornado.web
import tornado.ioloop

import asyncmongo

from RestaurantsHandler import RestaurantsHandler
from RestaurantUpdateHandler import RestaurantUpdateHandler
from RestaurantDeleteHandler import RestaurantDeleteHandler

class MainHandler(tornado.web.RequestHandler):

	@tornado.web.asynchronous
	def get(self):
						
		db = asyncmongo.Client(pool_id="my_db", host="127.0.0.1", port=27017, maxcached=10, maxconnections=50, dbname="VegTracker")
		db.venues.find(limit=10, callback=self._get_callback)					
	
	def _get_callback(self, response, error):
			self.render("Templates/Main/Index.html", venues=response)
	
application = tornado.web.Application([
	(r"/", MainHandler),
	(r"/restaurants/create/", RestaurantsHandler),
	(r"/restaurants/edit/", RestaurantUpdateHandler),
	(r"/restaurants/delete/", RestaurantDeleteHandler),
], debug=True)

if __name__ == "__main__":
	application.listen(8888)
	tornado.ioloop.IOLoop.instance().start()