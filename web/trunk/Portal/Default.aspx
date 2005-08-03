<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="Default.aspx.cs"
    Inherits="Wc3o.Pages.Portal.Default_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <div style="text-align: center">
        <table style="width: 100%">
            <tr>
                <td style="width: 96px; vertical-align: top;">
                    <asp:Image ID="imgIntroLeft" runat="server" /></td>
                <td style="font-size: 105%; text-align: justify; vertical-align: top;">
                    <strong>Warcraft 3 online</strong> is a massively multiplayer browser game based
                    on the famous Warcraft 3: The Frozen Throne. It is <strong>absolutely free</strong>
                    to play.&nbsp;<br />
                    <br />
                    Choose a fraction, either the <strong>Human Alliance</strong>, the <strong>Orc Horde</strong>,
                    the <strong>Undead Scourge</strong> or the <strong>Night Elves</strong> and fight
                    along with your allies against your human enemies on a huge and dangerous continent.
                    Each fraction has different strengths,&nbsp; abilities, units and buildings, and
                    therefore requires a unique strategy. Build up your bases with up to <strong>60 different
                        buildings</strong> and create an unstoppable army with more than <strong>160 different
                            units</strong> on land and in the air ...<br />
                    <br />
                    So, what are you waiting for? <a href="Players/SignUp.aspx">Sign up</a> and get
                    into the action right now.</td>
                <td style="width: 96px; vertical-align: top;">
                    <asp:Image ID="imgIntroRight" runat="server" /></td>
            </tr>
        </table>
        <br />
        <br />
        <br />
        <asp:Repeater ID="rptNews" runat="server" EnableViewState="False">
            <ItemTemplate>
                <div class="News">
                    <div class="Left">
                        <%# DataBinder.Eval(Container.DataItem, "Text") %>
                    </div>
                    <div class="Right">
                        <img src="<%# DataBinder.Eval(Container.DataItem, "Info.Image") %>" title="<%# DataBinder.Eval(Container.DataItem, "Info.Description") %>" /></div>
                    <div class="Footer">
                        <%# DataBinder.Eval(Container.DataItem, "Name") %>
                        -
                        <%# Wc3o.Game.Format((DateTime)DataBinder.Eval(Container.DataItem, "Date"),false) %>
                    </div>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
</asp:Content>
