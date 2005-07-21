using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Portal.Help {
	public partial class Units_aspx : System.Web.UI.Page {
		protected void drpUnit_SelectedIndexChanged(object sender, EventArgs e) {
			Response.Redirect("Units.aspx?Unit=" + drpUnit.SelectedValue);
		}

		protected void Page_Load(object sender, EventArgs e) {
			pnlUnit.Visible = false;

			if (Request.QueryString["Unit"] != null) {
				pnlUnit.Visible = true;

				imgGold.ImageUrl = Configuration.Default_Gfx_Path + "/Game/Gold.gif";
				imgLumber.ImageUrl = Configuration.Default_Gfx_Path + "/Game/Lumber.gif";
				imgFood.ImageUrl = Configuration.Default_Gfx_Path + "/Game/Food.gif";

				UnitInfo i = UnitInfo.Get((UnitType)Enum.Parse(typeof(UnitType), Request.QueryString["Unit"]));
				imgUnit.ImageUrl = Configuration.Default_Gfx_Path + i.Image;
				imgFace.ImageUrl = Configuration.Default_Gfx_Path + i.CreateImage;
				if (i.Image == i.CreateImage)
					imgFace.Visible = false;
				lblName.Text = i.Name;
				lblGold.Text = Game.Format(i.Gold);
				lblLumber.Text = Game.Format(i.Lumber);
				lblFood.Text = Game.Format(i.Food);
				lblMinutes.Text = Game.Format(i.Minutes);
				lblFraction.Text = FractionInfo.Get(i.Fraction).ToString();
				lblScore.Text = Game.Format(i.Score);
				lblHitpoints.Text = Game.Format(i.Hitpoints);
				lblArmor.Text = Game.Format(i.Armor);
				lblArmorType.Text = Game.Format(i.ArmorType);
				if (i.Flies)
					lblFlies.Text = "Yes";
				else
					lblFlies.Text = "No";
				lblVisibility.Text = Game.Format(i.Visibility);
				lblSpeed.Text = Game.Format(i.Speed);
				lblRange.Text = Game.Format(i.Range);
				if (i.ForGold)
					lblForGold.Text = "Yes";
				else
					lblForGold.Text = "No";
				if (i.ForLumber)
					lblForLumber.Text = "Yes";
				else
					lblForLumber.Text = "No";
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

				Response.Write(i.MalusAuraAttackAir);
				
				lblMalusAttackAir.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraAttackAir) * 100));
				lblMalusAttackGround.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraAttackGround) * 100));
				lblMalusCooldown.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraCooldown) * 100));
				lblMalusHitpoints.Text = Game.Format(Convert.ToInt32((1 - i.MalusAuraHitpoints) * 100));

				lblRequirements.Text = "<ul>";
				foreach (BuildingType t in i.Requirements)
					lblRequirements.Text += "<li><a href='Buildings.aspx?Building=" + t.ToString() + "'>" + BuildingInfo.Get(t).Name + "</a></li>";
				lblRequirements.Text += "</ul>";

				if (i.TrainedAt != BuildingType.None)
					lblTrainedAt.Text += "<a href='Buildings.aspx?Building=" + i.TrainedAt.ToString() + "'>" + BuildingInfo.Get(i.TrainedAt).Name + "</a>";
			}

			if (!IsPostBack) {
				foreach (UnitType t in Enum.GetValues(typeof(UnitType))) {
					if (t != UnitType.None) {
						UnitInfo i = UnitInfo.Get(t);
						drpUnit.Items.Insert(GetIndex("[" + FractionInfo.Get(i.Fraction).ToString() + "] " + i.Name), (new System.Web.UI.WebControls.ListItem("[" + FractionInfo.Get(i.Fraction).ToString() + "] " + i.Name, i.Type.ToString())));
					}
				}
			}
		}

		int GetIndex(string s) {
			int i = 0;
			for (int j = 0; j < drpUnit.Items.Count; j++)
				if (drpUnit.Items[j].Text.CompareTo(s) <=0)
					i++;
			return i;
		}


	}

}
