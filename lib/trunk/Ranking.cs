using System;
using System.Collections.Generic;

namespace Wc3o {
	public class Ranking {

		System.Collections.ArrayList users;

		public void CalculateRanking() {
			users = new System.Collections.ArrayList();

			foreach (Player p in Game.GameData.Players.Values)
				users.Add(p);

			int maxRessources = 0;
			int maxSector = 0;
			int maxUnits = 0;
			int maxBuildings = 0;

			foreach (Player user in users) {
				int tmp;

				tmp = GetRessourcesScore(user);
				if (tmp > maxRessources)
					maxRessources = tmp;

				tmp = GetSectorScore(user);
				if (tmp > maxSector)
					maxSector = tmp;

				tmp = GetUnitScore(user);
				if (tmp > maxUnits)
					maxUnits = tmp;

				tmp = GetBuildingScore(user);
				if (tmp > maxBuildings)
					maxBuildings = tmp;
			}

			foreach (Player user in users)
				user.Score = Convert.ToInt32(Convert.ToDouble(GetUnitScore(user)) / maxUnits * 400) + Convert.ToInt32(Convert.ToDouble(GetBuildingScore(user)) / maxBuildings * 200) + Convert.ToInt32(Convert.ToDouble(GetRessourcesScore(user)) / maxRessources * 100) + Convert.ToInt32(Convert.ToDouble(GetSectorScore(user)) / maxSector * 300);

			users.Sort(new UserScoreComparer());
			for (int i = 1; i <= users.Count; i++) {
				Player u = (Player)users[i - 1];
				u.Rank = i;
				
				int bestRank=(int)u.Statistics["BestRank"];
				if (bestRank <= 0 || u.Rank < bestRank) {
					u.Statistics["BestRank"] = u.Rank;
					u.Statistics["BestScore"] = u.Score;
				}
			}
		}

		private int GetUnitScore(Player user) {
			int score = 1;
			foreach (Unit unit in user.Units)
				if (!unit.IsInTraining)
					score += unit.UnitInfo.Score * unit.Number;
			return score;
		}

		private int GetBuildingScore(Player user) {
			int score = 1;
			foreach (Building building in user.Buildings)
				if (building.IsAvailable)
					score += building.BuildingInfo.Score;
			return score;
		}

		private int GetSectorScore(Player user) {
			return user.Sectors.Count;
		}

		private int GetRessourcesScore(Player user) {
			return 1 + user.Gold + user.Lumber * 3;
		}

	}

	public class UserScoreComparer : System.Collections.IComparer {
		public int Compare(Object a, Object b) {
			return ((Player)b).Score - ((Player)a).Score;
		}
	}

	public class UserRankComparer : System.Collections.IComparer {
		public int Compare(Object a, Object b) {
			return ((Player)a).Rank - ((Player)b).Rank;
		}
	}
}