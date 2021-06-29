<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="Statistics.aspx.cs" Inherits="WebFormPages_Statistics" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="text/javascript" src="https://www.gstatic.com/charts/loader.js"></script>
    <script type="text/javascript">
        google.charts.load('current', { 'packages': ['corechart'] });
        google.charts.setOnLoadCallback(GeneralHandCountF);
        google.charts.setOnLoadCallback(UserHandCountF);
        google.charts.setOnLoadCallback(GeneralWinRateF);
        google.charts.setOnLoadCallback(UserWinRateF);
        google.charts.setOnLoadCallback(UserCashGraph);
        


        <% var serializer = new System.Web.Script.Serialization.JavaScriptSerializer(); %>

        function GeneralHandCountF() {
            var hands =<%=serializer.Serialize(hands)%>;
            var handMonim =<%=serializer.Serialize(GeneralHandCount())%>;
            var arr = new Array();
            for (var i = 0; i < hands.length; i++) {
                arr[i] = [hands[i], parseInt(handMonim[i])];
            }
            var data = google.visualization.arrayToDataTable(arr, true);
            var options = {
                title: 'General Hands count'
            };
            var chart = new google.visualization.PieChart(document.getElementById('GeneralHandCount'));
            chart.draw(data, options);
        }

        function UserHandCountF() {
            var hands =<%=serializer.Serialize(hands)%>;
            var handMonim =<%=serializer.Serialize(UserHandCount())%>;
            var arr = new Array();
            for (var i = 0; i < hands.length; i++) {
                arr[i] = [hands[i], parseInt(handMonim[i])];
            }
            var data = google.visualization.arrayToDataTable(arr, true);
            var data = google.visualization.arrayToDataTable(arr, true);

            var options = {
                title: 'Your Hands count'
            };

            var chart = new google.visualization.PieChart(document.getElementById('UserHandCount'));

            chart.draw(data, options);
        }

        function GeneralWinRateF() {

            var winLose =<%=serializer.Serialize(winLose)%>;
            var winRateArr =<%=serializer.Serialize(GeneralWinRate())%>;
            var arr = new Array();
            for (var i = 0; i < winLose.length; i++) {
                arr[i] = [winLose[i], parseInt(winRateArr[i])];
            }

            var data = google.visualization.arrayToDataTable(arr, true);

            var options = {
                title: 'Non ai players win rate'
            };

            var chart = new google.visualization.PieChart(document.getElementById('GeneralWinRate'));

            chart.draw(data, options);
        }
        function UserWinRateF() {

            var winLose =<%=serializer.Serialize(winLose)%>;
            var winRateArr =<%=serializer.Serialize(UserWinRate())%>;
            var arr = new Array();
            for (var i = 0; i < winLose.length; i++) {
                arr[i] = [winLose[i], parseInt(winRateArr[i])];
            }

            var data = google.visualization.arrayToDataTable(arr, true);

            var options = {
                title: 'Your win rate'
            };

            var chart = new google.visualization.PieChart(document.getElementById('UserWinRate'));

            chart.draw(data, options);
        }

        function UserCashGraph() {

            var dates =<%=serializer.Serialize(CashLogDates())%>;
            var values =<%=serializer.Serialize(CashLogValues())%>;
            var arr = new Array();
            for (var i = 0; i < dates.length; i++) {
                arr[i] = [dates[i], parseInt(values[i])];
            }
            var data = google.visualization.arrayToDataTable(arr, true);

            var options = {
                title: 'Your Cash',
                curveType: 'function',
                legend: { position: 'bottom' }
            };

            var chart =  new google.visualization.LineChart(document.getElementById('UserCashGraph'));

            chart.draw(data, options);

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="ContentDiv" style="background-color: white">
        <div runat="server" id="UserCashGraph_Div">
            <div id="UserCashGraph" style="height:350px"></div>

        </div>
        
        <br />
        <div class="LeftElement">
            <div id="GeneralHandCount" style="width: 450px; height: 350px;"></div>
            <div id="GeneralWinRate" style="width: 450px; height: 350px;"></div>
        </div>
        <div class="RightElement">
            <div id="UserHandCount" style="width: 450px; height: 350px;"></div>
            <div id="UserWinRate" style="width: 450px; height: 350px;"></div>
        </div>
    </div>


</asp:Content>

