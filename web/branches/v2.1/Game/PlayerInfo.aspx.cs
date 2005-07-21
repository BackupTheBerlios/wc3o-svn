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
	public partial class PlayerInfo_aspx : System.Web.UI.Page {

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			Player player = null;
			try {
				player = Wc3o.Game.GameData.Players[Request.QueryString["Player"]];
			} catch { }
			if (player == null)
				Response.Redirect("Default.aspx");
			Player user = Wc3o.Game.CurrentPlayer;
			imgFraction.ImageUrl = user.Gfx + "/" + player.FractionInfo.Emblem;
			lblName.Text = player.FullName;
			lblLogonDate.Text = Wc3o.Game.Format(player.Online, true);
			lblRegistrationDate.Text = Wc3o.Game.Format(player.Registration, true);
			lblRank.Text = Wc3o.Game.Format(player.Rank);
			lblLeague.Text = Wc3o.Game.Format(player.League);
			lblBestRank.Text = Wc3o.Game.Format((int) player.Statistics["BestRank"]);
			lblBestLeague.Text = Wc3o.Game.Format(player.BestLeague);
			hplMessage.NavigateUrl = "Mail.aspx?Recipient=" + player.Name;
			lblDescription.Text = player.Description;

			lblSectors.Text = "<ul>";
			foreach (Wc3o.Sector s in player.Sectors)
				lblSectors.Text += "<li><a href='Sector.aspx?Sector=" + s.Coordinate.X + "_" + s.Coordinate.Y + "'>" + s.FullName + "</a></li>";

			lblSectors.Text += "</ul>";
		}

	}
}
