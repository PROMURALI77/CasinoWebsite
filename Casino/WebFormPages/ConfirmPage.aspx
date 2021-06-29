<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ConfirmPage.aspx.cs" Inherits="WebFormPages_ConfirmPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../StyleSheets/ConfirmPageStyle.css" rel="stylesheet" />    
</head>
<body>
    <form id="form1" runat="server">
        <div class="content" >
            <h1 id="welcome_H1" style="display:none" runat="server">welcome</h1>
            <img src="../Images/x%20mark.png" style="display:none"  id="Xmark_Img" runat="server" />
            <img src="../Images/check%20mark.jpg" style="display:none" id="Vmark_Img" runat="server" />
            <br />
            <asp:Label ID="Messege_Lbl" runat="server" Text="" CssClass="Label"></asp:Label>
            <br />
            <a href="HomePage.aspx">Back to homepage</a>
        </div>
    </form>
</body>
</html>
