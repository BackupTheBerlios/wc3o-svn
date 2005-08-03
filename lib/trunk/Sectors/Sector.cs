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
        public Sector(Coordinate c) {
            this.coordinate = c;
            this.buildings = new List<Building>();
            this.units = new List<Unit>();
            Game.GameData.Sectors.Add(this.Coordinate, this);
        }
        #endregion

        #region " Properties "
		protected Player owner;
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

        protected string name;
        public string Name {
            get {
                return name;
            }
			set {
				name = value;
			}
		}

        protected Coordinate coordinate;
        public Coordinate Coordinate {
            get {
                return coordinate;
            }
        }


        protected List<Unit> units;
        public List<Unit> Units {
            get {
                return units;
            }
        }


        protected List<Building> buildings;
        public List<Building> Buildings {
            get {
                return buildings;
            }
        }


		public override string ToString() {
			return name + " [" + Coordinate.X + ":" + Coordinate.Y + "]";
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
	}
}
