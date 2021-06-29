<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="RegistraionPage.aspx.cs" Inherits="WebFormPages_RegistraionPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link href="../StyleSheets/RegistrationStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
        <div class="Container">
        <asp:TextBox ID="UserName_TB" runat="server" placeholder="Username" CssClass="TextBox"></asp:TextBox>
        <br />
        <asp:RegularExpressionValidator ID="UserName_Val" runat="server" ErrorMessage="UserName Between 3 and 20 charachters" ControlToValidate="UserName_TB" EnableClientScript="False"  ValidationExpression="[a-z0-9A-Z]{3,20}"  ValidationGroup="ValidationGroup" ></asp:RegularExpressionValidator>
        <asp:RequiredFieldValidator ID="UserName_RFV"  runat="server" ErrorMessage="Missing Username" ControlToValidate="UserName_TB" EnableClientScript="False" ValidationGroup="ValidationGroup"></asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="Password_TB" runat="server" placeholder="Password" type="password"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="Password_RFV" runat="server" ErrorMessage="Missing Password" ControlToValidate="Password_TB" EnableClientScript="False" ValidationGroup="ValidationGroup" ></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="Password_Val" runat="server" ErrorMessage="Password Between 3 and 15 charachters" ControlToValidate="Password_TB" EnableClientScript="False" ValidationExpression="[a-zA-Z0-9]{3,15}"  ValidationGroup="ValidationGroup" ></asp:RegularExpressionValidator>
        <br />
        <asp:TextBox ID="FirstName_TB" runat="server" placeholder="FirstName" ></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="FirstName_RFV" runat="server" ErrorMessage="Missing First Name" ControlToValidate="FirstName_TB" EnableClientScript="False" ValidationGroup="ValidationGroup" ></asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="LastName_TB" runat="server" placeholder="LastName"></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="LastName_RFV" runat="server" ErrorMessage="Missing Last Name" ControlToValidate="LastName_TB" EnableClientScript="False" ValidationGroup="ValidationGroup" ></asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="Email_TB" runat="server" placeholder="Email" ></asp:TextBox>
        <br />
        <asp:RequiredFieldValidator ID="Email_RFV" runat="server" ErrorMessage="Missing Email" ControlToValidate="Email_TB" EnableClientScript="False" ValidationGroup="ValidationGroup" ></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="Email_Val" runat="server" ErrorMessage="Mail not valid" ControlToValidate="Email_TB" EnableClientScript="False" ValidationExpression="^[a-zA-Z0-9_.]+@[a-zA-Z0-9]+\.[a-zA-Z]+$"  ValidationGroup="ValidationGroup"></asp:RegularExpressionValidator>
        <br /> 
        <asp:Button ID="Register_Btn" runat="server" Text="Register" OnClick="Register_Btn_Click" CssClass="Button" />
        <br />
        <asp:Label ID="Messege_Lbl" runat="server" Text=""></asp:Label>
     </div>
    
</asp:Content>

