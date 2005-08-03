using System;

namespace Wc3o.Pages.Portal {
	public partial class Portal_master:System.Web.UI.MasterPage {

		protected void Page_Load(object sender, EventArgs e) {
			pnlLogon.Visible = false;
			pnlPlayer.Visible = false;

			hplLogo.ImageUrl = Configuration.Default_Gfx_Path + "/Portal/Logo.gif";
			hplLogo.NavigateUrl = "~/Portal/Default.aspx";

			lblPlayer.Text = Game.Format(Game.GameData.Players.Count);
			lblPlayerAvailable.Text = Game.Format(Configuration.Max_Player- Game.GameData.Players.Count);

			lblSectors.Text = Game.Format(Game.GameData.Sectors.Count);
			int sectors = 0;
			int availableSectors = 0;
			int units = 0;
			int creeps = 0;
			int buildings = 0;
			foreach (Sector s in Game.GameData.Sectors.Values) {
				
				if (s.Owner == null)
					availableSectors++;
				else
					sectors++;

				foreach (Unit u in s.Units)
					if (u.Owner == null)
						creeps += u.Number;
					else
						units += u.Number;
				foreach (Building b in s.Buildings)
					buildings += b.Number;
			}

			lblUnits.Text = Game.Format(units);
			lblCreeps.Text = Game.Format(creeps);
			lblBuildings.Text = Game.Format(buildings);

			lblSectors.Text = Game.Format(sectors);
			lblAvailableSectors.Text = Game.Format(availableSectors);

			if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated && Game.GameData.Players.ContainsKey(System.Web.HttpContext.Current.User.Identity.Name)) {
				pnlPlayer.Visible = true;
				lblUsername.Text = System.Web.HttpContext.Current.User.Identity.Name;
			}
			else {
				pnlLogon.Visible = true;
			}
		}

		protected void btnLogon_Click(object sender, EventArgs e) {
			if (Game.GameData.Players.ContainsKey(txtName.Text) && Game.GameData.Players[txtName.Text].Password == txtPassword.Text) {
				if (Request.QueryString["ReturnUrl"] == null) {
					System.Web.Security.FormsAuthentication.SetAuthCookie(txtName.Text, true);
					Response.Redirect("~/Game");
				}
				else
					System.Web.Security.FormsAuthentication.RedirectFromLoginPage(txtName.Text, true);
			}
			else
				Response.Redirect("~/Portal/Players/LogonFailed.aspx");
		}

	}
}
