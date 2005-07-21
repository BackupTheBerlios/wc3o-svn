<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" CodeFile="Ticks.aspx.cs"
    Inherits="Wc3o.Pages.Admin.Ticks_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    Ressource Tick:
    <asp:TextBox ID="txtRessource" runat="server" Width="100px"></asp:TextBox>
    <asp:Button ID="btnRessource" runat="server" OnClick="btnRessource_Click" Text="Set" /><br />
    Ranking Tick:
    <asp:TextBox ID="txtRanking" runat="server" Width="100px"></asp:TextBox>
    <asp:Button ID="btnRanking" runat="server" OnClick="btnRanking_Click" Text="Set" /></asp:Content>
