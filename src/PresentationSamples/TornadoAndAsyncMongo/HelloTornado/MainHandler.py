import tornado.web
import tornado.ioloop

#Handlers must inherit from tornado.web.RequestHandler
class MainHandler(tornado.web.RequestHandler):

	#post and get methods handle the respective HTTP verbs
	def get(self):
		self.write("Hello, Tornado World!")

#create	an instance of the Application object
#map the route to the root ("/") to the MainHandler
application = tornado.web.Application([
		(r"/", MainHandler)
])

#listen on port 8888 and start the server
if __name__ == "__main__":

	application.listen(8888)
	tornado.ioloop.IOLoop.instance().start()