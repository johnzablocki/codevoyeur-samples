<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
    Home Page
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Posts on IronPython </h2>
    <p>
        <%= Html.RouteLink("On Using PyMongo with IronPython", new { controller = "posts", action = "index", year = 2010, month = 5, day = 27, title = "On-Using-PyMongo-with-IronPython" })%><p/>
        <%= Html.RouteLink("On a Simple IronPython ActionFilter for ASP.NET", new { controller = "posts", action = "index", year = 2009, month = 9, day = 10, title = "On-a-Simple-IronPython-ActionFilter-for-ASP.NET" })%><p />
        <%= Html.RouteLink("On a Simple IronPython Validation Framework", new { controller = "posts", action = "index", year = 2008, month = 4, day = 8, title = "On-a-Simple-IronPython-Validation-Framework" })%>
    </p>
</asp:Content>
