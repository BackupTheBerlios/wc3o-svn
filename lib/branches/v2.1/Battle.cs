using System;
using System.Collections.Generic;

namespace Wc3o {
	public class Battle {

		Army attacker, defender;
		System.IO.StreamWriter writer;
		BattleResult result;
		Random random;
		Sector sector;

		bool attackBuildings = false;

		#region " Constructur, Destructor, BattleLog "
		public Battle(Army attacker, Army defender, Sector sector) {
			this.attacker = attacker;
			this.defender = defender;
			this.sector = sector;

			random = new Random();
			result = new BattleResult();

			string defenderName = "-";
			if (defender.Owner != null)
				defenderName = defender.Owner.Name;

			result.Log = attacker.Owner.Name + "@" + defenderName + "@" + sector.Coordinate + "@" + DateTime.Now.Year.ToString() + "_" + DateTime.Now.Month.ToString() + "_" + DateTime.Now.Day.ToString() + "_" + DateTime.Now.Hour.ToString() + "_" + DateTime.Now.Minute.ToString() + "_" + DateTime.Now.Second.ToString() + "_" + DateTime.Now.Millisecond.ToString() + ".txt";
			writer = new System.IO.StreamWriter(Configuration.Physical_Application_Path + "\\Game\\Logs\\BattleLogs\\" + result.Log, true);
		}


		~Battle() {
			writer.Close();
		}

		private void BattleLog(string message) {
			writer.WriteLine(message);
			writer.Flush();
		}
		#endregion

		public BattleResult DoBattle(int rounds, int attackBuildingsRound, bool arriveTogether) {
			#region " Initializations "
			string defenderName = "some Creeps";
			if (defender.Owner != null)
				defenderName = defender.Owner.FullName;
			BattleLog("The epic battle between " + attacker.Owner.FullName + " and " + defenderName + " at " + sector.FullName + " starts. The battle will last " + rounds + " rounds and buildings will first be attacked in the " + attackBuildingsRound + " round.");

			attacker.CalculateAuras();
			defender.CalculateAuras();
			attacker.CalculateArmy(defender);
			defender.CalculateArmy(attacker);

			BattleLog("\r\n\r\nThe attacker has:");
			foreach (Entity e in attacker.Entities)
				BattleLog("\t" + Game.Format(e.Number) + " " + e.Info.Name + " (" + e.Hitpoints + " hitpoints each)");
			BattleLog("\r\nThe defender has:");
			foreach (Entity e in defender.Entities)
				BattleLog("\t" + Game.Format(e.Number) + " " + e.Info.Name + " (" + e.Hitpoints + " hitpoints each)");

			attacker.Entities = Game.Split(attacker.Entities);
			defender.Entities = Game.Split(defender.Entities);

			List<BattleContainer> l = new List<BattleContainer>();
			foreach (Entity e in attacker.Entities)
				l.Add(new BattleContainer(e, true));
			foreach (Entity e in defender.Entities)
				l.Add(new BattleContainer(e, false));
			l.Sort(new BattleContainerComparer());
			#endregion

			#region " Fights "
			for (int round = 1; round <= rounds; round++) {
				if (attacker.Entities.Count <= 0 || defender.Entities.Count <= 0)
					break;
				BattleLog("\r\nRound " + Wc3o.Game.Format(round) + " - Attacker: " + Wc3o.Game.Format(attacker.Entities.Count) + " vs. Defender: " + Wc3o.Game.Format(defender.Entities.Count) + ".");

				if (attackBuildingsRound == round)
					attackBuildings = true;

				for (int i = 0; i < l.Count; i++) {
					if (attacker.Entities.Count <= 0 || defender.Entities.Count <= 0)
						break;

					BattleContainer a = l[i];
					if (a.Entity.Info.Cooldown <= 0)
						continue;

					while (round - 1 >= a.Cooldown) {
						if (attacker.Entities.Count <= 0 || defender.Entities.Count <= 0)
							break;
						a.Cooldown += a.Entity.Cooldown;
						BattleContainer b = GetNextDefender(l, a);
						if (b != null) {
							Fight(a.Entity, b.Entity);
							if (a.IsFromAttacker) {
								BattleLog("\t{A}" + a.Entity.Info.Name + " attacked {D}" + b.Entity.Info.Name + " (" + b.Entity.Hitpoints + ").");
								if (b.Entity.Hitpoints <= 0) {
									i--;
									l.Remove(b);
									defender.Entities.Remove(b.Entity);
									result.HostileLosses.Add(b.Entity);
									b.Entity.Destroy();
								}
							}
							else {
								BattleLog("\t{D}" + a.Entity.Info.Name + " attacked {A}" + b.Entity.Info.Name + " (" + b.Entity.Hitpoints + ").");
								if (b.Entity.Hitpoints <= 0) {
									i--;
									l.Remove(b);
									attacker.Entities.Remove(b.Entity);
									result.PlayerLosses.Add(b.Entity);
									b.Entity.Destroy();
								}
							}
						}
					}
				}
			}

			result.PlayerLosses = Wc3o.Game.Merge(result.PlayerLosses, true);
			result.HostileLosses = Wc3o.Game.Merge(result.HostileLosses, true);
			attacker.Entities = Wc3o.Game.Merge(attacker.Entities);
			defender.Entities = Wc3o.Game.Merge(defender.Entities);
			#endregion

			#region " Ressources "
			if (attacker.Entities.Count <= 0) {
				result.Status = BattleStatus.DefenderWins;
				if (defender.Owner != null) {
					result.Gold = -GetWon(attacker.Owner.Gold, 1, 80);
					result.Lumber = -GetWon(attacker.Owner.Lumber, 1, 80);
				}
			}
			else if (defender.Entities.Count <= 0) {
				result.Status = BattleStatus.AttackerWins;
				if (defender.Owner != null) {
					result.Gold = GetWon(defender.Owner.Gold, 10, 50);
					result.Lumber = GetWon(defender.Owner.Lumber, 10, 50);
				}
				else {
					result.Gold = GetKilledScoreFromDefender() * 2;
				}
			}
			else {
				result.Status = BattleStatus.NonWins;
				if (defender.Owner != null) {
					result.Gold = GetWon(defender.Owner.Gold, 1, 5);
					result.Lumber = GetWon(defender.Owner.Lumber, 1, 5);
				}
				else {
					result.Gold = GetKilledScoreFromDefender() * 2;
				}
			}

			if (result.Gold > 0 || result.Lumber > 0) {
				if (defender.Owner != null) {
					defender.Owner.Gold -= result.Gold;
					defender.Owner.Lumber -= result.Lumber;
				}
				attacker.Owner.Gold += result.Gold;
				attacker.Owner.Lumber += result.Lumber;
			}
			#endregion

			#region " Returns "
			double factor = Configuration.Return_Factor_After_Attack;
			if (sector.Owner == attacker.Owner)
				factor = Configuration.Return_Factor_After_Defend;

			if (arriveTogether) {
				int speed = 0;
				foreach (Unit u in attacker.Entities) {
					if (u.UnitInfo.Speed > speed)
						speed = u.UnitInfo.Speed;
				}
				DateTime date = DateTime.Now.AddMinutes(speed * factor);
				foreach (Unit u in attacker.Entities) {
					u.Date = date;
					u.SourceDate = DateTime.Now;
					u.Action = UnitAction.Returning;
					u.Sector = sector;
				}
			}
			else
				foreach (Unit u in attacker.Entities) {
					u.SourceDate = DateTime.Now;
					u.Action = UnitAction.Returning;
					u.Sector = sector;
					u.Date = DateTime.Now.AddMinutes(u.UnitInfo.Speed * factor);
				}
			#endregion

			BattleLog("\r\n\r\nThe battle is over. The attacker won " + Game.Format(result.Gold) + " gold and " + Game.Format(result.Lumber) + " Lumber.");
			return result;
		}

		#region " Misc. Methods "
		private BattleContainer GetNextDefender(List<BattleContainer> l, BattleContainer a) {
			if (a.Entity.Info.AttackTypeAir == AttackType.None && a.Entity.Info.AttackTypeGround == AttackType.None)
				return null;

			int r = Random(l.Count - 1);
			for (int i = r; i < l.Count; i++) {
				BattleContainer b = l[i];
				if (a.IsFromAttacker != b.IsFromAttacker && CanAttack(a.Entity, b.Entity))
					return b;
			}
			for (int i = r - 1; i >= 0; i--) {
				BattleContainer b = l[i];
				if (a.IsFromAttacker != b.IsFromAttacker && CanAttack(a.Entity, b.Entity))
					return b;
			}
			return null;
		}

		private bool CanAttack(Entity attacker, Entity defender) {
			if (!defender.Mobile && !attackBuildings)
				return false;

			if (!attacker.Mobile && attacker.Range < defender.Range)
				return false;

			if (defender.Info.Flies && attacker.Info.AttackTypeAir != AttackType.None)
				return true;
			else if (!defender.Info.Flies && attacker.Info.AttackTypeGround != AttackType.None)
				return true;
			return false;
		}

		private void Fight(Entity a, Entity b) {
			if (b.Info.Flies)
				b.Hitpoints -= GetDamage(a.AttackAir, b.Armor, a.Info.AttackTypeAir, b.Info.ArmorType);
			else
				b.Hitpoints -= GetDamage(a.AttackGround, b.Armor, a.Info.AttackTypeGround, b.Info.ArmorType);
		}

		private int GetDamage(int attack, int armor, AttackType attackType, ArmorType armorType) {
			int damage;
			if (armor < 0)
				damage = ((int)(attack * GetDamageFactor(attackType, armorType) + (System.Math.Pow(2 - 0.94, armor))));
			else
				damage = ((int)(attack * GetDamageFactor(attackType, armorType) - ((armor * 0.06) / (1 + 0.06 * armor))));
			if (damage < 0)
				damage = 0;
			return damage;
		}

		private double GetDamageFactor(AttackType attack, ArmorType armor) {
			switch (attack) {
				case AttackType.Chaos:
					return 1;
				case AttackType.Hero:
					switch (armor) {
						case ArmorType.Fort:
							return 0.5;
						default:
							return 1;
					}
				case AttackType.Magic:
					switch (armor) {
						case ArmorType.Fort:
							return 0.35;
						case ArmorType.Heavy:
							return 2;
						case ArmorType.Hero:
							return 0.5;
						case ArmorType.Light:
							return 1.25;
						case ArmorType.Medium:
							return 0.75;
						default:
							return 1;
					}
				case AttackType.Normal:
					switch (armor) {
						case ArmorType.Fort:
							return 0.7;
						case ArmorType.Medium:
							return 1.5;
						default:
							return 1;
					}
				case AttackType.Pierce:
					switch (armor) {
						case ArmorType.Fort:
							return 0.35;
						case ArmorType.Hero:
							return 0.5;
						case ArmorType.Light:
							return 2;
						case ArmorType.Medium:
							return 0.75;
						case ArmorType.Unarmored:
							return 1.5;
						default:
							return 1;
					}
				case AttackType.Siege:
					switch (armor) {
						case ArmorType.Fort:
							return 1.5;
						case ArmorType.Hero:
							return 0.5;
						case ArmorType.Medium:
							return 0.5;
						case ArmorType.Unarmored:
							return 1.5;
						default:
							return 1;
					}
				case AttackType.Spells:
					switch (armor) {
						case ArmorType.Hero:
							return 0.7;
						default:
							return 1;
					}
				case AttackType.None:
					return 0;
			}
			return 1;
		}

		private int GetWon(int total, int min, int max) {
			return Convert.ToInt32(total * Convert.ToDouble(random.Next(min, max)) / 100);
		}

		private int Random(int max) {
			if (max == 0)
				return 0;
			else
				return random.Next(max);
		}

		int GetKilledScoreFromDefender() {
			int i = 0;
			foreach (Entity e in result.HostileLosses)
				i += e.Info.Score * e.Number;
			return i;
		}
		#endregion
	}

	#region " BattleResult "
	public class BattleResult {

		List<Entity> playerLosses;
		public List<Entity> PlayerLosses {
			get {
				return playerLosses;
			}
			set {
				playerLosses = value;
			}
		}

		List<Entity> hostileLosses;
		public List<Entity> HostileLosses {
			get {
				return hostileLosses;
			}
			set {
				hostileLosses = value;
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

		int lumber;
		public int Lumber {
			get {
				return lumber;
			}
			set {
				lumber = value;
			}
		}

		BattleStatus status;
		public BattleStatus Status {
			get {
				return status;
			}
			set {
				status = value;
			}
		}

		string log;
		public string Log {
			get {
				return log;
			}
			set {
				log = value;
			}
		}

		public BattleResult() {
			playerLosses = new List<Entity>();
			hostileLosses = new List<Entity>();
		}
	}

	public enum BattleStatus {
		AttackerWins,
		DefenderWins,
		NonWins
	}
	#endregion

	#region " BattleContainer "
	public class BattleContainer {
		public BattleContainer(Entity entity, bool isFromAttacker) {
			this.entity = entity;
			this.isFromAttacker = isFromAttacker;
		}

		Entity entity;
		public Entity Entity {
			get {
				return entity;
			}
			set {
				entity = value;
			}
		}

		bool isFromAttacker;
		public bool IsFromAttacker {
			get {
				return isFromAttacker;
			}
			set {
				isFromAttacker = value;
			}
		}

		double cooldown;
		public double Cooldown {
			get {
				return cooldown;
			}
			set {
				cooldown = value;
			}
		}
	}

	class BattleContainerComparer : System.Collections.Generic.Comparer<BattleContainer> {
		public override int Compare(BattleContainer a, BattleContainer b) {
			return b.Entity.Range.CompareTo(a.Entity.Range);
		}
	}
	#endregion
}

