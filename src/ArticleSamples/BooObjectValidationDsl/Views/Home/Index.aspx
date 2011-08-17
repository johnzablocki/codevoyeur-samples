<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="BooObjectValidationDsl.Views.Home.Index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Code Voyeur - Boo Validation DSL</title>
    <style type="text/css">
        body { font-family:verdana; }        
        .header { font-weight:bold; padding:10px 0 10px 0; }        
        .form { background-color: #c0c0c0; border: 2px solid #000; padding: 20px;}
        .form form div { padding:2px; }
        .error { color:Red; font-size:12px; }
        .message { color:Green; font-size:12px; }
    </style>
</head>
<body>
    <div style="margin:5% 20% 0 20%;">
    <div style="float:left;" class="form">
        <div class="header">
            Login
            <div class="error">
                <% 
                    if (TempData["Errors"] != null) {
                    foreach (string message in TempData["Errors"] as IList<string>) { %>
                        <div><%= message %></div>
                   <% }
                    } %>
            </div>
            <div class="message">
                <%= TempData["Message"] %>
            </div>
        </div>
        <% using (Html.BeginForm("ValidateForm", "Home", System.Web.Mvc.FormMethod.Post)) { %>
            
            <div>
                Username: <% = Html.TextBox("username") %>
            </div>
            <div>
                Password: <% = Html.Password("password") %>
            </div>
            <div>
                <input type="submit" value="Validate Form" />
                <input type="button" value="Validate Business Objects" onclick="javascript:document.forms[0].action='Home/ValidateBusinessObj';document.forms[0].submit();" />
            </div>
            
        <% } %>
    </div>    
    </div>
</body>
</html>
