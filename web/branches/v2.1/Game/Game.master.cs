using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Game_master : System.Web.UI.MasterPage {

		Player player;

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;
			player.Online = DateTime.Now;

			lblNavigation.Text = "<table cellspacing='0' cellpadding='0' width='100%' border='0'><tr><td valign='top' style='width:300px;padding-left:14px;padding-top:15px;height:155px;background-image: url(" + player.Gfx + "/" + player.Fraction + "/Navigation_Left.gif);'><table cellspacing='1' cellpadding='0' border='0'><tr><td><a title='Map' href='Map.aspx'><img src='" + player.Gfx + "/Game/Map.gif' /></a></td><td><a title='Overview' href='Overview.aspx'><img src='" + player.Gfx + "/Game/Overview.gif' /></a></td><td></td><td></td><td><a title='Frontpage' href='Default.aspx'><img src='" + player.Gfx + "/Game/Default.gif' /></a></td></tr><tr><td><a title='Sector' href='Sector.aspx'><img src='" + player.Gfx + "/Game/Sector.gif' /></a></td><td></td><td></td><td><a title='Alliance' href='Alliance.aspx'><img src='" + player.Gfx + "/Game/Alliance.gif' /></a></td><td><a title='Mail' href='Mail.aspx'><img src='" + player.Gfx + "/Game/Mail.gif' /></a></td></tr><tr><td><a title='Portal (Help)' href='../Portal'><img src='" + player.Gfx + "/Game/Portal.gif' /></a></td><td><a title='Forum' href='http://1.myfreebulletinboard.com/?mforum=wc3o' target='_blank'><img src='" + player.Gfx + "/Game/Forum.gif' /></a></td><td></td><td><a title='Player Ranking' href='Ranking.aspx'><img src='" + player.Gfx + "/Game/PlayerRanking.gif' /></a></td><td><a title='Alliance Ranking' href='Ranking.aspx?League=0'><img src='" + player.Gfx + "/Game/AllianceRanking.gif' /></a></td></tr></table></td><td valign='top' style='background-image: url(" + player.Gfx + "/" + player.Fraction + "/Navigation_Middle.gif);'><div style='text-align: center'><div style='height: 63px; width: 468px;'></div><br /><table style='width: 100%;' cellspacing='0' cellpadding='0' border='0'><tr><td style='width: 25px;' align='center'><span id='undersiege' name='undersiege'></span></td><td align='center'><span id='gold' name='gold'>-</span> <img src='" + player.Gfx + "/Game/Gold.gif' title='Gold' />&nbsp;&nbsp;&nbsp;&nbsp;<span id='lumber' name='lumber'>-</span> <img src='" + player.Gfx + "/Game/Lumber.gif' title='Lumber' />&nbsp;&nbsp;&nbsp;&nbsp;<span id='upkeep' name='upkeep'>-</span> / <span id='food' name='food'>-</span> <img src='" + player.Gfx + "/Game/Food.gif' title='Upkeep / Food' /></td><td style='width: 25px' align='center'><span id='message' name='message'></span></td></tr></table><span id='tick' name='tick'>-</span> <img src='" + player.Gfx + "/Game/Tick.gif' title='Ressource Tick' /></div></td><td style='width: 225px;background-image: url(" + player.Gfx + "/" + player.Fraction + "/Navigation_Right.gif);'></td></tr></table>";

		}
	}
}