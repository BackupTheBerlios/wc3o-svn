using System;

namespace Wc3o.Pages.Game.Details {
	public partial class Map_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (RemoteScripting.InvokeMethod(Page)) return;
		}


		public string GetMap(string coordinates) {
			string result = "";
			Player player = Wc3o.Game.CurrentPlayer;
			Coordinate coordinate = new Coordinate(coordinates);

			int startX = coordinate.X - (int)(Configuration.Sectors_To_Show_On_Map / 2);
			int startY = coordinate.Y - (int)(Configuration.Sectors_To_Show_On_Map / 2);
			if (startX <= 0)
				startX = 1;
			else if (startX + Configuration.Sectors_To_Show_On_Map > Configuration.Map_Size)
				startX = Configuration.Map_Size - Configuration.Sectors_To_Show_On_Map+1;
			if (startY <= 0)
				startY = 1;
			else if (startY + Configuration.Sectors_To_Show_On_Map > Configuration.Map_Size)
				startY = Configuration.Map_Size - Configuration.Sectors_To_Show_On_Map+1;

			result += player.Gfx + "@" + Configuration.Sectors_To_Show_On_Map + "@" + (startX + (int)(Configuration.Sectors_To_Show_On_Map / 2)) + "@" + (startY + (int)(Configuration.Sectors_To_Show_On_Map / 2)) + "@";

			for (int y = startY; y < startY + Configuration.Sectors_To_Show_On_Map; y++)
				for (int x = startX; x < startX + Configuration.Sectors_To_Show_On_Map; x++) {
					Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(x, y)];
					string type = "0";
					foreach (Building b in sector.Buildings)
						if (b.IsAvailable)
							if (b.Type == BuildingType.TownHall) {
								type = "1";
								break;
							}
							else if (b.Type == BuildingType.Keep) {
								type = "2";
								break;
							}
							else if (b.Type == BuildingType.Castle) {
								type = "3";
								break;
							}
							else if (b.Type == BuildingType.GreatHall) {
								type = "4";
								break;
							}
							else if (b.Type == BuildingType.Stronghold) {
								type = "5";
								break;
							}
							else if (b.Type == BuildingType.Fortress) {
								type = "6";
								break;
							}
							else if (b.Type == BuildingType.Necropolis) {
								type = "7";
								break;
							}
							else if (b.Type == BuildingType.HallsOfTheDead) {
								type = "8";
								break;
							}
							else if (b.Type == BuildingType.BlackCitadel) {
								type = "9";
								break;
							}
							else if (b.Type == BuildingType.TreeOfLife) {
								type = "10";
								break;
							}
							else if (b.Type == BuildingType.TreeOfAges) {
								type = "11";
								break;
							}
							else if (b.Type == BuildingType.TreeOfEternity) {
								type = "12";
								break;
							}

					if (type == "0")
						if (sector is GoldAndLumberSector)
							type = "13";
						else if (sector is GoldSector)
							type = "14";
						else if (sector is LumberSector)
							type = "15";
						else if (sector is HealingSector)
							type = "16";
						else if (sector is MercenarySector)
							type = "17";

					string color = Configuration.Color_League;
					if (sector.Owner == null)
						color = Configuration.Color_Neutral;
					else if (sector.Owner == player)
						color = Configuration.Color_Player;
					else if (player.IsAlly(sector.Owner))
						color = Configuration.Color_Ally;
					else if (player.CanAttack(sector.Owner))
						color = Configuration.Color_Enemy;

					string owner = "";
					if (sector.Owner != null)
						owner = sector.Owner.FullName;

					result += type + "$" + color + "$" + sector + "$" + sector.Coordinate.X + " : " + sector.Coordinate.Y + "$" + sector.Coordinate + "$" + owner + "@";
				}

			return result;
		}


		public string GetInfo(string coordinates) {
			string result = "";
			Player player = Wc3o.Game.CurrentPlayer;
			Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(coordinates)];

			result += player.Gfx + "@" + sector.Coordinate + "@" + sector + "@";
			if (sector.Owner == null)
				result += "@@";
			else
				result += sector.Owner.Name + "@" + sector.Owner.FullName + "@";

			if (player.HasView(sector)) {
				result += "1@";
				bool playerUnits = false;
				bool playerBuildings = false;
				bool allyUnits = false;
				bool allyBuildings = false;
				bool enemyUnits = false;
				bool enemyBuildings = false;
				bool leagueUnits = false;
				bool leagueBuildings = false;
				bool neutralUnits = false;
				bool neutralBuildings = false;

				foreach (Unit u in sector.Units)
					if (u.IsAvailable || u.IsWorking) {
						if (u.Owner == null)
							neutralUnits = true;
						else if (u.Owner == player)
							playerUnits = true;
						else if (player.IsAlly(u.Owner))
							allyUnits = true;
						else if (player.CanAttack(u.Owner))
							enemyUnits = true;
						else
							neutralUnits = true;
					}

				foreach (Building b in sector.Buildings) {
					if (sector.Owner == null)
						neutralBuildings = true;
					else if (sector.Owner == player)
						playerBuildings = true;
					else if (player.IsAlly(sector.Owner))
						allyBuildings = true;
					else if (player.CanAttack(sector.Owner))
						enemyBuildings = true;
					else
						neutralBuildings = true;
					break;
				}


				if (playerUnits || playerBuildings) {
					result += Configuration.Color_Player + "@";
					if (playerUnits) result += "1@"; else result += "0@";
					if (playerBuildings) result += "1@"; else result += "0@";
				}
				if (neutralUnits || neutralBuildings) {
					result += Configuration.Color_Neutral + "@";
					if (neutralUnits) result += "1@"; else result += "0@";
					if (neutralBuildings) result += "1@"; else result += "0@";
				}
				if (allyUnits || allyBuildings) {
					result += Configuration.Color_Ally + "@";
					if (allyUnits) result += "1@"; else result += "0@";
					if (allyBuildings) result += "1@"; else result += "0@";
				}
				if (enemyUnits || enemyBuildings) {
					result += Configuration.Color_Enemy + "@";
					if (enemyUnits) result += "1@"; else result += "0@";
					if (enemyBuildings) result += "1@"; else result += "0@";
				}
				if (leagueUnits || leagueBuildings) {
					result += Configuration.Color_League + "@";
					if (leagueUnits) result += "1@"; else result += "0@";
					if (leagueBuildings) result += "1@"; else result += "0@";
				}


			}
			else
				result += "0@";

			return result;
		}

	}
}
