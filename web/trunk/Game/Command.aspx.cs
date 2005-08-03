using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Command_aspx : System.Web.UI.Page {

		Player player;
		Sector sector;

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.QueryString["Action"] == null) {
				CloseWindow(0);
				return;
			}

			player = Wc3o.Game.CurrentPlayer;
			sector = Wc3o.Game.CurrentSector;

			if (!IsPostBack) {
				pnlDestroy.Visible = false;
				pnlSplit.Visible = false;
				pnlMove.Visible = false;

				#region " Train Units "
				if (Request.QueryString["Action"] == "Train") {
					UnitInfo i = UnitInfo.Get((UnitType)Enum.Parse(typeof(UnitType), Request.QueryString["Unit"]));
					int c = 0;
					for (int j = 1; j <= int.Parse(Request.QueryString["Number"]); j++) {
						if (!Wc3o.Game.IsAvailable(player, sector, i))
							break;
						c++;
						DateTime d = DateTime.Now;
						foreach (Unit u in sector.Units)
							if (u.IsInTraining && Wc3o.Game.TrainedInSameBuilding(i, u.UnitInfo) && u.Date > d)
								d = u.Date;
						new Unit(i.Type, sector, player, d.AddMinutes(i.Minutes));
						player.Gold -= i.Gold;
						player.Lumber -= i.Lumber;
					}
					if (c <= 0)
						Wc3o.Game.Message(this, "You cannot train a " + i.Name + ".", MessageType.Error);
					else
						Wc3o.Game.Message(this, "You started the training of " + Wc3o.Game.Format(c) + " " + i.Name + ".", MessageType.Acknowledgement);
					RefreshPage(sector.Coordinate.ToString(), "Training");
					CloseWindow(3000);
				}
				#endregion
				#region " Construct Buildings "
				else if (Request.QueryString["Action"] == "Construct") {
					BuildingInfo i = BuildingInfo.Get((BuildingType)Enum.Parse(typeof(BuildingType), Request.QueryString["Building"]));
					int c = 0;
					for (int j = 1; j <= int.Parse(Request.QueryString["Number"]); j++) {
						if (!Wc3o.Game.IsAvailable(player, sector, i))
							break;
						c++;
						DateTime d = DateTime.Now;
						foreach (Building b in sector.Buildings)
							if (b.IsInConstruction && !b.IsUpgrading && b.Date > d)
								d = b.Date;
						new Building(i.Type, sector, d.AddMinutes(i.Minutes));
						player.Gold -= i.Gold;
						player.Lumber -= i.Lumber;
					}
					if (c <= 0)
						Wc3o.Game.Message(this, "You cannot construct a " + i.Name + ".", MessageType.Error);
					else
						Wc3o.Game.Message(this, "You started the construction of " + Wc3o.Game.Format(c) + " " + i.Name + ".", MessageType.Acknowledgement);
					RefreshPage(sector.Coordinate.ToString(), "Constructing");
					CloseWindow(3000);
				}
				#endregion
				#region " Destroy Unit(s) "
				else if (Request.QueryString["Action"] == "DestroyUnit") {
					int i = int.Parse(Request.QueryString["Unit"]);
					if (i == -1) {
						lblDestroy.Text = "Are you sure to destroy all units?";
						imgDestroy.Visible = false;
					}
					else if (i == -2) {
						lblDestroy.Text = "Are you sure to stop the training of all units?";
						imgDestroy.Visible = false;
					}
					else {
						Unit u = player.GetUnitByHashcode(i);
						if (u == null) {
							CloseWindow(0);
							return;
						}
						imgDestroy.ImageUrl = player.Gfx + u.Info.Image;
						if (u.IsInTraining)
							lblDestroy.Text = "Are you sure you want to stop the training of this " + u.Info.Name + "?";
						else
							lblDestroy.Text = "Are you sure you want to destroy " + Wc3o.Game.Format(u.Number) + " " + u.Info.Name + "?";
					}
					pnlDestroy.Visible = true;
				}
				#endregion
				#region " Destroy Building(s) "
				else if (Request.QueryString["Action"] == "DestroyBuilding") {
					int i = int.Parse(Request.QueryString["Building"]);
					if (i == -1) {
						lblDestroy.Text = "Are you sure to destroy all buildings?";
						imgDestroy.Visible = false;
					}
					else if (i == -2) {
						lblDestroy.Text = "Are you sure to stop the construction of all buildings?";
						imgDestroy.Visible = false;
					}
					else {
						Building b = player.GetBuildingByHashcode(i);
						if (b == null) {
							CloseWindow(0);
							return;
						}
						imgDestroy.ImageUrl = player.Gfx + b.Info.Image;
						if (b.IsInConstruction) {
							if (b.UpgradingFrom == BuildingType.None)
								lblDestroy.Text = "Are you sure you want to stop the construction of this " + b.Info.Name + "?";
							else
								lblDestroy.Text = "Are you sure you want to stop upgrading of this " + b.Info.Name + "?";
						}
						else
							lblDestroy.Text = "Are you sure you want to destroy " + Wc3o.Game.Format(b.Number) + " " + b.Info.Name + "?";
					}
					pnlDestroy.Visible = true;
				}
				#endregion
				#region " Upgrade Buildings "
				else if (Request.QueryString["Action"] == "UpgradeBuilding") {
					Building b = player.GetBuildingByHashcode(int.Parse(Request.QueryString["Building"].ToString()));
					BuildingInfo i = BuildingInfo.Get((BuildingType)Enum.Parse(typeof(BuildingType), Request.QueryString["To"]));

					int c = 0;
					for (int j = 1; j <= b.Number; j++)
						if (Wc3o.Game.IsAvailable(player, sector, i, true)) {
							player.Gold -= i.Gold;
							player.Lumber -= i.Lumber;
							c++;
						}
						else
							break;

					if (c <= 0)
						Wc3o.Game.Message(this, "You cannot upgrade to " + i.Name + ".", MessageType.Error);
					else {
						b.Number -= c;
						if (b.Number <= 0)
							b.Destroy();

						Building newBuilding = new Building(i.Type, b.Sector, DateTime.Now.AddMinutes(i.Minutes * c));
						newBuilding.Number = c;
						newBuilding.UpgradingFrom = b.Type;
						newBuilding.Hitpoints -= b.Info.Hitpoints - b.Hitpoints;

						Wc3o.Game.Message(this, "You started upgrading to " + i.Name + " of " + Wc3o.Game.Format(c) + " buildings.", MessageType.Acknowledgement);
					}

					Refresh(7, b.Sector.Coordinate.ToString());
					Refresh(5, "");
					CloseWindow(3000);
				}
				#endregion
				#region " Work For Gold "
				else if (Request.QueryString["Action"] == "WorkForGold") {
					Unit u = player.GetUnitByHashcode(int.Parse(Request.QueryString["Unit"]));
					if (u == null || !u.IsAvailable || !u.Sector.Owner.IsAlly(player)) {
						CloseWindow(0);
						return;
					}

					int i = 0;
					foreach (Unit unit in sector.Units)
						if (u.IsWorking && u.Action == UnitAction.WorkForGold)
							i += unit.Number;

					if (i + u.Number > Configuration.Max_Gold_Worker_Per_Sector) {
						Wc3o.Game.Message(this, "You can only send a maximum of " + Configuration.Max_Gold_Worker_Per_Sector + " workers per sector to mine gold.", MessageType.Error);
						CloseWindow(3000);
						return;
					}

					u.Action = UnitAction.WorkForGold;
					Wc3o.Game.Message(this, Wc3o.Game.Format(u.Number) + " " + u.Info.Name + " started to mine gold.", MessageType.Acknowledgement);
					Refresh(6, u.Sector.Coordinate.ToString());
					CloseWindow(3000);
				}
				#endregion
				#region " Work For Lumber "
				else if (Request.QueryString["Action"] == "WorkForLumber") {
					Unit u = player.GetUnitByHashcode(int.Parse(Request.QueryString["Unit"]));
					if (u == null || !u.IsAvailable || !u.Sector.Owner.IsAlly(player)) {
						CloseWindow(0);
						return;
					}
					u.Action = UnitAction.WorkForLumber;
					Wc3o.Game.Message(this, Wc3o.Game.Format(u.Number) + " " + u.Info.Name + " started to cut lumber.", MessageType.Acknowledgement);
					Refresh(6, u.Sector.Coordinate.ToString());
					CloseWindow(3000);
				}
				#endregion
				#region " Stop Working "
				else if (Request.QueryString["Action"] == "StopWorking") {
					Unit u = player.GetUnitByHashcode(int.Parse(Request.QueryString["Unit"]));
					if (u == null || !u.IsWorking) {
						CloseWindow(0);
						return;
					}
					u.Action = UnitAction.None;
					Wc3o.Game.Message(this, Wc3o.Game.Format(u.Number) + " " + u.Info.Name + " stopped working.", MessageType.Acknowledgement);
					Refresh(6, u.Sector.Coordinate.ToString());
					CloseWindow(3000);
				}
				#endregion
				#region " Merge Unit "
				else if (Request.QueryString["Action"] == "MergeUnits") {
					int i = int.Parse(Request.QueryString["Unit"]);
					if (i == -1) {
						Sector s = Wc3o.Game.GameData.Sectors[new Coordinate(Request.QueryString["Sector"])];
						List<Entity> l = new List<Entity>();
						foreach (Unit u in s.Units)
							if (u.IsAvailable && u.Owner == player)
								l.Add(u);
						Wc3o.Game.Merge(l);
						Wc3o.Game.Message(this, "You merged all your units.", MessageType.Acknowledgement);
						Refresh(6, s.Coordinate.ToString());
						CloseWindow(3000);
					}
					else {
						Unit unit = player.GetUnitByHashcode(i);
						List<Entity> l = new List<Entity>();
						foreach (Unit u in unit.Sector.Units)
							if (u.IsAvailable && u.Owner == player && u.Type == unit.Type)
								l.Add(u);
						Wc3o.Game.Merge(l);
						Wc3o.Game.Message(this, "You merged your " + unit.Info.Name + ".", MessageType.Acknowledgement);
						Refresh(6, unit.Sector.Coordinate.ToString());
						CloseWindow(3000);
					}
				}
				#endregion
				#region " Merge Building "
				else if (Request.QueryString["Action"] == "MergeBuildings") {
					int i = int.Parse(Request.QueryString["Building"]);
					if (i == -1) {
						Sector s = Wc3o.Game.GameData.Sectors[new Coordinate(Request.QueryString["Sector"])];
						List<Entity> l = new List<Entity>();
						foreach (Building b in s.Buildings)
							if (b.IsAvailable && s.Owner == player)
								l.Add(b);
						Wc3o.Game.Merge(l);
						Wc3o.Game.Message(this, "You merged all your buildings.", MessageType.Acknowledgement);
						Refresh(7, s.Coordinate.ToString());
						CloseWindow(3000);
					}
					else {
						Building building = player.GetBuildingByHashcode(i);
						List<Entity> l = new List<Entity>();
						foreach (Building b in building.Sector.Buildings)
							if (b.IsAvailable && b.Sector.Owner == player && b.Type == building.Type)
								l.Add(b);
						Wc3o.Game.Merge(l);
						Wc3o.Game.Message(this, "You merged your " + building.Info.Name + ".", MessageType.Acknowledgement);
						Refresh(7, building.Sector.Coordinate.ToString());
						CloseWindow(3000);
					}
				}
				#endregion
				#region " Split Units "
				else if (Request.QueryString["Action"] == "SplitUnits") {
					Unit u = player.GetUnitByHashcode(int.Parse(Request.QueryString["Unit"]));
					if (u == null || !u.IsAvailable) {
						CloseWindow(0);
						return;
					}
					pnlSplit.Visible = true;
					imgSplit.ImageUrl = player.Gfx + u.Info.Image;
					lblSplit.Text = "How many of your " + u.Info.Name + " do you want to split into a new unit?";
					for (int i = 1; i < u.Number; i++)
						drpSplit.Items.Add(i.ToString());
				}
				#endregion
				#region " Split Buildings "
				else if (Request.QueryString["Action"] == "SplitBuildings") {
					Building b = player.GetBuildingByHashcode(int.Parse(Request.QueryString["Building"]));
					if (b == null || b.IsInConstruction) {
						CloseWindow(0);
						return;
					}
					pnlSplit.Visible = true;
					imgSplit.ImageUrl = player.Gfx + b.Info.Image;
					lblSplit.Text = "How many of your " + b.Info.Name + " do you want to split?";
					for (int i = 1; i < b.Number; i++)
						drpSplit.Items.Add(i.ToString());
				}
				#endregion
				#region " Move "
				else if (Request.QueryString["Action"] == "Move") {
					int i = int.Parse(Request.QueryString["Unit"]);
					if (i == -1) {
						Sector s = Wc3o.Game.GameData.Sectors[new Coordinate(Request.QueryString["Sector"])];
						bool hasUnits = false;
						foreach (Unit u in s.Units)
							if (u.Owner == player) {
								hasUnits = true;
								break;
							}
						if (!hasUnits) {
							Wc3o.Game.Message(this, "You have no units on this sector.", MessageType.Error);
							CloseWindow(3000);
							return;
						}
						pnlMove.Visible = true;
						imgMove.Visible = false;
					}
					else {
						Unit u = player.GetUnitByHashcode(i);
						if (u == null || !u.IsAvailable) {
							CloseWindow(0);
							return;
						}
						pnlMove.Visible = true;
						imgMove.ImageUrl = player.Gfx + u.Info.Image;
						chkIgnore.Visible = false;
						chkTime.Visible = false;
					}
				}
				#endregion
				#region " Return "
				else if (Request.QueryString["Action"] == "Return") {
					int i = int.Parse(Request.QueryString["Unit"]);
					if (i == -1) {
						Sector s = Wc3o.Game.GameData.Sectors[new Coordinate(Request.QueryString["Sector"])];
						List<Unit> l = new List<Unit>();
						foreach (Unit u in s.Units)
							if (u.Owner == player && u.IsMoving)
								l.Add(u);

						foreach (Unit u in l)
							Return(u);

						Wc3o.Game.Message(this, "Your units return.", MessageType.Acknowledgement);
						RefreshPage(s.Coordinate.ToString(), "Units");
						CloseWindow(3000);
					}
					else {
						Unit u = player.GetUnitByHashcode(i);
						if (u == null || !u.IsMoving) {
							CloseWindow(0);
							return;
						}
						Return(u);
						Wc3o.Game.Message(this, "Your " + u.Info.Name + " return.", MessageType.Acknowledgement);
						RefreshPage(u.Sector.Coordinate.ToString(), "Units");
						CloseWindow(3000);
					}
				}
				#endregion
				#region " Morph Units "
				else if (Request.QueryString["Action"] == "MorphUnits") {
					Unit u = player.GetUnitByHashcode(int.Parse(Request.QueryString["Unit"]));

					double factor = (double)u.Hitpoints / (double)u.Info.Hitpoints;

					Response.Write(factor.ToString());
					int c = 0;
					switch (u.Type) {
						case UnitType.DruidOfTheClawBearForm:
							c = u.Number;
							u.Type = UnitType.DruidOfTheClawDruidForm;
							break;
						case UnitType.DruidOfTheClawDruidForm:
							c = u.Number;
							u.Type = UnitType.DruidOfTheClawBearForm;
							break;
						case UnitType.DruidOfTheTalonCrowForm:
							c = u.Number;
							u.Type = UnitType.DruidOfTheTalonDruidForm;
							break;
						case UnitType.DruidOfTheTalonDruidForm:
							c = u.Number;
							u.Type = UnitType.DruidOfTheTalonCrowForm;
							break;
						case UnitType.Peasant:
							if (sector.Owner == player)
								foreach (Building b in sector.Buildings)
									if (!b.IsInConstruction && (b.Type == BuildingType.TownHall || b.Type == BuildingType.Keep || b.Type == BuildingType.Castle)) {
										c = u.Number;
										u.Type = UnitType.Militia;
										break;
									}
							break;
						case UnitType.Militia:
							if (sector.Owner == player)
								foreach (Building b in sector.Buildings)
									if (!b.IsInConstruction && (b.Type == BuildingType.TownHall || b.Type == BuildingType.Keep || b.Type == BuildingType.Castle)) {
										c = u.Number;
										u.Type = UnitType.Peasant;
										break;
									}
							break;
						case UnitType.Hippogryph:
							foreach (Unit unit in sector.Units)
								if (unit.Owner == player && unit.IsAvailable && unit.Type == UnitType.Archer) {
									if (unit.Number <= u.Number - c) {
										c += unit.Number;
										unit.Destroy();
									}
									else {
										Unit newUnit = (Unit)unit.Clone();
										newUnit.Number = unit.Number - (u.Number - c);
										unit.Destroy();
										c = u.Number;
									}
								}
							Unit newHippgoryphRiders = new Unit(UnitType.HippogryphRider, sector, player, DateTime.Now);
							newHippgoryphRiders.Number = c;
							break;
						case UnitType.HippogryphRider:
							c = u.Number;
							u.Type = UnitType.Hippogryph;
							Unit newArchers = new Unit(UnitType.Archer, sector, player, DateTime.Now);
							newArchers.Number = c;
							break;
						case UnitType.Acolyte:
							if (sector.Owner == player)
								foreach (Building b in sector.Buildings)
									if (!b.IsInConstruction && b.Type == BuildingType.SacrificialPit) {
										c = u.Number;
										u.Type = UnitType.Shade;
										break;
									}
							break;
						case UnitType.ObsidianStatue:
							c = u.Number;
							u.Type = UnitType.Destroyer;
							break;
					}

					u.Hitpoints = (int)(u.Info.Hitpoints * factor);

					if (c > 0)
						Wc3o.Game.Message(this, "You morphed " + Wc3o.Game.Format(c) + " units.", MessageType.Acknowledgement);
					else
						Wc3o.Game.Message(this, "You cannot morph.", MessageType.Error);
					Refresh(6, u.Sector.Coordinate.ToString());
					CloseWindow(3000);
				}
				#endregion
			}
		}

		#region " Methods for javascript "
		void CloseWindow(int timeout) {
			lblScript.Text += "<script language='JavaScript'>setTimeout('close()'," + timeout + ");</script>";
		}
		void Refresh(int type, string sector) {
			//1=UNUSED,2=Training,3=Constructing,4=Overview,5=Navigation,6=Player Units, 7=Player Buildings
			lblScript.Text += "<script language='JavaScript'>RefreshParent(" + type + ",'" + sector + "');</script>";
		}
		void RefreshPage(string sector, string refresh) {
			//refresh = Units || Buildings || Constructing || Training
			lblScript.Text += "<script language='JavaScript'>window.opener.document.location=\"Sector.aspx?Sector=" + sector + "&Refresh=" + refresh + "\";</script>";
		}
		#endregion

		#region " Events "
		protected void btnDestroyNo_Click(object sender, EventArgs e) {
			CloseWindow(0);
		}

		protected void btnDestroyYes_Click(object sender, EventArgs e) {
			Sector s = sector;
			#region " Destroy Unit "
			if (Request.QueryString["Action"] == "DestroyUnit") {
				int i = int.Parse(Request.QueryString["Unit"]);
				if (i == -1) {
					List<Unit> l = new List<Unit>();
					foreach (Unit u in sector.Units)
						if (u.Owner == player && u.IsAvailable)
							l.Add(u);
					foreach (Unit u in l) {
						s = u.Sector;
						DestroyUnit(u);
					}
				}
				else if (i == -2) {
					List<Unit> l = new List<Unit>();
					foreach (Unit u in sector.Units)
						if (u.Owner == player && u.IsInTraining)
							l.Add(u);
					foreach (Unit u in l) {
						s = u.Sector;
						DestroyUnit(u);
					}
				}
				else {
					Unit u = player.GetUnitByHashcode(i);
					s = u.Sector;
					DestroyUnit(u);
				}
				RefreshPage(s.Coordinate.ToString(), "Units");
				CloseWindow(0);
				return;
			}
			#endregion
			#region " Destroy Building "
			else if (Request.QueryString["Action"] == "DestroyBuilding") {
				int i = int.Parse(Request.QueryString["Building"]);
				if (i == -1) {
					if (sector.Owner != player) {
						CloseWindow(0);
						return;
					}
					List<Building> l = new List<Building>();
					foreach (Building b in sector.Buildings)
						if (b.IsAvailable)
							l.Add(b);
					foreach (Building b in l) {
						s = b.Sector;
						DestroyBuilding(b);
					}
				}
				else if (i == -2) {
					if (sector.Owner != player) {
						RefreshPage(s.Coordinate.ToString(), "Buildings");
						CloseWindow(0);
						return;
					}
					List<Building> l = new List<Building>();
					foreach (Building b in sector.Buildings)
						if (b.IsInConstruction)
							l.Add(b);
					foreach (Building b in l) {
						s = b.Sector;
						DestroyBuilding(b);
					}
				}
				else {
					Building b = player.GetBuildingByHashcode(i);
					s = b.Sector;
					if (b.IsUpgrading) {
						int hitpointsDiff = b.Info.Hitpoints - b.Hitpoints;
						b.Date = DateTime.Now;
						player.Gold += b.Info.Gold;
						player.Lumber += b.Info.Lumber;
						b.Type = b.UpgradingFrom;
						b.Hitpoints = b.Info.Hitpoints - hitpointsDiff;
					}
					else
						DestroyBuilding(b);
				}
				RefreshPage(s.Coordinate.ToString(), "Buildings");
				CloseWindow(0);
				return;
			}
			#endregion
			CloseWindow(0);
		}

		protected void DestroyUnit(Unit u) {
			u.Destroy();

			if (u.IsInTraining) {
				DateTime date = DateTime.Now;

				foreach (Unit unit in sector.Units)
					if (unit.Owner==player && unit.IsInTraining && Wc3o.Game.TrainedInSameBuilding(u.UnitInfo,unit.UnitInfo))
						if (unit.Date < u.Date) { //units that are finished earlier are always on top of the queue
							if (unit.Date > date)
								date = unit.Date;
						}
						else {
							date = date.AddMinutes(unit.Info.Minutes);
							unit.Date = date;
						}

				player.Gold += u.Number * u.Info.Gold;
				player.Lumber += u.Number * u.Info.Lumber;
			}
		}

		protected void DestroyBuilding(Building b) {
			b.Destroy();

			if (b.IsInConstruction) {
				DateTime date = DateTime.Now;

				foreach (Building building in sector.Buildings)
					if (building.IsInConstruction && !building.IsUpgrading)
						if (building.Date < b.Date) { //units that are finished earlier are always on top of the queue
							if (building.Date > date)
								date = building.Date;
						}
						else {
							date = date.AddMinutes(building.Info.Minutes);
							building.Date = date;
						}

				if (b.IsUpgrading) {
					player.Gold += (int)(b.Number * b.Info.Gold * 0.66);
					player.Lumber += (int)(b.Number * b.Info.Lumber * 0.66);
				}
				else {
					player.Gold += (int)(b.Number * b.Info.Gold * 0.75);
					player.Lumber += (int)(b.Number * b.Info.Lumber * 0.75);
				}
			}
		}

		protected void btnSplit_Click(object sender, EventArgs e) {
			Sector s = sector;
			#region " Split Unit "
			if (Request.QueryString["Action"] == "SplitUnits") {
				Unit u = player.GetUnitByHashcode(int.Parse(Request.QueryString["Unit"]));
				if (u == null) {
					CloseWindow(0);
					return;
				}
				int i = int.Parse(drpSplit.SelectedItem.Text);
				Unit v = (Unit)u.Clone();
				u.Number -= i;
				v.Number = i;
				s = u.Sector;
			}
			#endregion
			#region " Split Building "
			else if (Request.QueryString["Action"] == "SplitBuildings") {
				Building b = player.GetBuildingByHashcode(int.Parse(Request.QueryString["Building"]));
				if (b == null) {
					CloseWindow(0);
					return;
				}
				int i = int.Parse(drpSplit.SelectedItem.Text);
				Building c = (Building)b.Clone();
				b.Number -= i;
				c.Number = i;
				s = b.Sector;
			}
			#endregion
			Refresh(7, s.Coordinate.ToString());
			CloseWindow(0);
		}

		protected void btnSplitCancel_Click(object sender, EventArgs e) {
			CloseWindow(0);
		}

		protected void btnMoveCancel_Click(object sender, EventArgs e) {
			CloseWindow(0);
		}

		protected void btnMove_Click(object sender, EventArgs e) {
			Sector s = null;
			if (txtSector.Text.Length > 0)
				s = Wc3o.Game.GetSectorByName(txtSector.Text);
			try {
				s = Wc3o.Game.GameData.Sectors[new Coordinate(int.Parse(txtX.Text), int.Parse(txtY.Text))];
			} catch {
			}

			if (s == null) {
				Wc3o.Game.Message(this, "This sector does not exist.", MessageType.Error);
				return;
			}

			if (Request.QueryString["Action"] == "Move" && Request.QueryString["Unit"] == "-1") {
				List<Unit> l = new List<Unit>();
				foreach (Unit u in sector.Units)
					if (u.Owner == player && u.IsAvailable && (!chkIgnore.Checked || (chkIgnore.Checked && !u.UnitInfo.ForLumber && !u.UnitInfo.ForGold)))
						l.Add(u);

				if (l.Count <= 0) {
					CloseWindow(0);
					return;
				}

				int speed = 1;
				Sector unitSector = null;
				foreach (Unit u in l)
					if (u.UnitInfo.Speed > speed) {
						speed = u.UnitInfo.Speed;
						unitSector = u.Sector;
					}

				DateTime d = DateTime.Now.AddMinutes(GetMoveTime(speed, unitSector, s));
				foreach (Unit u in l) {
					if (chkTime.Checked)
						u.Date = d;
					else
						u.Date = DateTime.Now.AddMinutes(GetMoveTime(u.UnitInfo.Speed, unitSector, s));
					u.SourceSector = u.Sector;
					u.SourceDate = DateTime.Now;
					u.Sector = s;
					u.Action = UnitAction.Moving;
				}
				RefreshPage(s.Coordinate.ToString(), "Units");
				CloseWindow(0);
			}
			else {
				Unit u = player.GetUnitByHashcode(int.Parse(Request.QueryString["Unit"]));
				u.Date = DateTime.Now.AddMinutes(GetMoveTime(u.UnitInfo.Speed, u.Sector, s));
				u.SourceSector = u.Sector;
				u.SourceDate = DateTime.Now;
				u.Sector = s;
				u.Action = UnitAction.Moving;
				RefreshPage(s.Coordinate.ToString(), "Units");
				CloseWindow(0);
			}
		}
		#endregion

		#region " Methods "
		static void Return(Unit u) {
			u.Sector = u.SourceSector;
			double alreadyMoved = ((TimeSpan)(DateTime.Now - u.SourceDate)).TotalMinutes;
			u.Date = DateTime.Now.AddMinutes(alreadyMoved);
		}

		static double GetMoveTime(int i, Sector s, Sector t) {
			int distance = 0;
			if (s.Coordinate.X > t.Coordinate.X)
				distance += s.Coordinate.X - t.Coordinate.X;
			else
				distance += t.Coordinate.X - s.Coordinate.X;
			if (s.Coordinate.Y > t.Coordinate.Y)
				distance += s.Coordinate.Y - t.Coordinate.Y;
			else
				distance += t.Coordinate.Y - s.Coordinate.Y;
			return distance * i;
		}
		#endregion

	}
}
