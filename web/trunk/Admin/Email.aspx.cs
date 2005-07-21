using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Admin {
	public partial class Email_aspx : System.Web.UI.Page {

		protected void btnSend_Click(object sender, EventArgs e) {

			string[] recipients = txtRecipients.Text.Split('\n');

			foreach (string s in recipients) {
				string recipient = s.Replace("\r","");

				try {
					Game.SendEmail(recipient, txtSubject.Text, txtText.Text);
					Response.Write("Mail to " + recipient + " successful <br />");
				} catch (Exception ex) {
					Response.Write("Mail to " + recipient + "failed: " + ex.Message + " <br />");
				}
			}

		}
	}
}
