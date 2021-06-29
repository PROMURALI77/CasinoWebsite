<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PokerGame.aspx.cs" Inherits="WebFormPages_PokerGame" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hadad's Holed'em</title>
    <link href="../StyleSheets/PokerGameStyleSheet.css" rel="stylesheet" />
</head>
<body style="background-image: url(/Images/PokerTableBackground.jpg);background-size:cover">
    <form id="form1" runat="server">
        <div style="position:absolute;top:0;left:0">
            <asp:ImageButton ID="ExitGame_Btn" runat="server" OnClick="ExitGame_Btn_Click" ImageUrl="~/Images/Xmark.png" Height="20px" Width="20px" />
        </div>
       <div style="margin: auto; width: 50%;text-align:center">        
        <div id="EnterGame_Div" runat="server">
             <br /><br /><br /><br /><br /><br /><br /><br />
            <asp:Button ID="EnterSession_Btn" runat="server" Text="Enter Session" OnClick="EnterSession_Btn_Click" CssClass="bigbutton" />
        </div>
        <div id="StartGame_Div" runat="server">
            <br /><br /><br /><br /><br /><br /><br /><br />
            <asp:Button ID="StartGame_Btn" runat="server" Text="Start Game" OnClick="StartGame_Btn_Click" CssClass="bigbutton" />
            
        </div>
       </div>
        

        <div id="Game_Div"  runat="server">

            <div id="">               
                <br />
                
                    <div runat="server" id="Player2_Div" style="position:absolute; top:0px;right:25%;left:50%;    margin-left:-150px;">    
                    </div>
                
                <br /><br /><br /><br /><br /><br />
               <div style="text-align:center">
                <asp:Label ID="CurrentCall_Lbl" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="TableMoney_Lbl" runat="server" Text=""></asp:Label>
               </div>
                <br />
                <div runat="server" id="GameCards_Div" style="margin: auto; width: 50%;">
                    
                </div> 

                <div style="position:absolute;bottom:0">
                    <div runat="server" id="Continue_Div" >
                    <asp:Button ID="StartGame2_Div" runat="server" Text="Continue Playing" OnClick="StartGame_Btn_Click" CssClass="bigbutton" />
                    <br />
                     <asp:Label ID="Winner_Lbl" runat="server" Text=""></asp:Label>
                </div>
                <div runat="server" id="Buttons_Div" >
                    <asp:Label ID="Result_Lbl" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button ID="Fold_Btn" runat="server" Text="Fold" OnClick="Fold_Btn_Click" CssClass="button" />
                <br />
                <asp:Button ID="Call_Btn" runat="server" Text="Call" OnClick="Call_Btn_Click" CssClass="button" />
                <br />
                <asp:TextBox ID="RaiseValue_TB" runat="server" placeholder="Enter Value" CssClass="button"></asp:TextBox>                
                <asp:Button ID="Raise_Btn" runat="server" Text="Raise" OnClick="Raise_Btn_Click" CssClass="button" />
                <br />
                <asp:RegularExpressionValidator ID="RaiseValue_Val" runat="server" ErrorMessage="Invalid number" ControlToValidate="RaiseValue_TB" EnableClientScript="False" ValidationExpression="[0-9]{1,9}"></asp:RegularExpressionValidator>
                <asp:RequiredFieldValidator ID="RaiseValue_RFV" runat="server" ErrorMessage="EnterValue" ControlToValidate="RaiseValue_TB" EnableClientScript="false"></asp:RequiredFieldValidator>
                <asp:Label ID="Raise_Lbl" runat="server" Text=""></asp:Label>
                <br />
                <asp:Button ID="Check_Btn" runat="server" Text="Check" OnClick="Check_Btn_Click" CssClass="button" />
                <br />                
                <br />
                <asp:Label ID="Error_Lbl" runat="server" Text=""></asp:Label>
                <br />
                <asp:Label ID="PlayerMoney_Lbl" runat="server" Text=""></asp:Label>
                <br />   
                </div>
                <!--<img src="../Images/Cards/14c.png" width="125" height="181" />-->
            </div>

                </div>
                
            <div runat="server" id="Player1_Div" style="  position:absolute;    
    bottom:0px;
    right:25%;
    left:50%;
    margin-left:-150px;">
                </div>
        </div>  
        <div style="position:absolute;bottom:0;right:0;overflow:auto;background-color:white;height:200px">
            <p runat="server" id="Log_p" ></p>

        </div>
         
    </form>
</body>
</html>
