<%--<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<List<Customer>>" %>--%>
<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MvcContrib.Pagination.IPagination<Customer>>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Grid
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <%=
        Html.Grid(Model)
            .Columns(column => { 
                column.For(c => c.FirstName);
                column.For(c => c.LastName);
                column.For(c => c.Email).Partial("EmailColumnTemplate").CellCondition(c=> ! string.IsNullOrEmpty(c.Email));
            }).Attributes(style => "width:50%")
     	    .Empty("There are no customers.")
     	    .RowStart(row => string.Format("<tr{0}>", row.IsAlternate ? " style='background-color:#ccc;'" : ""))                           
    %>
    
    <p />
    <%= Html.Pager(Model) %>
</asp:Content>
