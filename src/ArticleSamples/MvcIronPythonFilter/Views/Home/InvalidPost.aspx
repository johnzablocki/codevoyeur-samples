<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	InvalidPost
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>InvalidPost</h2>
    
    <%
        string tags = "";
        foreach (string tag in (TempData["invalid_tags"] ?? new string[] {""}) as IEnumerable) {
            tags += Html.Encode(tag);   
        }    
    %>
    <p style="color:#cc0000;font-size:14px;">You have attempted to use HTML characters that are not allowed!<p />
    <p>You included: <strong><%= tags %></strong></p>
    <p>Only b, strong, i and em are allowed.</p>
    <div><a href="javascript:history.go(-1);">Click here</a> to try again.</div>
    
</asp:Content>
