<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Overview.aspx.cs"
    Inherits="Wc3o.Pages.Game.Overview_aspx" %>
<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    
    <script src="Scripts/Buildings.js" type="text/javascript"></script>
    <script src="Scripts/Constructing.js" type="text/javascript"></script>
    <script src="Scripts/Overview.js" type="text/javascript"></script>
    <script src="Scripts/Popup.js" type="text/javascript"></script>
    <script src="Scripts/Refresh.js" type="text/javascript"></script>
    <script src="Scripts/Training.js" type="text/javascript"></script>
    <script src="Scripts/Units.js" type="text/javascript"></script>

<h1>
    Your kingdom</h1>
    <asp:Label ID="lblOverview" Runat="server" EnableViewState="False"></asp:Label><br />
</asp:Content>
