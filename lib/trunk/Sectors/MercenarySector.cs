using System;
using System.Collections.Generic;
using System.Text;

namespace Wc3o {
	[Serializable]
	public class MercenarySector : Sector {

		/// <summary>
		/// Creates a new MercenarySector and fills it with random mercenaries.
		/// </summary>
		/// <param name="numberOfMercenaries">The number of available Mercenary Slots of this sector.</param>
		/// <param name="coordinate">The Coordinates of the sector.</param>
		public MercenarySector(int numberOfMercenaries, Coordinate coordinate)
			: base(coordinate) {
			mercenaries = new MercenaryInfo[numberOfMercenaries];

			for (int i = 0; i < mercenaries.Length; i++)
				mercenaries[i] = CreateNewMercenary();
		}


		MercenaryInfo[] mercenaries;
		/// <summary>
		/// Gets a list of all available mercenaries on this sector.
		/// </summary>
		public MercenaryInfo[] Mercenaries {
			get {
				return mercenaries;
			}
		}


		/// <summary>
		/// Creates a new MercenaryInfo with random Number and UnitType. The Date is set to now.
		/// </summary>
		/// <returns>A new MercenaryInfo with random Number and UnitType. The Date is set to now.</returns>
		public MercenaryInfo CreateNewMercenary() {
			//Get the UnitTypes of all neutral units
			List<UnitType> neutralUnits = new List<UnitType>();
			foreach (UnitType unitType in Enum.GetValues(typeof(UnitType)))
				if (unitType != UnitType.None) {
					UnitInfo unitInfo = UnitInfo.Get(unitType);
					if (unitInfo.Fraction == Fraction.Neutrals)
						neutralUnits.Add(unitType);
				}

			return new MercenaryInfo(Game.Random.Next(1, Configuration.Max_Mercenaries_Per_Slot), neutralUnits[Game.Random.Next(0, neutralUnits.Count - 1)], DateTime.Now);
		}
	}
}
