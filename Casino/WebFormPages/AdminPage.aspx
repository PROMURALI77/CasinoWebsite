<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AdminPage.aspx.cs" Inherits="WebFormPages_AdminPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link href="../StyleSheets/UsersDataStyle.css" rel="stylesheet" />
    <link href="../StyleSheets/AdminpageStylecss.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="ContentDiv" style="background-color: white; color: black">
        <asp:UpdatePanel runat="server">
            <ContentTemplate>
                <div class="tab">
                    <button class="tablinks" onclick="openTab(event, 'UserTable_Div')">Users Table</button>
                    <button class="tablinks" onclick="openTab(event, 'LogTable_Div')">Log Table</button>
                    <button class="tablinks" onclick="openTab(event, 'SendEmail_Div')">Send Email</button>
                    <button class="tablinks" onclick="openTab(event, 'GetFiles_Div')">Get Files</button>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>

        <div id="UserTable_Div" class="tabcontent">
            <div>
                <asp:Label ID="Label1" runat="server" Text="Users:"></asp:Label>
                <br />
                <asp:GridView ID="UsersData_Tbl" runat="server" OnRowCommand="UsersData_Tbl_RowCommand" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                    <Columns>
                        <asp:ButtonField Text="Update" ButtonType="Button" CommandName="UpdateUser" ControlStyle-Width="100%"/>
                        <asp:ButtonField Text="Delete" ButtonType="Button" CommandName="DeleteUser" ControlStyle-Width="100%"/>
                    </Columns>
                    <FooterStyle BackColor="#CCCCCC" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <SortedAscendingCellStyle BackColor="#F1F1F1" />
                    <SortedAscendingHeaderStyle BackColor="#808080" />
                    <SortedDescendingCellStyle BackColor="#CAC9C9" />
                    <SortedDescendingHeaderStyle BackColor="#383838" />
                </asp:GridView>
            </div>
        </div>

        <div id="LogTable_Div" class="tabcontent">
             <asp:Label ID="Label2" runat="server" Text="Enter log:"></asp:Label>
            <br />
            <asp:GridView ID="Log_Tbl" runat="server" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
                <AlternatingRowStyle BackColor="#CCCCCC"></AlternatingRowStyle>

                <FooterStyle BackColor="#CCCCCC"></FooterStyle>

                <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White"></HeaderStyle>

                <PagerStyle HorizontalAlign="Center" BackColor="#999999" ForeColor="Black"></PagerStyle>

                <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White"></SelectedRowStyle>

                <SortedAscendingCellStyle BackColor="#F1F1F1"></SortedAscendingCellStyle>

                <SortedAscendingHeaderStyle BackColor="#808080"></SortedAscendingHeaderStyle>

                <SortedDescendingCellStyle BackColor="#CAC9C9"></SortedDescendingCellStyle>

                <SortedDescendingHeaderStyle BackColor="#383838"></SortedDescendingHeaderStyle>
            </asp:GridView>
        </div>

        <div id="SendEmail_Div" class="tabcontent">
            <div class="LeftElement">
                <asp:Label ID="Email_Lbl" runat="server" Text="Send Mail to all users:"></asp:Label>
                <asp:TextBox ID="EmailSubject_TB" runat="server" placeholder="Subject"></asp:TextBox>
                <br />
                <textarea id="EmailContent_TA" runat="server" placeholder="content" cols="50" rows="20" style="border: 2px solid blue"></textarea>
                <br />
              <!--  <asp:FileUpload ID="EmailFile_FU" runat="server" />-->
                <br />
                <asp:Button ID="SendEmail_Btn" runat="server" Text="Send" OnClick="SendEmail_Btn_Click" />
            </div>
        </div>

        <div id="GetFiles_Div" class="tabcontent">
            <asp:Button ID="GetUserXMLFile_Btn" runat="server" Text="Get user table in xml" OnClick="GetUserXMLFile_Btn_Click" />
            <br />
            <asp:Button ID="GetLogXmlFile" runat="server" Text="Get log table in xml" OnClick="GetLogXmlFile_Click" />
        </div>

    </div>
    <script>
        function openTab(evt, TabName) {
            // Declare all variables
            var i, tabcontent, tablinks;

            // Get all elements with class="tabcontent" and hide them
            tabcontent = document.getElementsByClassName("tabcontent");
            for (i = 0; i < tabcontent.length; i++) {
                tabcontent[i].style.display = "none";
            }

            // Get all elements with class="tablinks" and remove the class "active"
            tablinks = document.getElementsByClassName("tablinks");
            for (i = 0; i < tablinks.length; i++) {
                tablinks[i].className = tablinks[i].className.replace(" active", "");
            }

            // Show the current tab, and add an "active" class to the button that opened the tab
            document.getElementById(TabName).style.display = "block";
            evt.currentTarget.className += " active";

            document.getElementById('<%= EmailSubject_TB.ClientID %>').value = "";
            document.getElementById('<%= EmailContent_TA.ClientID %>').value = "";
        }
    </script>

</asp:Content>

