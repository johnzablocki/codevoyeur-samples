import clr
clr.AddReference("System")

from System import *

def trace_action_executing(context):

	__write_trace(context, "Action Executign")
	
def trace_action_executed(context):

	__write_trace(context, "Action Executed")
	
	
def trace_result_executing(context):

	__write_trace(context, "Result Executing")
	
	
def trace_result_executed(context):

	__write_trace(context, "Result Executed")
		
	
def __write_trace(context, event):

	if context.HttpContext.Request.QueryString["debug"] == "true":
		context.HttpContext.Response.Write("Tracing: %s %s at %s<br />" % (event, context.RouteData.Values["action"], DateTime.Now))