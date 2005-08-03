using System;

namespace Wc3o.Pages.Admin {
	public partial class Admin_master : System.Web.UI.MasterPage {

		protected void Page_Load(object sender, EventArgs e) {
			//if (!System.Web.HttpContext.Current.User.Identity.IsAuthenticated || !Game.CurrentPlayer.IsAdmin)
			//	System.Web.Security.FormsAuthentication.RedirectToLoginPage();
		}

	}
}