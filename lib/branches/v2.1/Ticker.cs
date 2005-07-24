using System;
using System.Collections.Generic;

namespace Wc3o.Tick {
	public class Ticker {

		#region " Constructor "
		System.Timers.Timer saveTimer, ressourceTimer, rankingTimer;
		public Ticker() {
			Start();
		}
		#endregion

		#region " GetInterval "
		long GetInterval(DateTime tick) {
			long i = Convert.ToInt64(((TimeSpan)(tick - DateTime.Now)).TotalSeconds);
			if (i < 5)
				i = new Random().Next(1, 5);
			return i * 1000;
		}
		#endregion

		#region " Save Tick "
		private void SaveTick(Object sender, System.Timers.ElapsedEventArgs args) {
			Game.Logger.Log("Save Tick ... ", Log.LogType.System);
			saveTimer.Interval = Configuration.Seconds_For_Save_Tick * 1000;
			GameData.Save(Game.GameData);
			Game.Logger.Log("Save Tick done.", Log.LogType.System);
		}
		#endregion

		#region " Ranking Tick "
		private void RankingTick(Object sender, System.Timers.ElapsedEventArgs args) {
			double difference = ((TimeSpan)(Game.GameData.Ticks.RankingTick - DateTime.Now)).TotalSeconds;
			int count = -(int)(difference / Configuration.Seconds_For_Ranking_Tick);

			if (count > 0) {
				Game.Logger.Log("Doing " + count + " Ranking Ticks ...", Log.LogType.System);

				for (int i = count; i > 0; i--) {
					try {
						new Ranking().CalculateRanking();
					} catch (Exception ex) {
						Game.Logger.Log("Ranking Tick Exception: " + ex.Message, Log.LogType.System);
					}
				}

				#region " Maintainance "
				//Some maintainance stuff
				foreach (System.IO.FileInfo f in new System.IO.DirectoryInfo(Configuration.Physical_Application_Path + "\\Game\\Logs\\BattleLogs\\").GetFiles())
					if (f.CreationTime < DateTime.Now.AddDays(-Configuration.Days_To_Keep_BattleLogs))
						f.Delete();

				List<Player> l = new List<Player>();
				foreach (Player p in Game.GameData.Players.Values)
					if (p.Online < DateTime.Now.AddDays(-Configuration.Days_To_Keep_Dead_Players))
						l.Add(p);

				foreach (Player p in l) {
					p.Destroy();
					Game.Logger.Log("Removed player '" + p.FullName + "'.", Log.LogType.System);
				}
				#endregion

				Game.GameData.Ticks.RankingTick = Game.GameData.Ticks.RankingTick.AddSeconds(Configuration.Seconds_For_Ranking_Tick * (count + 1));
				Game.Logger.Log(" ... Ranking Ticks done.", Log.LogType.System);
			}

			rankingTimer.Interval = GetInterval(Game.GameData.Ticks.RessourceTick);
		}
		#endregion

		#region " Ressource Tick "
		private void RessourceTick(Object sender, System.Timers.ElapsedEventArgs args) {
			double difference = ((TimeSpan)(Game.GameData.Ticks.RessourceTick - DateTime.Now)).TotalSeconds;
			int count = -(int)(difference / Configuration.Seconds_For_Ressource_Tick);

			if (count > 0) {
				Game.Logger.Log("Doing " + count + " Ressource Ticks ...", Log.LogType.System);

				for (int i = count; i > 0; i--) {
					foreach (Player p in Game.GameData.Players.Values) {
						p.Gold += p.GoldPerTick;
						p.Lumber += p.LumberPerTick;

						#region " Healing Of Units "
						foreach (Unit u in p.Units) {
							if (!u.IsInTraining) {
								if (p.Fraction == Fraction.Humans || p.Fraction == Fraction.Orcs)
									u.Hitpoints += Configuration.Healing_Orc_Humans;
								else if (p.Fraction == Fraction.NightElves && Game.IsNight)
									u.Hitpoints += Configuration.Healing_NightElves;
								else if (p.Fraction == Fraction.Undead && u.Sector.Owner != null && u.Sector.Owner.Fraction == Fraction.Undead && (u.IsAvailable || u.IsWorking || (u.IsMoving && u.Action == UnitAction.Returning)))
									u.Hitpoints += Configuration.Healing_Undead;
								if (u.Hitpoints > u.Info.Hitpoints)
									u.Hitpoints = u.Info.Hitpoints;
							}
						}

						#endregion
					}
				}

				Game.GameData.Ticks.RessourceTick = Game.GameData.Ticks.RessourceTick.AddSeconds(Configuration.Seconds_For_Ressource_Tick * (count + 1));
				Game.Logger.Log(" ... Ressource Ticks done.", Log.LogType.System);
			}

			rankingTimer.Interval = GetInterval(Game.GameData.Ticks.RessourceTick);
		}
		#endregion

		#region " Start/Stop "
		public void Start() {
			rankingTimer = new System.Timers.Timer();
			rankingTimer.Elapsed += new System.Timers.ElapsedEventHandler(RankingTick);
			rankingTimer.Interval = GetInterval(Game.GameData.Ticks.RankingTick);

			ressourceTimer = new System.Timers.Timer();
			ressourceTimer.Elapsed += new System.Timers.ElapsedEventHandler(RessourceTick);
			ressourceTimer.Interval = GetInterval(Game.GameData.Ticks.RessourceTick);

			saveTimer = new System.Timers.Timer();
			saveTimer.Elapsed += new System.Timers.ElapsedEventHandler(SaveTick);
			saveTimer.Interval = Configuration.Seconds_For_Save_Tick * 1000;

			rankingTimer.Enabled = true;
			ressourceTimer.Enabled = true;
			saveTimer.Enabled = true;
		}


		public void Stop() {
			rankingTimer.Enabled = false;
			saveTimer.Enabled = false;
			ressourceTimer.Enabled = false;
		}
		#endregion

	}
}
