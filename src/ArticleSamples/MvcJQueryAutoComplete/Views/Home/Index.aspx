<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="MvcJQueryAutoComplete.Views.Home.Index" %>
<%@ Import Namespace="MvcJQueryAutoComplete.Extensions" %>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    
    
    <% using (Html.BeginForm("Save", "Home", FormMethod.Post)) { %>
    
    <div style="font-family:Verdana;color:#006600;font-size:12px;"><%= TempData["Message"] %></div>
    <div style="font-family:Verdana;color:#cc0000;font-size:12px;"><%= TempData["Error"] %></div>
    
    <div>
      
        <%= Html.AutoCompleteTextBox("City", "CityID", new { style = "width:200px;" })%>
        <input type="submit" value="Save" />
        
    </div>
    
    
    <%= Html.InitializeAutoComplete("City", "CityID", Url.Action("Cities"), new { delay = 100, minchars = 3 }, true)%>
    
    
    <% } %>
    
 
</asp:Content>
