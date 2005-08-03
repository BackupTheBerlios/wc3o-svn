using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Mail_aspx : System.Web.UI.Page {
		Player user;

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			user = Wc3o.Game.CurrentPlayer;

			if (!IsPostBack) {
				pnlDetails.Visible = false;
				pnlMessages.Visible = false;
				pnlSend.Visible = false;

				if (Request.QueryString["Recipient"] != null) {
					pnlSend.Visible = true;
					if (Request.QueryString["Alliance"] != null && user.Alliance.Name == Request.QueryString["Alliance"]) {
						txtRecipient.Text = "";
						foreach (Player member in user.Alliance.Members)
							txtRecipient.Text += member.Name + ";";
					}
					else {
						txtRecipient.Text = Request.QueryString["Recipient"];
						txtRecipient.Enabled = true;
					}
					if (Request.QueryString["Subject"] != null)
						txtSubject.Text = Request.QueryString["Subject"];
				}
				else if (Request.QueryString["Message"] != null) {
					pnlDetails.Visible = true;
					foreach (Message message in user.Messages)
						if (message.GetHashCode() == int.Parse(Request.QueryString["Message"])) {
							message.Unread = false;
							lblDate.Text = Wc3o.Game.Format(message.Date, true);
							lblSubject.Text = message.Subject;
							lblText.Text = message.Text;
							hplDelete.NavigateUrl = "Mail.aspx?Delete=" + message.GetHashCode();
							Player messageSender = message.Sender;
							if (messageSender != null) {
								lblFrom.Text = messageSender.FullName;
								hplReply.NavigateUrl = "Mail.aspx?Recipient=" + message.Sender.Name + "&Subject=Re: " + message.Subject;
								hplReply.Enabled = true;
							}
							else {
								hplReply.Enabled = false;
								lblFrom.Text = "-";
							}
							break;
						}
				}
				else if (Request.QueryString["Delete"] != null) {
					if (Request.QueryString["Delete"] == "0") {
						List<Message> l = new List<Message>();
						foreach (Message message in user.Messages)
							l.Add(message);

						foreach (Message m in l)
							m.Destroy();
					}
					else
						foreach (Message message in user.Messages)
							if (message.GetHashCode() == int.Parse(Request.QueryString["Delete"])) {
								user.Messages.Remove(message);
								break;
							}
					Response.Redirect("Mail.aspx");
				}
				else {
					pnlMessages.Visible = true;

					DataTable messages = new DataTable();
					messages.Columns.Add("Sender", typeof(String));
					messages.Columns.Add("Date", typeof(String));
					messages.Columns.Add("Subject", typeof(String));
					messages.Columns.Add("Reply", typeof(String));
					messages.Columns.Add("Delete", typeof(String));

					foreach (Message message in user.Messages) {
						DataRow row = messages.NewRow();
						if (message.Sender != null)
							row["Sender"] = message.Sender.FullName;
						else
							row["Sender"] = "";
						row["Date"] = Wc3o.Game.Format(message.Date, true);

						if (message.Unread)
							row["Subject"] = "<b><a href='Mail.aspx?Message=" + message.GetHashCode() + "'>" + message.Subject + "</a></b>";
						else
							row["Subject"] = "<a href='Mail.aspx?Message=" + message.GetHashCode() + "'>" + message.Subject + "</a>";

						if (message.Sender != null)
							row["Reply"] = "<a href='Mail.aspx?Recipient=" + message.Sender.Name + "&Subject=Re: " + message.Subject + "'>Reply</a>";
						else
							row["Reply"] = "";
						row["Delete"] = "<a href='Mail.aspx?Delete=" + message.GetHashCode() + "'>Delete</a>";
						messages.Rows.Add(row);
					}

					DataView dv = new DataView(messages);
					dv.Sort = "Date DESC";

					grdMessages.DataSource = dv;
					grdMessages.DataBind();
				}
			}
		}

		protected void btnSend_Click(object sender, EventArgs e) {
			bool ok = true;
			if (txtRecipient.Text.Length <= 0 || txtSubject.Text.Length <= 0) {
				ok = false;
				Wc3o.Game.Message(Master, "Fill out all necessary fields.", MessageType.Error);
			}

			if (ok) {
				int numberOfRecipients = 0;
				System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(@"\w*");
				foreach (System.Text.RegularExpressions.Match match in regex.Matches(txtRecipient.Text)) {
					if (match.Length > 0) {
						Player recipient = Wc3o.Game.GameData.Players[match.Value];
						if (recipient != null) {
							string subject = "";
							if (Request.QueryString["Alliance"] != null && Request.QueryString["Alliance"] == user.Alliance.Name)
								subject = "<i>[Alliance message] </i>";
							new Message(recipient, user, subject + Server.HtmlEncode(txtSubject.Text), Server.HtmlEncode(txtText.Text).Replace("\n", "<br />"));
							if (recipient.IsAlly(user))
								numberOfRecipients++;

							if (txtSubject.Text.Length <= 0)
								txtSubject.Text = "no subject";

							Wc3o.Game.Message(Master, "Your message was sent to '" + match.Value + "'.", MessageType.Acknowledgement);
						}
						else
							Wc3o.Game.Message(Master, "The Recipient '" + match.Value + "' does not exist.", MessageType.Error);

						if (numberOfRecipients >= 5)
							break;
					}
				}
				txtRecipient.Text = "";
			}
		}

	}
}
