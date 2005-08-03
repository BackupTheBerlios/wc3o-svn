<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Mail.aspx.cs"
    Inherits="Wc3o.Pages.Game.Mail_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">
    <asp:Panel ID="pnlSend" runat="server">
        <table style="width: 100%" cellspacing="0" cellpadding="5" border="0">
            <tr>
                <td style="width: 50%">
                    <table style="width: 100%" cellspacing="0" cellpadding="1" border="0">
                        <tr>
                            <td style="width: 30%" align="right">
                                Empfänger:</td>
                            <td>
                                <asp:TextBox ID="txtRecipient" runat="server" Style="width: 150px" MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px" align="right">
                            </td>
                            <td style="height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                Betreff:</td>
                            <td>
                                <asp:TextBox ID="txtSubject" runat="server" Style="width: 90%" MaxLength="100"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px" align="right">
                            </td>
                            <td style="height: 5px">
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                            </td>
                            <td>
                                <asp:LinkButton ID="btnSend" runat="server" OnClick="btnSend_Click">Send ...</asp:LinkButton></td>
                        </tr>
                    </table>
                </td>
                <td>
                    Text:<br />
                    <asp:TextBox ID="txtText" runat="server" TextMode="MultiLine" Style="width: 100%;
                        height: 80px" MaxLength="1000"></asp:TextBox>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlDetails" runat="server">
        <div style="text-align: center">
            <asp:HyperLink ID="hplReply" runat="server">Answer</asp:HyperLink>
            |
            <asp:HyperLink ID="hplDelete" runat="server">Delete</asp:HyperLink>
            | <a href="Mail.aspx">Back to inbox</a></div>
        <a href="Mail.aspx"></a>
        <br />
        <div style="text-align: center">
            <table style="width: 50%" cellspacing="0" cellpadding="5" border="0">
                <tr>
                    <td style="width: 50%" align="left">
                        Von:
                        <asp:Label ID="lblFrom" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                    <td align="right">
                        Datum:
                        <asp:Label ID="lblDate" runat="server" Font-Bold="True"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="text-align: center">
            <br />
            <asp:Label ID="lblSubject" runat="server" Font-Bold="True" Font-Size="Larger"></asp:Label>
            <div style="text-align: justify; width: 50%;">
                <br />
                <asp:Label ID="lblText" runat="server"></asp:Label></div>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlMessages" runat="server">
        <div style="text-align: center">
            <a href="Mail.aspx?Recipient=">Write new message</a> | <a href="Mail.aspx?Delete=0">
                Delete all</a></div>
        <br />
        <div style="text-align: center">
            <asp:GridView ID="grdMessages" runat="server" AutoGenerateColumns="False" Width="80%"
                CellPadding="2" GridLines="None">
                <AlternatingRowStyle BackColor="#0F0F0F"></AlternatingRowStyle>
                <Columns>
                    <asp:BoundField DataField="Sender" HtmlEncode="False">
                        <ItemStyle Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Subject" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Date" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Center" Width="230px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Reply" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="Delete" HtmlEncode="False">
                        <ItemStyle HorizontalAlign="Center" Width="100px"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView>
            &nbsp;</div>
    </asp:Panel>
</asp:Content>
