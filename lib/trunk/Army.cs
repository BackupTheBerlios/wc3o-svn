using System;
using System.Collections.Generic;

namespace Wc3o {
	public class Army {

		Player owner;
		List<Entity> entities;

		public Army(Player owner) {
			this.owner = owner;
			entities = new List<Entity>();
			bonusAuraArmor = 1;
			bonusAuraRange = 1;
			bonusAuraAttackAir = 1;
			bonusAuraAttackGround = 1;
			bonusAuraHitpoints = 1;
			bonusAuraCooldown = 1;
			malusAuraArmor = 1;
			malusAuraRange = 1;
			malusAuraAttackAir = 1;
			malusAuraAttackGround = 1;
			malusAuraHitpoints = 1;
			malusAuraCooldown = 1;
		}

		public void CalculateArmy(Army a) {
			foreach (Entity e in entities) {
				e.HitpointsBeforeAura = e.Hitpoints;
				e.Hitpoints = Convert.ToInt32(e.Hitpoints * bonusAuraHitpoints * a.MalusAuraHitpoints);
				e.AttackAir = Convert.ToInt32(e.Info.AttackAir * bonusAuraAttackAir * a.MalusAuraAttackAir);
				e.AttackGround = Convert.ToInt32(e.Info.AttackGround * bonusAuraAttackGround * a.MalusAuraAttackGround);
				e.Cooldown = e.Info.Cooldown * bonusAuraCooldown * a.MalusAuraCooldown;
				e.Range = Convert.ToInt32(e.Info.Range * bonusAuraRange * a.MalusAuraRange);
				e.Armor = Convert.ToInt32(e.Info.Armor * bonusAuraArmor * a.MalusAuraArmor);
			}
		}

		public void CalculateAuras() {
			foreach (Entity e in entities) {
				bonusAuraArmor = Bonus(bonusAuraArmor, e.Info.BonusAuraArmor, e.Number);
				bonusAuraRange = Bonus(bonusAuraRange, e.Info.BonusAuraRange, e.Number);
				bonusAuraAttackAir = Bonus(bonusAuraAttackAir, e.Info.BonusAuraAttackAir, e.Number);
				bonusAuraAttackGround = Bonus(bonusAuraAttackGround, e.Info.BonusAuraAttackGround, e.Number);
				bonusAuraHitpoints = Bonus(bonusAuraHitpoints, e.Info.BonusAuraHitpoints, e.Number);
				bonusAuraCooldown = Bonus(bonusAuraCooldown, e.Info.BonusAuraCooldown, e.Number);

				malusAuraArmor = Malus(malusAuraArmor, e.Info.MalusAuraArmor, e.Number);
				malusAuraRange = Malus(malusAuraRange, e.Info.MalusAuraRange, e.Number);
				malusAuraAttackAir = Malus(malusAuraAttackAir, e.Info.MalusAuraAttackAir, e.Number);
				malusAuraAttackGround = Malus(malusAuraAttackGround, e.Info.MalusAuraAttackGround, e.Number);
				malusAuraHitpoints = Malus(malusAuraHitpoints, e.Info.MalusAuraHitpoints, e.Number);
				malusAuraCooldown = Malus(malusAuraCooldown, e.Info.MalusAuraCooldown, e.Number);
			}
		}

		double Bonus(double total, double bonus, int count) {
			if (bonus == 1)
				return total;
			for (int i = 1; i <= count; i++)
				total = total * bonus * (1 - (total - 1) / 2.8);
			return total;
		}

		double Malus(double total, double malus, int count) {
			if (malus == 1)
				return total;
			for (int i = 1; i <= count; i++)
				total = total * malus * (1 + ((1 - total) / 2.8));
			return total;
		}

		#region " Properties "
		public Player Owner {
			get {
				return owner;
			}
			set {
				owner = value;
			}
		}

		public List<Entity> Entities {
			get {
				return entities;
			}
			set {
				entities = value;
			}
		}

		double bonusAuraArmor;
		public double BonusAuraArmor {
			get {
				return bonusAuraArmor;
			}
		}

		double malusAuraArmor;
		public double MalusAuraArmor {
			get {
				return malusAuraArmor;
			}
		}

		double bonusAuraRange;
		public double BonusAuraRange {
			get {
				return bonusAuraRange;
			}
		}

		double malusAuraRange;
		public double MalusAuraRange {
			get {
				return bonusAuraRange;
			}
		}

		double bonusAuraHitpoints;
		public double BonusAuraHitpoints {
			get {
				return bonusAuraHitpoints;
			}
		}

		double malusAuraHitpoints;
		public double MalusAuraHitpoints {
			get {
				return bonusAuraHitpoints;
			}
		}

		double bonusAuraAttackGround;
		public double BonusAuraAttackGround {
			get {
				return bonusAuraAttackGround;
			}
		}

		double malusAuraAttackGround;
		public double MalusAuraAttackGround {
			get {
				return bonusAuraAttackGround;
			}
		}

		double bonusAuraAttackAir;
		public double BonusAuraAttackAir {
			get {
				return bonusAuraAttackAir;
			}
		}

		double malusAuraAttackAir;
		public double MalusAuraAttackAir {
			get {
				return bonusAuraAttackAir;
			}
		}

		double bonusAuraCooldown;
		public double BonusAuraCooldown {
			get {
				return bonusAuraCooldown;
			}
		}

		double malusAuraCooldown;
		public double MalusAuraCooldown {
			get {
				return bonusAuraCooldown;
			}
		}
		#endregion
	}
}







