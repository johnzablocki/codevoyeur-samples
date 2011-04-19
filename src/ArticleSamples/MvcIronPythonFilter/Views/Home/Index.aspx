<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="indexTitle" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
  
  <% if (TempData["message"] != null) { %>
    <div style="font-weight:bold;color:#006600;margin:10px;font-size:14px;"><%=TempData["message"] %></div>
  <% } %>
  
  <% if (TempData["error"] != null) { %>
    <div style="font-weight:bold;color:#cc0000;margin:10px;font-size:14px;"><%=TempData["error"] %></div>
  <% } %>
  
  <div style="margin:10px;">
  <%using (Html.BeginForm("save", "home")) { %>
  
  <div style="font-weight:bold;margin-bottom:10px;">Leave a message!</div>
  <div>Name:</div> 
  <div style="margin-bottom:10px;"><%= Html.TextBox("Name") %></div>
  <div>Comments: (only b, strong, em and i tags are allowed) </div>
  <div><%= Html.TextArea("Comments") %></div>
  <div><input type="submit" value="Submit" /></div>
  
  <%} %>
  </div>
  
</asp:Content>
