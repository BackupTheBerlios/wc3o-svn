<%@ Page Language="C#" MasterPageFile="~/Portal/Portal.master" CodeFile="SignUp.aspx.cs"
    Inherits="Wc3o.Pages.Portal.Players.SignUp_aspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder" Runat="server"><h1>
    Sign Up</h1>
        Please note that you cannot change your name or your race later.<br />
    The name you choose must not contain any special characters. You also have
        to use a valid eMail address because you will receive your password via eMail.<table style="width: 100%" cellpadding="1"><tr>
            <th>
                Choose a Name</th>
            <td>
                <asp:TextBox ID="txtName" Style="width: 150px" Runat="server" MaxLength="30"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvName" Runat="server" ErrorMessage="?" Display="Dynamic"
                    ControlToValidate="txtName">
                </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revName" Runat="server"
                    ErrorMessage="!" Display="Dynamic" ValidationExpression="\w*" ControlToValidate="txtName">
                </asp:RegularExpressionValidator></td>
        </tr>
        <tr>
            <th>
                Your eMail address</th>
            <td>
                <asp:TextBox ID="txtEmail" Style="width: 150px" Runat="server" MaxLength="50"></asp:TextBox>
                <asp:RequiredFieldValidator ID="rfvEmail" Runat="server" ErrorMessage="?" Display="Dynamic"
                    ControlToValidate="txtEmail">
                </asp:RequiredFieldValidator><asp:RegularExpressionValidator ID="revEmail" Runat="server"
                    ErrorMessage="!" Display="Dynamic" ControlToValidate="txtEmail" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*">
                </asp:RegularExpressionValidator></td>
        </tr></table>
    <table style="width: 100%" cellpadding="5"><tr>
            <td style="width: 120px" align="center">
                <asp:Image ID="imgHumans" Runat="server" /></td>
            <td>
                <br />
                <asp:RadioButton ID="rdbHumans" Runat="server" GroupName="Fraction" Checked="True"
                    Text="I choose the Alliance" Font-Bold="True" /><br />
                The noble warriors of humanity employ both a strong military and powerful magics
                in the defense of their shining kingdoms. Both knights and wizards fight side by
                side on the field of battle against those who would threaten the sanctity and peace
                of the Alliance. Although the Alliance has all but fallen apart, the valiant citizens
                of Azeroth and Lordaeron have once again taken up arms against the enemies of humanity.</td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;<asp:Image ID="imgOrcs" Runat="server" /></td>
            <td>
                <br />
                <asp:RadioButton ID="rdbOrcs" Runat="server" GroupName="Fraction" Text="I fight for the Horde"
                    Font-Bold="True" /><br />
                The Orcs, who once cultivated a quiet Shamanistic society upon the world of Draenor,
                were corrupted by the chaos magics of the Burning Legion and formed into a voracious,
                unstoppable Horde. Lured to the world of Azeroth through a dimensional gateway,
                the Horde was manipulated into waging war against the human nations of Azeroth and
                Lordaeron. Hoping that the Horde would conquer the mortal armies of Azeroth, the
                Burning Legion made ready for its final invasion of the unsuspecting world.</td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;<asp:Image ID="imgNightElves" Runat="server" /></td>
            <td>
                <br />
                <asp:RadioButton ID="rdbNightElves" Runat="server" GroupName="Fraction" Text="I fight side by side with the Night Elves"
                    Font-Bold="True" /><br />
                The reclusive Night Elves were the first race to awaken in the World of Warcraft.
                These shadowy, immortal beings were the first to study magic and let it loose throughout
                the world nearly ten thousand years before Warcraft I. The Night Elves' reckless
                use of magic drew the Burning Legion into the world and led to a catastrophic war
                between the two titanic races. The Night Elves barely managed to banish the Legion
                from the world, but their wondrous homeland was shattered and drowned by the sea.
                Ever since, the Night Elves refused to use magic for fear that the dreaded Legion
                would return. The Night Elves closed themselves off from the rest of the world and
                remained hidden atop their holy mountain of Hyjal for many thousands of years. As
                a race, Night Elves are typically honorable and just, but they are very distrusting
                of the 'lesser races' of the world. They are nocturnal by nature and their shadowy
                powers often elicit the same distrust that they have for their mortal neighbors.</td>
        </tr>
        <tr>
            <td align="center">
                &nbsp;<asp:Image ID="imgUndead" Runat="server" /></td>
            <td>
                <br />
                <asp:RadioButton ID="rdbUndead" Runat="server" GroupName="Fraction" Text="I choose the Scorge"
                    Font-Bold="True" /><br />
                The horrifying Undead army, called the Scourge, consists of thousands of walking
                corpses, disembodied spirits, damned mortal men and insidious extra-dimensional
                entities. The Scourge was created by the Burning Legion for the sole purpose of
                sowing terror across the world in anticipation of the Legion's inevitable invasion.
                The Undead are ruled by Ner'zhul, the Lich King, who lords over the icy realm of
                Northrend from his frozen throne. Ner'zhul commands the terrible plague of undeath,
                which he sends ever southward into the human lands. As the plague encroaches on
                the southlands, more and more humans fall prey to Ner'zhul's mental control and
                life-draining sickness every day. In this way, Ner'zhul has swelled the ranks of
                the already considerable Scourge. The Undead employ necromantic magics and the elemental
                powers of the cold north against their enemies.</td>
        </tr></table>
    <br />
    <br />
    <div style="text-align: center">
        <asp:CheckBox ID="chkPlayerAgreement" Runat="server" />
        I've read and accepted the
        <asp:HyperLink ID="hplPlayerAgreement" Runat="server" NavigateUrl="~/Portal/Misc/PlayerAgreement.aspx">Player Agreement</asp:HyperLink>.<br />
        <br />
        <asp:Button ID="btnSignUp" Runat="server" Text="Sign up ..." OnClick="btnSignUp_Click" /></div>
</asp:Content>
