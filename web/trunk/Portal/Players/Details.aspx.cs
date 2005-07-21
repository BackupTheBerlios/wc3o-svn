using System;

namespace Wc3o.Pages.Portal.Players {
	public partial class Details_aspx : System.Web.UI.Page {

		Player player;

		protected void Page_Load(object sender, EventArgs e) {
			if (!User.Identity.IsAuthenticated)
				Response.Redirect("~/Portal");

			player = Game.GameData.Players[User.Identity.Name];

			if (!IsPostBack) {
				lblName.Text = player.Name;
				txtEmail.Text = player.Email;
				txtGfx.Text = player.Gfx;
			}
		}

		protected void btnEmail_Click(object sender, EventArgs e) {
			if (IsValid) {
				if (txtEmail.Text == player.Email)
					return;

				if (Game.GetPlayerByEmail(txtEmail.Text) != null) {
					Game.Message(Master, "This eMail address s already in use.", MessageType.Error);
					return;
				}

				player.Password = Session.SessionID.Substring(0, 10);
				Game.SendEmail(player.Email, "Your new password", player.Name + ", you changed your eMail address. To make sure it is correct your password has been changed. Your new password is: " + player.Password);
				System.Web.Security.FormsAuthentication.SignOut();
				Game.Message(Master, "Your eMail address has been changed. Please check you inbox, you should already have received a new password.", MessageType.Acknowledgement);
			}
			else
				Game.Message(Master, "Your data is not valid.", MessageType.Error);
		}

		protected void btnGfx_Click(object sender, EventArgs e) {
			if (IsValid) {
				if (txtGfx.Text.Length <= 0)
					txtGfx.Text = Configuration.Default_Gfx_Path;
				player.Gfx = txtGfx.Text;
			}
			else
				Game.Message(Master, "Your data is not valid.", MessageType.Error);
		}

		protected void btnPassword_Click(object sender, EventArgs e) {
			if (IsValid) {
				if (txtCurrentPassword.Text == player.Password) {
					player.Password = txtPassword.Text;
					Game.Message(Master, "Your password has been changed.", MessageType.Acknowledgement);
				}
				else
					Game.Message(Master, "Your current password is invalid.", MessageType.Error);
			}
			else
				Game.Message(Master, "Your data is not valid.", MessageType.Error);
		}

	}
}
