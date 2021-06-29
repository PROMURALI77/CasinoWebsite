<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AddUserPicture.aspx.cs" Inherits="WebFormPages_AddUserPicture" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
     <div class="ContentDiv" style="background-color: white; color: black" runat="server" id="Content_Div">
        <div class="LeftElement" runat="server" id="LinkButton_Div">           
                    <div runat="server" id="ProfilePicture_Div" >
                         <asp:FileUpload ID="ProfilePicture_FU" runat="server" />  
                        <br />
                        <asp:Button ID="ProfilePicture_Btn" runat="server" Text="Update" accept="image/jpg" OnClick="ProfilePicture_Btn_Click" />
                    </div>
            </div>
         </div>
</asp:Content>

