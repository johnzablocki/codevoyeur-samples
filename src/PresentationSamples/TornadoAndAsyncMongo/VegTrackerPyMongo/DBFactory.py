import pymongo

class DBFactory(object):

	@staticmethod
	def create():
	
		return pymongo.Connection("localhost", 27017).VegTracker
