<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.1//EN" "http://www.w3.org/TR/xhtml11/DTD/xhtml11.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Warcraft 3 online</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        Thank you for playing Warcraft 3 online.<br />
        <br />
        The round has been finished, the elite Brotherhood of w4nk did a great job and dominates
        the entire known and unknown universe - at least until the next round.<br />
        <br />
        Speaking of which: It will take me a few days to finish und and upgrade to Version
        2.1 (Codename Scipio), I can't tell the exact date when it goes online. But you
        can leave your eMail address in the form below, and I'll notify you.<br />
        <br />
        Have a nice time these few days off, and I'm looking forward seeing you next round.<br />
        <br />
        ps: The <a href="http://1.myfreebulletinboard.com/?mforum=wc3o">forum</a> is still
        online.<br />
        <br />
        <br />
        Your eMail address:
        <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvEmail" runat="server" ErrorMessage="?" ControlToValidate="txtEmail"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revEmail" runat="server" ErrorMessage="!" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>&nbsp;<asp:Button
            ID="btnEmail" runat="server" Text="Yes, pleeeease notify me when Wc3o goes back online >>" OnClick="btnEmail_Click" /></div>
    </form>
</body>
</html>
