<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Games.aspx.cs" Inherits="WebFormPages_Games" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <div runat="server" id="Error_Div" class="ContentDiv">
        <h1>You need to be signed in to play games</h1>
    </div>
    <div runat="server" id="Games_Div" class="ContentDiv">        
            <a href="PokerGame.aspx"><img src="../Images/Hadad'sHold'emlogo.jpg" class="LeftElement" /></a>    
    </div>
</asp:Content>

