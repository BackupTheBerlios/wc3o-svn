using System;
using System.Collections.Generic;
using System.Text;

namespace Wc3o {
	[Serializable]
	public class GoldAndLumberSector : Sector {

		public GoldAndLumberSector(double goldEfficiency,double lumberEfficiency, Coordinate c)
			: base(c) {
			this.goldEfficiency = goldEfficiency;
			this.lumberEfficiency = lumberEfficiency;
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

		double lumberEfficiency;
		public double LumberEfficiency {
			get {
				return lumberEfficiency;
			}
		}

		public int LumberPerTick {
			get {
				return (int)(Configuration.Lumber_Per_Ressource_Tick * lumberEfficiency);
			}
		}
	}
}
