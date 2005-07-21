using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Map_aspx : System.Web.UI.Page {

		Player player;
		Sector s;

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;

			if (RemoteScripting.InvokeMethod(Page))
				return;

			#region " Kalkulation des Kartenausschnittes "
			s = Wc3o.Game.CurrentSector;
			if (s == null)
				Response.Redirect("Map.aspx?Sector=" + player.Sectors[0].Coordinate.ToString());

			int centerX = s.Coordinate.X; int centerY = s.Coordinate.Y;
			int startX, startY, endX, endY;
			int maxX = Configuration.Map_Size;
			int maxY = Configuration.Map_Size;
			startX = centerX - Configuration.Sectors_To_Show_On_Map / 2;
			startY = centerY - Configuration.Sectors_To_Show_On_Map / 2;
			endX = startX + Configuration.Sectors_To_Show_On_Map;
			endY = startY + Configuration.Sectors_To_Show_On_Map - 1;
			#endregion

			#region " Scroll Buttons "
			if (startX <= 1)
				lblScrollLeft.Visible = false;
			else {
				lblScrollLeft.Visible = true;
				lblScrollLeft.Text = "<a href='Map.aspx?Sector=" + ((int)centerX - 5).ToString() + "_" + centerY.ToString() + "'><img src='" + player.Gfx + "/Game/Left.gif' title='Nach links scrollen' /></a>";
			}
			if (startY <= 1)
				lblScrollUp.Visible = false;
			else {
				lblScrollUp.Visible = true;
				lblScrollUp.Text = "<a href='Map.aspx?Sector=" + centerX.ToString() + "_" + ((int)centerY - 5).ToString() + "'><img src='" + player.Gfx + "/Game/Up.gif' title='Nach oben scrollen' /></a>";
			}
			if (endX > maxX)
				lblScrollRight.Visible = false;
			else {
				lblScrollRight.Visible = true;
				lblScrollRight.Text = "<a href='Map.aspx?Sector=" + ((int)centerX + 5).ToString() + "_" + centerY.ToString() + "'><img src='" + player.Gfx + "/Game/Right.gif' title='Nach rechts scrollen' /></a>";
			}
			if (endY >= maxY)
				lblScrollDown.Visible = false;
			else {
				lblScrollDown.Visible = true;
				lblScrollDown.Text = "<a href='Map.aspx?Sector=" + centerX.ToString() + "_" + ((int)centerY + 5).ToString() + "'><img src='" + player.Gfx + "/Game/Down.gif' title='Nach unten scrollen' /></a>";
			}
			#endregion

			#region " Kartenrand "
			imgTopLeft.ImageUrl = player.Gfx + "/Game/Tl.gif";
			imgTop.ImageUrl = player.Gfx + "/Game/T.gif";
			imgTopRight.ImageUrl = player.Gfx + "/Game/Tr.gif";
			imgLeft.ImageUrl = player.Gfx + "/Game/L.gif";
			imgRight.ImageUrl = player.Gfx + "/Game/R.gif";
			imgBottomLeft.ImageUrl = player.Gfx + "/Game/Bl.gif";
			imgBottom.ImageUrl = player.Gfx + "/Game/B.gif";
			imgBottomRight.ImageUrl = player.Gfx + "/Game/Br.gif";
			#endregion

			if (!IsPostBack) {
				foreach (Wc3o.Sector sector in player.Sectors)
					drpSectors.Items.Add(new System.Web.UI.WebControls.ListItem(sector.FullName, sector.Coordinate.X + "_" + sector.Coordinate.Y));
				if (!Wc3o.Game.SelectByValue(drpSectors, s.Coordinate.ToString())) {
					drpSectors.Items.Insert(0, new System.Web.UI.WebControls.ListItem("<< " + s.FullName + " >>", s.Coordinate.ToString()));
					drpSectors.SelectedIndex = 0;
				}
			}

			lblMap.Text = "<table class='Map' border='0' cellpadding='0' cellspacing='1'>";
			for (int y = startY; y <= endY; y++) {
				lblMap.Text += "<tr>";
				for (int x = startX; x < endX; x++) {
					Wc3o.Sector sector = null;
					try {
						sector = Wc3o.Game.GameData.Sectors[new Coordinate(x, y)];
					} catch { }
					if (sector == null)
						lblMap.Text += "<td></td>";
					else {
						string bgImage = player.Gfx + "/Game/n.gif";
						string color = "";
						#region " Image and Color "
						if (sector.Gold > 0 && sector.Lumber > 0) {
							if (sector.Owner == null)
								bgImage = player.Gfx + "/Game/Bh.gif";
							else
								switch (sector.Owner.Fraction) {
									case Wc3o.Fraction.Undead:
										bgImage = player.Gfx + "/Game/Bu.gif";
										break;
									case Wc3o.Fraction.NightElves:
										bgImage = player.Gfx + "/Game/Bn.gif";
										break;
									case Wc3o.Fraction.Orcs:
										bgImage = player.Gfx + "/Game/Bo.gif";
										break;
									case Wc3o.Fraction.Humans:
										bgImage = player.Gfx + "/Game/Bh.gif";
										break;
								}
						}
						else if (sector.Gold > 0)
							bgImage = player.Gfx + "/Game/M.gif";
						else if (sector.Lumber > 0)
							bgImage = player.Gfx + "/Game/F.gif";

						if (sector.Owner == null)
							color = Configuration.Color_Neutral;
						else if (sector.Owner == player)
							color = Configuration.Color_Player;
						else if (player.IsAlly(sector.Owner))
							color = Configuration.Color_Ally;
						else if (player.CanAttack(sector.Owner))
							color = Configuration.Color_Enemy;
						else
							color = Configuration.Color_League;
						#endregion

						string owner = "-";
						if (sector.Owner != null)
							owner = sector.Owner.FullName;

						lblMap.Text += "<td OnClick=\"C('" + sector.Coordinate.X + "','" + sector.Coordinate.Y + "')\" OnMouseOut=\"O()\" OnMouseOver=\"I('" + sector.Coordinate.X + "','" + sector.Coordinate.Y + "','" + sector.Name + "','" + owner + "')\" style=\"background-image:url(" + bgImage + "); color:" + color + ";\">" + x + ":" + y + "</td>";
					}
				}
				lblMap.Text += "</tr>";
			}
			lblMap.Text += "</table>";
		}


		protected void btnCoords_Click(object sender, EventArgs e) {
			int x = int.Parse(txtX.Text);
			int y = int.Parse(txtY.Text);
			if (x > 0 && y > 0 && x <= Configuration.Map_Size && y <= Configuration.Map_Size)
				Response.Redirect("Map.aspx?Sector=" + x.ToString() + "_" + y.ToString());
			else
				Wc3o.Game.Message(Master, "This sector does not exist.", MessageType.Error);
		}


		protected void btnSector_Click(object sender, EventArgs e) {
			Sector s = Wc3o.Game.GetSectorByName(txtSector.Text);
			if (s == null)
				Wc3o.Game.Message(Master, "There is no sector with this name.", MessageType.Error);
			else
				Response.Redirect("Map.aspx?Sector=" + s.Coordinate.X + "_" + s.Coordinate.Y);
		}


		protected void drpSectors_SelectedIndexChanged(object sender, EventArgs e) {
			Response.Redirect("Map.aspx?Sector=" + drpSectors.SelectedValue);
		}


		public string LoadInfo(string x, string y) {
			string str = "";

			Wc3o.Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(int.Parse(x), int.Parse(y))];
			str += "<b><a href='Sector.aspx?Sector=" + sector.Coordinate.ToString() + "'>" + sector.FullName + "</a></b><br />";
			if (sector.Owner == null)
				str += "<i>Neutral sector.</i><br /><br />";
			else
				str += "Owner: <b><a href='PlayerInfo.aspx?Player=" + sector.Owner.Name + "'>" + sector.Owner.FullName + "</a></b><br /><br />";

			if (player.HasView(sector)) {
				bool playerUnits = false;
				bool playerBuildings = false;
				bool allyUnits = false;
				bool allyBuildings = false;
				bool enemyUnits = false;
				bool enemyBuildings = false;
				bool leagueUnits = false;
				bool leagueBuildings = false;
				bool neutralUnits = false;
				bool neutralBuildings = false;

				foreach (Unit u in sector.Units)
					if (u.IsAvailable || u.IsWorking) {
						if (u.Owner == null)
							neutralUnits = true;
						else if (u.Owner == player)
							playerUnits = true;
						else if (player.IsAlly(u.Owner))
							allyUnits = true;
						else if (player.CanAttack(u.Owner))
							enemyUnits = true;
						else
							neutralUnits = true;
					}

				foreach (Building b in sector.Buildings) {
					if (sector.Owner == null)
						neutralBuildings = true;
					else if (sector.Owner == player)
						playerBuildings = true;
					else if (player.IsAlly(sector.Owner))
						allyBuildings=true;
					else if (player.CanAttack(sector.Owner))
						enemyBuildings = true;
					else
						neutralBuildings = true;
					break;
				}
	

				if (playerUnits || playerBuildings) {
					str += "<div class='Player'>";
					if (playerUnits)
						str += "<img src='" + player.Gfx + "/Game/Units.gif' />";
					if (playerBuildings)
						str += "<img src='" + player.Gfx + "/Game/Buildings.gif' />";
					str += "</div>";
				}
				if (neutralUnits || neutralBuildings) {
					str += "<div class='Player'>";
					if (neutralUnits)
						str += "<img src='" + player.Gfx + "/Game/Units.gif' />";
					if (neutralBuildings)
						str += "<img src='" + player.Gfx + "/Game/Buildings.gif' />";
					str += "</div>";
				}
				if (allyUnits || allyBuildings) {
					str += "<div class='Player'>";
					if (allyUnits)
						str += "<img src='" + player.Gfx + "/Game/Units.gif' />";
					if (allyBuildings)
						str += "<img src='" + player.Gfx + "/Game/Buildings.gif' />";
					str += "</div>";
				}
				if (enemyUnits || enemyBuildings) {
					str += "<div class='Player'>";
					if (enemyUnits)
						str += "<img src='" + player.Gfx + "/Game/Units.gif' />";
					if (enemyBuildings)
						str += "<img src='" + player.Gfx + "/Game/Buildings.gif' />";
					str += "</div>";
				}
				if (leagueUnits || leagueBuildings) {
					str += "<div class='Player'>";
					if (leagueUnits)
						str += "<img src='" + player.Gfx + "/Game/Units.gif' />";
					if (leagueBuildings)
						str += "<img src='" + player.Gfx + "/Game/Buildings.gif' />";
					str += "</div>";
				}


			}
			else
				str += "<i>You have no view on this sector.</i>";

			return str;
		}

	}
}
