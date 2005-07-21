<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" CodeFile="Data.aspx.cs" Inherits="Wc3o.Pages.Admin.Data_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    <strong>Game Data:</strong><br />
    <asp:Button ID="btnGameLoad" Runat="server" Text="Load" OnClick="btnGameLoad_Click" />
    <i>Loads all serialized data.</i><br />
    <asp:Button ID="btnGameSave" Runat="server" Text="Save" OnClick="btnGameSave_Click" />
    <i>Serializes and saves all data.</i><br />
        <asp:Button ID="btnGameCreate" Runat="server" Text="Create" OnClick="btnGameCreate_Click" />
        Creates a new data bundle (overwrites all data!).<br />
    <strong>Portal Data:</strong><br />
        <asp:Button ID="btnPortalLoad" Runat="server" Text="Load" OnClick="btnPortalLoad_Click" />
        <i>Loads all serialized data.</i><br />
        <asp:Button ID="btnPortalSave" Runat="server" Text="Save" OnClick="btnPortalSave_Click" />
        <i>Serializes and saves all data.<br />
            <asp:Button ID="btnPortalCreate" Runat="server" Text="Create" OnClick="btnPortalCreate_Click" />
            Creates a new data bundle (overwrites all data!).</i></asp:Content>
