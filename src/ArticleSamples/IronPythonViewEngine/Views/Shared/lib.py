from System.Text import *

def table(headers, rows, props):

	sb = StringBuilder()
	sb.Append("<table>")
	
	for header in headers:
		sb.AppendFormat("<th>{0}</th>", header)
	
	for row in rows:
		sb.Append("<tr>")		
		for prop in props:
			sb.AppendFormat("<td>{0}</td>", eval("row." + prop))
		sb.Append("</tr>")

	sb.Append("</table>")
	return sb.ToString()