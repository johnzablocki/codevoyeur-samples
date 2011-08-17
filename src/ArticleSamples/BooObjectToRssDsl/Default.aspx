<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="BooObjectToRssDsl._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Code Voyeur RSS from a Boo DSL Feeds</title>
    <link rel="alternate" type="application/rss+xml" title="Product Listings" href="RSS/Products.ashx" />
    <link rel="alternate" type="application/rss+xml" title="Product Reviews" href="RSS/ProductReviews.ashx" />

</head>
<body>
    <form id="form1" runat="server">
    <div>
        <a href="RSS/Products.ashx" style="display:block;">Product Listings</a>
        <a href="RSS/ProductReviews.ashx">Product Reviews</a>
    </div>
    </form>
</body>
</html>
