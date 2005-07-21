using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
using System.Collections.Generic;

namespace Wc3o {
	[Serializable]
	public class PortalData {

		#region " Constructor/Destructor "
		public PortalData() {
			news = new List<News>();
			changelog = new List<Changelog>();
		}

		~PortalData() {
			Save(this);
		}
		#endregion

		#region " Load/Save (Serialization) "
		public static void Save(PortalData d) {
			try {
				Game.Logger.Log("Saving Portal Data.....", Log.LogType.System);
				FileStream f = new FileStream(Configuration.Physical_Application_Path + "\\App_Data\\Portal.dat", FileMode.OpenOrCreate);
				new BinaryFormatter().Serialize(f, d);
				f.Close();
				Game.Logger.Log("Saving of Portal Data done.", Log.LogType.System);
			} catch (Exception e) {
				Game.Logger.Log("Exception while saving Portal Data: " + e.InnerException, Log.LogType.System);
			}
		}

		public static PortalData Load() {
			try {
				Game.Logger.Log("Loading Portal Data.....", Log.LogType.System);
				FileStream f = new FileStream(Configuration.Physical_Application_Path + "\\App_Data\\Portal.dat", FileMode.Open);
				PortalData d = (PortalData)new BinaryFormatter().Deserialize(f);
				f.Close();
				Game.Logger.Log("Loading of Portal Data done.", Log.LogType.System);
				return d;
			} catch (Exception e) {
				Game.Logger.Log("Exception while loading Portal Data: " + e.InnerException, Log.LogType.System);
				return new PortalData();
			}
		}
		#endregion

		#region " Properties "
		List<News> news;
		public List<News> News {
			get {
				return news;
			}
		}
		
		List<Changelog> changelog;
		public List<Changelog> Changelog {
			get {
				return changelog;
			}
		}
		#endregion
	}
}
