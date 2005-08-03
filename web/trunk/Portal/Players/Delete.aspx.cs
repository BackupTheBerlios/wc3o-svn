using System;

namespace Wc3o.Pages.Portal.Players {
	public partial class Delete_aspx : System.Web.UI.Page {

		Player player;

		protected void Page_Load(object sender, EventArgs e) {
			if (!User.Identity.IsAuthenticated)
				Response.Redirect("~/Portal");

			player = Game.GameData.Players[User.Identity.Name];
		}

		protected void btnDelete_Click(object sender, EventArgs e) {
			player.Destroy();

			Response.Redirect("~/Portal");
		}

	}
}
