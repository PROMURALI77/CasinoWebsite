<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdminUpdate.aspx.cs" Inherits="WebFormPages_AdminUpdate" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <link href="../StyleSheets/UsersDataStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="ContentDiv" style="background-color:white">
        <div class="LeftElement">
            <div runat="server" id="Update_Div">
                <asp:Label ID="UpdateUserName_Lbl" runat="server" placeholder="UserName" style="color:black"></asp:Label>
                <br />
                <asp:TextBox ID="UpdateValue_TB" runat="server" placeholder="Value"></asp:TextBox>
                <br />
                <asp:DropDownList ID="UpdateValueName_DDL" runat="server" placeholder="ValueName"></asp:DropDownList>  
                <br />
                <asp:Button ID="Update_Btn" runat="server" Text="Update" OnClick="Update_Btn_Click" />
                <br />
                <asp:Label ID="Update_Lbl" runat="server" Text=""></asp:Label>
            </div>
        </div>
    </div>
</asp:Content>

