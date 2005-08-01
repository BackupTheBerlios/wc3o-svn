using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wc3o {

    public class FractionInfo {
        #region " Get "
        static SortedDictionary<Fraction, FractionInfo> infos;
        public static FractionInfo Get(Fraction f) {
			if (infos == null)
				infos = new SortedDictionary<Fraction, FractionInfo>();
			if (!infos.ContainsKey(f))
                Create(f);
            return infos[f];
        }
        #endregion

        #region " Create "
        static void Create(Fraction f) {
            FractionInfo i = new FractionInfo();
            switch (f) {
                case Fraction.Humans:
                    i.name="Human Alliance";
					i.emblem = "/Humans/Emblem.gif";
					i.smallEmblem = "/Humans/SmallEmblem.gif";
					break;
                case Fraction.Orcs:
                    i.name = "Horde";
					i.emblem = "/Orcs/Emblem.gif";
					i.smallEmblem = "/Orcs/SmallEmblem.gif";
                    break;
                case Fraction.NightElves:
                    i.name = "Night Elves";
					i.emblem = "/NightElves/Emblem.gif";
					i.smallEmblem = "/NightElves/SmallEmblem.gif";
                    break;
                case Fraction.Undead:
                    i.name = "Undead Scourge";
					i.emblem = "/Undead/Emblem.gif";
					i.smallEmblem = "/Undead/SmallEmblem.gif";
                    break;
                case Fraction.Neutrals:
                    i.name = "Creeps";
					i.emblem = "";
					i.smallEmblem = "";
                    break;
            }
            infos[f] = i;
        }
        #endregion

        #region " Properties "
        string name;
        public string Name {
            get {
                return name;
            }
        }

		string emblem;
		public string Emblem {
			get {
				return emblem;
			}
		}

		string smallEmblem;
		public string SmallEmblem {
			get {
				return smallEmblem;
			}
		}
        #endregion

		#region " Methods "
		public override string ToString() {
			return this.Name;
		}
		#endregion
	}

	public enum Fraction { Humans, Orcs, Undead, NightElves, Neutrals }
}
