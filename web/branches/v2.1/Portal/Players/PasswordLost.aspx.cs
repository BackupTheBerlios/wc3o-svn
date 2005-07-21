using System;

namespace Wc3o.Pages.Portal.Players {
	public partial class PasswordLost_aspx : System.Web.UI.Page {

		protected void btnEmail_Click(object sender, EventArgs e) {
			if (IsValid) {
				Player player = Game.GetPlayerByEmail(txtEmail.Text);
				if (player == null) {
					Game.Message(Master, "This eMail address is not valid.", MessageType.Error);
					return;
				}

				player.Password = Session.SessionID.Substring(0, 10);
				Game.SendEmail(player.Email, "Your new password", player.Name + ", you requested a new password. So a new one has been created, and here it is: " + player.Password);
				System.Web.Security.FormsAuthentication.SignOut();
				Game.Message(Master, "Please check you inbox, you should already have received your new password.", MessageType.Acknowledgement);
			}
			else
				Game.Message(Master, "Your data is not valid.", MessageType.Error);
		}

	}
}