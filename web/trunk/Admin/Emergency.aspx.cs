using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Admin {
	public partial class Ticks_aspx : System.Web.UI.Page {

		protected void Button1_Click(object sender, EventArgs e) {
			Player player;
			foreach (Alliance a in Game.GameData.Alliances.Values)
				if (a.Name == "SoD") {
					Response.Write("Alliance Found");
					foreach (Player p in a.Members)
						if (a.Name == "HI_MOM") {
							Response.Write("Player Found");
							player = p;
							Game.GameData.Players[player.Name] = player;
							break;
						}
				}




		}
}
}
