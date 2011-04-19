import tornado.web
import tornado.ioloop

from RestaurantsHandler import RestaurantsHandler

class MainHandler(tornado.web.RequestHandler):

	def get(self):	
		self.render("templates/Main/Index.html")
		
application = tornado.web.Application([
	(r"/", MainHandler),
	(r"/restaurants/", RestaurantsHandler)
], debug=True)

if __name__ == "__main__":
	application.listen(8888)
	tornado.ioloop.IOLoop.instance().start()