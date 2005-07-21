using System;
using System.Collections.Generic;

namespace Wc3o {
    public abstract class EntityInfo {

        #region " Properties "
		protected bool buildable;
		public bool Buildable {
			get {
				return buildable;
			}
		}

		protected BuildingType[] requirements;
        public BuildingType[] Requirements {
            get {
                return requirements;
            }
        }

        protected int food;
        public int Food {
            get {
                return food;
            }
        }

        protected int gold;
        public int Gold {
            get {
                return gold;
            }
        }

        protected int lumber;
        public int Lumber {
            get {
                return lumber;
            }
        }

        protected int hitpoints;
        public int Hitpoints {
            get {
                return hitpoints;
            }
        }

        protected int armor;
        public int Armor {
            get {
                return armor;
            }
        }

        protected int minutes;
        public int Minutes {
            get {
                return minutes;
            }
        }

        protected int attackGround;
        public int AttackGround {
            get {
                return attackGround;
            }
        }

        protected int attackAir;
        public int AttackAir {
            get {
                return attackAir;
            }
        }    

        protected int range;
        public int Range {
            get {
                return range;
            }
        }

        protected string name;
        public string Name {
            get {
                return name;
            }
        }

        protected string image;
        public string Image {
            get {
                return image;
            }
        }

        protected string createImage;
        public string CreateImage {
            get {
                return createImage;
            }
        }

        protected Fraction fraction;
        public Fraction Fraction {
            get {
                return fraction;
            }
        }

        protected ArmorType armorType;
        public ArmorType ArmorType {
            get {
                return armorType;
            }
        }

        protected AttackType attackTypeGround;
        public AttackType AttackTypeGround {
            get {
                return attackTypeGround;
            }
        }

        protected AttackType attackTypeAir;
        public AttackType AttackTypeAir {
            get {
                return attackTypeAir;
            }
        }

        protected bool flies;
        public bool Flies {
            get {
                return flies;
            }
        }

        protected int score;
        public int Score {
            get {
                return score;
            }
        }

        protected double cooldown;
        public double Cooldown {
            get {
                return cooldown;
            }
        }

        protected Visibility visibility;
        public Visibility Visibility {
            get {
                return visibility;
            }
        }

        protected double bonusAuraArmor;
        public double BonusAuraArmor {
            get {
                return bonusAuraArmor;
            }
        }

        protected double malusAuraArmor;
        public double MalusAuraArmor {
            get {
                return malusAuraArmor;
            }
        }

        protected double bonusAuraRange;
        public double BonusAuraRange {
            get {
                return bonusAuraRange;
            }
        }

        protected double malusAuraRange;
        public double MalusAuraRange {
            get {
                return malusAuraRange;
            }
        }

        protected double bonusAuraHitpoints;
        public double BonusAuraHitpoints {
            get {
                return bonusAuraHitpoints;
            }
        }

        protected double malusAuraHitpoints;
        public double MalusAuraHitpoints {
            get {
                return malusAuraHitpoints;
            }
        }

        protected double bonusAuraAttackGround;
        public double BonusAuraAttackGround {
            get {
                return bonusAuraAttackGround;
            }
        }

        protected double malusAuraAttackGround;
        public double MalusAuraAttackGround {
            get {
                return malusAuraAttackGround;
            }
        }

        protected double bonusAuraAttackAir;
        public double BonusAuraAttackAir {
            get {
                return bonusAuraAttackAir;
            }
        }

        protected double malusAuraAttackAir;
        public double MalusAuraAttackAir {
            get {
                return malusAuraAttackAir;
            }
        }

        protected double bonusAuraCooldown;
        public double BonusAuraCooldown {
            get {
                return bonusAuraCooldown;
            }
        }

        protected double malusAuraCooldown;
        public double MalusAuraCooldown {
            get {
                return malusAuraCooldown;
            }
        }
        #endregion

    }

}
