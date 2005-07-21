<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="PlayerInfo.aspx.cs"
    Inherits="Wc3o.Pages.Game.PlayerInfo_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    <table style="width: 100%" cellspacing="0" cellpadding="10" border="0">
        <tr>
            <td style="width: 40%" valign="top">
                <div style="text-align: center">
                    <asp:Image ID="imgFraction" Runat="server" /></div>
            </td>
            <td valign="top">
                <h1>
                    <asp:Label ID="lblName" Runat="server"></asp:Label>&nbsp;</h1>
                <ul>
                    <li>signed up on&nbsp;<asp:Label ID="lblRegistrationDate" Runat="server" Font-Bold="True"></asp:Label>
                    </li>
                    <li>was the last time online on&nbsp;<asp:Label ID="lblLogonDate" Runat="server" Font-Bold="True"></asp:Label>
                    </li><li>is ranked&nbsp;<asp:Label ID="lblRank" Runat="server"></asp:Label>. in the&nbsp;<asp:Label ID="lblLeague" Runat="server"></asp:Label>.league</li><li>had his best score with rank&nbsp;<asp:Label ID="lblBestRank" Runat="server"></asp:Label>.
                        in the&nbsp;<asp:Label ID="lblBestLeague" Runat="server"></asp:Label>. league</li><li>you can send this player a &nbsp;<asp:HyperLink ID="hplMessage" Runat="server">message</asp:HyperLink>
                    </li></ul>
                <br />
                <br />
                <div class="Box">
                    <asp:Label ID="lblDescription" Runat="server"></asp:Label></div>
                <br />
                <br />
                <br />
                This player reigns over these sectors: :<asp:Label ID="lblSectors" Runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</asp:Content>
