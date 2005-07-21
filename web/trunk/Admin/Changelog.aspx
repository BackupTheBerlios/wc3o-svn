<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" CodeFile="Changelog.aspx.cs" Inherits="Wc3o.Pages.Admin.Changelog_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
            Creator:
            <asp:TextBox ID="txtCreator" Runat="server"></asp:TextBox><br />
            Datum:
            <asp:TextBox ID="txtDate" Runat="server"></asp:TextBox>&nbsp;<asp:Button ID="btnNow"
                Runat="server" Text="Now" OnClick="btnNow_Click" /><br />
            Text:
            <asp:TextBox ID="txtText" Runat="server" Width="800px"></asp:TextBox><br />
            <br />
            <asp:Button ID="btnAdd" Runat="server" Text="Save" OnClick="btnAdd_Click" /><br />
    <br />
    Edit existing news: <asp:DropDownList ID="drpEdit" Runat="server">
    </asp:DropDownList>
    <asp:Button ID="btnEdit" Runat="server" Text="Edit >>" OnClick="btnEdit_Click" />&nbsp;<asp:Button
        ID="btnDelete" runat="server" OnClick="btnDelete_Click" Text="Delete >>" /></asp:Content>
