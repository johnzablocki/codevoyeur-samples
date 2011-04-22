import asyncmongo

class DBFactory(object):

	@staticmethod
	def create():
	
		return asyncmongo.Client(pool_id="my_db", host="127.0.0.1", port=27017, dbname="VegTracker")
