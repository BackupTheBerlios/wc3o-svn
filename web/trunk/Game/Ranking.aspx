<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Ranking.aspx.cs"
    Inherits="Wc3o.Pages.Game.Ranking_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Panel ID="pnlPlayer" runat="server" Style="text-align: center"><br />
    <div style="text-align: right">
        <asp:Literal ID="lblUnranked" runat="server" EnableViewState="False"></asp:Literal>
        not ranked yet.<br />
        Next Ranking Tick in&nbsp;<b><asp:Literal ID="lblRankingTick" runat="server" EnableViewState="False"></asp:Literal></b>.</div>
        <h1>
            Ranking for the
            <asp:Literal ID="lblLeague" runat="server" EnableViewState="False"></asp:Literal>.
            League</h1>
        <asp:Literal ID="lblLeagues" runat="server" EnableViewState="False"></asp:Literal>
        <br />
        <br />
        <asp:GridView ID="grdPlayer" runat="server" Width="450px" CellPadding="2" GridLines="None"
            AutoGenerateColumns="False" PageSize="1" EnableViewState="False">
            <AlternatingRowStyle BackColor="#0f0f0f"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField HtmlEncode="False" DataField="Rank">
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HtmlEncode="False" DataField="Image">
                    <ItemStyle Width="30px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HtmlEncode="False" DataField="Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HtmlEncode="False" DataField="Score">
                    <ItemStyle HorizontalAlign="Right" Width="100px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
    <asp:Panel ID="pnlAlliance" runat="server" Style="text-align: center">
        <h1>
            Ranking for all Alliances</h1>
        <asp:GridView ID="grdAlliance" runat="server" Width="600px" CellPadding="1" GridLines="None"
            AutoGenerateColumns="False" EnableViewState="False">
            <AlternatingRowStyle BackColor="#0F0F0F"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField HeaderText="Rank" HtmlEncode="False" DataField="Rank">
                    <ItemStyle HorizontalAlign="Center" Width="40px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Name" HtmlEncode="False" DataField="Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Members" HtmlEncode="False" DataField="Members">
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Average" HtmlEncode="False" DataField="Average">
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Score" HtmlEncode="False" DataField="Score">
                    <ItemStyle HorizontalAlign="Right" Width="70px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView>
    </asp:Panel>
</asp:Content>
