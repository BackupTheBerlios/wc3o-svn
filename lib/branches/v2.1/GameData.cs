using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections.Generic;

namespace Wc3o {
	[Serializable]
	public class GameData {

		#region " Constructor/Destructor "
		public GameData() {
			players = new SortedDictionary<string, Player>();
			sectors = new SortedDictionary<Coordinate, Sector>();
			alliances = new SortedDictionary<string, Alliance>();
			ticks = new TickInfo();
		}

		~GameData() {
			Save(this);
		}
		#endregion

		#region " Load/Save (Serialization) "
		public static void Save(GameData d) {
			try {
				Game.Logger.Log("Saving Game Data ...");
				DateTime date = Game.GetCorrectedDate();
				new System.IO.FileInfo(Configuration.Physical_Application_Path + "\\App_Data\\Game.dat").CopyTo(Configuration.Physical_Application_Path + "\\App_Data\\Backup\\Game_" + date.Day.ToString() + "." + date.Month.ToString() + "." + date.Year.ToString() + ", " + date.Hour.ToString() + "-" + date.Minute.ToString() + "-" + date.Second.ToString() + ".dat");
				FileStream f = new FileStream(Configuration.Physical_Application_Path + "\\App_Data\\Game.dat", FileMode.OpenOrCreate);
				new BinaryFormatter().Serialize(f, d);
				f.Close();
				Game.Logger.Log("Saving of Game Data done.");
			} catch (Exception e) {
				Game.Logger.Log("Exception while saving Game Data: "+e.Message);
			}
		}

		public static GameData Load() {
			try {
				Game.Logger.Log("Loading Game Data ...");
				FileStream f = new FileStream(Configuration.Physical_Application_Path + "\\App_Data\\Game.dat", FileMode.Open);
				GameData d = (GameData)new BinaryFormatter().UnsafeDeserialize(f,null);
				f.Close();
				Game.Logger.Log("Loading of Game Data done.");
				return d;
			} catch (Exception e) {
				Game.Logger.Log("Exception while loading Game Data: " + e.Message);
				return new GameData();
			}
		}
		#endregion

		#region " Players "
		SortedDictionary<string, Player> players;
		public SortedDictionary<string, Player> Players {
			get {
				return players;
			}
		}
		#endregion

		#region " Alliances "
		SortedDictionary<string, Alliance> alliances;
		public SortedDictionary<string, Alliance> Alliances {
			get {
				return alliances;
			}
		}
		#endregion

		#region " Sectors "
		SortedDictionary<Coordinate, Sector> sectors;
		public SortedDictionary<Coordinate, Sector> Sectors {
			get {
				return sectors;
			}
		}
		#endregion

		#region " Ticks "
		TickInfo ticks;
		public TickInfo Ticks {
			get {
				return ticks;
			}
		}
		#endregion

	}

	#region " DataObject "
	[Serializable]
	public abstract class DataObject {
		public abstract void Destroy();
	}
	#endregion

	#region " TickInfo "
	[Serializable]
	public class TickInfo : DataObject {
		#region " Constructor "
		public TickInfo() {
			ressourceTick = DateTime.Now.AddSeconds(Configuration.Seconds_For_Ressource_Tick);
			rankingTick = DateTime.Now.AddSeconds(Configuration.Seconds_For_Ranking_Tick);
		}
		#endregion

		#region " Destroy "
		public override void Destroy() { }
		#endregion

		#region " Properties "
		DateTime rankingTick;
		public DateTime RankingTick {
			get {
				return rankingTick;
			}
			set {
				rankingTick = value;
			}
		}

		DateTime ressourceTick;
		public DateTime RessourceTick {
			get {
				return ressourceTick;
			}
			set {
				ressourceTick = value;
			}
		}
		#endregion
	}
	#endregion
}
