using System;
using System.Collections.Generic;
using System.Text;

namespace Wc3o {
	[Serializable]
	public class HealingSector : Sector {

		public HealingSector(double healingEfficiency, Coordinate c)
			: base(c) {
			this.healingEfficiency = healingEfficiency;
		}

		double healingEfficiency;
		public double HealingEfficiency {
			get {
				return healingEfficiency;
			}
		}
	}
}
