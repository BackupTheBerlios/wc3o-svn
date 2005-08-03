<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Alliance.aspx.cs"
    Inherits="Wc3o.Pages.Game.Alliance_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    <asp:Panel ID="pnlNoAlliance" Runat="server">
        <table style="width: 100%" cellspacing="0" cellpadding="10" border="0">
            <tr>
                <td style="width: 50%">
                    <div style="text-align: center">
                        <table style="width: 100%" cellspacing="0" cellpadding="1" border="0">
                            <tr>
                                <td style="width: 50%" align="right">
                                    Apply to an existing alliance:</td>
                                <td align="left">
                                    <asp:DropDownList ID="drpApply" Runat="server">
                                        <asp:ListItem Value="0">- Choose an alliance -</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%" align="right">
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnApply" Runat="server" Text="Apply ..." OnClick="btnApply_Click" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </td>
                <td>
                    <table style="width: 100%" cellspacing="0" cellpadding="1" border="0">
                        <tr>
                            <td style="width: 50%" align="right">
                                Found a new alliance:</td>
                            <td>
                                <asp:TextBox ID="txtFound" Runat="server" MaxLength="5" Style="width: 50px"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvFound" Runat="server" ErrorMessage="?"
                                    Display="Dynamic" ControlToValidate="txtFound" ValidationGroup="Found">
                                </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revFound" Runat="server"
                                    ErrorMessage="!" Display="Dynamic" ValidationGroup="Found" ControlToValidate="txtFound" ValidationExpression="\w{2,5}"></asp:RegularExpressionValidator></td>
                        </tr>
                        <tr>
                            <td style="width: 50%" align="right">
                            </td>
                            <td>
                                <asp:Button ID="btnFound" Runat="server" Text="Found ..." ValidationGroup="Found"
                                    OnClick="btnFound_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <em>Please note that this is only the shortcut for your alliance. You can specify the
                        full name later.</em></td>
            </tr>
        </table>
    </asp:Panel>
    <asp:Panel ID="pnlWaiting" Runat="server">
        <div class="Info">
            You are waiting that you get accepted by the leader of
            <asp:Literal ID="lblWaiting" Runat="server" EnableViewState="False"></asp:Literal>.<br />
            <asp:Button ID="btnWaiting" Runat="server" Text="Stop waiting ..." OnClick="btnWaiting_Click" /></div>
    </asp:Panel>
    <asp:Panel ID="pnlAlliance" Runat="server">
        <h1>
            <asp:Literal ID="lblName" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></h1>
        <div style="text-align: center;">
            <asp:Image ID="imgImage" Runat="server" /><br />
            <br />
            <div class="Box">
                <asp:Literal ID="lblDirective" Runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></div>
            <br />
            <asp:GridView ID="grdMembers" Runat="server" Width="50%" CellPadding="2" GridLines="None"
                AutoGenerateColumns="False" PageSize="1">
                <AlternatingRowStyle BackColor="#0F0F0F"></AlternatingRowStyle>
                <Columns>
                    <asp:BoundField HtmlEncode="False" DataField="SmallEmblem">
                        <ItemStyle HorizontalAlign="Right" Width="30px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Name" HtmlEncode="False" DataField="MailLink">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Rank / League" HtmlEncode="False" DataField="RankLeague">
                        <ItemStyle HorizontalAlign="Right" Width="90px"></ItemStyle>
                    </asp:BoundField>
                    <asp:BoundField HeaderText="Score" DataField="Score">
                        <ItemStyle HorizontalAlign="Right" Width="50px"></ItemStyle>
                    </asp:BoundField>
                </Columns>
            </asp:GridView></div>
        <br />
        <br />
        <div class="Box">
            <table style="width: 100%" cellspacing="0" cellpadding="10" border="0">
                <tr>
                    <td style="width: 50%" valign="top">
                        <div style="text-align: center">
                            <table style="width: 100%" cellspacing="0" cellpadding="1" border="0">
                                <tr>
                                    <td style="width: 50%" align="right">
                                        Vote for a leader:</td>
                                    <td align="left">
                                        <asp:DropDownList ID="drpVote" Runat="server">
                                            <asp:ListItem Value="0">- Choose a member -</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnVote" Runat="server" Text="Vote ..." OnClick="btnVote_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td valign="top" align="center">
                        <asp:Button ID="btnLeave" Runat="server" Text="Leave this alliance ..." OnClick="btnLeave_Click" /><br />
                        <br />
                        <asp:HyperLink ID="hplMessage" runat="server">Send a message to all members of this alliance</asp:HyperLink>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <asp:Panel ID="pnlLeader" Runat="server">
        <br />
        <div class="Box">
            <table style="width: 100%" cellspacing="0" cellpadding="10" border="0">
                <tr>
                    <td style="width: 50%" valign="top">
                        <div style="text-align: center">
                            <table style="width: 100%" cellspacing="0" cellpadding="1" border="0">
                                <tr>
                                    <td style="width: 50%" align="right">
                                        Long Name:</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtLongName" Style="width: 150px" Runat="server" MaxLength="30"
                                            ValidationGroup="Details"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%" align="right">
                                        Logo:</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtImage" Style="width: 300px" Runat="server" MaxLength="100" ValidationGroup="Details"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%" align="right">
                                        Description (public):</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDescription" Style="width: 300px; height: 50px;" Runat="server"
                                            MaxLength="30" TextMode="MultiLine" ValidationGroup="Details"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%" align="right">
                                        Directive (private):</td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDirective" Style="width: 300px; height: 50px;" Runat="server"
                                            MaxLength="30" TextMode="MultiLine" ValidationGroup="Details"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 50%" align="right">
                                    </td>
                                    <td align="left">
                                        <asp:Button ID="btnDetails" Runat="server" Text="Save details ..." ValidationGroup="Details"
                                            OnClick="btnDetails_Click" /></td>
                                </tr>
                            </table>
                        </div>
                    </td>
                    <td valign="top" align="center">
                        <table style="width: 100%" cellspacing="0" cellpadding="1" border="0">
                            <tr>
                                <td style="width: 50%;" align="right">
                                    Pending appliances:</td>
                                <td align="left">
                                    <asp:DropDownList ID="drpWaiting" Runat="server">
                                        <asp:ListItem Value="0">- Choose a player -</asp:ListItem>
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%" align="right">
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnAccept" Runat="server" CausesValidation="False"
                                            Text="Accept >>" OnClick="btnAccept_Click" /><asp:Button ID="btnReject" Runat="server" CausesValidation="False" Text="<< Reject"
                                        OnClick="btnReject_Click" /></td>
                            </tr>
                        </table>
                        <br />
                        <br />
                        <br />
                        <table style="width: 100%" cellspacing="0" cellpadding="1" border="0">
                            <tr>
                                <td align="center" colspan="2">
                                    <asp:DropDownList ID="drpMembers" Runat="server">
                                        <asp:ListItem Value="0">- Choose a member -</asp:ListItem>
                                    </asp:DropDownList><br />
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 50%" align="right">
                                    Kick this ally out:<br />
                                </td>
                                <td align="left">
                                    <asp:Button ID="btnKick" Runat="server" CausesValidation="False" Text="Kick >>"
                                        OnClick="btnKick_Click" /><br />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" style="width: 50%; height: 48px">
                                    Give this ally a new rank:</td>
                                <td align="left" style="height: 48px">
                                    <asp:DropDownList ID="drpRank" runat="server">
                                    </asp:DropDownList>
                                    <br />
                                    <asp:Button ID="btnRank" runat="server" Text="Promote >>" OnClick="btnRank_Click" /></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel></asp:Content>
