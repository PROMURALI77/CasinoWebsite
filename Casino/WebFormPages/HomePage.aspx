<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="HomePage.aspx.cs" Inherits="WebFormPages_HomePage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server"> 
    <div class="ContentDiv" style="font-family:Miriam">
       <div class="LeftElement" >
           <h2>Welcome to hadad's casino, the most profitable casino... for hadad
           </h2>
           <h2>
               And remember,the house always wins....
           </h2>
           <img src="../Images/Hadad'sHold'emlogo.jpg" style="height:100%;width:100%" />           
           <div id="OurCashGraph" style="height:350px"></div>          
       </div>
        <div class="RightElement">
            <img src="../Images/FullsizeLogo.jpg" style="height:100%;width:100%" />
            <h1><a href="RegistraionPage.aspx" style="color:unset;text-decoration:none;background-color: #f44336;">Register for FREE!!!!!!!!</a></h1>
            
            <div id="UserCashGraph" style="height:350px"></div>
        </div>
    </div>


     <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] })
        google.charts.setOnLoadCallback(OurCashGraph);
        google.charts.setOnLoadCallback(UserCashGraph);

        <% var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>
         function OurCashGraph() {

            var dates =<%=serializer.Serialize(Dates())%>;
            var values =<%=serializer.Serialize(OurEarnings())%>;
            var arr = new Array();
            for (var i = 0; i < dates.length; i++) {
                arr[i] = [dates[i], parseInt(values[i])];
            }
            var data = google.visualization.arrayToDataTable(arr, true);

            var options = {
                title: 'Our future earnings',
                //curveType: 'function',
                legend: { position: 'bottom' }
            };

            var chart =  new google.visualization.LineChart(document.getElementById('OurCashGraph'));

            chart.draw(data, options);

        }
        function UserCashGraph() {

            var dates =<%=serializer.Serialize(Dates())%>;
            var values =<%=serializer.Serialize(UserEarnings())%>;
            var arr = new Array();
            for (var i = 0; i < dates.length; i++) {
                arr[i] = [dates[i], parseInt(values[i])];
            }
            var data = google.visualization.arrayToDataTable(arr, true);

            var options = {
                title: 'Your future earnings',
                //curveType: 'function',
                legend: { position: 'bottom' }
            };

            var chart =  new google.visualization.LineChart(document.getElementById('UserCashGraph'));

            chart.draw(data, options);

        }
    </script>
</asp:Content>

