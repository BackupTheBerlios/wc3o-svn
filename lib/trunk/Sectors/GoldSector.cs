using System;
using System.Collections.Generic;
using System.Text;

namespace Wc3o {
	[Serializable]
	public class GoldSector : Sector {

		public GoldSector(double goldEfficiency, Coordinate c)
			: base(c) {
			this.goldEfficiency = goldEfficiency;
		}

		double goldEfficiency;
		public double GoldEfficiency {
			get {
				return goldEfficiency;
			}
		}

		public int GoldPerTick {
			get {
				return (int)(Configuration.Gold_Per_Ressource_Tick * goldEfficiency);
			}
		}
	}
}
