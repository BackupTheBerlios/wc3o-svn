using System;

namespace Wc3o {

	[Serializable]
	public class Coordinate : IComparable {

		#region " Constructor "
		public Coordinate(int x, int y) {
			this.x = x;
			this.y = y;
		}

		public Coordinate(string s) {
			this.x = int.Parse(s.Substring(0, s.IndexOf("_")));
			this.y = int.Parse(s.Substring(s.IndexOf("_") + 1, s.Length - s.IndexOf("_") - 1));
		}
		#endregion

		#region " Properties "
		int x;
		public int X {
			get {
				return x;
			}
		}

		int y;
		public int Y {
			get {
				return y;
			}
		}
		#endregion

		#region " CompareTo, ToString, Equals, GetHashCode "
		public int CompareTo(object o) {
			return ToString().CompareTo(o.ToString());
		}

		public override string ToString() {
			return X.ToString() + "_" + Y.ToString();
		}

		public override bool Equals(object o) {
			Coordinate c = (Coordinate)o;
			return c.X == X && c.Y == y;
		}

		public override int GetHashCode() {
			return this.GetHashCode();
		}
		#endregion

	}
}
