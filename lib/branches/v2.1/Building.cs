using System;
using System.Collections.Generic;

namespace Wc3o {
	[Serializable]
	public class Building : Entity {

		#region " Constructor "
		public Building() {
		}

		public Building(BuildingType t, Sector s, DateTime d) {
			this.type = t;
			this.sector = s;
			this.date = d;
			this.hitpoints = Info.Hitpoints;
			this.number = 1;
			this.UpgradingFrom = BuildingType.None;
			Sector.Buildings.Add(this);
			if (Sector.Owner != null)
				Sector.Owner.Buildings.Add(this);
		}
		#endregion

		#region " Create "
		public override void Create(Sector s) {
			if (!s.Buildings.Contains(this))
				s.Buildings.Add(this);
			if (s.Owner != null)
				s.Owner.Buildings.Add(this);
		}
		#endregion

		#region " Destroy "
		public override void Destroy() {
			Sector.Buildings.Remove(this);
			if (Sector.Owner != null)
				Sector.Owner.Buildings.Remove(this);

			//Destroy all units in construction of this particular building
			if (!Sector.HasAvailableBuilding(this.Type)) {
				List<Unit> l = new List<Unit>();
				foreach (Unit u in Sector.Units)
					if (u.IsInTraining && u.UnitInfo.TrainedAt == this.Type)
						l.Add(u);
				foreach (Unit u in l)
					u.Destroy();
			}
		}
		#endregion

		#region " Properties "
		public bool CanFight {
			get {
				return Info.AttackTypeGround != AttackType.None || Info.AttackTypeAir != AttackType.None;
			}
		}

		public override bool IsAvailable {
			get {
				return !IsInConstruction;
			}
		}

		public bool IsUpgrading {
			get {
				return IsInConstruction && UpgradingFrom != BuildingType.None;
			}
		}

		public bool IsInConstruction {
			get {
				return Date >= DateTime.Now;
			}
		}

		public override EntityInfo Info {
			get {
				return BuildingInfo.Get(Type);
			}
		}

		public BuildingInfo BuildingInfo {
			get {
				return BuildingInfo.Get(Type);
			}
		}

		BuildingType upgradingFrom;
		public BuildingType UpgradingFrom {
			get {
				return upgradingFrom;
			}
			set {
				upgradingFrom = value;
			}
		}

		BuildingType type;
		public BuildingType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}
		#endregion

		#region " Methods "
		public override Entity Clone() {
			Building b = new Building(Type, Sector, Date);
			b.Hitpoints = Hitpoints;
			b.Number = Number;
			b.Armor = Armor;
			b.AttackAir = AttackAir;
			b.AttackGround = AttackGround;
			b.Cooldown = Cooldown;
			return b;
		}
		#endregion

	}

	public enum BuildingType { AltarOfDarkness, AltarOfElders, AltarOfKings, AltarOfStorms, AncientOfLore, AncientOfWar, AncientOfWind, AncientOfWonders, AncientProtector, ArcaneSanctum, ArcaneTower, ArcaneVault, Beastiary, BlackCitadel, Blacksmith, Boneyard, Burrow, CannonTower, Castle, ChimaeraRoost, Crypt, Farm, Fortress, Graveyard, GreatHall, GryphonAviary, GuardTower, HallsOfTheDead, HauntedGoldMine, HumanBarracks, HuntersHall, Keep, LumberMill, MoonWell, Necropolis, NerubianTower, None, OrcBarracks, SacrificialPit, ScoutTower, Slaughterhouse, SpiritLodge, SpiritTower, Stronghold, TaurenTotem, TempleOfTheDamned, TombOfRelics, TownHall, TreeOfAges, TreeOfEternity, TreeOfLife, VoodooLounge, WarMill, WatchTower, Workshop, Ziggurat };

}
