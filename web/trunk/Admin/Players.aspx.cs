using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Admin {
	public partial class Players_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack)
				foreach (Player p in Game.GameData.Players.Values)
					drpPlayers.Items.Add(new System.Web.UI.WebControls.ListItem(p.FullName + " [" + p.Password + "]", p.Name));
		}

		protected void btnDelete_Click(object sender, EventArgs e) {
			Game.GameData.Players[drpPlayers.SelectedValue].Destroy();
			drpPlayers.Items.Remove(drpPlayers.SelectedItem);
		}

		protected void btnRessources_Click(object sender, EventArgs e) {
			Player p = Game.GameData.Players[drpPlayers.SelectedValue];
			p.Gold += int.Parse(txtGold.Text);
			p.Lumber += int.Parse(txtLumber.Text);
		}

		protected void btnAdmin_Click(object sender, EventArgs e) {
			Player p = Game.GameData.Players[drpPlayers.SelectedValue];
			p.IsAdmin = chkAdmin.Checked;
		}


		protected void btnShowUnits_Click(object sender, EventArgs e) {
			Player p = Game.GameData.Players[drpPlayers.SelectedValue];
			foreach (Unit u in p.Units) {
				Response.Write(u.Number + " " + u.Info.Name + " on " + u.Sector+"<br />");

			}

		}


		protected void btnGiveSector_Click(object sender, EventArgs e) {
			Game.GetSectorByName(txtGiveSector.Text).Owner = Game.GameData.Players[drpPlayers.SelectedValue];
		}
}
}
