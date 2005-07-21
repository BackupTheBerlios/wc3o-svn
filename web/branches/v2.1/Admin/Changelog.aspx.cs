using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Admin {
	public partial class Changelog_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				txtCreator.Text = User.Identity.Name;
				txtDate.Text = DateTime.Now.ToString();

				foreach (Changelog c in Game.PortalData.Changelog) {
					drpEdit.Items.Add(new System.Web.UI.WebControls.ListItem(c.Text, c.GetHashCode().ToString()));
					if (Request.QueryString["Edit"] != null && Request.QueryString["Edit"] == c.GetHashCode().ToString()) {
						txtCreator.Text = c.Name;
						txtDate.Text = c.Date.ToString();
						txtText.Text = c.Text;
					}
				}

			}
		}


		protected void btnAdd_Click(object sender, EventArgs e) {
			Changelog changelog = null;
			if (Request.QueryString["Edit"] == null) {
				changelog = new Changelog();
			}
			else
				foreach (Changelog c in Game.PortalData.Changelog)
					if (Request.QueryString["Edit"] == c.GetHashCode().ToString()) {
						changelog = c;
						break;
					}
			changelog.Name = txtCreator.Text;
			changelog.Text = txtText.Text;
			changelog.Date = DateTime.Parse(txtDate.Text);

			Game.PortalData.Changelog.Sort(new Changelog.ChangelogComparer());

			Response.Redirect("Changelog.aspx");
		}

		protected void btnNow_Click(object sender, EventArgs e) {
			txtDate.Text = DateTime.Now.ToString();
		}

		protected void btnEdit_Click(object sender, EventArgs e) {
			Response.Redirect("Changelog.aspx?Edit=" + drpEdit.SelectedValue);
		}

		protected void btnDelete_Click(object sender, EventArgs e) {
			foreach (Changelog c in Game.PortalData.Changelog) {
				if (c.GetHashCode() == int.Parse(drpEdit.SelectedValue)) {
					c.Destroy();
					break;
				}
			}
			Response.Redirect("Changelog.aspx");
		}
	}
}