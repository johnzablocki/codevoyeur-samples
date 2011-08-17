<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="SimpleGeoWebForms.Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        
        <div>Latitude:<asp:TextBox ID="txtLatitude" runat="server" text="41.773693"/></div>
        <div>Longitude:<asp:TextBox ID="txtLongitude" runat="server" text="-72.672483"/></div>
        <div>Search:<asp:TextBox ID="txtSearch" runat="server" text="Restaurant"/></div>
        <asp:Button ID="btnSubmit" runat="server" text="Find" OnClick="bntSumbit_Click" />
        
        <asp:Label ID="lblMessage" ForeColor="Red" runat="server"/>
        
        <div style="margin:20px;">
        <asp:Repeater ID="rptPlaces" runat="server" Visible="false">
            <ItemTemplate>
                <div>
                    <%# Eval("Properties.Name") %>
                    <span style="display:block;margin-left:10px;font-style:italic;margin-bottom:10px;"><%# Eval("Properties.City") %>, <%# Eval("Properties.Province") %> <%# Eval("Properties.PostCode") %></span>
                </div>
            </ItemTemplate>
        </asp:Repeater>
        </div>
    </div>
    </form>
</body>
</html>
