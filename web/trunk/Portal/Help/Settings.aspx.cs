using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Portal.Help {
	public partial class Settings_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			lblMaxPlayers.Text = Configuration.Max_Player.ToString();
			lblPlayersPerLeague.Text = Configuration.Player_Per_League.ToString();
			lblDelete.Text = Configuration.Days_To_Keep_Dead_Players.ToString();
			lblProtection.Text = Configuration.Hours_To_Be_Protected.ToString();
			lblRessourceTick.Text = Game.Format(Configuration.Seconds_For_Ressource_Tick);
			lblRankingTick.Text = Game.Format(Configuration.Seconds_For_Ranking_Tick);
			lblNightStart.Text = Configuration.Start_Night.ToString();
			lblNightEnd.Text = Configuration.End_Night.ToString();

			lblMaxFood.Text = Configuration.Max_Food.ToString();
			lblCaptureFactor.Text = Configuration.Unit_Factor_For_Annect.ToString();
			lblGoldPerWorker.Text = Configuration.Gold_Per_Ressource_Tick.ToString();
			lblLumberPerWorker.Text = Configuration.Lumber_Per_Ressource_Tick.ToString();
			lblMaxWorkers.Text = Configuration.Max_Gold_Worker_Per_Sector.ToString();
			lblMinGold.Text = Configuration.Min_Gold_Income.ToString();
			lblMinLumber.Text = Configuration.Min_Lumber_Income.ToString();

			lblUpkeepLevel1.Text = Configuration.Upkeep_Level1.ToString();
			lblFactorLevel1.Text = Configuration.Upkeep_Level1_Factor.ToString();
			lblUpkeepLevel2.Text = Configuration.Upkeep_Level2.ToString();
			lblFactorLevel2.Text = Configuration.Upkeep_Level2_Factor.ToString();

			lblMaxSectors.Text = Configuration.Max_Sectors_Per_Player.ToString();
			lblMinSectors.Text = Configuration.Min_Sectors_Per_Player.ToString();
			lblMinutes.Text = Configuration.Minutes_To_See_Arriving_Units.ToString();
			lblReturnAttack.Text = Configuration.Return_Factor_After_Attack.ToString();
			lblReturnDefend.Text = Configuration.Return_Factor_After_Defend.ToString();

			lblHealingHumansOrcs.Text = Configuration.Healing_Orc_Humans.ToString();
			lblHealingUndead.Text = Configuration.Healing_Undead.ToString();
			lblHealingNightElves.Text = Configuration.Healing_NightElves.ToString();
		}

	}

}
