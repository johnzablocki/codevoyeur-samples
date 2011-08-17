<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>About</title>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.4/jquery.min.js"></script>
</head>
<body>
    <div>
        This is the about page
        
        <form action="<%= ResolveUrl("~/Home/API") %>" method="post">
            <input type="button" id="postButton" value="post" />
        </form>
        
        
        <script language"javascript" type="text/javascript">
            $().ready(function() {
                $("#postButton").bind("click", function(e) {                                        
                    $.post("<%=ResolveUrl("~/Home/API") %>", function(data) {
                        alert(data.FirstName + ' ' + data.LastName);
                    });
                });
            });
        </script>
        
    </div>
</body>
</html>
