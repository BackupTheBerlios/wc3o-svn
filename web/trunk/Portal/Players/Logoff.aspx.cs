using System;

namespace Wc3o.Pages.Portal.Players {
	public partial class Logoff_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			System.Web.Security.FormsAuthentication.SignOut();
			Response.Redirect("~/Portal");
		}

	}
}
