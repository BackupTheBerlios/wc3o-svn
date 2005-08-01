using System;

namespace Wc3o.Pages.Portal.Players {
	public partial class SignUp_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			imgHumans.ImageUrl = Configuration.Default_Gfx_Path + "/Portal/Humans.gif";
			imgOrcs.ImageUrl = Configuration.Default_Gfx_Path + "/Portal/Orcs.gif";
			imgNightElves.ImageUrl = Configuration.Default_Gfx_Path + "/Portal/NightElves.gif";
			imgUndead.ImageUrl = Configuration.Default_Gfx_Path + "/Portal/Undead.gif";
		}

		protected void btnSignUp_Click(object sender, EventArgs e) {
			if (Page.IsValid) {
				if (Game.GameData.Players.Count > Configuration.Max_Player) {
					Game.Message(Master, "There are no more players allowed. You cannot sign up at this moment.", MessageType.Error);
					return;
				}

				if (!chkPlayerAgreement.Checked) {
					Game.Message(Master, "You have to read and accept the Player Agreement.", MessageType.Error);
					return;
				}

				if (Game.GameData.Players.ContainsKey(txtName.Text)) {
					Game.Message(Master, "There is already a player with this name.", MessageType.Error);
					return;
				}

				if (Game.GetPlayerByEmail(txtEmail.Text) != null) {
					Game.Message(Master, "Your eMail address is already in use.", MessageType.Error);
					return;
				}

				Sector sector = FindFreeSector();
				if (sector == null) {
					Game.Message(Master, "There is no free sector left.", MessageType.Error);
					return;
				}

				sector.Destroy();
				string name = sector.Name;
				sector= new GoldAndLumberSector(1,1,sector.Coordinate);
				sector.Name = name;


				Player player = new Player(txtName.Text);
				player.Email = txtEmail.Text;
				//player.Password = Session.SessionID.Substring(0, 10);
				player.Password = "asdf";

				player.Gold = 500;
				player.Lumber = 150;
				player.Online = DateTime.Now;
				player.Registration = DateTime.Now;
				player.Gfx = Configuration.Default_Gfx_Path;

				sector.Owner = player;

				if (rdbOrcs.Checked) {
					player.Fraction = Fraction.Orcs;
					new Building(BuildingType.GreatHall, sector, DateTime.Now);
					new Building(BuildingType.OrcBarracks, sector, DateTime.Now);
					new Building(BuildingType.Burrow, sector, DateTime.Now);
					new Unit(UnitType.Peon, sector, player, DateTime.Now);
					new Unit(UnitType.Peon, sector, player, DateTime.Now);
					new Unit(UnitType.Peon, sector, player, DateTime.Now);
				}
				else if (rdbNightElves.Checked) {
					player.Fraction = Fraction.NightElves;
					new Building(BuildingType.TreeOfLife, sector, DateTime.Now);
					new Building(BuildingType.AncientOfWar, sector, DateTime.Now);
					new Building(BuildingType.MoonWell, sector, DateTime.Now);
					new Unit(UnitType.Wisp, sector, player, DateTime.Now);
					new Unit(UnitType.Wisp, sector, player, DateTime.Now);
					new Unit(UnitType.Wisp, sector, player, DateTime.Now);
				}
				else if (rdbUndead.Checked) {
					player.Fraction = Fraction.Undead;
					new Building(BuildingType.Necropolis, sector, DateTime.Now);
					new Building(BuildingType.HauntedGoldMine, sector, DateTime.Now);
					new Building(BuildingType.Crypt, sector, DateTime.Now);
					new Building(BuildingType.Ziggurat, sector, DateTime.Now);
					new Unit(UnitType.Acolyte, sector, player, DateTime.Now);
					new Unit(UnitType.Acolyte, sector, player, DateTime.Now);
					new Unit(UnitType.Ghoul, sector, player, DateTime.Now);
				}
				else if (rdbHumans.Checked) {
					player.Fraction = Fraction.Humans;
					new Building(BuildingType.TownHall, sector, DateTime.Now);
					new Building(BuildingType.HumanBarracks, sector, DateTime.Now);
					new Building(BuildingType.Farm, sector, DateTime.Now);
					new Building(BuildingType.Farm, sector, DateTime.Now);
					new Unit(UnitType.Peasant, sector, player, DateTime.Now);
					new Unit(UnitType.Peasant, sector, player, DateTime.Now);
					new Unit(UnitType.Peasant, sector, player, DateTime.Now);
				}

				Game.SendEmail(player.Email, "Your password, " + player.Name+".", "Welcome at Warcraft 3 online.\r\n\r\nYour Password is: " + player.Password + "\r\nThe first thing you should do after you logged on is to change it. \r\nYou can now start playing at http://wc3o.sachsenhofer.com/. Have fun.");
				new Message(player,null,"Welcome at Warcraft 3 online!","This is your Welcome Message. If you have any more questions, please take a look at the help or the forum.<br />The first thing you should do, is to send your workers to mine some gold and cut some lumber. Then build some more workers or even your first battle units. Your next big step should be to annect another sector with ressources; but be careful, it's dangerous out there!");
				System.IO.StreamWriter w = new System.IO.StreamWriter(Configuration.Physical_Application_Path + "\\App_Data\\Email Addresses.txt", true);
				w.WriteLine(player.Email);
				w.Close();

				Response.Redirect("~/Portal/Players/SignUpSuccessful.aspx", true);

			}
			else {
				Game.Message(Master, "Your data is not valid.", MessageType.Error);
			}
		}


		Wc3o.Sector FindFreeSector() {
			Random r = new Random();

			Sector sector = null;
			int count = Configuration.Map_Size * Configuration.Map_Size;

			int x = r.Next(1, Configuration.Map_Size);
			int y = r.Next(1, Configuration.Map_Size);

			while (sector == null && count > 0) {
				count--;

				sector = Game.GameData.Sectors[new Coordinate(x, y)];

				if (sector.Owner != null || sector.GetType() != typeof(Sector))
					sector = null;

				y++;
				if (y > Configuration.Map_Size) {
					x++;
					y = 1;
				}

				if (x > Configuration.Map_Size)
					x = 1;
			}

			return sector;
		}


	}
}
