<%@ Page Language="C#" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import namespace="MvcJWFlvPlayerExtensions.Extensions" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title></title>
    <script type='text/javascript' src='<%= ResolveClientUrl("~/Content/Scripts/swfobject.js") %>'></script>
    <style type="text/css">
        
        .header { font-family:Arial;font-size:14px;color:000;font-weight:bold; }
        
    </style>
</head>
<body>    
    <div>
    <div class="header">SWF ObjectMethod</div>
    <p id='preview'>The player will show in this paragraph</p>   
    <%= Html.FlashPlayer(ResolveClientUrl("~/Content/Flash/Player.swf"), "preview", 400, 300, "9.0.0",
                                 new { file = "video.flv" }, new { allowscriptaccess = "always", allowfullscreen="true" })%>
    <div>
    </div>
    
    <div style="margin-top:20px;">
    <div class="header">Embed Method</div>
    
    <%= Html.FlashPlayerEmbed(ResolveClientUrl("~/Content/Flash/Player.swf"), "", 400, 300,
                            new { flashvars = "file=video.flv&autostart=false", allowscriptaccess = "always", allowfullscreen = "true" })%>        
                            
    </div>
    </div>       
                                           
</body>
</html>
