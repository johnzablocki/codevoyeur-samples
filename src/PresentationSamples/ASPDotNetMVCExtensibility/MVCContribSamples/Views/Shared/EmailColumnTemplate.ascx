<%@ Control Language="C#" Inherits="System.Web.Mvc.ViewUserControl<Customer>" %>

<td>
<a href="mailto:<%= Model.Email %>"><%= Model.Email %></a>
</td>
