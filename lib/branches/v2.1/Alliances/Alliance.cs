using System;
using System.Collections.Generic;

namespace Wc3o {
	[Serializable]
	public class Alliance : DataObject {

		#region " Constructor "
		public Alliance() {
		}

		public Alliance(string name) {
			this.name = name;
			this.members = new List<Player>();
			Game.GameData.Alliances.Add(name, this);
		}
		#endregion

		#region " Destroy "
		public override void Destroy() {
			Game.GameData.Alliances.Remove(this.Name);
			//TODO: Remove Players from this Alliance, set Vote to null
		}
		#endregion

		#region " Properties "
		Player leader;
		public Player Leader {
			get {
				return leader;
			}
			set {
				leader = value;
			}
		}

		string name;
		public string Name {
			get {
				return name;
			}
			set {
				name = value;
			}
		}

		string image;
		public string Image {
			get {
				return image;
			}
			set {
				image = value;
			}
		}

		string description;
		public string Description {
			get {
				return description;
			}
			set {
				description = value;
			}
		}

		string directive;
		public string Directive {
			get {
				return directive;
			}
			set {
				directive = value;
			}
		}

		public string FullName {
			get {
				return "[" + Name + "] " + longName;
			}
		}

		string longName;
		public string LongName {
			get {
				return longName;
			}
			set {
				longName = value;
			}
		}

		List<Player> members;
		public List<Player> Members {
			get {
				return members;
			}
		}

		public List<Player> AcceptedMembers {
			get {
				List<Player> l = new List<Player>();
				foreach (Player p in members)
					if (p.IsAccepted)
						l.Add(p);
				return l;
			}
		}

		public int Score {
			get {
				int i = 0;
				foreach (Player p in Members)
					if (p.IsAccepted)
						i += p.Score;
				return i;
			}

		}

		public int AverageScore {
			get {
				return (Score / Members.Count);
			}
		}
		#endregion

	}

	public enum AllianceRank {
		Level0, Level1, Level2, Level3
	}
}
