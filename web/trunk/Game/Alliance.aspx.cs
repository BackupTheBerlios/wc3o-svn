using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Alliance_aspx : System.Web.UI.Page {

		Player player;

		protected void Page_PreInit(object sender, EventArgs e) {
			Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			player = Wc3o.Game.CurrentPlayer;

			if (!IsPostBack) {
				pnlAlliance.Visible = false;
				pnlNoAlliance.Visible = false;
				pnlWaiting.Visible = false;
				pnlLeader.Visible = false;

				if (player.Alliance != null && player.IsAccepted) {
					pnlAlliance.Visible = true;

					imgImage.ImageUrl = player.Alliance.Image;
					lblDirective.Text = player.Alliance.Directive;
					lblName.Text = player.Alliance.FullName;
					hplMessage.NavigateUrl = "Mail.aspx?Alliance=" + player.Alliance.Name;

					foreach (Player p in player.Alliance.AcceptedMembers)
						drpVote.Items.Add(new System.Web.UI.WebControls.ListItem(p.RankedName, p.Name));

					List<Player> l = new List<Player>();
					foreach (Player p in player.Alliance.Members)
						if (p.IsAccepted)
							l.Add(p);
					grdMembers.DataSource = l;
					grdMembers.DataBind();

					if (player.Alliance.Leader == player) {
						pnlLeader.Visible = true;
						txtDescription.Text = player.Alliance.Description.Replace("<br />", "\r\n");
						txtDirective.Text = player.Alliance.Directive.Replace("<br />", "\r\n");
						txtLongName.Text = player.Alliance.LongName;
						txtImage.Text = player.Alliance.Image;

						drpRank.Items.Add(new System.Web.UI.WebControls.ListItem("Level 0", "0"));
						drpRank.Items.Add(new System.Web.UI.WebControls.ListItem("Level 1 (" + Wc3o.Game.Format(AllianceRank.Level1, Fraction.Humans) + " / " + Wc3o.Game.Format(AllianceRank.Level1, Fraction.Orcs) + " / " + Wc3o.Game.Format(AllianceRank.Level1, Fraction.Undead) + " / " + Wc3o.Game.Format(AllianceRank.Level1, Fraction.NightElves) + ")", "1"));
						drpRank.Items.Add(new System.Web.UI.WebControls.ListItem("Level 2 (" + Wc3o.Game.Format(AllianceRank.Level2, Fraction.Humans) + " / " + Wc3o.Game.Format(AllianceRank.Level2, Fraction.Orcs) + " / " + Wc3o.Game.Format(AllianceRank.Level2, Fraction.Undead) + " / " + Wc3o.Game.Format(AllianceRank.Level2, Fraction.NightElves) + ")", "2"));
						drpRank.Items.Add(new System.Web.UI.WebControls.ListItem("Level 3 (" + Wc3o.Game.Format(AllianceRank.Level3, Fraction.Humans) + " / " + Wc3o.Game.Format(AllianceRank.Level3, Fraction.Orcs) + " / " + Wc3o.Game.Format(AllianceRank.Level3, Fraction.Undead) + " / " + Wc3o.Game.Format(AllianceRank.Level3, Fraction.NightElves) + ")", "3"));

						foreach (Player p in player.Alliance.Members)
							if (!p.IsAccepted)
								drpWaiting.Items.Add(new System.Web.UI.WebControls.ListItem(p.FullName, p.Name));
							else if (p != player && p != player)
								drpMembers.Items.Add(new System.Web.UI.WebControls.ListItem(p.RankedName, p.Name));
					}
				}
				else if (player.Alliance != null && !player.IsAccepted) {
					pnlWaiting.Visible = true;
					lblWaiting.Text = player.Alliance.FullName;
				}
				else {
					pnlNoAlliance.Visible = true;
					foreach (Alliance a in Wc3o.Game.GameData.Alliances.Values)
						drpApply.Items.Add(new System.Web.UI.WebControls.ListItem(a.FullName, a.Name));
				}
			}
		}

		protected void btnVote_Click(object sender, EventArgs e) {
			if (drpVote.SelectedIndex > 0)
				player.Vote=Wc3o.Game.GameData.Players[drpVote.SelectedValue];
			Response.Redirect("Alliance.aspx");
		}


		protected void btnLeave_Click(object sender, EventArgs e) {
			player.Alliance = null;
			player.IsAccepted = false;
			Response.Redirect("Alliance.aspx");
		}


		protected void btnReject_Click(object sender, EventArgs e) {
			if (drpWaiting.SelectedIndex > 0) {
				Player p=Wc3o.Game.GameData.Players[drpWaiting.SelectedValue];
				p.Alliance = null;
				new Message(p, null, "Your appliance for " + player.Alliance.FullName + " has been rejected.", "");
			}
			Response.Redirect("Alliance.aspx");
		}


		protected void btnAccept_Click(object sender, EventArgs e) {
			if (drpWaiting.SelectedIndex > 0) {
				Player p = Wc3o.Game.GameData.Players[drpWaiting.SelectedValue];
				if (p == null)
					return;
				p.IsAccepted = true;
				new Message(p, null, "Your appliance for " + player.Alliance.FullName + " has been accepted.", "");
			}
			Response.Redirect("Alliance.aspx");
		}


		protected void btnKick_Click(object sender, EventArgs e) {
			if (drpMembers.SelectedIndex > 0) {
				Player p = Wc3o.Game.GameData.Players[drpMembers.SelectedValue];
				if (p == null)
					return;
				new Message(p, null, "The leader of " + player.Alliance.FullName + " has kicked you out.", "");
				player.Alliance = null;
				player.IsAccepted = false;
			}
			Response.Redirect("Alliance.aspx");
		}


		protected void btnFound_Click(object sender, EventArgs e) {
			if (IsValid) {
				Alliance a = new Alliance(txtFound.Text);
				a.Image = Configuration.Default_Gfx_Path+"/Portal/Logo.gif";
				a.Description = "no description given.";
				a.Directive = "no directive given.";
				a.LongName = a.Name;

				player.Alliance = a;
				player.IsAccepted = true;
				player.Vote = player;
				Response.Redirect("Alliance.aspx");
			}
			else
				Wc3o.Game.Message(Master, "Your data is not valid.", MessageType.Error);

		}


		protected void btnWaiting_Click(object sender, EventArgs e) {
			if (!player.IsAccepted) {
				player.Alliance = null;
				player.IsAccepted = false;
			}
			Response.Redirect("Alliance.aspx");
		}


		protected void btnApply_Click(object sender, EventArgs e) {
			if (drpApply.SelectedIndex > 0) {
				Alliance a = Wc3o.Game.GameData.Alliances[drpApply.SelectedValue];
				player.Alliance = a;
				if (a.Members.Count <= 1)
					player.IsAccepted = true;
				else {
					player.IsAccepted = false;
					new Message(a.Leader, null, player.FullName + " applies for your alliance.", "");
				}
				Response.Redirect("Alliance.aspx");
			}
		}


		protected void btnDetails_Click(object sender, EventArgs e) {
			player.Alliance.Description = Server.HtmlEncode(txtDescription.Text).Replace("\r\n", "<br />");
			player.Alliance.Directive = Server.HtmlEncode(txtDirective.Text).Replace("\r\n", "<br />");
			player.Alliance.LongName = Server.HtmlEncode(txtLongName.Text);
			player.Alliance.Image = txtImage.Text;
			Response.Redirect("Alliance.aspx");
		}

		protected void btnRank_Click(object sender, EventArgs e) {
			if (drpMembers.SelectedIndex > 0) {
				Player p = Wc3o.Game.GameData.Players[drpMembers.SelectedValue];
				if (p == null)
					return;

				AllianceRank rank = AllianceRank.Level0;
				if (drpRank.SelectedValue == "1")
					rank = AllianceRank.Level1;
				else if (drpRank.SelectedValue == "2")
					rank = AllianceRank.Level2;
				else if (drpRank.SelectedValue == "3")
					rank = AllianceRank.Level3;

				if (p.AllianceRank != rank) {
					p.AllianceRank = rank;
					if (rank != AllianceRank.Level0)
						new Message(p, null, "The leader of " + player.Alliance.FullName + " has promoted you. You are now " + p.RankedName + ".", "");
					else
						new Message(p, null, "The leader of " + player.Alliance.FullName + " has demotet you. You have no rank now.", "");
				}
			}
			Response.Redirect("Alliance.aspx");
		}
}
}
