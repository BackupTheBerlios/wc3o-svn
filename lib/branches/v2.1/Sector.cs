using System;
using System.Collections.Generic;

namespace Wc3o {
    [Serializable]
    public class Sector : DataObject {

        #region " Destroy "
        public override void Destroy() {
            Game.GameData.Sectors.Remove(this.Coordinate);
        }
        #endregion

        #region " Constructor "
		public Sector() {
		}

        public Sector(Coordinate c) {
            this.coordinate = c;
            this.buildings = new List<Building>();
            this.units = new List<Unit>();
            Game.GameData.Sectors.Add(this.Coordinate, this);
        }
        #endregion

        #region " Properties "
		public string FullName {
			get {
				return Name + " [" + Coordinate.X + ":" + Coordinate.Y + "]";
			}
		}

		Player owner;
        public Player Owner {
            get {
                return owner;
            }
            set {
				if (Owner != null) {
					Game.RemoveRange<Building>(Owner.Buildings, Buildings);
					Owner.Sectors.Remove(this);
				}
				if (value != null) {
					value.Buildings.AddRange(Buildings);
					value.Sectors.Add(this);
				}

				owner = value;
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

        Coordinate coordinate;
        public Coordinate Coordinate {
            get {
                return coordinate;
            }
        }

        List<Unit> units;
        public List<Unit> Units {
            get {
                return units;
            }
        }

        List<Building> buildings;
        public List<Building> Buildings {
            get {
                return buildings;
            }
        }

		bool hasArtifacts;
		public bool HasArtifacts {
			get {
				return hasArtifacts;
			}
			set {
				hasArtifacts = value;
			}
		}

		bool hasMercenaries;
		public bool HasMercenaries {
			get {
				return hasMercenaries;
			}
			set {
				hasMercenaries = value;
			}
		}

		int gold;
		public int Gold {
			get {
				return gold;
			}
			set {
				gold = value;
			}
		}

		public int GoldPerTick {
			get {
				return Configuration.Gold_Per_Ressource_Tick * Gold / 100;
			}
		}

		int lumber;
		public int Lumber {
			get {
				return lumber;
			}
			set {
				lumber = value;
			}
		}

		public int LumberPerTick {
			get {
				return Configuration.Lumber_Per_Ressource_Tick * Lumber / 100;
			}
		}
        #endregion

		#region " Methods "
		public bool HasAvailableBuilding(BuildingType t) {
			foreach (Building building in buildings)
				if (building.Type == t && building.IsAvailable)
					return true;
			return false;
		}

		public bool HasBuildingForRequirement(BuildingType t) {
			if (t == BuildingType.None)
				return true;
			else if (HasAvailableBuilding(t))
				return true;
			else if (t == BuildingType.TownHall) {
				if (HasAvailableBuilding(BuildingType.Keep) || HasAvailableBuilding(BuildingType.Castle))
					return true;
			}
			else if (t == BuildingType.Keep) {
				if (HasAvailableBuilding(BuildingType.Castle))
					return true;
			}
			else if (t == BuildingType.GreatHall) {
				if (HasAvailableBuilding(BuildingType.Stronghold) || HasAvailableBuilding(BuildingType.Fortress))
					return true;
			}
			else if (t == BuildingType.Stronghold) {
				if (HasAvailableBuilding(BuildingType.Fortress))
					return true;
			}
			else if (t == BuildingType.TreeOfLife) {
				if (HasAvailableBuilding(BuildingType.TreeOfAges) || HasAvailableBuilding(BuildingType.TreeOfEternity))
					return true;
			}
			else if (t == BuildingType.TreeOfAges) {
				if (HasAvailableBuilding(BuildingType.TreeOfEternity))
					return true;
			}
			else if (t == BuildingType.Necropolis) {
				if (HasAvailableBuilding(BuildingType.HallsOfTheDead) || HasAvailableBuilding(BuildingType.BlackCitadel))
					return true;
			}
			else if (t == BuildingType.HallsOfTheDead) {
				if (HasAvailableBuilding(BuildingType.BlackCitadel))
					return true;
			}

			return false;
		}

		public bool HasBuilding(BuildingType t) {
			foreach (Building building in buildings)
				if (building.Type == t)
					return true;
			return false;
		}
		#endregion

		#region " ToString "
		public override string ToString() {
			return Name + "[" + Coordinate.X + ":" + Coordinate.Y + "]";
		}
		#endregion

	}

    #region " Coordinate "
    [Serializable]
    public struct Coordinate : IComparable {

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
    #endregion

}
