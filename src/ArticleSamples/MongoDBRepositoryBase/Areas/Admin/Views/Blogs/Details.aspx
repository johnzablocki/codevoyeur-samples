<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MongoDBRepositoryBase.Models.Blog>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Details
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Details</h2>

    <fieldset>
        <legend>Fields</legend>
        
        <div class="display-label">Name</div>
        <div class="display-field"><%= Html.Encode(Model.Name) %></div>
        
        <div class="display-label">Author</div>
        <div class="display-field"><%= Html.Encode(Model.Author) %></div>
        
    </fieldset>
    <p>
        <%=Html.ActionLink("Edit", "Edit", new {  id=Model.Id  }) %> |
        <%=Html.ActionLink("Back to List", "Index") %>
    </p>

</asp:Content>

