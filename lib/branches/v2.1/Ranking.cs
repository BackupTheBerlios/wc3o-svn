using System;
using System.Collections.Generic;

namespace Wc3o {
	public class Ranking {

		List<Player> players;

		public void Calculate() {
			int maxRessourceScore = 0;
			int maxSectorScore = 0;
			int maxUnitScore = 0;
			int maxBuildingScore = 0;

			players = new List<Player>();
			foreach (Player player in Game.GameData.Players.Values)
				players.Add(player);

			//finds the maximum for each score category
			foreach (Player player in players) {
				int i;

				i = GetRessourceScore(player);
				if (i > maxRessourceScore)
					maxRessourceScore = i;

				i = GetSectorScore(player);
				if (i > maxSectorScore)
					maxSectorScore = i;

				i = GetUnitScore(player);
				if (i > maxUnitScore)
					maxUnitScore = i;

				i = GetBuildingScore(player);
				if (i > maxBuildingScore)
					maxBuildingScore = i;
			}

			//calculates the score for each player
			foreach (Player player in players) {
				player.Score = (int)((double)GetUnitScore(player) / maxUnitScore * 400 + (double)GetBuildingScore(player) / maxBuildingScore * 200 + (double)GetRessourceScore(player) / maxRessourceScore * 100 + (double)GetSectorScore(player) / maxSectorScore * 300);
				if (player.Score > player.BestScore) //is it a new best score?
					player.BestScore = player.Score;
			}

			players.Sort(new PlayerScoreComparer());

			int rank = 1;
			int league = 1;
			for (int i = 0; i <= players.Count; i++) {
				Player player = players[i];
				player.Rank = rank;
				player.League = league;

				//is it a new best rank?
				if (player.BestRank <= 0 || player.BestLeague > league || (player.BestRank > rank && player.BestLeague >= league)) {
					player.BestRank = rank;
					player.League = league;
				}

				if (league >= Configuration.Player_Per_League) {
					rank = 1;
					league++;
				}
				else
					rank++;
			}
		}

		int GetUnitScore(Player player) {
			int score = 1;
			foreach (Unit unit in player.Units)
				if (!unit.IsInTraining)
					score += unit.UnitInfo.Score * unit.Number;
			return score;
		}

		int GetBuildingScore(Player player) {
			int score = 1;
			foreach (Building building in player.Buildings)
				if (building.IsAvailable)
					score += building.BuildingInfo.Score;
			return score;
		}

		int GetSectorScore(Player player) {
			return player.Sectors.Count;
		}

		int GetRessourceScore(Player player) {
			return 1 + player.Gold + player.Lumber * 3;
		}

	}


	public class PlayerScoreComparer : IComparer<Player>{
		public int Compare(Player x, Player y) {
			return y.Score.CompareTo(x.Score);
		}
	}

}