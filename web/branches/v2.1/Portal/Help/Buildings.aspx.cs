using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

namespace Wc3o.Pages.Portal.Help {
	public partial class Buildings_aspx : System.Web.UI.Page {
		protected void drpBuilding_SelectedIndexChanged(object sender, EventArgs e) {
			Response.Redirect("Buildings.aspx?Building=" + drpBuilding.SelectedValue);
		}

		protected void Page_Load(object sender, EventArgs e) {
			pnlBuilding.Visible = false;

			if (Request.QueryString["Building"] != null) {
				pnlBuilding.Visible = true;

				imgGold.ImageUrl = Configuration.Default_Gfx_Path + "/Game/Gold.gif";
				imgLumber.ImageUrl = Configuration.Default_Gfx_Path + "/Game/Lumber.gif";

				BuildingInfo i = BuildingInfo.Get((BuildingType)Enum.Parse(typeof(BuildingType), Request.QueryString["Building"]));
				imgBuilding.ImageUrl = Configuration.Default_Gfx_Path + i.Image;
				lblName.Text = i.Name;
				lblGold.Text = Game.Format(i.Gold);
				lblLumber.Text = Game.Format(i.Lumber);
				lblMinutes.Text = Game.Format(i.Minutes);
				lblUpkeep.Text = Game.Format(i.Food);
				lblMax.Text = Game.Format(i.Number);
				lblMaxPerSector.Text = Game.Format(i.NumberPerSector);

				lblFraction.Text = FractionInfo.Get(i.Fraction).ToString();
				lblScore.Text = Game.Format(i.Score);
				lblHitpoints.Text = Game.Format(i.Hitpoints);
				lblArmor.Text = Game.Format(i.Armor);
				lblArmorType.Text = Game.Format(i.ArmorType);
				lblRange.Text = Game.Format(i.Range);
				lblAttackGround.Text = Game.Format(i.AttackGround);
				lblAttackAir.Text = Game.Format(i.AttackAir);
				lblAttackTypeGround.Text = Game.Format(i.AttackTypeGround);
				lblAttackTypeAir.Text = Game.Format(i.AttackTypeAir);
				lblCoolDown.Text = Game.Format(i.Cooldown);
				lblBonusArmor.Text = Game.Format(Convert.ToInt32((i.BonusAuraArmor - 1) * 100));
				lblBonusRange.Text = Game.Format(Convert.ToInt32((i.BonusAuraRange - 1) * 100));
				lblBonusAttackAir.Text = Game.Format(Convert.ToInt32((i.BonusAuraAttackAir - 1) * 100));
				lblBonusAttackGround.Text = Game.Format(Convert.ToInt32((i.BonusAuraAttackGround - 1) * 100));
				lblBonusCooldown.Text = Game.Format(Convert.ToInt32((i.BonusAuraCooldown - 1) * 100));
				lblBonusHitpoints.Text = Game.Format(Convert.ToInt32((i.BonusAuraHitpoints - 1) * 100));
				lblMalusArmor.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraArmor) * 100));
				lblMalusRange.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraRange) * 100));
				lblMalusAttackAir.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraAttackAir) * 100));
				lblMalusAttackGround.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraAttackGround) * 100));
				lblMalusCooldown.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraCooldown) * 100));
				lblMalusHitpoints.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraHitpoints) * 100));

				lblRequirements.Text = "<ul>";
				foreach (BuildingType t in i.Requirements)
					lblRequirements.Text += "<li><a href='Buildings.aspx?Building=" + t.ToString() + "'>" + BuildingInfo.Get(t).Name + "</a></li>";
				lblRequirements.Text += "</ul>";
				lblUpgradesTo.Text = "<ul>";
				foreach (BuildingType t in i.UpgradesTo)
					lblUpgradesTo.Text += "<li><a href='Buildings.aspx?Building=" + t.ToString() + "'>" + BuildingInfo.Get(t).Name + "</a></li>";
				lblUpgradesTo.Text += "</ul>";

			}

			if (!IsPostBack) {
				foreach (BuildingType t in Enum.GetValues(typeof(BuildingType))) {
					if (t != BuildingType.None) {
						BuildingInfo i = BuildingInfo.Get(t);
						drpBuilding.Items.Insert(GetIndex("[" + FractionInfo.Get(i.Fraction).ToString() + "] " + i.Name), new ListItem("[" + FractionInfo.Get(i.Fraction).ToString() + "] " + i.Name, i.Type.ToString()));
					}
				}
			}
		}

		int GetIndex(string s) {
			int i = 0;
			for (int j = 0; j < drpBuilding.Items.Count; j++)
				if (drpBuilding.Items[j].Text.CompareTo(s) <= 0)
					i++;
			return i;
		}

	}
}
