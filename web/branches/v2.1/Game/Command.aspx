<%@ Page Language="C#" CodeFile="Command.aspx.cs" Inherits="Wc3o.Pages.Game.Command_aspx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head" runat="server">
    <title>Warcraft 3 online</title>
    <script src="Scripts.js" type="text/javascript"></script>
</head>
<body>
    <form id="Form1" runat="server">
        <asp:Literal ID="lblMessage" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal><asp:Panel
            ID="pnlDestroy" Runat="server" HorizontalAlign="Center">
            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 30%; text-align: center;">
                        <asp:Image ID="imgDestroy" Runat="server" /></td>
                    <td style="width: 70%; text-align: left;">
                        <asp:Literal ID="lblDestroy" Runat="server" Mode="PassThrough"></asp:Literal>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnDestroyYes" Runat="server" Text="Yes, proceed ..." OnClick="btnDestroyYes_Click" />
            <asp:Button ID="btnDestroyNo" Runat="server" Text="No ..." OnClick="btnDestroyNo_Click" /></asp:Panel>
        <asp:Panel ID="pnlSplit" Runat="server" HorizontalAlign="Center">
            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 30%; text-align: center;">
                        <asp:Image ID="imgSplit" Runat="server" /></td>
                    <td style="width: 70%; text-align: left;">
                        <asp:Literal ID="lblSplit" Runat="server" Mode="PassThrough"></asp:Literal>
                        <asp:DropDownList ID="drpSplit" Runat="server">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnSplit" Runat="server" Text="Split ..." OnClick="btnSplit_Click" />
            <asp:Button ID="btnSplitCancel" Runat="server" Text="Cancel ..." OnClick="btnSplitCancel_Click" /></asp:Panel>
        <asp:Panel ID="pnlMove" Runat="server" HorizontalAlign="Center">
            <table style="width: 100%" cellspacing="0" cellpadding="0" border="0">
                <tr>
                    <td style="width: 30%; text-align: center;">
                        <asp:Image ID="imgMove" Runat="server" /></td>
                    <td style="width: 70%;">
                        <table style="width: 100%" cellspacing="3" cellpadding="0" border="0">
                            <tr>
                                <th>
                                    Target Sector:
                                </th>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtSector" Runat="server" Style="width: 100px;"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <th>
                                    or Target Coordinates:
                                </th>
                                <td style="text-align: left;">
                                    <asp:TextBox ID="txtX" Runat="server" Style="width: 15px;" MaxLength="2"></asp:TextBox>:<asp:TextBox
                                        ID="txtY" Runat="server" Style="width: 15px;" MaxLength="2"></asp:TextBox></td>
                            </tr>
                        </table>
                        <div style="text-align: center;">
                            <asp:CheckBox ID="chkIgnore" Runat="server" Text="Ignore workers.<br />" Checked="True" />
                            <asp:CheckBox ID="chkTime" Runat="server" Text="Same arrival time." Checked="True" /></div>
                    </td>
                </tr>
            </table>
            <asp:Button ID="btnMove" Runat="server" Text="Move ..." OnClick="btnMove_Click" />
            <asp:Button ID="btnMoveCancel" Runat="server" Text="Cancel ..." OnClick="btnMoveCancel_Click" /></asp:Panel>
        <asp:Literal ID="lblScript" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
    </form>
</body>
</html>
