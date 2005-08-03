#region Using directives

using System;
using System.Collections.Generic;
using System.Text;

#endregion

namespace Wc3o {
	public class AllianceScoreComparer : System.Collections.IComparer {
		public int Compare(Object a, Object b) {
			return ((Alliance)b).Score - ((Alliance)a).Score;
		}
	}

}
