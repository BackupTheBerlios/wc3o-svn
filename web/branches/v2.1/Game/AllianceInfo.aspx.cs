using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Wc3o.Pages.Game {
	public partial class AllianceInfo_aspx : System.Web.UI.Page {

		protected void Page_PreInit(object sender, EventArgs e) {
			Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			Alliance alliance = null;
			try {
				alliance=Wc3o.Game.GameData.Alliances[Request.QueryString["Alliance"]];
			} catch { }
			if (alliance == null)
				Response.Redirect("Default.aspx");

			Player user = Wc3o.Game.CurrentPlayer;

			imgAlliance.ImageUrl = alliance.Image;
			lblMotto.Text = alliance.Description;
			lblName.Text = alliance.Name + " - " + alliance.FullName;

			System.Data.DataTable members = new System.Data.DataTable();
			members.Columns.Add("Image", typeof(String));
			members.Columns.Add("Name", typeof(String));
			members.Columns.Add("Score", typeof(String));
			members.Columns.Add("Rank", typeof(String));

			foreach (Player member in alliance.Members) {
				if (member.IsAccepted) {
					System.Data.DataRow row = members.NewRow();
					row["Image"] = "<a href='PlayerInfo.aspx?Player=" + member.Name + "'><img src='" + user.Gfx + "/" + member.FractionInfo.SmallEmblem + "' /></a>";
					if (member == alliance.Leader)
						row["Name"] = "<b>" + member.MailLink + "</b>";
					else
						row["Name"] = member.MailLink;
					row["Score"] = Wc3o.Game.Format(member.Score);
					row["Rank"] = member.RankLeague;
					members.Rows.Add(row);
				}
			}

			grdMembers.DataSource = members;
			grdMembers.DataBind();
		}

	}
}
