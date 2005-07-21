using System;

namespace Wc3o.Pages.Game {
	public partial class Details_aspx : System.Web.UI.Page {

		Player player;

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;

			if (RemoteScripting.InvokeMethod(Page)) return;
		}

		#region " GetUnits "
		public string GetUnits(string div, string s, string t, string isOverview) {
			Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(s)];
			Player p = player; //if name is empty, it is the current player
			if (t == "-") //if name is "-", it is the creeps
				p = null;
			else if (t.Length > 0) //otherwise, it's another player
				p = Wc3o.Game.GameData.Players[t];

			string unitOwner = "";
			string isUnitOwnerAlly = "1";
			string canAttack = "";
			if (isOverview != "1") { //it is always the current player when it is an overview
				if (p == null)
					unitOwner = "-";
				else if (p != player)
					unitOwner = p.Name;
				if (!player.IsAlly(p))
					isUnitOwnerAlly = "";
				if (player.CanAttack(p))
					canAttack = "1";
			}

			string sectorOwner = "";
			if (sector.Owner != player)
				if (sector.Owner == null)
					sectorOwner = "-";
				else
					sectorOwner = sector.Owner.Name;
			string isSectorOwnerAlly = "";
			if (player.IsAlly(sector.Owner))
				isSectorOwnerAlly = "1";

			string result = div + "@" + s + "@" + isOverview + "@" + unitOwner + "@" + isUnitOwnerAlly + "@" + sectorOwner + "@" + isSectorOwnerAlly + "@" + canAttack + "@" + Wc3o.Game.Gfx + "@";

			bool hasView = player.HasView(sector);

			foreach (Unit u in sector.Units)
				//You see the units when a) it is a unit of the selected player and b) it is a allied unit or you have view on the sector
				if (u.Owner == p && ((player.IsAlly(u.Owner)) || (hasView && (u.IsAvailable || u.IsInTraining || u.IsWorking || (u.IsReturning && u.IsVisible) || (u.IsMoving && u.IsVisible && ((TimeSpan)(u.Date - DateTime.Now)).TotalMinutes <= Configuration.Minutes_To_See_Arriving_Units))))) {
					string action = "";
					if (u.IsInTraining)
						action = "1";
					else if (u.IsWorking && u.Action == UnitAction.WorkForGold)
						action = "2";
					else if (u.IsWorking && u.Action == UnitAction.WorkForLumber)
						action = "3";
					else if (u.IsMoving)
						action = "4";
					else if (u.IsReturning)
						action = "5";

					string sourceSector = "";
					if (u.SourceSector != null)
						sourceSector = u.SourceSector.Coordinate.ToString();

					string source = "";
					if (u.SourceSector != null)
						source = u.SourceSector.FullName;

					int damage = 100 - (int)(100 / ((double)u.UnitInfo.Hitpoints) * ((double)u.Hitpoints));

					string gold = "";
					if (u.UnitInfo.ForGold)
						gold = "1";
					string lumber = "";
					if (u.UnitInfo.ForLumber)
						lumber = "1";

					#region " Morph units "
					string morph = "$";
					UnitInfo morphTo = null;
					if (action.Length <= 0) {
						switch (u.Type) {
							case UnitType.DruidOfTheClawBearForm:
								morphTo = UnitInfo.Get(UnitType.DruidOfTheClawDruidForm);
								break;
							case UnitType.DruidOfTheClawDruidForm:
								morphTo = UnitInfo.Get(UnitType.DruidOfTheClawBearForm);
								break;
							case UnitType.DruidOfTheTalonCrowForm:
								morphTo = UnitInfo.Get(UnitType.DruidOfTheTalonDruidForm);
								break;
							case UnitType.DruidOfTheTalonDruidForm:
								morphTo = UnitInfo.Get(UnitType.DruidOfTheTalonCrowForm);
								break;
							case UnitType.Peasant:
								morphTo = UnitInfo.Get(UnitType.Militia);
								break;
							case UnitType.Militia:
								morphTo = UnitInfo.Get(UnitType.Peasant);
								break;
							case UnitType.Hippogryph:
								morphTo = UnitInfo.Get(UnitType.HippogryphRider);
								break;
							case UnitType.HippogryphRider:
								morphTo = UnitInfo.Get(UnitType.Hippogryph);
								break;
							case UnitType.Acolyte:
								morphTo = UnitInfo.Get(UnitType.Shade);
								break;
							case UnitType.ObsidianStatue:
								morphTo = UnitInfo.Get(UnitType.Destroyer);
								break;
						}
					}
					if (morphTo != null)
						morph = "$" + morphTo.Name;
					#endregion

					result += u.UnitInfo.Name + "$" + u.Type.ToString() + "$" + Wc3o.Game.Format(u.Number) + "$" + u.Info.Image + "$" + Wc3o.Game.TimeSpan(u.Date) + "$" + action + "$" + sourceSector + "$" + source + "$" + u.GetHashCode().ToString() + "$" + Wc3o.Game.Format(damage) + "$" + gold + "$" + lumber + morph + "@";
				}
			return result;
		}
		#endregion

		#region " GetBuildings "
		public string GetBuildings(string div, string s, string t, string isOverview) {
			Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(s)];
			Player p = player; //if name is empty, it is the current player
			if (t == "-") //if name is "-", it is the creeps
				p = null;
			else if (t.Length > 0) //otherwise, it's another player
				p = Wc3o.Game.GameData.Players[t];

			string sectorOwner = "";
			if (sector.Owner != player) {
				if (sector.Owner == null)
					sectorOwner = "-";
				else
					sectorOwner = sector.Owner.Name;
			}
			string isSectorOwnerAlly = "";
			if (player.IsAlly(p))
				isSectorOwnerAlly = "1";
			string canAttack = "";
			if (player.CanAttack(p))
				canAttack = "1";

			string result = div + "@" + s + "@" + isOverview + "@" + sectorOwner + "@" + isSectorOwnerAlly + "@" + canAttack + "@"+Wc3o.Game.Gfx+"@";

			bool hasView = player.HasView(sector);

			if (sector.Owner == p && hasView) {
				foreach (Building b in sector.Buildings) {
					string action = "";
					if (b.IsInConstruction) {
						if (b.UpgradingFrom == BuildingType.None)
							action = "1";
						else
							action = "2";
					}

					int damage = 100 - (int)(100 / ((double)b.BuildingInfo.Hitpoints) * ((double)b.Hitpoints));

					result += b.Info.Name + "$" + b.Type.ToString() + "$" + Wc3o.Game.Format(b.Number) + "$" + b.Info.Image + "$" + Wc3o.Game.TimeSpan(b.Date) + "$" + action + "$" + b.GetHashCode() + "$" + Wc3o.Game.Format(damage);

					foreach (BuildingType type in b.BuildingInfo.UpgradesTo) { //upgrade buildings
						BuildingInfo i = BuildingInfo.Get(type);
						result += "$" + i.Name + "$" + i.Type;
					}

					result += "@";
				}
			}

			return result;
		}
		#endregion

		#region " GetTraining "
		public string GetTraining(string div, string s) {
			Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(s)];
			string result = div + "@" + s + "@" + player.Gfx + "@";

			if (sector.Owner != player)
				return result;

			foreach (UnitType t in Enum.GetValues(typeof(UnitType))) {
				UnitInfo i = UnitInfo.Get(t);
				if (Wc3o.Game.IsAvailable(player, sector, i))
					result += i.Name + "$" + i.CreateImage + "$" + i.Gold + "$" + i.Lumber + "$" + i.Food + "$" + i.Type + "@";
			}
			return result;
		}
		#endregion

		#region " GetConstructing "
		public string GetConstructing(string div, string s) {
			Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(s)];
			string result = div + "@" + s + "@";

			if (sector.Owner != player)
				return result;

			foreach (BuildingType t in Enum.GetValues(typeof(BuildingType))) {
				BuildingInfo i = BuildingInfo.Get(t);

				if (Wc3o.Game.IsAvailable(player, sector, i)) {
					int finished = 0;
					int constructing = 0;
					foreach (Building b in sector.Buildings) //counts the finished and constructing buildings on this sector
						if (b.Type == t && b.IsInConstruction)
							constructing += b.Number;
						else if (b.Type == t)
							finished += b.Number;
					
					result += i.Name + "$" + player.Gfx + i.CreateImage + "$" + Wc3o.Game.Format(finished) + "$" + Wc3o.Game.Format(constructing) + "$" + Wc3o.Game.Format(i.Gold) + "$" + player.Gfx + "/Game/Gold.gif" + "$" + Wc3o.Game.Format(i.Lumber) + "$" + player.Gfx + "/Game/Lumber.gif" + "$" + i.Type + "@";
				}
			}
			return result;
		}
		#endregion

		#region " GetNavigation "
		public string GetNavigation() {
			string underSiege = "";
			foreach (Sector s in player.Sectors) {
				foreach (Unit u in s.Units)
					if (!player.IsAlly(u.Owner) && (u.IsAvailable || u.IsWorking || u.IsReturning || (u.IsMoving && u.Date.AddMinutes(Configuration.Minutes_To_See_Arriving_Units) >= DateTime.Now))) {
						underSiege = "<a href='Sector.aspx?Sector=" + s.Coordinate.ToString() + "'><img src='" + player.Gfx + "/Game/UnderSiege.gif' title='You are under siege' /></a>";
						break;
					}
			}

			string message = "";
			foreach (Message m in player.Messages)
				if (m.Unread) {
					message = "<a href='Mail.aspx'><img src='" + player.Gfx + "/Game/Message.gif' title='New message' /></a>";
					break;
				}

			return Wc3o.Game.Format(player.Gold) + "@" + Wc3o.Game.Format(player.Lumber) + "@" + Wc3o.Game.Format(player.Upkeep) + "@" + Wc3o.Game.Format(player.Food) + "@" + Wc3o.Game.TimeSpan(Wc3o.Game.GameData.Ticks.RessourceTick) + "@" + message + "@" + underSiege + "@";
		}
		#endregion

	}
}
