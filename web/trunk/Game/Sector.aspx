<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Sector.aspx.cs"
    Inherits="Wc3o.Pages.Game.Sector_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src="Scripts/Buildings.js" type="text/javascript"></script>
    <script src="Scripts/Constructing.js" type="text/javascript"></script>
    <script src="Scripts/Mercenaries.js" type="text/javascript"></script>    
    <script src="Scripts/Popup.js" type="text/javascript"></script>
    <script src="Scripts/Refresh.js" type="text/javascript"></script>
    <script src="Scripts/Sector.js" type="text/javascript"></script>
    <script src="Scripts/Training.js" type="text/javascript"></script>
    <script src="Scripts/Units.js" type="text/javascript"></script>

    <h1>
        <asp:HyperLink ID="hplSector" runat="server" EnableViewState="False"></asp:HyperLink>
    </h1>
    <div style="text-align: center">
        Owner:
        <asp:HyperLink ID="hplOwner" runat="server" EnableViewState="False"></asp:HyperLink><asp:Literal
            ID="lblSectorInfo" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
        <br />
        <asp:DropDownList ID="drpSectors" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpSectors_SelectedIndexChanged">
        </asp:DropDownList><br />
        <asp:Button ID="btnCapture" runat="server" OnClick="btnCapture_Click" Text="Capture this sector ..." /></div>
    <table style="width: 100%" cellspacing="20" cellpadding="0" border="0">
        <tr>
            <td style="width: 50%; vertical-align: top; height: 19px;">
                <asp:Literal ID="lblPlayer" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            <td style="vertical-align: top; height: 19px;">
                <asp:Literal ID="lblOthers" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
        </tr>
    </table>

    <asp:Literal ID="lblOpen" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></asp:Content>
