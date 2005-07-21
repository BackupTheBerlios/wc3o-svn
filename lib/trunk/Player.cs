using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace Wc3o {
    [Serializable]
    public class Player : DataObject {
       
        #region " Constructor "
		public Player() {
		}

        public Player(string name) {
            this.name = name;
            this.messages = new List<Message>();
            this.units = new List<Unit>();
            this.buildings = new List<Building>();
			this.sectors = new List<Sector>();
			this.statistics = new Dictionary<string, object>();
			statistics["BestRank"] = 0;
			statistics["BestScore"] = 0;
			Game.GameData.Players.Add(name,this);
        }
        #endregion

        #region " Destroy "
        public override void Destroy() {
			if (Alliance != null) {
				vote = null;
				Alliance.Members.Remove(this);
			}

			List<DataObject> objects = new List<DataObject>();
			foreach (Building b in Buildings)
				objects.Add(b);
			foreach (Unit u in Units)
				objects.Add(u);
			foreach (DataObject o in objects)
				o.Destroy();

			List<Sector> sectors = new List<Sector>();
			foreach (Sector s in Sectors)
				sectors.Add(s);
			foreach (Sector s in sectors)
				s.Owner = null;

			foreach (Player p in Game.GameData.Players.Values)
				if (p.Vote == this)
					p.Vote = p;

			Game.GameData.Players.Remove(this.Name);
        }
        #endregion

        #region " Properties "

		#region " Ressources "
		void GetRessourcesPerTick(out int gold, out int lumber) {
			gold = 0;
			lumber = 0;

			foreach (Sector s in Sectors) {
				int workerForGold = 0;
				int workerForLumber = 0;
				foreach (Unit u in s.Units)
					if (u.Owner == this && u.Action == UnitAction.WorkForGold)
						workerForGold += u.Number;
					else if (u.Owner == this && u.Action == UnitAction.WorkForLumber)
						workerForLumber += u.Number;
				workerForGold = Game.Min(workerForGold, Configuration.Max_Gold_Worker_Per_Sector);

				if (Fraction == Wc3o.Fraction.Humans) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.TownHall) || s.HasBuilding(BuildingType.Keep) || s.HasBuilding(BuildingType.Castle);
					if (hasMainBuilding)
						gold += workerForGold * s.GoldPerTick;
					if (hasMainBuilding || s.HasAvailableBuilding(BuildingType.LumberMill))
						lumber += workerForLumber * s.LumberPerTick;
				}
				else if (Fraction == Wc3o.Fraction.Orcs) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.GreatHall) || s.HasBuilding(BuildingType.Stronghold) || s.HasBuilding(BuildingType.Fortress);
					if (hasMainBuilding)
						gold += workerForGold * s.GoldPerTick;
					if (hasMainBuilding || s.HasAvailableBuilding(BuildingType.WarMill))
						lumber += workerForLumber * s.LumberPerTick;
				}
				else if (Fraction == Wc3o.Fraction.NightElves) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.TreeOfLife) || s.HasBuilding(BuildingType.TreeOfAges) || s.HasBuilding(BuildingType.TreeOfEternity);
					if (hasMainBuilding)
						gold += workerForGold * s.GoldPerTick;
					lumber += workerForLumber * s.LumberPerTick;
				}
				else if (Fraction == Wc3o.Fraction.Undead) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.Necropolis) || s.HasBuilding(BuildingType.HallsOfTheDead) || s.HasBuilding(BuildingType.BlackCitadel);
					if (s.HasBuilding(BuildingType.HauntedGoldMine))
						gold += workerForGold * s.GoldPerTick;
					if (hasMainBuilding || s.HasAvailableBuilding(BuildingType.Graveyard))
						lumber += workerForLumber * s.LumberPerTick;
				}
			}

			int upkeep = Upkeep;
			if (upkeep >= Configuration.Upkeep_Level2)
				gold = (int)(gold * Configuration.Upkeep_Level2_Factor);
			else if (upkeep >= Configuration.Upkeep_Level1)
				gold = (int)(gold * Configuration.Upkeep_Level1_Factor);

			gold = Game.Max(gold, Configuration.Min_Gold_Income);
			lumber = Game.Max(lumber, Configuration.Min_Lumber_Income);
		}

		public int GoldPerTick {
			get {
				int gold,lumber;
				GetRessourcesPerTick(out gold,out lumber);
				return gold;
			}
		}

		public int LumberPerTick {
			get {
				int gold,lumber;
				GetRessourcesPerTick(out gold,out lumber);
				return lumber;
			}
		}
		#endregion

		public string MailLink {
			get {
				return "<a href='Mail.aspx?Recipient=" + name + "'>" + FullName + "</a>";
			}
		}

		public string FullName {
			get {
				if (Alliance == null || !IsAccepted)
					return Name;
				else
					return "[" + Alliance.Name + "] " + Name;
			}
		}

		public int Upkeep {
			get {
				int i=0;
				foreach (Unit u in Units)
					if (!u.IsInTraining)
						i += u.Info.Food*u.Number;
				return i;
			}
		}

		public int Food {
			get {
				int i = 0;
				foreach (Building b in Buildings)
					if (!b.IsInConstruction || b.IsUpgrading)
						i += b.Info.Food * b.Number;
				if (i > Configuration.Max_Food)
					return Configuration.Max_Food;
				return i;
			}
		}

		string name;
		public string Name {
			get {
				return name;
			}
		}

		List<Message> messages;
        public List<Message> Messages {
            get {
                return messages;
            }
        }

        Alliance alliance;
		public Alliance Alliance {
			get {
				return alliance;
			}
			set {
				if (alliance != null) {
					foreach (Player p in alliance.Members)
						if (p.Vote == this)
							p.vote = p;
					alliance.Members.Remove(this);
					Vote = null;
				}
				alliance = value;
				if (alliance != null) {
					alliance.Members.Add(this);
					Vote = this;
				}
			}
		}

		List<Unit> units;
        public List<Unit> Units {
            get {
                return units;
            }
        }

        List<Building> buildings;
        public List<Building> Buildings {
            get {
                return buildings;
            }
        }

		List<Sector> sectors;
		public List<Sector> Sectors {
			get {
				return sectors;
			}
		}

		string password;
        public string Password {
            get {
                return password;
            }
            set {
                password = value;
            }
        }

        string gfx;
        public string Gfx {
            get {
                return gfx;
            }
            set {
                gfx = value;
            }
        }

        string description;
        public string Description {
            get {
                return description;
            }
            set {
                description = value;
            }
        }

        string email;
        public string Email {
            get {
                return email;
            }
            set {
                email = value;
            }
        }

        Player vote;
		public Player Vote {
			get {
				return vote;
			}
			set {
				vote = value;
				if (alliance != null && accepted) {
					Player[] players = alliance.Members.ToArray();
					int[] votes = new int[alliance.Members.Count];
					foreach (Player p in alliance.Members)
						for (int i = 0; i < players.Length; i++)
							if (players[i] == p.Vote) {
								votes[i]++;
								break;
							}
					Player player = null;
					int v = 0;
					for (int i = 0; i < votes.Length; i++)
						if (votes[i] > v) {
							v = votes[i];
							player = players[i];
						}
					alliance.Leader = player;
				}
			}
		}

		Fraction fraction;
        public Fraction Fraction {
            get {
                return fraction;
            }
            set {
                fraction = value;
            }
        }

        public FractionInfo FractionInfo {
            get {
                return FractionInfo.Get(Fraction);
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

        int score;
        public int Score {
            get {
                return score;
            }
            set {
                score = value;
            }
        }

        int rank;
        public int Rank {
            get {
                return rank;
            }
            set {
                rank = value;
            }
        }

        DateTime online;
        public DateTime Online {
            get {
                return online;
            }
            set {
                online = value;
            }
        }

        DateTime registration;
        public DateTime Registration {
            get {
                return registration;
            }
            set {
                registration = value;
            }
        }

        bool accepted;
        public bool IsAccepted {
            get {
                return accepted;
            }
            set {
                accepted = value;
			}
        }

        bool admin;
        public bool IsAdmin {
            get {
                return admin;
            }
            set {
                admin = value;
            }
        }

		Dictionary<string, object> statistics;
		public Dictionary<string, object> Statistics {
			get {
				return statistics;
			}
		}

		public int League {
			get {
				return Game.GetLeague(Rank);
			}
		}

		public int BestLeague {
			get {
				return Game.GetLeague((int)statistics["BestRank"]);
			}
		}

		public int LeagueRank {
			get {
				return rank - (League - 1) * Configuration.Player_Per_League;
			}
		}

		public int BestLeagueRank {
			get {
				return (int)statistics["BestRank"] - (BestLeague - 1) * Configuration.Player_Per_League;
			}
		}

		public string RankLeague {
			get {
				return LeagueRank + " / " + League;
			}
		}

		public bool IsProtected {
			get {
				return Registration.AddHours(Configuration.Hours_To_Be_Protected) >= DateTime.Now;
			}
		}

		public string SmallEmblem {
			get {
				return "<img src='" + Gfx + FractionInfo.SmallEmblem + "' />";
			}
		}
        #endregion

		#region " Methods "
		public Unit GetUnitByHashcode(int hash) {
			foreach (Unit u in Units)
				if (u.GetHashCode() == hash)
					return u;
			return null;
		}

		public Building GetBuildingByHashcode(int hash) {
			foreach (Building b in Buildings)
				if (b.GetHashCode() == hash)
					return b;
			return null;
		}

		public bool IsAlly(Player p) {
			if (p == null)
				return false;
			if (this == p)
				return true;
			if (p.Alliance == null || !p.IsAccepted || this.Alliance == null || !this.IsAccepted)
				return false;
			return (p.Alliance == this.Alliance);
		}

		public bool HasView(Sector s) {
			if (IsAlly(s.Owner))
				return true;

			foreach (Unit u in s.Units)
				if ((u.IsAvailable || u.IsWorking) && IsAlly(u.Owner))
					return true;

			return false;
		}

		public bool CanAttack(Player p) {
			return p == null || (!IsProtected && !p.IsProtected && League != 0 && p.League == League && !IsAlly(p));
		}

		public bool HasAvailableBuilding(BuildingType t) {
			foreach (Building b in Buildings)
				if (b.Type == t && !b.IsInConstruction)
					return true;
			return false;
		}

		public bool HasBuildingForRequirement(BuildingType t) {
			if (t == BuildingType.None)
				return true;
			else if (HasAvailableBuilding(t))
				return true;
			else if (t == BuildingType.TownHall) {
				if (HasAvailableBuilding(BuildingType.Keep) || HasAvailableBuilding(BuildingType.Castle))
					return true;
			}
			else if (t == BuildingType.Keep) {
				if (HasAvailableBuilding(BuildingType.Castle))
					return true;
			}
			else if (t == BuildingType.GreatHall) {
				if (HasAvailableBuilding(BuildingType.Stronghold) || HasAvailableBuilding(BuildingType.Fortress))
					return true;
			}
			else if (t == BuildingType.Stronghold) {
				if (HasAvailableBuilding(BuildingType.Fortress))
					return true;
			}
			else if (t == BuildingType.TreeOfLife) {
				if (HasAvailableBuilding(BuildingType.TreeOfAges) || HasAvailableBuilding(BuildingType.TreeOfEternity))
					return true;
			}
			else if (t == BuildingType.TreeOfAges) {
				if (HasAvailableBuilding(BuildingType.TreeOfEternity))
					return true;
			}
			else if (t == BuildingType.Necropolis) {
				if (HasAvailableBuilding(BuildingType.HallsOfTheDead) || HasAvailableBuilding(BuildingType.BlackCitadel))
					return true;
			}
			else if (t == BuildingType.HallsOfTheDead) {
				if (HasAvailableBuilding(BuildingType.BlackCitadel))
					return true;
			}

			return false;
		}
		#endregion
	}

    #region " FractionInfo "
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
    #endregion

	public enum Fraction { Humans, Orcs, Undead, NightElves, Neutrals }
}
