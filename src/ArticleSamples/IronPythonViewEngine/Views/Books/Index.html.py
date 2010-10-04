from System import *
from System.Text import *

def books(context):

	headers = ["Title", "Author", "ISBN"]
	props = ["Title", "Author.FullName", "ISBN"]
	return table(headers, context.ViewData.Model, props)
