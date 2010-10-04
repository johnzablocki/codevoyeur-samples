<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<dynamic>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Index
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2><%: (RouteData.Values["Title"] as string).Replace("-", " ") %></h2>

    <div>
        Posted on <%: new DateTime(int.Parse(RouteData.Values["Year"] as string), 
                                   int.Parse(RouteData.Values["Month"] as string), 
                                   int.Parse(RouteData.Values["Day"] as string)).ToString("MMMM dd, yyyy") %>
    </div>

    <div style="margin-top:20px;">
        Some content would go here if there were content...
    </div>

    <p />
    <a href="javascript:history.go(-1);">Back</a>


</asp:Content>
