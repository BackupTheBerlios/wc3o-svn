<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="Units.aspx.cs"
    Inherits="Wc3o.Pages.Portal.Help.Units_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server"><div
    style="text-align: center">
    <asp:Panel ID="pnlUnit" Runat="server">
        <table style="width: 80%" cellspacing="0" cellpadding="5" border="0">
            <tr>
                <td style="width: 200px" align="center" valign="top">
                    <asp:Image ID="imgUnit" Runat="server" EnableViewState="False" /><br />
                    <br />
                    <asp:Image ID="imgFace" Runat="server" EnableViewState="False" />
                </td>
                <td valign="top" align="center">
                    <h1>
                        <asp:Literal ID="lblName" Runat="server" EnableViewState="False"></asp:Literal></h1>
                    <table cellspacing="0" cellpadding="15" border="0">
                        <tr>
                            <td align="center">
                                <asp:Literal ID="lblGold" Runat="server" EnableViewState="False"></asp:Literal>
                                <asp:Image ID="imgGold" Runat="server" EnableViewState="False" />
                            </td>
                            <td align="center">
                                <asp:Literal ID="lblLumber" Runat="server" EnableViewState="False"></asp:Literal>
                                <asp:Image ID="imgLumber" Runat="server" EnableViewState="False" />
                            </td>
                            <td align="center">
                                <asp:Literal ID="lblFood" Runat="server" EnableViewState="False"></asp:Literal>
                                <asp:Image ID="imgFood" Runat="server" EnableViewState="False" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table cellspacing="0" cellpadding="3" border="0">
                        <tr>
                            <td style="width: 200px" align="right">
                                Fraction:
                            </td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblFraction" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px" align="right">
                                Score:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblScore" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table cellspacing="0" cellpadding="3" border="0">
                        <tr>
                            <td style="width: 200px" align="right">
                                &nbsp;Time to train:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblMinutes" Runat="server" EnableViewState="False"></asp:Literal>
                                minutes
                            </td>
                            <td style="width: 200px" align="right">
                                Hitpoints:
                            </td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblHitpoints" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Armor type:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblArmorType" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px;" align="right">
                                Armor:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblArmor" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Flies:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblFlies" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px;" align="right">
                                Visible:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblVisibility" Runat="server" EnableViewState="False"></asp:Literal></td>
                        </tr>
                        <tr>
                            <td style="width: 200px" align="right">
                                Mines gold:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblForGold" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px" align="right">
                                Chops lumber:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblForLumber" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <table cellspacing="0" cellpadding="3" border="0">
                        <tr>
                            <td style="width: 200px;" align="right">
                                Range:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblRange" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px;" align="right">
                                Speed:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblSpeed" Runat="server" EnableViewState="False"></asp:Literal>
                                minutes</td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Attack type ground:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblAttackTypeGround" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px;" align="right">
                                Attack ground:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblAttackGround" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Attack type air:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblAttackTypeAir" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px;" align="right">
                                Attack air:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblAttackAir" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px" align="right">
                                Cooldown:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblCoolDown" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                            <td style="width: 200px" align="right">
                            </td>
                            <td style="width: 150px" align="left">
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px; vertical-align: top;" align="right">
                                Requirements:</td>
                            <td style="width: 150px; vertical-align: top;" align="left">
                                <asp:Literal ID="lblRequirements" Runat="server" EnableViewState="False"></asp:Literal></td>
                            <td style="width: 200px; vertical-align: top;" align="right">
                                Trained at:</td>
                            <td style="width: 150px; vertical-align: top;" align="left">
                                <asp:Literal ID="lblTrainedAt" Runat="server" EnableViewState="False"></asp:Literal>
                            </td>
                        </tr>
                    </table>
                    <br />
                    <b>
                    <br />
                    Boni for friendly units<br />
                    </b>
                    <table cellspacing="0" cellpadding="3" border="0">
                        <tr>
                            <td style="width: 200px;" align="right">
                                Hitpoints:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblBonusHitpoints" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                            <td style="width: 200px;" align="right">
                                Armor:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblBonusArmor" Runat="server" EnableViewState="False"></asp:Literal>
                                %
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Attack ground:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblBonusAttackGround" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                            <td style="width: 200px;" align="right">
                                Attack air:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblBonusAttackAir" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Range:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblBonusRange" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                            <td style="width: 200px;" align="right">
                                Cooldown:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblBonusCooldown" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                        </tr>
                    </table>
                    <br />
                    <b>Mali for hostile units</b><br />
                    <table cellspacing="0" cellpadding="3" border="0">
                        <tr>
                            <td style="width: 200px;" align="right">
                                Hitpoints:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblMalusHitpoints" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                            <td style="width: 200px;" align="right">
                                Armor:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblMalusArmor" Runat="server" EnableViewState="False"></asp:Literal>
                                %
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Attack ground:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblMalusAttackGround" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                            <td style="width: 200px;" align="right">
                                Attack air:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblMalusAttackAir" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                        </tr>
                        <tr>
                            <td style="width: 200px;" align="right">
                                Range:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblMalusRange" Runat="server" EnableViewState="False"></asp:Literal>
                                %
                            </td>
                            <td style="width: 200px;" align="right">
                                Cooldown:</td>
                            <td style="width: 150px" align="left">
                                <asp:Literal ID="lblMalusCooldown" Runat="server" EnableViewState="False"></asp:Literal>
                                %</td>
                        </tr>
                    </table>
                    <br />
                </td>
            </tr>
        </table>
        <br />
        <br />
    </asp:Panel>
    <asp:DropDownList ID="drpUnit" Runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpUnit_SelectedIndexChanged">
        <asp:ListItem Value="0">- Choose a unit -</asp:ListItem>
    </asp:DropDownList></div>
</asp:Content>
