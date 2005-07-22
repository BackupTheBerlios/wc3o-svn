using System;

namespace Wc3o.Pages.Portal.Misc {
	public partial class Changelog_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {

			int count = 0;
			DateTime d = DateTime.Now.AddYears(-100);
			string author = "";

			foreach (Changelog c in Game.PortalData.Changelog) {
				if (c.Date.Day != d.Day || c.Date.Month != d.Month || c.Date.Year != d.Year || c.Name != author) {
					lblChangelogs.Text += "<br /><b>" + Game.Format(c.Date, false, false) + "</b> - <i>by " + c.Name + "</i><br />";
					d = c.Date;
					author = c.Name;
					count++;
				}

				lblChangelogs.Text += "&nbsp;&nbsp;- " + c.Text + "<br />";

				if (count > Configuration.Number_Of_Changelogs)
					break;
			}


		}

	}
}
