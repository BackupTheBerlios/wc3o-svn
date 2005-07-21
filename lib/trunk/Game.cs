using System;
using System.Collections.Generic;

namespace Wc3o {
    public static class Game {

		#region " Game Data "
		static GameData gameData;
		public static GameData GameData {
			get {
				return gameData;
			}
			set {
				gameData = value;
			}
		}
		#endregion

		#region " Portal Data "
		static PortalData portalData;
		public static PortalData PortalData {
			get {
				return portalData;
			}
			set {
				portalData = value;
			}
		}
		#endregion

		#region " Properties "
		public static string Gfx {
			get {
				if (System.Web.HttpContext.Current.User.Identity.IsAuthenticated) {
					Player p = CurrentPlayer;
					if (p != null)
						return p.Gfx;
				}
				return Configuration.Default_Gfx_Path;
			}
		}
		#endregion

		#region " Logger "
		static Log.Logger logger;
        public static Log.Logger Logger {
            get {
                return logger;
            }
            set {
                logger = value;
            }
        }
        #endregion

		#region " Ticker "
		static Tick.Ticker ticker;
		public static Tick.Ticker Ticker {
			get {
				return ticker;
			}
			set {
				ticker = value;
			}
		}
		#endregion

		#region " Email/Message methods "
		public static void SendEmail(string recipient, string subject, string body) {
			System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
			mail.BodyEncoding = System.Text.Encoding.UTF7;
			mail.IsBodyHtml = false;
			mail.From = new System.Net.Mail.MailAddress(Configuration.Email_From_Address);
			mail.To.Add(new System.Net.Mail.MailAddress(recipient));
			mail.Subject = subject;
			mail.Body = body;

			System.Net.Mail.SmtpClient smtp = new System.Net.Mail.SmtpClient(Configuration.Email_Server);

			smtp.Credentials = new System.Net.NetworkCredential(Configuration.Email_Username, Configuration.Email_Password);

			try {
				smtp.Send(mail);
			} catch {
				Game.Logger.Log("An error occured while sending an email to '" + recipient + "'.", Log.LogType.System);
			}

		}
		#endregion

		#region " Finder methods "
		public static Sector GetSectorByName(string name) {
			foreach (Sector s in Game.GameData.Sectors.Values)
				if (s.Name == name)
					return s;
			return null;
		}

		public static Player GetPlayerByEmail(string email) {
			foreach (Player p in Game.GameData.Players.Values)
				if (p.Email.ToLower() == email.ToLower())
					return p;
			return null;
		}

		public static List<Player> GetPlayers(int league) {
			List<Player> l = new List<Player>();
			foreach (Player p in GameData.Players.Values)
				if (p.League == league)
					l.Add(p);
			return l;
		}

		public static Player CurrentPlayer {
			get {
				try {
					return Game.GameData.Players[System.Web.HttpContext.Current.User.Identity.Name];
				} catch {
					System.Web.Security.FormsAuthentication.SignOut();					
				}
				return null;
			}
		}

		public static Sector CurrentSector {
			get {
				if (System.Web.HttpContext.Current.Request.QueryString["Sector"] != null)
					System.Web.HttpContext.Current.Session["Sector"] = Game.GameData.Sectors[new Coordinate(System.Web.HttpContext.Current.Request.QueryString["Sector"])];
				else
					if (System.Web.HttpContext.Current.Session["Sector"] == null)
						System.Web.HttpContext.Current.Session["Sector"] = CurrentPlayer.Sectors[0];
				return (Sector)System.Web.HttpContext.Current.Session["Sector"];
			}
		}
		#endregion

		#region " Misc methods "
		public static int Min(int a, int b) {
			if (a > b)
				return b;
			return a;
		}

		public static int Max(int a, int b) {
			if (a > b)
				return a;
			return b;
		}

		public static bool IsNight {
			get {
				if (Game.GetCorrectedDate().Hour >= Configuration.Start_Night || Game.GetCorrectedDate().Hour < Configuration.End_Night)
					return true;
				return false;
			}
		}

		public static int GetDamage(Entity e) {
			return 100 - 100 * e.Hitpoints / e.Info.Hitpoints;
		}

		public static List<Entity> Merge(List<Entity> l) {
			return Merge(l, false);
		}

		public static List<Entity> Merge(List<Entity> l, bool ignoreHitpoints) {
			List<Entity> m = new List<Entity>();
			foreach (Entity e in l) {
				bool b = false;
				foreach (Entity f in m)
					if (e.Info == f.Info && (ignoreHitpoints || e.Hitpoints == f.Hitpoints)) {
						f.Number += e.Number;
						e.Destroy();
						b = true;
					}
				if (!b)
					m.Add(e);
			}
			return m;
		}

		public static List<Entity> Split(List<Entity> l) {
			List<Entity> m = new List<Entity>();
			foreach (Entity e in l) {
				while (e.Number > 1) {
					Entity f = e.Clone();
					f.Destroy();
					f.Number = 1;
					m.Add(f);
					e.Number--;
				}
				m.Add(e);
			}
			return m;
		}


		public static bool IsAvailable(Player p, Sector s, BuildingInfo i) {
			return IsAvailable(p, s, i, false);
		}


		public static bool IsAvailable(Player p, Sector s, UnitInfo i) {
			if (i.Type==UnitType.None || !i.Buildable || p.Gold < i.Gold || p.Lumber < i.Lumber)
				return false;

			if (!s.HasBuildingForRequirement(i.TrainedAt))
				return false;

			foreach (BuildingType t in i.Requirements)
				if (!p.HasBuildingForRequirement(t))
					return false;

			int upkeep = 0;
			foreach (Unit u in p.Units)
				upkeep += u.Number * u.Info.Food;
			if (p.Food < upkeep + i.Food)
				return false;

			return true;
		}


		public static bool IsAvailable(Player p, Sector s, BuildingInfo i, bool isAnUpgrade) {
			if (i.Type == BuildingType.None || p.Fraction != i.Fraction)
				return false;

			if (!isAnUpgrade && !i.Buildable)
				return false;

			if (p.Gold < i.Gold || p.Lumber < i.Lumber)
				return false;

			if (i.Type == BuildingType.TownHall && (s.HasBuilding(BuildingType.Keep) || s.HasBuilding(BuildingType.Castle)))
				return false;
			else if (i.Type == BuildingType.GreatHall && (s.HasBuilding(BuildingType.Stronghold) || s.HasBuilding(BuildingType.Fortress)))
				return false;
			else if (i.Type == BuildingType.TreeOfLife && (s.HasBuilding(BuildingType.TreeOfAges) || s.HasBuilding(BuildingType.TreeOfEternity)))
				return false;
			else if (i.Type == BuildingType.Necropolis && (s.HasBuilding(BuildingType.HallsOfTheDead) || s.HasBuilding(BuildingType.BlackCitadel)))
				return false;

			foreach (BuildingType t in i.Requirements)
				if (!p.HasBuildingForRequirement(t))
					return false;

			int j = 0;
			foreach (Building b in s.Buildings) //check number per sector
				if (b.BuildingInfo == i)
					j += b.Number;
			if (j >= i.NumberPerSector)
				return false;

			j = 0;
			foreach (Building b in p.Buildings) //check total number
				if (b.BuildingInfo == i)
					j += b.Number;
			if (j >= i.Number)
				return false;

			return true;
		}

		public static int GetLeague(int rank) {
			if (rank == 0)
				return 0;
			if (rank % Configuration.Player_Per_League == 0)
				return rank / Configuration.Player_Per_League;
			else
				return rank / Configuration.Player_Per_League + 1;
		}

		public static void RemoveRange<T>(ICollection<T> from,IEnumerable<T> remove) {
            foreach (T t in remove)
                from.Remove(t);
		}

		public static bool TrainedInSameBuilding(UnitInfo a, UnitInfo b) {
			return a.TrainedAt == b.TrainedAt;
		}
		#endregion

		#region " UI methods "
		public static string Theme {
			get {
				return Game.GameData.Players[System.Web.HttpContext.Current.User.Identity.Name].Fraction.ToString();
			}
		}

		public static bool SelectByValue(System.Web.UI.WebControls.DropDownList l, string s) {
			l.SelectedIndex = -1;
			foreach (System.Web.UI.WebControls.ListItem i in l.Items)
				if (i.Value == s) {
					i.Selected = true;
					return true;
				}
			return false;
		}

		public static System.Web.UI.Control GetControlByName(System.Web.UI.Control parent, string name) {
			foreach (System.Web.UI.Control c in parent.Controls) {
				if (c.ID == name)
					return c;
				System.Web.UI.Control cntrl = GetControlByName(c, name);
				if (cntrl != null)
					return cntrl;
			}
			return null;
		}

		public static void Message(System.Web.UI.Control parent, string message, MessageType type) {
			System.Web.UI.WebControls.Literal l = (System.Web.UI.WebControls.Literal)Game.GetControlByName(parent, "lblMessage");

			l.Text = "<div style=\"text-align:center;\">";
			switch (type) {
				case MessageType.Acknowledgement:
					l.Text += "<div class=\"Message_Acknowledgement\">" + message + "</div>";
					break;
				case MessageType.Normal:
					l.Text += "<div class=\"Message_Normal\">" + message + "</div>";
					break;
				case MessageType.Error:
					l.Text += "<div class=\"Message_Error\">" + message + "</div>";
					break;
			}
			l.Text += "</div>";
		}

		public static void Message(System.Web.UI.Control parent, string message) {
			Game.Message(parent, message, MessageType.Normal);
		}	
		#endregion

        #region " Format Methods "
        public static string Format(int number) {
            if (number == 0)
                return "0";
            else
                return number.ToString("N").Substring(0, number.ToString("N").Length - 3).Replace(",", ".");
        }

        public static string Format(double number) {
            if (number == 0)
                return "0";
            else {
                return number.ToString("N").Replace(".", "_").Replace(",", ".").Replace("_", ",");
            }
        }


		public static string Format(DateTime input, bool WriteSeconds) {
			return Format(input, true, WriteSeconds);
		}

		public static string Format(DateTime input, bool writeTime, bool writeSeconds) {
			input = GetCorrectedDate(input);
			string weekday = "";
			switch (input.DayOfWeek.GetHashCode()) {
				case 0:
					weekday = "Sunday, ";
					break;
				case 1:
					weekday = "Monday, ";
					break;
				case 2:
					weekday = "Tuesday, ";
					break;
				case 3:
					weekday = "Wednesday, ";
					break;
				case 4:
					weekday = "Thursday, ";
					break;
				case 5:
					weekday = "Friday, ";
					break;
				case 6:
					weekday = "Saturday, ";
					break;
			}

			string month = "";
			switch (input.Month) {
				case 1:
					month = "January";
					break;
				case 2:
					month = "February";
					break;
				case 3:
					month = "March";
					break;
				case 4:
					month = "April";
					break;
				case 5:
					month = "May";
					break;
				case 6:
					month = "June";
					break;
				case 7:
					month = "July";
					break;
				case 8:
					month = "August";
					break;
				case 9:
					month = "September";
					break;
				case 10:
					month = "October";
					break;
				case 11:
					month = "November";
					break;
				case 12:
					month = "December";
					break;
			}

			if (writeTime) {
				string minuteCorrection = "";
				string secondCorrection = "";
				if (input.Minute < 10) {
					minuteCorrection = "0";
				}
				if (input.Second < 10) {
					secondCorrection = "0";
				}
				string seconds;
				if (writeSeconds)
					seconds = ":" + secondCorrection + input.Second;
				else
					seconds = "";

				return weekday + month + " " + input.Day + ", " + input.Hour + ":" + minuteCorrection + input.Minute + seconds;
			}
			else
				return weekday + month + " " + input.Day;
		}

        public static string TimeSpan(DateTime d) {
            TimeSpan t;

            if (d > DateTime.Now)
                t = d - DateTime.Now;
            else
                t = DateTime.Now - d;

            int hour = t.Hours + t.Days * 24;
            string hours, minutes, seconds;
            if (hour > 9)
                hours = hour.ToString();
            else
                hours = "0" + hour.ToString();
            if (t.Minutes > 9)
                minutes = t.Minutes.ToString();
            else
                minutes = "0" + t.Minutes.ToString();
            if (t.Seconds > 9)
                seconds = t.Seconds.ToString();
            else
                seconds = "0" + t.Seconds.ToString();

            if (hour > 0)
                return hours + ":" + minutes + ":" + seconds;
            else
                return minutes + ":" + seconds;
        }

        public static DateTime GetCorrectedDate(DateTime d) {
            return d.AddHours(Configuration.Hour_Correction);
        }

        public static DateTime GetCorrectedDate() {
            return GetCorrectedDate(DateTime.Now);
        }

		public static string Format(AttackType type) {
			switch (type) {
				case AttackType.Chaos:
					return "Chaos";
				case AttackType.Hero:
					return "Hero";
				case AttackType.Magic:
					return "Magic";
				case AttackType.None:
					return "-";
				case AttackType.Normal:
					return "Normal";
				case AttackType.Pierce:
					return "Pierce";
				case AttackType.Siege:
					return "Siege";
				case AttackType.Spells:
					return "Spell";
			}
			return null;
		}

		public static string Format(ArmorType type) {
			switch (type) {
				case ArmorType.Fort:
					return "Fortified";
				case ArmorType.Heavy:
					return "Heavy";
				case ArmorType.Hero:
					return "Hero";
				case ArmorType.Light:
					return "Light";
				case ArmorType.Medium:
					return "Medium";
				case ArmorType.Unarmored:
					return "Unarmored";
			}
			return null;
		}

		public static string Format(Visibility visibility) {
			switch (visibility) {
				case Visibility.Always:
					return "Always";
				case Visibility.AtDay:
					return "At day";
				case Visibility.AtNight:
					return "At night";
				case Visibility.Never:
					return "Never";
			}
			return null;
		}
        #endregion

    }

	public enum MessageType { Error, Acknowledgement, Normal }

}
