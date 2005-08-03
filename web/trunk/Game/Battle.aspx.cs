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

			//the player (aka the attacker) is always the current player
			player = Wc3o.Game.CurrentPlayer;

			//get the enemy
			if (Request.QueryString["Enemy"] != null)
				if (Request.QueryString["Enemy"].ToString() == "-")
					enemy = null;
				else
					enemy = Wc3o.Game.GameData.Players[Request.QueryString["Enemy"]];
			else {
				Wc3o.Game.Message(Master, "This enemy does not exist.", MessageType.Error);
				return;
			}

			//get the sector
			try {
				sector = Wc3o.Game.GameData.Sectors[new Coordinate(Request.QueryString["Sector"])];
			} catch {
				Wc3o.Game.Message(Master, "This sector does not exist.", MessageType.Error);
				return;
			}

			if (!IsPostBack) {
				if (!player.CanAttack(enemy)) {
					Wc3o.Game.Message(Master, "You cannot attack this enemy.", MessageType.Error);
					return;
				}

				bool hasUnits = false;
				bool hasAlliedUnits = false;
				foreach (Unit u in sector.Units)
					if (u.IsAvailable)
						if (player == u.Owner)
							hasUnits = true;
						else if (player.IsAlly(u.Owner) && player.HasAHigherAllianceRank(u.Owner))
							hasAlliedUnits = true;
				if (!hasUnits) {
					Wc3o.Game.Message(Master, "You have no available units on this sector.", MessageType.Error);
					return;
				}

				if (hasAlliedUnits)
					if (Request.QueryString["Type"] != null && Request.QueryString["Type"].ToString() == "Allied")
						chkAllied.Checked = true;
					else
						chkAllied.Enabled = false;

				if (enemy == null)
					lblHostile.Text = "some Creeps";
				else
					lblHostile.Text = enemy.FullName;
				lblSector.Text = sector.ToString();

				pnlAttack.Visible = false;
				pnlSettings.Visible = true;
			}
		}

		protected void btnAttackSettings_Click(object sender, EventArgs e) {
			if (!player.CanAttack(enemy)) {
				Response.Redirect("Sector.aspx");
				return;
			}

			bool hasUnits = false;
			bool hasAlliedUnits = false;
			foreach (Unit u in sector.Units)
				if (u.IsAvailable)
					if (player == u.Owner)
						hasUnits = true;
					else if (player.IsAlly(u.Owner) && player.HasAHigherAllianceRank(u.Owner))
						hasAlliedUnits = true;
			if (!hasUnits) {
				Response.Redirect("Sector.aspx");
				return;
			}

			Army attackersArmy = new Army(player);
			Army defendersArmy = new Army(enemy);

			List<Player> otherAttackers = new List<Player>();
			List<Player> otherDefenders = new List<Player>();

			foreach (Unit u in sector.Units)
				if (u.IsAvailable || u.IsWorking) {
					if (u.Owner == player)
						attackersArmy.Entities.Add(u);
					else if (chkAllied.Checked && player.IsAlly(u.Owner) && player.HasAHigherAllianceRank(u.Owner) && !u.IsWorking) {
						if (!otherAttackers.Contains(u.Owner))
							otherAttackers.Add(u.Owner);
						attackersArmy.Entities.Add(u);
					}
					else if (u.Owner == enemy)
						defendersArmy.Entities.Add(u);
					else if (enemy.IsAlly(u.Owner)) {
						if (!otherDefenders.Contains(u.Owner))
							otherDefenders.Add(u.Owner);
						defendersArmy.Entities.Add(u);
					}
				}

			if (sector.Owner == enemy)
				foreach (Building b in sector.Buildings)
					if (!b.IsInConstruction)
						defendersArmy.Entities.Add(b);


			foreach (Entity f in attackersArmy.Entities)
				f.Destroy();
			foreach (Entity f in defendersArmy.Entities)
				f.Destroy();
			Battle battle = new Battle(attackersArmy, defendersArmy, sector);
			BattleResult result = battle.DoBattle(int.Parse(drpRounds.SelectedValue), int.Parse(drpBuildingRounds.SelectedValue), chkArrival.Checked);
			foreach (Entity f in attackersArmy.Entities)
				f.Create(sector);
			foreach (Entity f in defendersArmy.Entities)
				f.Create(sector);


			//TODO proceed here

			string message = "";
			switch (result.Status) {
				case BattleStatus.AttackerWins:
					message += "You've been attacked by " + attackersArmy.Owner.FullName + " at " + sector.ToString() + " and suffered a defeat. You've lost " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber. In addition, you lost <ul>";
					lblMessage.Text = "You won the battle and " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber.";
					break;
				case BattleStatus.DefenderWins:
					message += "You've been attacked by " + attackersArmy.Owner.FullName + " at " + sector.ToString() + ", but your surpreme forces were able to defeat the enemy. Therefore you won " + Wc3o.Game.Format(-result.Gold) + " gold and " + Wc3o.Game.Format(-result.Lumber) + " lumber. Your losses were <ul>";
					lblMessage.Text = "You've lost the battle and " + Wc3o.Game.Format(-result.Gold) + " gold and " + Wc3o.Game.Format(-result.Lumber) + " lumber.";
					break;
				case BattleStatus.NonWins:
					message += "You've been attacked by " + attackersArmy.Owner.FullName + " at " + sector.ToString() + ", but none of you was able to win that battle. Nevertheless, you lost " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber. Your losses were <ul>";
					lblMessage.Text = "This battle had no winner. But you were able to gain " + Wc3o.Game.Format(result.Gold) + " gold and " + Wc3o.Game.Format(result.Lumber) + " lumber.";
					break;
			}

			lblHostileLosses.Text += "<table style='width:100%;' cellspacing='0' cellpadding='0' border='0'>";
			foreach (Entity f in result.DefendersLosses) {
				message += "<li>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</li>";
				lblHostileLosses.Text += "<tr><td style='width:1px;'><img src='" + player.Gfx + f.Info.Image + "'></td><td align='left'>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</td></tr>";
			}
			lblHostileLosses.Text += "</table>";

			message += "</ul>" + attackersArmy.Owner.FullName + " lost:<ul>";
			lblPlayerLosses.Text = "<table style='width:300px;' cellspacing='0' cellpadding='0' border='0'>";
			foreach (Entity f in result.PlayerLosses) {
				message += "<li>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</li>";
				lblPlayerLosses.Text += "<tr><td style='width:1px;'><img src='" + player.Gfx + f.Info.Image + "'></td><td align='left'>" + Wc3o.Game.Format(f.Number) + " " + f.Info.Name + "</td></tr>";
			}
			lblPlayerLosses.Text += "</table>";

			message += "</ul><br />If you need more details, take a look at the  <a href='Logs/BattleLogs/" + result.Log + "'>Battle Log</a>.";

			hplLog.NavigateUrl = "~/Game/Logs/BattleLogs/" + result.Log;

			if (enemy != null)
				new Message(defendersArmy.Owner, null, "You've been attacked by " + attackersArmy.Owner.FullName + " at " + sector.ToString() + ".", message);


			pnlAttack.Visible = true;
			pnlSettings.Visible = false;
		}

	}
}
