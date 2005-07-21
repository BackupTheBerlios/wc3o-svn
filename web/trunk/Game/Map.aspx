<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Map.aspx.cs"
    Inherits="Wc3o.Pages.Game.Map_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server">
    <input type="hidden" name="txtInfo" id="txtInfo" />
    <div style="text-align: center">
        <asp:DropDownList ID="drpSectors" Runat="server" OnSelectedIndexChanged="drpSectors_SelectedIndexChanged"
            AutoPostBack="True">
        </asp:DropDownList></div>
    <br />
    <table style="width: 100%;" cellspacing="10" cellpadding="0" border="0">
        <tr>
            <td style="width: 150px; text-align: center;">
                <div id="Info">
                </div>
            </td>
            <td align="center">
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td>
                            <asp:Image ID="imgTopLeft" Runat="server" />
                        </td>
                        <td>
                            <asp:Image ID="imgTop" Runat="server" />
                        </td>
                        <td>
                            <asp:Image ID="imgTopRight" Runat="server" /></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="imgLeft" Runat="server" />
                        </td>
                        <td align="center">
                            <asp:Label ID="lblMap" Runat="server" EnableViewState="False"></asp:Label>
                        </td>
                        <td>
                            <asp:Image ID="imgRight" Runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Image ID="imgBottomLeft" Runat="server" /></td>
                        <td>
                            <asp:Image ID="imgBottom" Runat="server" />
                        </td>
                        <td>
                            <asp:Image ID="imgBottomRight" Runat="server" /></td>
                    </tr>
                </table>
            </td>
            <td style="width: 150px;" align="center">
                <table cellspacing="0" cellpadding="3" border="0">
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblScrollUp" Runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblScrollLeft" Runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblScrollRight" Runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                            <asp:Label ID="lblScrollDown" Runat="server"></asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table style="width: 100%" cellspacing="0" cellpadding="3" border="0">
        <tr>
            <td style="width: 50%" align="right">
                Center map on coordinates:</td>
            <td>
                <asp:TextBox ID="txtX" Runat="server" MaxLength="3" Style="width: 17px;"></asp:TextBox>
                :
                <asp:TextBox ID="txtY" Runat="server" MaxLength="3" Style="width: 17px;"></asp:TextBox>
                <asp:LinkButton ID="btnCoords" Runat="server" OnClick="btnCoords_Click">Center ...</asp:LinkButton>
            </td>
        </tr>
        <tr>
            <td style="width: 50%" align="right">
                Center map on sector:</td>
            <td>
                <asp:TextBox ID="txtSector" Runat="server" MaxLength="50" Style="width: 150px;"></asp:TextBox>
                <asp:LinkButton ID="btnSector" Runat="server" OnClick="btnSector_Click">Center ...</asp:LinkButton></td>
        </tr>
    </table>
</asp:Content>
