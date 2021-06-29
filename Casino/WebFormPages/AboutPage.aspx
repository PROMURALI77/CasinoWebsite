<%@ Page Title="" Language="C#" MasterPageFile="~/SiteMasterPage.master" AutoEventWireup="true" CodeFile="AboutPage.aspx.cs" Inherits="WebFormPages_AboutPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #map {
            height: 300px;
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <div class="ContentDiv">
        <p class="LeftElement" runat="server" id="About_p">
            Welcome to hadad's Casino
            <br />
            Have you had a dream of making a lot of money from gambling on the internet?
            <br />
            Do you want to make some easy cash?
            <br />
            well you came to the wrong place beacause here like every other casino the house always wins...
            <br />
            This casino was founded in september 2017 when hadad needed to make a new project for his last year in highschool
        </p>
        <img src="/Images/FullsizeLogo.jpg" class="RightElement">       
           <div id="map"></div>
        <script>
            function initMap() {
                var uluru = { lat: 31.934426, lng: 34.789731 };
                var map = new google.maps.Map(document.getElementById('map'), {
                    zoom: 16,
                    center: uluru
                });
                var marker = new google.maps.Marker({
                    position: uluru,
                    map: map
                });
            }
        </script>
        <script async defer
            src="https://maps.googleapis.com/maps/api/js?key=AIzaSyAd0L8NNaL0t1B6oc2mwJPHq2hJBql5O2w&callback=initMap">
        </script>
    </div>

</asp:Content>

