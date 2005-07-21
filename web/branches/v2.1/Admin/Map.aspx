<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" CodeFile="Map.aspx.cs"
    Inherits="Wc3o.Pages.Admin.Map_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    <asp:Button ID="btnCreateMap" Runat="server" Text="Create Map" OnClick="btnCreateMap_Click" />
    <i>Creates the entire map.<br />
    <asp:Button ID="btnSectors" Runat="server" Text="Sectors" OnClick="btnSectors_Click" />
    Prints the names and coordinates of all sectors.</i><br />
</asp:Content>
