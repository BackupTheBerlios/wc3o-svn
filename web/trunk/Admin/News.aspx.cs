using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Admin {
	public partial class News_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				txtCreator.Text = User.Identity.Name;
				txtDate.Text = DateTime.Now.ToString();

				drpType.Items.Add(new System.Web.UI.WebControls.ListItem(NewsType.Information.ToString(), ((int)NewsType.Information).ToString()));
				drpType.Items.Add(new System.Web.UI.WebControls.ListItem(NewsType.Rules.ToString(), ((int)NewsType.Rules).ToString()));
				drpType.Items.Add(new System.Web.UI.WebControls.ListItem(NewsType.Update.ToString(), ((int)NewsType.Update).ToString()));

				foreach (News n in Game.PortalData.News) {
					drpEdit.Items.Add(new System.Web.UI.WebControls.ListItem(n.Date.ToString(), n.GetHashCode().ToString()));
					if (Request.QueryString["Edit"] != null && Request.QueryString["Edit"] == n.GetHashCode().ToString()) {
						txtCreator.Text = n.Name;
						txtDate.Text = n.Date.ToString();
						txtText.Text = n.Text;
						Game.SelectByValue(drpType, ((int)n.Type).ToString());
					}
				}

			}
		}


		protected void btnAdd_Click(object sender, EventArgs e) {
			News news = null;
			if (Request.QueryString["Edit"] == null) {
				news = new News((Wc3o.NewsType)int.Parse(drpType.SelectedValue));
			}
			else
				foreach (News n in Game.PortalData.News)
					if (Request.QueryString["Edit"] == n.GetHashCode().ToString()) {
						news = n;
						break;
					}
			news.Name = txtCreator.Text;
			news.Text = txtText.Text;
			news.Date = DateTime.Parse(txtDate.Text);
			Response.Redirect("News.aspx");
		}

		protected void btnNow_Click(object sender, EventArgs e) {
			txtDate.Text = DateTime.Now.ToString();
		}

		protected void btnEdit_Click(object sender, EventArgs e) {
			Response.Redirect("News.aspx?Edit=" + drpEdit.SelectedValue);
		}

		protected void btnDelete_Click(object sender, EventArgs e) {
			foreach (News n in Game.PortalData.News) {
				if (n.GetHashCode() == int.Parse(drpEdit.SelectedValue)) {
					n.Destroy();
					break;
				}
			}
			Response.Redirect("News.aspx");
		}
	}
}