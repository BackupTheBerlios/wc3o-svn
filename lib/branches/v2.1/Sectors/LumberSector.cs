using System;
using System.Collections.Generic;
using System.Text;

namespace Wc3o {
	[Serializable]
	public class LumberSector : Sector {

		public LumberSector(double lumberEfficiency, Coordinate c)
			: base(c) {
			this.lumberEfficiency = lumberEfficiency;
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
