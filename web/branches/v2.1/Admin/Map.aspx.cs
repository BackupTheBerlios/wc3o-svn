using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Admin {
	public partial class Map_aspx : System.Web.UI.Page {

		Random r = new Random();
		protected void btnCreateMap_Click(object sender, EventArgs e) {
			Game.GameData.Sectors.Clear();

			#region " Namen generieren "
			string[] part1 ={ "North", "Okkana", "West", "Clay", "East", "Blythe", "Long", "South", "Short", "Wall", "Mil", "Wood", "Walnut", "Indiana", "Green", "Saint", "Trumboro", "Strat", "Norson", "Steam", "Enter", "Pirate", "Canon", "Flag", "Pine", "Water", "Santa", "Carls", "Nash", "Love", "Little", "El", "Lennies", "Elves", "Spring" };
			string[] part2 ={ "rend", "side", "hampton", "end", "shire", "side", " Castle", " Rock", "chester", "ham", "mont", "dale", "bury", "polis", "smith", "down", " Shores", "land", "bad", "staff", " Prime", "church", "grove", "court", "core", "field" };

			List<string> names=new List<string>();
			for (int i = 0; i < part1.Length; i++)
				for (int j = 0; j < part2.Length; j++)
					names.Add(part1[i] + part2[j]);
			#endregion

			for (int x = 1; x <= Configuration.Map_Size; x++) {
				for (int y = 1; y <= Configuration.Map_Size; y++) {
					Sector newSector;
					Coordinate newCoordinate=new Coordinate(x,y);

					int randomValue = r.Next(1, 40);
					if (randomValue == 1)
						newSector = new GoldAndLumberSector(r.NextDouble() * 10,r.NextDouble() * 10,newCoordinate);
					else if (randomValue == 2 || randomValue == 3)
						newSector = new MercenarySector(r.Next(3,7),newCoordinate);
					else if (randomValue == 4 || randomValue == 5)
						newSector = new HealingSector(r.NextDouble()*3,newCoordinate);
					else if (randomValue == 6 || randomValue == 7 || randomValue==8)
						newSector = new GoldSector(r.NextDouble()*3,newCoordinate);
					else if (randomValue == 9 || randomValue == 10 || randomValue==11)
						newSector = new LumberSector(r.NextDouble()*3,newCoordinate);
					else
						newSector=new Sector(newCoordinate);


					newSector.Name = names[r.Next(names.Count - 1)];
					names.Remove(newSector.Name);

					int numberOfDifferentUnits = r.Next(5);
					for (int j = 1; j < numberOfDifferentUnits; j++) {
						Unit u = new Unit(GetNeutralUnit(), newSector, null, DateTime.Now);
						u.Number = r.Next(1, 10);
					}

					Game.GameData.Sectors[newSector.Coordinate] = newSector;
				}
			}
		}


		List<UnitType> neutralUnits;
		UnitType GetNeutralUnit() {
			if (neutralUnits == null) {
				neutralUnits = new List<UnitType>();
				foreach (UnitType t in Enum.GetValues(typeof(UnitType))) {
					UnitInfo i = UnitInfo.Get(t);
					if (i.Fraction == Fraction.Neutrals)
						neutralUnits.Add(t);
				}
			}

			return neutralUnits[r.Next(neutralUnits.Count - 1)];
		}


		protected void btnSectors_Click(object sender, EventArgs e) {
			foreach (Sector s in Game.GameData.Sectors.Values) {
				Response.Write(s.ToString() + " (");
				foreach (Unit u in s.Units)
					Response.Write(u.Number + " " + u.UnitInfo.Name + ", ");
				Response.Write(")<br />");
			}

		}

	}
}
