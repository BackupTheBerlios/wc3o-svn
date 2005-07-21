<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="PasswordLost.aspx.cs" Inherits="Wc3o.Pages.Portal.Players.PasswordLost_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server"><h1>
    Password Lost</h1>
    You can retrieve a new Password by providing the eMail address you use with your
    account.<br />
    <div style="text-align: center">
        <br />
        <asp:TextBox ID="txtEmail" Style="width: 250px" Runat="server"></asp:TextBox>
        <asp:Button ID="btnPassword" OnClick="btnEmail_Click" Runat="server" Text="Send password"
            ValidationGroup="Email" />&nbsp;<asp:RequiredFieldValidator ID="rfvEmail" Runat="server"
                ValidationGroup="Email" ErrorMessage="?" Display="Dynamic" ControlToValidate="txtEmail">
            </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revEmail" Runat="server"
                ValidationGroup="Email" ErrorMessage="!" Display="Dynamic" ControlToValidate="txtEmail"
                ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
            </asp:RegularExpressionValidator></div>
</asp:Content>
