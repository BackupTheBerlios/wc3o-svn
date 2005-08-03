<%@ Page Language="C#" MasterPageFile="~/Game/Game.master" CodeFile="Map.aspx.cs"
    Inherits="Wc3o.Pages.Game.Map_aspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" runat="server">

    <script src="Scripts/Map.js" type="text/javascript"></script>

    <div id="divMap" style="text-align: center; height: 540px;">
    </div><br />
    <table style="width: 100%" cellspacing="0" cellpadding="3" border="0">
        <tr>
            <td align="right" style="width: 50%">
                Center map on:</td>
            <td>
                <asp:Literal ID="lblSectors" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal>
                &nbsp;&nbsp;<input type="button" value="Center >>" OnClick="CenterOnSector()" />
            </td>
        </tr>
        <tr>
            <td style="width: 50%" align="right">
                Center map on coordinates:</td>
            <td>
                <input type="text" id="txtX" maxlength="3" style="width: 17px;" />
                &nbsp;:
                <input type="text" id="txtY" maxlength="3" style="width: 17px;" />
                &nbsp;&nbsp;<input type="button" value="Center >>" OnClick="CenterOnCoordinates()" /></td>
        </tr>
    </table>
    <asp:Literal ID="lblJavaScript" runat="server" EnableViewState="False" Mode="PassThrough"></asp:Literal></asp:Content>
