<%@ Page Language="C#" MasterPageFile="~/Admin/Admin.master" CodeFile="Email.aspx.cs"
    Inherits="Wc3o.Pages.Admin.Email_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    Recipients (one per line):
    <br />
    <asp:TextBox ID="txtRecipients" runat="server" Height="104px" TextMode="MultiLine"
        Width="720px"></asp:TextBox><br />
    <br />
    Subject:
    <br />
    <asp:TextBox ID="txtSubject" runat="server" Width="720px"></asp:TextBox><br />
    Text:<br />
    <asp:TextBox ID="txtText" runat="server" Height="208px" TextMode="MultiLine" Width="720px"></asp:TextBox><br />
    <br />
    <asp:Button ID="btnSend" runat="server" OnClick="btnSend_Click" Text="Send >>" /></asp:Content>
