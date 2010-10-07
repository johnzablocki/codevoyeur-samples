def posts():
	
	from System import *
	url = "Posts/Index/{year}/{month}/{day}/{title}"
	defaults = { 
		"controller" : "Posts", 
		"action" : "Index", 
		"year" : DateTime.Now.Year.ToString(), 
		"month" : DateTime.Now.Month.ToString(), 
		"day" : DateTime.Now.Day.ToString(), 
		"title" : "" 
		}
	constraints = { "year" : "\d{4}", "month" : "\d{1,2}", "day" : "\d{1,2}" }

	return url, defaults, constraints

def default():		
	
	url = "{controller}/{action}/{id}"
	defaults = { "controller" : "Home", "action" : "Index", "id" : "" }
	constraints = {}

	return url, defaults, constraints
