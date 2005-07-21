<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="Delete.aspx.cs" Inherits="Wc3o.Pages.Portal.Players.Delete_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server"><h1>
    Delete Account</h1>
    <div style="text-align: center">
        <asp:CheckBox ID="chkDelete" Runat="server" Text="Yes, I'm absolutely sure that I want to delete my account forever." />
        <asp:Button ID="btnDelete" Runat="server" Text="Delete Account ..." OnClick="btnDelete_Click" />
    </div>
</asp:Content>
