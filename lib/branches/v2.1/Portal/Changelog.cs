using System;
using System.Collections.Generic;

namespace Wc3o {
    [Serializable]
    public class Changelog : DataObject {

        #region " Destroy "
        public override void Destroy() {
            Game.PortalData.Changelog.Remove(this);
        }
        #endregion

        #region " Constructor "
        public Changelog() {
			Game.PortalData.Changelog.Add(this);
        }
        #endregion

        #region " Properties "
		string text;
		public string Text {
			get {
				return text;
			}
			set {
				text = value;
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

		DateTime date;
		public DateTime Date {
			get {
				return date;
			}
			set {
				date = value;
			}
		}
        #endregion


		public class ChangelogComparer : IComparer<Changelog> {
			public int Compare(Changelog a, Changelog b) {
				return b.Date.CompareTo(a.Date);
			}
		}

    }

}
