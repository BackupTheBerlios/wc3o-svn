<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="Buildings.aspx.cs"
    Inherits="Wc3o.Pages.Portal.Help.Buildings_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="text-align: center">
        <asp:Panel ID="pnlBuilding" runat="server">
            <table style="width: 80%" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td style="width: 200px" align="center" valign="top">
                        <asp:Image ID="imgBuilding" runat="server" EnableViewState="False" /></td>
                    <td valign="top" align="center">
                        <h1>
                            <asp:Literal ID="lblName" runat="server" EnableViewState="False"></asp:Literal></h1>
                        <table cellspacing="0" cellpadding="15" border="0">
                            <tr>
                                <td align="center">
                                    <asp:Literal ID="lblGold" runat="server" EnableViewState="False"></asp:Literal>
                                    <asp:Image ID="imgGold" runat="server" EnableViewState="False" />
                                </td>
                                <td align="center">
                                    <asp:Literal ID="lblLumber" runat="server" EnableViewState="False"></asp:Literal>
                                    <asp:Image ID="imgLumber" runat="server" EnableViewState="False" />
                                </td>
                                <td align="center">
                                    &nbsp;</td>
                            </tr>
                        </table>
                        <br />
                        <table cellspacing="0" cellpadding="3" border="0">
                            <tr>
                                <td style="width: 200px" align="right">
                                    Fraction:
                                </td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblFraction" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                                <td style="width: 200px" align="right">
                                    Score:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblScore" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                        </table>
                        <br />
                        <table cellspacing="0" cellpadding="3" border="0">
                            <tr>
                                <td style="width: 200px" align="right">
                                    &nbsp;Time to construct:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblMinutes" runat="server" EnableViewState="False"></asp:Literal>
                                    minutes
                                </td>
                                <td style="width: 200px" align="right">
                                    Hitpoints:
                                </td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblHitpoints" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Armor type:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblArmorType" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                                <td style="width: 200px;" align="right">
                                    Armor:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblArmor" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Upkeep:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblUpkeep" runat="server" EnableViewState="False"></asp:Literal></td>
                                <td style="width: 200px;" align="right">
                                </td>
                                <td style="width: 150px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 200px">
                                    Max. per sector:</td>
                                <td align="left" style="width: 150px">
                                    <asp:Literal ID="lblMaxPerSector" runat="server" EnableViewState="False"></asp:Literal></td>
                                <td align="right" style="width: 200px">
                                    Max. total:</td>
                                <td align="left" style="width: 150px">
                                    <asp:Literal ID="lblMax" runat="server" EnableViewState="False"></asp:Literal></td>
                            </tr>
                        </table>
                        <br />
                        <table cellspacing="0" cellpadding="3" border="0">
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Range:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblRange" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                                <td style="width: 200px;" align="right">
                                </td>
                                <td style="width: 150px" align="left">
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Attack type ground:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblAttackTypeGround" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                                <td style="width: 200px;" align="right">
                                    Attack ground:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblAttackGround" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Attack type air:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblAttackTypeAir" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                                <td style="width: 200px;" align="right">
                                    Attack air:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblAttackAir" runat="server" EnableViewState="False"></asp:Literal>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px" align="right">
                                    Cooldown:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblCoolDown" runat="server" EnableViewState="False"></asp:Literal>
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
                                    <asp:Literal ID="lblRequirements" runat="server" EnableViewState="False"></asp:Literal></td>
                                <td style="width: 200px; vertical-align: top;" align="right">
                                    Upgrades to:</td>
                                <td style="width: 150px; vertical-align: top;" align="left">
                                    <asp:Literal ID="lblUpgradesTo" runat="server" EnableViewState="False"></asp:Literal>
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
                                    <asp:Literal ID="lblBonusHitpoints" runat="server" EnableViewState="False"></asp:Literal>
                                    %</td>
                                <td style="width: 200px;" align="right">
                                    Armor:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblBonusArmor" runat="server" EnableViewState="False"></asp:Literal>
                                    %
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Attack ground:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblBonusAttackGround" runat="server" EnableViewState="False"></asp:Literal>
                                    %</td>
                                <td style="width: 200px;" align="right">
                                    Attack air:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblBonusAttackAir" runat="server" EnableViewState="False"></asp:Literal>
                                    %</td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Range:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblBonusRange" runat="server" EnableViewState="False"></asp:Literal>
                                    %</td>
                                <td style="width: 200px;" align="right">
                                    Cooldown:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblBonusCooldown" runat="server" EnableViewState="False"></asp:Literal>
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
                                    <asp:Literal ID="lblMalusHitpoints" runat="server" EnableViewState="False"></asp:Literal>
                                    %</td>
                                <td style="width: 200px;" align="right">
                                    Armor:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblMalusArmor" runat="server" EnableViewState="False"></asp:Literal>
                                    %
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Attack ground:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblMalusAttackGround" runat="server" EnableViewState="False"></asp:Literal>
                                    %</td>
                                <td style="width: 200px;" align="right">
                                    Attack air:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblMalusAttackAir" runat="server" EnableViewState="False"></asp:Literal>
                                    %</td>
                            </tr>
                            <tr>
                                <td style="width: 200px;" align="right">
                                    Range:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblMalusRange" runat="server" EnableViewState="False"></asp:Literal>
                                    %
                                </td>
                                <td style="width: 200px;" align="right">
                                    Cooldown:</td>
                                <td style="width: 150px" align="left">
                                    <asp:Literal ID="lblMalusCooldown" runat="server" EnableViewState="False"></asp:Literal>
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
        <asp:DropDownList ID="drpBuilding" runat="server" AutoPostBack="True" OnSelectedIndexChanged="drpBuilding_SelectedIndexChanged">
            <asp:ListItem Value="0">- Choose a building -</asp:ListItem>
        </asp:DropDownList></div>
</asp:Content>
