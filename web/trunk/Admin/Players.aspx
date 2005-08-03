<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" CodeFile="Players.aspx.cs"
    Inherits="Wc3o.Pages.Admin.Players_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    <asp:DropDownList ID="drpPlayers" Runat="server">
    </asp:DropDownList>
    <br />
    <br />
    Gold: <asp:TextBox ID="txtGold" runat="server" Width="30px">1000</asp:TextBox>
    Lumber:
    <asp:TextBox ID="txtLumber" runat="server" Width="30px">1000</asp:TextBox>&nbsp;<asp:Button
        ID="btnRessources" runat="server" OnClick="btnRessources_Click" Text="Add ressources >>" /><br />
    <asp:CheckBox ID="chkAdmin" runat="server" Text="Make this user an admin" />
    <asp:Button ID="btnAdmin" runat="server" OnClick="btnAdmin_Click" Text="Set >>" /><br />
    <br />
    <asp:Button ID="btnDelete" Runat="server" Text="Delete selected Player ..." OnClick="btnDelete_Click" /><br />
    <br />
    <asp:Button ID="btnShowUnits" runat="server" OnClick="btnShowUnits_Click" Text="Show all units >>" /><br />
    <br />
    Sector name:
    <asp:TextBox ID="txtGiveSector" runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnGiveSector" runat="server" OnClick="btnGiveSector_Click" Text="Give sector >>" /></asp:Content>
