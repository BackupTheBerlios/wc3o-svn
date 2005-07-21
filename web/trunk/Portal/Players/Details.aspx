<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="Details.aspx.cs"
    Inherits="Wc3o.Pages.Portal.Players.Details_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server"><h1>
    Player Details</h1>
    You may edit your account details here. Please note that you have to confirm your
    eMail address to be able to play, so be sure you use a correct one.<table style="width: 100%"
        cellspacing="5" cellpadding="0" border="0"><tr>
            <th>
                Your name:</th>
            <td>
                <asp:Literal ID="lblName" Runat="server" Mode="PassThrough"></asp:Literal></td>
        </tr>
        <tr>
            <th>
                Your eMail address:</th>
            <td>
                <asp:TextBox ID="txtEmail" Runat="server" Style="width: 250px;"></asp:TextBox>
                <asp:Button ID="btnEmail" Runat="server" Text="Edit ..." ValidationGroup="Email" OnClick="btnEmail_Click" />&nbsp;<asp:RequiredFieldValidator
                    ID="rfvEmail" Runat="server" ErrorMessage="?" Display="Dynamic" ControlToValidate="txtEmail"
                    ValidationGroup="Email">
                </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revEmail" Runat="server"
                    ErrorMessage="!" Display="Dynamic" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                    ValidationGroup="Email">
                </asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <th>
                Path to your Gfx Pack:</th>
            <td>
                <asp:TextBox ID="txtGfx" Runat="server" Style="width: 250px;"></asp:TextBox>
                <asp:Button ID="btnGfx" Runat="server" Text="Edit ..." ValidationGroup="Gfx" OnClick="btnGfx_Click" />
            </td>
        </tr></table>
    <br />
    You can <a href="../Misc/Gfx.zip">download the Gfx Pack</a> (~6 MB) to your local harddrive to load all the images and animations from there. This extremely increases the speed of Warcraft 3 online for you. Specify the path to your personal copy of the Gfx pack above; on Windows, it must look something like <i>file:///c:/Gfx</i>.
    <br /><br />
    <table style="width: 100%" cellspacing="5" cellpadding="0" border="0"><tr>
            <th>
                Your current password:</th>
            <td>
                <asp:TextBox ID="txtCurrentPassword" Runat="server" Style="width: 150px;" TextMode="Password"></asp:TextBox>&nbsp;<asp:RequiredFieldValidator
                    ID="rfvCurrentPassword" Runat="server" ErrorMessage="?" Display="Dynamic" ControlToValidate="txtCurrentPassword"
                    ValidationGroup="Password">
                </asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <th>
                Your new password:</th>
            <td>
                <asp:TextBox ID="txtPassword" Runat="server" Style="width: 150px;" TextMode="Password"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvPassword" Runat="server" ErrorMessage="?" Display="Dynamic"
                    ControlToValidate="txtPassword" ValidationGroup="Password">
                </asp:RequiredFieldValidator></td>
        </tr>
        <tr>
            <th>
                Repeat new password:</th>
            <td>
                <asp:TextBox ID="txtRepeatPassword" Runat="server" Style="width: 150px;" TextMode="Password"></asp:TextBox>&nbsp;<asp:CompareValidator
                    ID="cpvPassword" Runat="server" ErrorMessage="!" ValidationGroup="Password" ControlToValidate="txtRepeatPassword" ControlToCompare="txtPassword">
                </asp:CompareValidator></td>
        </tr>
        <tr>
            <th>
            </th>
            <td>
                <asp:Button ID="btnPassword" Runat="server" Text="Change password ..." ValidationGroup="Password" OnClick="btnPassword_Click" /></td>
        </tr></table>
</asp:Content>
