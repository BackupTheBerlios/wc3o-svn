using System;

namespace Wc3o.Pages.Game {
	public partial class Default_aspx : System.Web.UI.Page {

		Player player;

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;

			imgFace.ImageUrl = player.Gfx + "/" + player.Fraction + "/Face.jpg";

			lblPlayer.Text = player.FullName;
			lblFraction.Text = player.FractionInfo.Name;
			lblGold.Text = Wc3o.Game.Format(player.GoldPerTick);
			imgGold.ImageUrl = player.Gfx + "/Game/Gold.gif";
			lblLumber.Text = Wc3o.Game.Format(player.LumberPerTick);
			imgLumber.ImageUrl = player.Gfx + "/Game/Lumber.gif";
			lblRegistration.Text = Wc3o.Game.Format(Wc3o.Game.GetCorrectedDate(player.Registration), false);
			if (player.IsProtected)
				lblProtection.Text = "You are new on the battlefield, therefore you are protected for another " + Wc3o.Game.TimeSpan(player.Registration.AddHours(Configuration.Hours_To_Be_Protected)) + ". You cannot be attacked by other players, and you cannot attack them.";

			lblScore.Text = Wc3o.Game.Format(player.Score);
			lblRank.Text = Wc3o.Game.Format(player.LeagueRank);
			lblLeague.Text = Wc3o.Game.Format(player.League);

			hplDelete.NavigateUrl = "~/Portal/Players/Delete.aspx";
			hplDetails.NavigateUrl = "~/Portal/Players/Details.aspx";
			hplLogoff.NavigateUrl = "~/Portal/Players/Logoff.aspx";

			lblSectors.Text = "<ul>";
			foreach (Sector s in player.Sectors)
				lblSectors.Text += "<li><a href='Sector.aspx?Sector=" + s.Coordinate + "'>" + s.FullName + "</a></li>";
			lblSectors.Text += "</ul>";

		}
	}
}
