<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="Spread.Web._Default" %>

<html>
<body>
Hellow World!

<%= Resources.Common.Shared.My1_Text %>
<asp:Literal runat="server" Text="<%$ Resources:Common.Shared,My %>"></asp:Literal>
<%--<asp:Label runat="server" meta:ResourceKey="My1"></asp:Label>--%>
</body>
</html>
