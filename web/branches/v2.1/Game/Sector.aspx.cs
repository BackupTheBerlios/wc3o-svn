using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Sector_aspx : System.Web.UI.Page {

		Player player;
		Sector sector;

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;
			sector = Wc3o.Game.CurrentSector;

			if (!IsPostBack) {
				hplSector.Text = sector.FullName;
				hplSector.NavigateUrl = "Map.aspx?Sector=" + sector.Coordinate.ToString();
				if (sector.Owner == null)
					hplSector.ForeColor = System.Drawing.Color.FromName(Configuration.Color_Neutral);
				else if (sector.Owner == player)
					hplSector.ForeColor = System.Drawing.Color.FromName(Configuration.Color_Player);
				else if (player.IsAlly(sector.Owner))
					hplSector.ForeColor = System.Drawing.Color.FromName(Configuration.Color_Ally);
				else if (player.CanAttack(sector.Owner))
					hplSector.ForeColor = System.Drawing.Color.FromName(Configuration.Color_Enemy);
				else
					hplSector.ForeColor = System.Drawing.Color.FromName(Configuration.Color_League);

				if (sector.Owner != null) {
					hplOwner.Text = sector.Owner.FullName;
					hplOwner.NavigateUrl = "PlayerInfo.aspx?Player=" + sector.Owner.Name;
				}
				else
					hplOwner.Text = "<i>this sector has no owner.</i>";

				lblGold.Text = Wc3o.Game.Format(sector.Gold);
				imgGold.ImageUrl = player.Gfx + "/Game/Gold.gif";
				lblLumber.Text = Wc3o.Game.Format(sector.Lumber);
				imgLumber.ImageUrl = player.Gfx + "/Game/Lumber.gif";

				foreach (Sector s in player.Sectors)
					drpSectors.Items.Add(new System.Web.UI.WebControls.ListItem(s.FullName, s.Coordinate.ToString()));

				foreach (Unit u in player.Units)
					if (drpSectors.Items.FindByValue(u.Sector.Coordinate.ToString()) == null)
						drpSectors.Items.Add(new System.Web.UI.WebControls.ListItem("< " + u.Sector.FullName + " >", u.Sector.Coordinate.ToString()));

				if (!Wc3o.Game.SelectByValue(drpSectors, sector.Coordinate.ToString()))
					drpSectors.Items.Insert(0, new System.Web.UI.WebControls.ListItem("- " + sector.FullName + " -", sector.Coordinate.ToString()));

				bool canAnnect = false;
				bool creepsOnSector = false;
				bool creepBuildingsOnSector = false;
				List<Player> l = new List<Player>();
				foreach (Unit u in sector.Units) {
					if (u.Owner == null)
						creepsOnSector = true;
					else if (!l.Contains(u.Owner)) {
						if (u.Owner == player && u.IsAvailable)
							canAnnect = true;
						l.Add(u.Owner);
					}
				}

				if (canAnnect && !player.IsAlly(sector.Owner))
					btnCapture.Visible = true;
				else
					btnCapture.Visible = false;

				if (sector.Buildings.Count > 0)
					if (sector.Owner == null)
						creepBuildingsOnSector = true;
					else if (!l.Contains(sector.Owner))
						l.Add(sector.Owner);

				bool hasView = player.HasView(sector);

				foreach (Player p in l)
					if (p == player)
						lblPlayer.Text += "<div class='Player'><a href=\"javascript:LoadUnits('" + sector.Coordinate + "','')\"><b>Your units</b></a><br /><div id='u_' name='u_'></div></div>";
					else if (p.IsAlly(player))
						lblOthers.Text += "<div class='Ally'><a href=\"javascript:LoadSector('" + sector.Coordinate + "','" + p.Name + "')\"><b>" + p.FullName + "</b></a><br /><div id='u_" + p.Name + "' name='u_" + p.Name + "'></div><div id='b_" + p.Name + "' name='b_" + p.Name + "'></div></div><br />";
					else if (hasView && player.CanAttack(p))
						lblOthers.Text += "<div class='Enemy'><a href=\"javascript:LoadSector('" + sector.Coordinate + "','" + p.Name + "')\"><b>" + p.FullName + "</b></a><br /><div id='u_" + p.Name + "' name='u_" + p.Name + "'></div><div id='b_" + p.Name + "' name='b_" + p.Name + "'></div></div><br />";
					else if (hasView)
						lblOthers.Text += "<div class='League'><a href=\"javascript:LoadSector('" + sector.Coordinate + "','" + p.Name + "')\"><b>" + p.FullName + "</b></a><br /><div id='u_" + p.Name + "' name='u_" + p.Name + "'></div><div id='b_" + p.Name + "' name='b_" + p.Name + "'></div></div><br />";

				if (hasView && (creepsOnSector || creepBuildingsOnSector)) //creeps are represented as "-"
					lblOthers.Text += "<div class='Neutral'><a href=\"javascript:LoadSector('" + sector.Coordinate + "','-')\"><b>Creeps</b></a><br /><br /><div id='u_-' name='u_-'></div><div id='b_-' name='b_-'></div></div><br />";

				if (sector.Owner == player) {
					lblPlayer.Text += "<br /><div class='Player'><a href=\"javascript:LoadBuildings('" + sector.Coordinate + "','')\"><b>Your buildings</b></a><br /><div id='b_' name='b_'></div></div>";
					lblPlayer.Text += "<br /><div class='Player'><a href=\"javascript:LoadTraining('" + sector.Coordinate + "')\"><b>Train units</b></a><br /><div id='training' name='training'></div></div>";
					lblPlayer.Text += "<br /><div class='Player'><a href=\"javascript:LoadConstructing('" + sector.Coordinate + "')\"><b>Construct buildings</b></a><br /><div id='constructing' name='constructing'></div></div>";
				}

				if (!hasView)
					lblOthers.Text = "<i>You have no view on this sector.</i>";
				else if (lblOthers.Text.Length <= 0)
					lblOthers.Text = "<i>There are no other players on this sector.</i>";
			}

			#region " Refresh "
			if (Request.QueryString["Refresh"] != null)
				switch (Request.QueryString["Refresh"]) {
					case "Units":
						lblOpen.Text = "<script language='javascript'>Refresh('6','" + sector.Coordinate + "')</script>";
						break;
					case "Buildings":
						lblOpen.Text = "<script language='javascript'>Refresh('7','" + sector.Coordinate + "')</script>";
						break;
					case "Constructing":
						lblOpen.Text = "<script language='javascript'>Refresh('2','" + sector.Coordinate + "')</script>";
						break;
					case "Training":
						lblOpen.Text = "<script language='javascript'>Refresh('3','" + sector.Coordinate + "')</script>";
						break;
				}
			#endregion
		}

		protected void drpSectors_SelectedIndexChanged(object sender, EventArgs e) {
			Response.Redirect("Sector.aspx?Sector=" + drpSectors.SelectedValue);
		}

		protected void btnCapture_Click(object sender, EventArgs e) {
			if (player.Sectors.Count >= Configuration.Max_Sectors_Per_Player) {
				Wc3o.Game.Message(Master, "You cannot capture another sector.", MessageType.Error);
				return;
			}

			if (sector.Owner != null && sector.Owner.Sectors.Count <= Configuration.Min_Sectors_Per_Player) {
				Wc3o.Game.Message(Master, "You cannot capture this sector because the owner cannot loose more.", MessageType.Error);
				return;
			}

			bool hostileUnitsOnSector = false;
			foreach (Unit u in sector.Units)
				if ((u.IsAvailable || u.IsWorking) && player.CanAttack(u.Owner)) {
					hostileUnitsOnSector = true;
					break;
				}

			if (hostileUnitsOnSector) {
				Wc3o.Game.Message(Master, "You cannot capture this sector because there are still hostile units on it.", MessageType.Error);
				return;
			}

			bool hostileBuilingsOnSector = false;
			foreach (Building b in sector.Buildings)
				if (b.IsAvailable && b.CanFight) {
					hostileBuilingsOnSector = true;
					break;
				}
			if (hostileBuilingsOnSector) {
				Wc3o.Game.Message(Master, "You cannot capture this sector because there are still hostile buildings on it.", MessageType.Error);
				return;
			}

			int upkeep = 0;
			foreach (Unit u in sector.Units)
				if (u.Owner == player && u.IsAvailable)
					upkeep += u.UnitInfo.Food * u.Number;
			if (upkeep < player.Sectors.Count * Configuration.Unit_Factor_For_Annect) {
				Wc3o.Game.Message(Master, "You need more units on this sector to capture it.", MessageType.Error);
				return;
			}

			sector.Owner = player;
			Response.Redirect("Sector.aspx");
		}

	}
}
