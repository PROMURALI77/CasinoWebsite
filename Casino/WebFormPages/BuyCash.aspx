<%@ Page Language="C#" AutoEventWireup="true" CodeFile="BuyCash.aspx.cs" Inherits="WebFormPages_BuyCash" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../StyleSheets/RegistrationStyle.css" rel="stylesheet" />    
</head>
<body style="text-align: center;background-image: url(/Images/FullsizeLogo.jpg);background-size:cover;background-repeat: no-repeat;">
    <form id="form1" runat="server">
       <div class="Container" style="opacity:0.95">          
           <asp:TextBox ID="Amount_TB" runat="server" Width="51%" placeholder="Amount"></asp:TextBox>
           <br />
           <br />
           <asp:TextBox ID="CreditCardOwner_TB" runat="server" Width="51%" placeholder="Credit card owner"></asp:TextBox>
           <br />
           <br />
           <asp:TextBox ID="CreditCardNumber_TB" runat="server" Width="51%" placeholder="Credit card number"></asp:TextBox>           
           <br />
           <br />
           <asp:TextBox ID="Ccv_TB" runat="server" Width="20%" placeholder="Ccv"></asp:TextBox>
           <asp:DropDownList ID="Month_DDL" runat="server" Width="15%" CssClass="ddl"></asp:DropDownList>
           <asp:DropDownList ID="Year_DDL" runat="server" Width="15%" CssClass="ddl"></asp:DropDownList>
           <br />
           <asp:RequiredFieldValidator ID="Amount_RFV" runat="server" ErrorMessage="Missing amount" ControlToValidate="Amount_TB" EnableClientScript="False" ></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="Amount_Val" runat="server" ErrorMessage="Invalid amount" ControlToValidate="Amount_TB" EnableClientScript="False" ValidationExpression="[0-9]{1,}" ></asp:RegularExpressionValidator>
           <br />
           <asp:RequiredFieldValidator ID="CreditCardOwner_RFV" runat="server" ErrorMessage="Missing Credit Card owner" ControlToValidate="CreditCardOwner_TB" EnableClientScript="False"></asp:RequiredFieldValidator>
           <br />
           <asp:RequiredFieldValidator ID="CreditCardNumber_RFV" runat="server" ErrorMessage="Missing Credit Card number" ControlToValidate="CreditCardNumber_TB" EnableClientScript="False" ></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="CreditCard_Val" runat="server" ErrorMessage="Invalid Credit Card number" ControlToValidate="CreditCardNumber_TB" EnableClientScript="False" ValidationExpression="[0-9]{16,16}" ></asp:RegularExpressionValidator>
           <br />
           <asp:RequiredFieldValidator ID="Ccv_RFV" runat="server" ErrorMessage="Missing Ccv" ControlToValidate="Ccv_TB" EnableClientScript="False"></asp:RequiredFieldValidator>
           <asp:RegularExpressionValidator ID="Ccv_Val" runat="server" ErrorMessage="Invlid Ccv" ControlToValidate="Ccv_TB" EnableClientScript="False" ValidationExpression="[0-9]{3,3}"></asp:RegularExpressionValidator>
           <br />
           <br />
           <asp:Button ID="Buy_Btn" runat="server" Text="Buy" CssClass="Button" OnClick="Buy_Btn_Click" />
           <br />
           <a href="UsersData.aspx">Back to user data</a>
       </div>
        
        
    </form>
</body>
</html>
