using System;
using System.Collections.Generic;

namespace Wc3o {
    [Serializable]
    public class News : DataObject {

        #region " Destroy "
        public override void Destroy() {
            Game.PortalData.News.Remove(this);
        }
        #endregion

        #region " Constructor "
        public News(NewsType t) {
            this.type = t;
            Game.PortalData.News.Add(this);
        }

		public News() {
		}
        #endregion

        #region " Properties "
        NewsType type;
        public NewsType Type {
            get {
                return type;
            }
            set {
                type = value;
            }
        }

        public NewsInfo Info {
            get {
                return NewsInfo.Get(type);
            }
        }

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

    }

    #region " NewsInfo "
    public class NewsInfo {

        #region " Get "
        static SortedDictionary<NewsType,NewsInfo> infos=new SortedDictionary<NewsType,NewsInfo>();
        public static NewsInfo Get(NewsType t) {
            if (!infos.ContainsKey(t))
                Create(t);
            return infos[t];
        }
        #endregion

        #region " Create "
		static void Create(NewsType t) {
			NewsInfo i = new NewsInfo();
			switch (t) {
				case NewsType.Information:
					i.image = Configuration.Default_Gfx_Path + "/Portal/Information.gif";
					i.description = "Information";
					break;
				case NewsType.Rules:
					i.image = Configuration.Default_Gfx_Path + "/Portal/Rules.gif";
					i.description = "Rules";
					break;
				case NewsType.Update:
					i.image = Configuration.Default_Gfx_Path + "/Portal/Update.gif";
					i.description = "Update";
					break;
			}
			infos[t] = i;
		}
        #endregion

        #region " Properties "
        string image;
        public string Image {
            get {
                return image;
            }
        }

        string description;
        public string Description {
            get {
                return description;
            }
        }
        #endregion

    }
    #endregion

    public enum NewsType { Information, Update, Rules }

}
