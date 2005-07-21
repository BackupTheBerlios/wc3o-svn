<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Default.aspx.cs"
    Inherits="Wc3o.Pages.Game.Default_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    <table style="width: 100%" cellspacing="0" cellpadding="20" border="0"><tr>
            <td style="width: 30%; text-align: center;" valign="top">
                <asp:Image ID="imgFace" Runat="server" />
            </td>
            <td valign="top">
                Welcome
                <asp:Literal ID="lblPlayer" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                of the
                <asp:Literal ID="lblFraction" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>!<br />
                <br />
                <ul>
                    <li>Your hard-working workers mine
                <asp:Literal ID="lblGold" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                <asp:Image ID="imgGold" Runat="server" EnableViewState="False" />
                and chop
                <asp:Literal ID="lblLumber" Runat="server" EnableViewState="False"></asp:Literal>
                <asp:Image ID="imgLumber" Runat="server" EnableViewState="False" />
                per Ressource Tick.</li>
                    <li>You are a part of the war since
                    <asp:Literal ID="lblRegistration" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>.
                    <asp:Literal ID="lblProtection" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                    </li>
                    <li>Your score is
                    <asp:Literal ID="lblScore" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>,
                    so you are ranked
                    <asp:Literal ID="lblRank" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>.
                    in the
                    <asp:Literal ID="lblLeague" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>.
                    league.</li>
                    <li>You reign the sectors:
                    <asp:Literal ID="lblSectors" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                    </li>
                </ul>
                <br />
                <br />
                <br />
                <br />
                <div style="text-align: center">
                    Your Account:
                    <asp:HyperLink ID="hplDetails" Runat="server">Details</asp:HyperLink>
                    |
                    <asp:HyperLink ID="hplLogoff" Runat="server">Logoff</asp:HyperLink>
                    |
                    <asp:HyperLink ID="hplDelete" Runat="server">Delete</asp:HyperLink></div>
            </td>
        </tr></table>
</asp:Content>
