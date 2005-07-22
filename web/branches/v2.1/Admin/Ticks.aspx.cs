using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Admin {
	public partial class Ticks_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				txtRanking.Text = Game.GameData.Ticks.RankingTick.ToString();
				txtRessource.Text = Game.GameData.Ticks.RessourceTick.ToString();
			}
		}

		protected void btnRessource_Click(object sender, EventArgs e) {
			Game.GameData.Ticks.RessourceTick = DateTime.Parse(txtRessource.Text);
			Game.Ticker = new Wc3o.Tick.Ticker();
			Response.Redirect("Ticks.aspx");
		}

		protected void btnRanking_Click(object sender, EventArgs e) {
			Game.GameData.Ticks.RankingTick = DateTime.Parse(txtRanking.Text);
			Game.Ticker = new Wc3o.Tick.Ticker();
			Response.Redirect("Ticks.aspx");
		}
}
}