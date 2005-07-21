using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Overview_aspx : System.Web.UI.Page {

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		Player player;

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;
			List<Sector> l = new List<Sector>();

			foreach (Sector s in player.Sectors)
				l.Add(s);

			foreach (Unit u in player.Units)
				if (!l.Contains(u.Sector))
					l.Add(u.Sector);

			foreach (Sector s in l) {
				string c = "League";
				if (s.Owner == player)
					c = "Player";
				else if (s.Owner == null)
					c = "Neutral";
				else if (s.Owner.IsAlly(player))
					c = "Ally";
				else if (player.CanAttack(s.Owner))
					c = "Enemy";

				string hostileUnits = "";
				if (player.HasView(s)) {
					foreach (Unit u in s.Units)
						if (!player.IsAlly(u.Owner)) {
							hostileUnits = "&nbsp;&nbsp;<a href='Sector.aspx?Sector=" + s.Coordinate.ToString() + "&Refresh=Player'><img src='" + player.Gfx + "/Game/UnderSiege.gif' title='Hostile units on this sector!' /></a><br />";
							break;
						}
				}

				lblOverview.Text += "<div class='" + c + "'>" + hostileUnits + "<a href=\"javascript:LoadOverview('" + s.Coordinate + "')\"><b>" + s.FullName + "</b></a><br /><table style='width: 100%' cellspacing='5' cellpadding='0' border='0'><tr><td style='width: 50%;vertical-align:top;'><div id='u_" + s.Coordinate + "' name='u_" + s.Coordinate + "'></div></td><td style='vertical-align:top;'><div id='b_" + s.Coordinate + "' name='b_" + s.Coordinate + "'></div></td></tr></table></div><br />";
			}
		}
	}
}
