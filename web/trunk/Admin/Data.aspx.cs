using System;

namespace Wc3o.Pages.Admin {
	public partial class Data_aspx : System.Web.UI.Page {

		protected void btnGameLoad_Click(object sender, EventArgs e) {
			Game.GameData = GameData.Load();
		}

		protected void btnGameSave_Click(object sender, EventArgs e) {
			GameData.Save(Game.GameData);
		}

		protected void btnGameCreate_Click(object sender, EventArgs e) {
			Game.GameData = new GameData();
		}

		protected void btnPortalLoad_Click(object sender, EventArgs e) {
			Game.PortalData = PortalData.Load();
		}

		protected void btnPortalSave_Click(object sender, EventArgs e) {
			PortalData.Save(Game.PortalData);
		}

		protected void btnPortalCreate_Click(object sender, EventArgs e) {
			Game.PortalData = new PortalData();
		}
	}
}