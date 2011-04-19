import clr
clr.AddReference("System")

from System.Text.RegularExpressions import *

def injection_action_executing(context):
	
	tag_pattern = r"<(.|\n)*?>"
	comments = context.HttpContext.Request["Comments"]
	
	matches = Regex.Matches(comments, tag_pattern)	
	
	invalid_tags = []
	for match in matches:
		invalid_tags.append(match.Value)
		
	if len(invalid_tags) > 0:
		context.Controller.TempData["invalid_tags"] = invalid_tags
		context.HttpContext.Response.Redirect("~/Home/InvalidPost")
	