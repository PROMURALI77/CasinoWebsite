<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="UsersData.aspx.cs" Inherits="WebFormPages_UsersData" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../StyleSheets/UsersDataStyle.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="ContentDiv" style="background-color: white; color: black" runat="server" id="Content_Div">
        <div class="LeftElement" runat="server" id="LinkButton_Div">
            <asp:ImageButton ID="ProfilePicture_Img" runat="server" onclick="ProfilePicture_Img_Click"  />
            <br />
            <asp:LinkButton ID="UserName_LB" runat="server" CssClass="LabelButton" OnClick="DisplayWanted">UserName</asp:LinkButton>
            <br />
            <asp:LinkButton ID="Password_LB" runat="server" CssClass="LabelButton" OnClick="DisplayWanted">Password</asp:LinkButton>
            <br />
            <asp:LinkButton ID="FirstName_LB" runat="server" CssClass="LabelButton" OnClick="DisplayWanted">FirstName</asp:LinkButton>
            <asp:LinkButton ID="LastName_LB" runat="server" CssClass="LabelButton" OnClick="DisplayWanted">LastName</asp:LinkButton>
            <br />
            <asp:LinkButton ID="Email_LB" runat="server" CssClass="LabelButton" OnClick="DisplayWanted">Email</asp:LinkButton>
            <br />
            <asp:Label ID="IsConfirmed_Lbl" runat="server" Text=""></asp:Label>
            <br />
            <a href="AdminPage.aspx" id="Admin_a" runat="server" style="display:none">To Admin Page</a>
            <br />
            <asp:Button ID="BuyCash_Btn" runat="server" Text="Buy cash" Style="bottom:0;margin-bottom:0px;width:30%;background-color: #4CAF50;" OnClick="BuyCash_Btn_Click" />
            <br />
            <asp:Button ID="SignOut_Btn" runat="server" Text="Sign out" Style="bottom:0;margin-bottom:0px;width:30%" OnClick="SignOut_Btn_Click" />
            <br /> 
            <br />
        </div>          
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div class="RightElement" runat="server">
                    <div runat="server" id="UserName_Div">
                        <asp:TextBox ID="UserName_TB" runat="server" placeholder="User name" CssClass="TextBox"></asp:TextBox>
                        <br />
                        <asp:RegularExpressionValidator ID="UserName_Val" runat="server" ErrorMessage="UserName Between 3 and 20 charachters" ControlToValidate="UserName_TB" EnableClientScript="False" ValidationExpression="[a-z0-9A-Z]{3,20}" ValidationGroup="UserName" CssClass="validator"></asp:RegularExpressionValidator>
                        <asp:RequiredFieldValidator ID="UserName_RFV" runat="server" ErrorMessage="Missing Username" ControlToValidate="UserName_TB" EnableClientScript="False" ValidationGroup="UserName" CssClass="validator"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="UserName_Btn" runat="server" Text="Update" OnClick="UpdateValue" autopostback="false" />
                    </div>

                    <div runat="server" id="Password_Div">
                        <asp:TextBox ID="Password_TB" runat="server" placeholder="Password" type="password"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="Password_RFV" runat="server" ErrorMessage="Missing Password" ControlToValidate="Password_TB" EnableClientScript="False" ValidationGroup="Password" CssClass="validator"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="Password_Val" runat="server" ErrorMessage="UserName Between 3 and 15 charachters" ControlToValidate="Password_TB" EnableClientScript="False" ValidationExpression="[a-zA-Z0-9]{4,15}" ValidationGroup="Password" CssClass="validator"></asp:RegularExpressionValidator>
                        <br />
                        <asp:Button ID="Password_Btn" runat="server" Text="Update" OnClick="UpdateValue" />
                    </div>

                    <div runat="server" id="FirstName_Div">
                        <asp:TextBox ID="FirstName_TB" runat="server" placeholder="FirstName"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="FirstName_RFV" runat="server" ErrorMessage="Missing First Name" ControlToValidate="FirstName_TB" EnableClientScript="False" ValidationGroup="FirstName" CssClass="validator"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="FirstName_Btn" runat="server" Text="Update" OnClick="UpdateValue" />
                    </div>

                    <div runat="server" id="LastName_Div">
                        <asp:TextBox ID="LastName_TB" runat="server" placeholder="LastName"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="LastName_RFV" runat="server" ErrorMessage="Missing Last Name" ControlToValidate="LastName_TB" EnableClientScript="False" ValidationGroup="LastName" CssClass="validator"></asp:RequiredFieldValidator>
                        <br />
                        <asp:Button ID="LastName_Btn" runat="server" Text="Update" OnClick="UpdateValue" />
                    </div>

                    <div runat="server" id="Email_Div">
                        <asp:TextBox ID="Email_TB" runat="server" placeholder="Email"></asp:TextBox>
                        <br />
                        <asp:RequiredFieldValidator ID="Email_RFV" runat="server" ErrorMessage="Missing Email" ControlToValidate="Email_TB" EnableClientScript="False" ValidationGroup="Email" CssClass="validator"></asp:RequiredFieldValidator>
                        <asp:RegularExpressionValidator ID="Email_Val" runat="server" ErrorMessage="Mail not valid" ControlToValidate="Email_TB" EnableClientScript="False" ValidationExpression="^[a-zA-Z0-9_.]+@[a-zA-Z0-9]+\.[a-zA-Z]+$" ValidationGroup="Email" CssClass="validator"></asp:RegularExpressionValidator>
                        <asp:Label ID="Email_Lbl" runat="server" Text=""></asp:Label>
                        <br />
                        <asp:Button ID="Email_Btn" runat="server" Text="Update" OnClick="UpdateValue" />
                    </div>

                </div>
            </ContentTemplate>
            
        </asp:UpdatePanel>
                
    </div>
</asp:Content>

