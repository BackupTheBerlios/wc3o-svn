using System;
using System.Collections.Generic;
using System.Data;

namespace Wc3o.Pages.Game {
	public partial class Ranking_aspx : System.Web.UI.Page {

		Player player;

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;

			if (Request.QueryString["League"] != null) {

				DataTable ranking = new DataTable();
				ranking.Columns.Add("Rank", typeof(String));
				ranking.Columns.Add("Name", typeof(String));
				ranking.Columns.Add("Score", typeof(String));

				if (Request.QueryString["League"] == "0") {
					pnlPlayer.Visible = false;
					pnlAlliance.Visible = true;
					ranking.Columns.Add("Members", typeof(String));
					ranking.Columns.Add("Average", typeof(String));

					System.Collections.ArrayList alliances = new System.Collections.ArrayList();
					foreach (Alliance a in Wc3o.Game.GameData.Alliances.Values)
						alliances.Add(a);


					alliances.Sort(new Wc3o.AllianceScoreComparer());
					int rank = 1;

					foreach (Alliance alliance in alliances) {
						int numberOfMembers = alliance.AcceptedMembers.Count;
						int score = alliance.Score;
						if (numberOfMembers > 0 && score > 0) {

							string rankFormat = "<div>";
							string userFormat = "<div>";

							if (player.Alliance == alliance)
								userFormat = "<div style='color:" + Configuration.Color_Ally + "'>";
							else
								userFormat = "<div style='color:" + Configuration.Color_Enemy + "'>";

							if (rank == 1)
								rankFormat = "<div style='font-size:19px;'>";
							else if (rank == 2)
								rankFormat = "<div style='font-size:17px;'>";
							else if (rank == 3)
								rankFormat = "<div style='font-size:15px;'>";
							else if (rank < 11)
								rankFormat = "<div style='font-size:13px;'>";
							else
								rankFormat = "<div style='font-size:11xp;'>";

							DataRow row = ranking.NewRow();
							row["Rank"] = rankFormat + userFormat + Wc3o.Game.Format(rank) + "</div></div>";
							row["Name"] = "<a href='AllianceInfo.aspx?Alliance=" + alliance.Name + "'>" + rankFormat + userFormat + alliance.FullName + "</div></div></a>";
							row["Score"] = rankFormat + userFormat + Wc3o.Game.Format(score) + "</div></div>";
							row["Members"] = rankFormat + userFormat + Wc3o.Game.Format(numberOfMembers) + "</div></div>";
							row["Average"] = rankFormat + userFormat + Wc3o.Game.Format((int)(alliance.Score / numberOfMembers)) + "</div></div>";
							ranking.Rows.Add(row);
							rank++;
						}
					}

					grdAlliance.DataSource = ranking;
					grdAlliance.DataBind();
				}
				else {
					pnlPlayer.Visible = true;
					pnlAlliance.Visible = false;
					ranking.Columns.Add("Image", typeof(String));

					int league = int.Parse(Request.QueryString["League"]);
					List<Player> players = new List<Player>();
					foreach (Player p in Wc3o.Game.GetPlayers(league))
						players.Add(p);
					players.Sort(new PlayerScoreComparer());

					#region " Checks if the given league is valid "
					int rankedPlayers = 0;
					foreach (Player p in Wc3o.Game.GameData.Players.Values)
						if (p.Score > 0)
							rankedPlayers++;

					int maxLeague = Wc3o.Game.GetLeague(rankedPlayers);
					if (league > maxLeague || league < 0)
						Response.Redirect("Ranking.aspx?League=" + player.League, true);
					#endregion

					#region " Fills lblLeague and lblLeagues "
					for (int i = 1; i <= maxLeague; i++)
						if (i != league)
							lblLeagues.Text += "[<a href='Ranking.aspx?League=" + i.ToString() + "'>League " + i + "</a>] ";
					lblLeague.Text = Wc3o.Game.Format(league);
					#endregion

					foreach (Player p in players) {
						string rankFormat = "";
						string userFormat = "";

						if (player == p)
							userFormat = "<div style='color:" + Configuration.Color_Player + "'>";
						else if (player.IsAlly(p))
							userFormat = "<div style='color:" + Configuration.Color_Ally + "'>";
						else if (player.CanAttack(p))
							userFormat = "<div style='color:" + Configuration.Color_Enemy + "'>";
						else
							userFormat = "<div style='color:" + Configuration.Color_League + "'>";

						if (p.Rank == 1)
							rankFormat = "<div style='font-size:19px;'>";
						else if (p.Rank == 2)
							rankFormat = "<div style='font-size:17px;'>";
						else if (p.Rank == 3)
							rankFormat = "<div style='font-size:15px;'>";
						else if (p.Rank < 11)
							rankFormat = "<div style='font-size:13px;'>";
						else
							rankFormat = "<div style='font-size:11px;'>";

						DataRow row = ranking.NewRow();
						row["Rank"] = rankFormat + userFormat + Wc3o.Game.Format(p.Rank) + "</div></div>";
						row["Name"] = rankFormat + userFormat + p.FullName + "</div></div>";
						row["Score"] = rankFormat + userFormat + Wc3o.Game.Format(p.Score) + "</div></div>";
						row["Image"] = "<a href='PlayerInfo.aspx?Player=" + p.Name + "'><img src='" + player.Gfx + "/" + p.FractionInfo.SmallEmblem + "' /></a>";
						ranking.Rows.Add(row);
					}

					grdPlayer.DataSource = ranking;
					grdPlayer.DataBind();

					if (lblLeagues.Text == "")
						lblLeagues.Visible = false;
				}

				int numberOfUnrankedPlayers = 0;
				foreach (Player p in Wc3o.Game.GameData.Players.Values)
					if (p.Score <= 0)
						numberOfUnrankedPlayers++;
				if (numberOfUnrankedPlayers == 1)
					lblUnranked.Text = "<b>" + Wc3o.Game.Format(numberOfUnrankedPlayers) + " Player</b> has no score and is";
				else
					lblUnranked.Text = "<b>" + Wc3o.Game.Format(numberOfUnrankedPlayers) + " Players</b> have no score and are";

			}
			else {
				int l = player.League;
				if (l == 0) l = 1;
				Response.Redirect("Ranking.aspx?League=" + l, true);
			}

			lblRankingTick.Text = Wc3o.Game.TimeSpan(Wc3o.Game.GameData.Ticks.RankingTick);
		}

	}
}