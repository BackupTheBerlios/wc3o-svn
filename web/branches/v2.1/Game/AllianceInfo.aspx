<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="AllianceInfo.aspx.cs"
    Inherits="Wc3o.Pages.Game.AllianceInfo_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server"><h1>
    <asp:Label ID="lblName" Runat="server"></asp:Label>&nbsp;</h1>
    <h1>
    </h1>
    <div style="text-align: center">
        <asp:Image ID="imgAlliance" Runat="server" /><br />
        <br />
        <div class="Box">
            <asp:Label ID="lblMotto" Runat="server"></asp:Label></div>
        <br />
        <br />
        <asp:GridView ID="grdMembers" Runat="server" Width="50%" CellPadding="2" GridLines="None"
            AutoGenerateColumns="False" PageSize="1">
            <AlternatingRowStyle BackColor="#0F0F0F"></AlternatingRowStyle>
            <Columns>
                <asp:BoundField HtmlEncode="False" DataField="Image">
                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Name" HtmlEncode="False" DataField="Name">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Rank / League" HtmlEncode="False" DataField="Rank">
                    <ItemStyle HorizontalAlign="Right" Width="90px"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField HeaderText="Score" DataField="Score">
                    <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                </asp:BoundField>
            </Columns>
        </asp:GridView></div>
</asp:Content>
