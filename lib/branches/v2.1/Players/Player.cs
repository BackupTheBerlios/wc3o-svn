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
			this.allianceRank = Wc3o.AllianceRank.Level0;
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

		public void GetRessourcesPerTick(out int gold, out int lumber) {
			gold = 0;
			lumber = 0;

			foreach (Sector s in Sectors) {
				int goldPerTick = 0;
				int lumberPerTick = 0;
				int workerForGold = 0;
				int workerForLumber = 0;
				if (s is GoldSector) //here is only gold
					goldPerTick = (s as GoldSector).GoldPerTick;
				else if (s is LumberSector) //here is only lumber
					lumberPerTick = (s as LumberSector).LumberPerTick;
				else if (s is GoldAndLumberSector) { //here is gold and lumber
					GoldAndLumberSector goldAndLumberSector = s as GoldAndLumberSector;
					goldPerTick = goldAndLumberSector.GoldPerTick;
					lumberPerTick = goldAndLumberSector.LumberPerTick;
				}
				else //no ressources are here, move to the next sector
					continue;

				foreach (Unit u in s.Units)
					if (u.Owner == this && u.Action == UnitAction.WorkForGold)
						workerForGold += u.Number;
					else if (u.Owner == this && u.Action == UnitAction.WorkForLumber)
						workerForLumber += u.Number;
				workerForGold = Game.Min(workerForGold, Configuration.Max_Gold_Worker_Per_Sector);

				if (Fraction == Wc3o.Fraction.Humans) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.TownHall) || s.HasBuilding(BuildingType.Keep) || s.HasBuilding(BuildingType.Castle);
					if (hasMainBuilding)
						gold += workerForGold * goldPerTick;
					if (hasMainBuilding || s.HasAvailableBuilding(BuildingType.LumberMill))
						lumber += workerForLumber * lumberPerTick;
				}
				else if (Fraction == Wc3o.Fraction.Orcs) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.GreatHall) || s.HasBuilding(BuildingType.Stronghold) || s.HasBuilding(BuildingType.Fortress);
					if (hasMainBuilding)
						gold += workerForGold * goldPerTick;
					if (hasMainBuilding || s.HasAvailableBuilding(BuildingType.WarMill))
						lumber += workerForLumber * lumberPerTick;
				}
				else if (Fraction == Wc3o.Fraction.NightElves) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.TreeOfLife) || s.HasBuilding(BuildingType.TreeOfAges) || s.HasBuilding(BuildingType.TreeOfEternity);
					if (hasMainBuilding)
						gold += workerForGold * goldPerTick;
					lumber += workerForLumber * lumberPerTick;
				}
				else if (Fraction == Wc3o.Fraction.Undead) {
					bool hasMainBuilding = s.HasAvailableBuilding(BuildingType.Necropolis) || s.HasBuilding(BuildingType.HallsOfTheDead) || s.HasBuilding(BuildingType.BlackCitadel);
					if (s.HasBuilding(BuildingType.HauntedGoldMine))
						gold += workerForGold * goldPerTick;
					if (hasMainBuilding || s.HasAvailableBuilding(BuildingType.Graveyard))
						lumber += workerForLumber * lumberPerTick;
				}
			}

			gold += Configuration.Min_Gold_Income;
			lumber += Configuration.Min_Lumber_Income;

			int upkeep = Upkeep;
			if (upkeep >= Configuration.Upkeep_Level2)
				gold = (int)(gold * Configuration.Upkeep_Level2_Factor);
			else if (upkeep >= Configuration.Upkeep_Level1)
				gold = (int)(gold * Configuration.Upkeep_Level1_Factor);
		}

		//remove (it should be in the GUI)
		public string MailLink {
			get {
				return "<a href='Mail.aspx?Recipient=" + name + "'>" + RankedName + "</a>";
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


		public int EstimatedUpkeep {
			get {
				int i = 0;
				foreach (Unit u in Units)
					i += u.Info.Food * u.Number;
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
					allianceRank = Wc3o.AllianceRank.Level0;
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

		int league;
		public int League {
			get {
				return league;
			}
			set {
				league = value;
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

		int bestLeague;
		public int BestLeague {
			get {
				return bestLeague;
			}
			set {
				bestLeague = value;
			}
		}

		int bestRank;
		public int BestRank {
			get {
				return bestRank;
			}
			set {
				bestRank = value;
			}
		}

		int bestScore;
		public int BestScore {
			get {
				return bestScore;
			}
			set {
				bestScore = value;
			}
		}

		AllianceRank allianceRank;
		public AllianceRank AllianceRank {
			get {
				return allianceRank;
			}
			set {
				allianceRank = value;
			}
		}

		public string RankLeague {
			get {
				return Rank + " / " + League;
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

		public string RankedName {
			get {
				string rank = Game.Format(allianceRank,fraction);
				if (rank != "")
					return rank + " " + name;
				else
					return name;
			}
		}
        #endregion

		#region " Methods "
		public bool HasAHigherAllianceRank(Player p) {
			if (p.AllianceRank == AllianceRank.Level2 && allianceRank == AllianceRank.Level3)
				return true;
			if (p.AllianceRank == AllianceRank.Level1 && (allianceRank == AllianceRank.Level3 || allianceRank == AllianceRank.Level2))
				return true;
			if (p.AllianceRank == AllianceRank.Level0 && (allianceRank == AllianceRank.Level3 || allianceRank == AllianceRank.Level2 || allianceRank == AllianceRank.Level1))
				return true;
			return false;
		}

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

		public bool HasBuilding(BuildingType t) {
			foreach (Building b in Buildings)
				if (b.Type == t)
					return true;
			return false;
		}

		public bool HasBuildingForRequirement(BuildingType t) {
			if (t == BuildingType.None)
				return true;
			else if (HasAvailableBuilding(t))
				return true;
			else if (t == BuildingType.TownHall) {
				if (HasBuilding(BuildingType.Keep) || HasBuilding(BuildingType.Castle))
					return true;
			}
			else if (t == BuildingType.Keep) {
				if (HasBuilding(BuildingType.Castle))
					return true;
			}
			else if (t == BuildingType.GreatHall) {
				if (HasBuilding(BuildingType.Stronghold) || HasBuilding(BuildingType.Fortress))
					return true;
			}
			else if (t == BuildingType.Stronghold) {
				if (HasBuilding(BuildingType.Fortress))
					return true;
			}
			else if (t == BuildingType.TreeOfLife) {
				if (HasBuilding(BuildingType.TreeOfAges) || HasBuilding(BuildingType.TreeOfEternity))
					return true;
			}
			else if (t == BuildingType.TreeOfAges) {
				if (HasBuilding(BuildingType.TreeOfEternity))
					return true;
			}
			else if (t == BuildingType.Necropolis) {
				if (HasBuilding(BuildingType.HallsOfTheDead) || HasBuilding(BuildingType.BlackCitadel))
					return true;
			}
			else if (t == BuildingType.HallsOfTheDead) {
				if (HasBuilding(BuildingType.BlackCitadel))
					return true;
			}

			return false;
		}
		#endregion
	}

}
