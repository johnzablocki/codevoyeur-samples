<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<MongoDBRepositoryBase.Models.Post>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Edit
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <h2>Edit</h2>

    <% using (Html.BeginForm()) {%>

        <fieldset>
            <legend>Fields</legend>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Title) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Title) %>
                <%= Html.ValidationMessageFor(model => model.Title) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.Content) %>
            </div>
            <div class="editor-field">
                <%= Html.TextBoxFor(model => model.Content) %>
                <%= Html.ValidationMessageFor(model => model.Content) %>
            </div>
            
            <div class="editor-label">
                <%= Html.LabelFor(model => model.BlogId) %>
            </div>
            <div class="editor-field">
                <%= Html.DropDownListFor(model => model.BlogId, ViewData["Blogs"] as IEnumerable<SelectListItem>, new { id = "BlogId" })%>
            </div>
            
            <p>
                <input type="submit" value="Save" />
            </p>
        </fieldset>

    <% } %>

    <div>
        <%=Html.ActionLink("Back to List", "Index") %>
    </div>

</asp:Content>

