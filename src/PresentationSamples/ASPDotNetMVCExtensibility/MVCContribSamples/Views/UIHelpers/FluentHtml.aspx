<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="ModelViewPage<MVCContribSamples.Models.Customer>" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	FluentHtml
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <% Html.BeginForm(); %>
    <div><%= this.TextBox(x => x.FirstName).Label("First Name: ").Title("first name is required") %></div>
    <div><%= this.TextBox(x => x.LastName).Label("Last Name: ").Title("last name is required") %></div>
    <div><%= this.SubmitButton("Save") %></div>
    <% Html.EndForm(); %>
    
</asp:Content>
