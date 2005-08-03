<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="Settings.aspx.cs"
    Inherits="Wc3o.Pages.Portal.Help.Settings_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <h1>
        Settings</h1>
    Here are some general settings that affect the gameplay of Warcraft 3 online.<br />
    <br />
    <div style="text-align: center;">
        <table cellpadding="2" style="width: 70%">
            <tr>
                <td style="width: 80%; text-align: left">
                    Max. players:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblMaxPlayers" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Players per league:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblPlayersPerLeague" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Days until a dead player gets deleted:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblDelete" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Hours, a player is under protection after sign up:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblProtection" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Seconds a ressource tick lasts:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblRessourceTick" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Seconds a next ranking tick lasts:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblRankingTick" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Start of night:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblNightStart" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>:00
                    CET</td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    End of night:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblNightEnd" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>:00
                    CET</td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    <br />
                </td>
                <td style="text-align: right">
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left;">
                    Max. food a player can have (total upkeep limit):</td>
                <td style="text-align: right;">
                    <asp:Literal ID="lblMaxFood" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Unit upkeep factor for capturing sectors:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblCaptureFactor" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Gold per worker and ressource tick:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblGoldPerWorker" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Lumber per worker and ressource tick:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblLumberPerWorker" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Max. workers mining gold per sector:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblMaxWorkers" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Min. gold income:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblMinGold" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Min. lumber income:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblMinLumber" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    <br />
                </td>
                <td style="text-align: right">
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Upkeep to reach cost level 1:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblUpkeepLevel1" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Ressource factor on cost level 1:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblFactorLevel1" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Upkeep to reach cost level 2:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblUpkeepLevel2" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Ressource factor on cost level 2:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblFactorLevel2" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left;">
                    <br />
                </td>
                <td style="text-align: right;">
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Max. sectors a player can own:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblMaxSectors" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Min. sectors a player can own (a player cannot loose more):</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblMinSectors" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Minutes until arrival of moving units so that the enemy (on target sector) can see
                    them:</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblMinutes" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left;">
                    Factor on speed after attacking (time to rest):</td>
                <td style="text-align: right;">
                    <asp:Literal ID="lblReturnAttack" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left;">
                    Factor on speed after attacking on own sector (time to rest):</td>
                <td style="text-align: right;">
                    <asp:Literal ID="lblReturnDefend" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    <br />
                </td>
                <td style="text-align: right">
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Humans and Orcs healing rate (per ressource tick):</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblHealingHumansOrcs" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>&nbsp;hitpoints
                </td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Undead healing rate (per ressource tick):</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblHealingUndead" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>&nbsp;hitpoints</td>
            </tr>
            <tr>
                <td style="width: 80%; text-align: left">
                    Night Elves healing rate (per ressource tick):</td>
                <td style="text-align: right">
                    <asp:Literal ID="lblHealingNightElves" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>&nbsp;hitpoints</td>
            </tr>
        </table>
    </div>
</asp:Content>
