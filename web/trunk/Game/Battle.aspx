<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Battle.aspx.cs"
    Inherits="Wc3o.Pages.Game.Battle_aspx" %>

<asp:Content ID="Content" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Panel ID="pnlSettings" runat="server" Font-Bold="False" Width="100%" Visible="False">
        <div style="text-align: center">
            You are going to attack
            <asp:Label ID="lblHostile" runat="server" Font-Bold="True"></asp:Label>
            on sector
            <asp:Label ID="lblSector" runat="server" Font-Bold="True"></asp:Label>.</div>
        <br />
        <br />
        <table cellpadding="2" style="width: 100%">
            <tr>
                <td style="width: 70%; text-align: right;">
                    How many rounds shall the battle last?</td>
                <td>
                    <asp:DropDownList ID="drpRounds" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem Selected="True">15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    As from what round on shall buildings be attacked?</td>
                <td>
                    <asp:DropDownList ID="drpBuildingRounds" runat="server">
                        <asp:ListItem>1</asp:ListItem>
                        <asp:ListItem>2</asp:ListItem>
                        <asp:ListItem>3</asp:ListItem>
                        <asp:ListItem>4</asp:ListItem>
                        <asp:ListItem>5</asp:ListItem>
                        <asp:ListItem>6</asp:ListItem>
                        <asp:ListItem>7</asp:ListItem>
                        <asp:ListItem>8</asp:ListItem>
                        <asp:ListItem>9</asp:ListItem>
                        <asp:ListItem>10</asp:ListItem>
                        <asp:ListItem>11</asp:ListItem>
                        <asp:ListItem>12</asp:ListItem>
                        <asp:ListItem>13</asp:ListItem>
                        <asp:ListItem>14</asp:ListItem>
                        <asp:ListItem Selected="True">15</asp:ListItem>
                        <asp:ListItem>16</asp:ListItem>
                        <asp:ListItem>17</asp:ListItem>
                        <asp:ListItem>18</asp:ListItem>
                        <asp:ListItem>19</asp:ListItem>
                        <asp:ListItem>20</asp:ListItem>
                        <asp:ListItem>21</asp:ListItem>
                        <asp:ListItem>22</asp:ListItem>
                        <asp:ListItem>23</asp:ListItem>
                        <asp:ListItem>24</asp:ListItem>
                        <asp:ListItem>25</asp:ListItem>
                        <asp:ListItem>26</asp:ListItem>
                        <asp:ListItem>27</asp:ListItem>
                        <asp:ListItem>28</asp:ListItem>
                        <asp:ListItem>29</asp:ListItem>
                        <asp:ListItem>30</asp:ListItem>
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td style="text-align: right;">
                    Make sure that the units stop resting together.</td>
                <td>
                    <asp:CheckBox ID="chkArrival" runat="server" Checked="True" /></td>
            </tr>
        </table>
        <div style="text-align: center">
            <br />
            <asp:Button ID="btnAttack" runat="server" Text="Attack >>" OnClick="btnAttackSettings_Click" /></div>
    </asp:Panel>
    <asp:Panel ID="pnlAttack" runat="server" Width="100%" Visible="False">
        <h1>
            The battle is over</h1>
        <asp:Label ID="lblMessage" runat="server"></asp:Label><br />
        For a detailed report of the fighting, take a look at the&nbsp;<asp:HyperLink ID="hplLog"
            runat="server" Target="_blank">Battle Log</asp:HyperLink>.<br />
        <br />
        <table cellspacing="0" cellpadding="20" width="100%" border="0">
            <tr>
                <td valign="top" style="width: 50%;">
                    <div style="text-align: center">
                        <b>Your losses:</b><br />
                        <br />
                        <asp:Label ID="lblPlayerLosses" runat="server" EnableViewState="False"></asp:Label>&nbsp;</div>
                </td>
                <td valign="top">
                    <div style="text-align: center">
                        <b>Hostile losses:<br />
                        </b>
                        <br />
                        <asp:Label ID="lblHostileLosses" runat="server" EnableViewState="False"></asp:Label>&nbsp;</div>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
