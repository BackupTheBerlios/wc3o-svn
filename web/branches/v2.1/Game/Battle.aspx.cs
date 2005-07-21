using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Battle_aspx : System.Web.UI.Page {

		Player player, enemy;
		Sector sector;

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;

			if (Request.QueryString["Enemy"] != null)
				if (Request.QueryString["Enemy"].ToString() == "-")
					enemy = null;
				else
					enemy = Wc3o.Game.GameData.Players[Request.QueryString["Enemy"]];
			else
				return;
			sector = Wc3o.Game.GameData.Sectors[new Coordinate(Request.QueryString["Sector"])];

			if (!IsPostBack) {
				if (!player.CanAttack(enemy)) {
					Wc3o.Game.Message(Master, "You cannot attack this target.", MessageType.Error);
					return;
				}

				bool hasUnits = false;
				foreach (Unit u in sector.Units)
					if (u.Owner == player && u.IsAvailable) {
						hasUnits = true;
						break;
					}
				if (!hasUnits) {
					Wc3o.Game.Message(Master, "You have no available units on this sector.", MessageType.Error);
					return;
				}

				if (enemy == null)
					lblHostile.Text = "some Creeps";
				else
					lblHostile.Text = enemy.FullName;
				lblSector.Text = sector.FullName;

				pnlAttack.Visible = false;
				pnlSettings.Visible = true;
			}
		}

		protected void btnAttackSettings_Click(object sender, EventArgs e) {
			bool hasUnits = false;
			foreach (Unit u in sector.Units)
				if (u.Owner == player && u.IsAvailable) {
					hasUnits = true;
					break;
				}
			if (!hasUnits || !player.CanAttack(enemy))
				Response.Redirect("Default.aspx");

			Army attacker = new Army(player);
			Army defender = new Army(enemy);

			foreach (Unit u in sector.Units)
				if (u.IsAvailable || u.IsWorking) {
					if (u.Owner == player)
						attacker.Entities.Add(u);
					else if (u.Owner == enemy)
						defender.Entities.Add(u);
				}

			if (sector.Owner == enemy)
				foreach (Building b in sector.Buildings)
					if (!b.IsInConstruction)
						defender.Entities.Add(b);

			foreach (Entity f in attacker.Entities)
				f.Destroy();
			foreach (Entity f in defender.Entities)
				f.Destroy();

			try {
				Battle battle = new Battle(attacker, defender, sector);
				BattleResult result = battle.DoBattle(int.Parse(drpRounds.SelectedValue), int.Parse(drpBuildingRounds.SelectedValue), chkArrival.Checked);

				string message = "";
				switch (result.Status) {
					case BattleStatus.AttackerWins:
						message += "You've been attacked by " + attacker.Owner.FullName + " at " + sector.FullName + " and suffered a defeat. You've lost " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber. In addition, you lost <ul>";
						lblMessage.Text = "You won the battle and " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber.";
						break;
					case BattleStatus.DefenderWins:
						message += "You've been attacked by " + attacker.Owner.FullName + " at " + sector.FullName + ", but your surpreme forces were able to defeat the enemy. Therefore you won " + Wc3o.Game.Format(-result.Gold) + " gold and " + Wc3o.Game.Format(-result.Lumber) + " lumber. Your losses were <ul>";
						lblMessage.Text = "You've lost the battle and " + Wc3o.Game.Format(-result.Gold) + " gold and " + Wc3o.Game.Format(-result.Lumber) + " lumber.";
						break;
					case BattleStatus.NonWins:
						message += "You've been attacked by " + attacker.Owner.FullName + " at " + sector.FullName + ", but none of you was able to win that battle. Nevertheless, you lost " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber. Your losses were <ul>";
						lblMessage.Text = "This battle had no winner. But you were able to gain " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber.";
						break;
				}

				lblHostileLosses.Text += "<table style='width:100%;' cellspacing='0' cellpadding='0' border='0'>";
				foreach (Entity f in result.HostileLosses) {
					message += "<li>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</li>";
					lblHostileLosses.Text += "<tr><td style='width:1px;'><img src='" + player.Gfx + f.Info.Image + "'></td><td align='left'>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</td></tr>";
				}
				lblHostileLosses.Text += "</table>";

				message += "</ul>" + attacker.Owner.FullName + " lost:<ul>";
				lblPlayerLosses.Text = "<table style='width:300px;' cellspacing='0' cellpadding='0' border='0'>";
				foreach (Entity f in result.PlayerLosses) {
					message += "<li>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</li>";
					lblPlayerLosses.Text += "<tr><td style='width:1px;'><img src='" + player.Gfx + f.Info.Image + "'></td><td align='left'>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</td></tr>";
				}
				lblPlayerLosses.Text += "</table>";

				message += "</ul><br />If you need more details, take a look at the  <a href='Logs/BattleLogs/" + result.Log + "'>Battle Log</a>.";

				hplLog.NavigateUrl = "~/Game/Logs/BattleLogs/" + result.Log;

				if (enemy != null)
					new Message(defender.Owner, null, "You've been attacked by " + attacker.Owner.FullName + " at " + sector.FullName + ".", message);

			} catch(Exception ex) {
				Response.Write(ex.StackTrace);
			}
			finally {
				foreach (Entity f in attacker.Entities)
					f.Create(sector);
				foreach (Entity f in defender.Entities)
					f.Create(sector);
			}

			pnlAttack.Visible = true;
			pnlSettings.Visible = false;
		}

	}
}
