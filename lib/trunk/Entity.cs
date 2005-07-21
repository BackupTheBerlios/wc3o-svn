using System;
using System.Collections.Generic;

namespace Wc3o {
    [Serializable]
    public abstract class Entity : DataObject {

        #region " Properties "
        protected Sector sector;
        public Sector Sector {
            get {
                return sector;
            }
            set {
                sector = value;
            }
        }

		public abstract bool IsAvailable {
			get;
		}

		public abstract EntityInfo Info {
            get;
        }

		public bool Mobile {
			get {
				return this.GetType() == typeof(Unit);
			}
		}

		protected int number;
        public int Number {
            get {
                return number;
            }
            set {
                number = value;
            }
        }

        protected DateTime date;
        public DateTime Date {
            get {
                return date;
            }
            set {
                date = value;
            }
        }

        protected int hitpoints;
        public int Hitpoints {
            get {
                return hitpoints;
            }
            set {
                hitpoints = value;
            }
        }

		protected double cooldown;
		public double Cooldown {
			get {
				return cooldown;
			}
			set {
				cooldown = value;
			}
		}

		protected int armor;
		public int Armor {
			get {
				return armor;
			}
			set {
				armor = value;
			}
		}

		protected int range;
		public int Range {
			get {
				return range;
			}
			set {
				range = value;
			}
		}

		protected int attackAir;
		public int AttackAir {
			get {
				return attackAir;
			}
			set {
				attackAir = value;
			}
		}

		protected int attackGround;
		public int AttackGround {
			get {
				return attackGround;
			}
			set {
				attackGround = value;
			}
		}
		#endregion

		#region " Methods "
		public abstract void Create(Sector s);

		public abstract Entity Clone();

		public static int Count(List<Entity> l) {
			int i = 0;
			foreach (Entity e in l)
				i += e.Number;
			return i;
		}
		#endregion
	}

	public enum Visibility { Always, AtNight, AtDay, Never }
    public enum ArmorType { Light, Medium, Heavy, Fort, Hero, Unarmored };
    public enum AttackType { None, Normal, Pierce, Siege, Magic, Chaos, Spells, Hero };

}
