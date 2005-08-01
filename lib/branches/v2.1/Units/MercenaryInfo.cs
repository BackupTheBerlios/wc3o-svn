using System;
using System.Collections.Generic;
using System.Text;

namespace Wc3o {
	[Serializable]
	public class MercenaryInfo {

		public MercenaryInfo(int number, UnitType type, DateTime date) {
			this.number = number;
			this.type = type;
			this.date = date;
		}


		int number;
		/// <summary>
		/// Gets or sets the maximal number of mercenaries that can be recruted.
		/// </summary>
		public int Number {
			get {
				return number;
			}
			set {
				number = value;
			}
		}


		UnitType type;
		/// <summary>
		/// Gets or sets the UnitType of the mercenaries.
		/// </summary>
		public UnitType Type {
			get {
				return type;
			}
			set {
				type = value;
			}
		}


		DateTime date;
		/// <summary>
		/// Gets or sets the date until the mercenaries are enabled for recruitment.
		/// </summary>
		public DateTime Date {
			get {
				return date;
			}
			set {
				date = value;
			}
		}
	}
}
