using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game.Commands {
	public partial class Mercenaries_aspx : System.Web.UI.Page {

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			if (Request.QueryString["Action"] == null) {
				CloseWindow(0);
				return;
			}

			Player player = Wc3o.Game.CurrentPlayer;
			MercenarySector sector = Wc3o.Game.CurrentSector as MercenarySector;

			if (!IsPostBack) {
				if (Request.QueryString["Action"] == "Recruit") {
					if (!player.IsAlly(sector.Owner)) {
						Wc3o.Game.Message(this, "You cannot recruit any mercenaries on this sector because the owner of it is not an ally of yours.", MessageType.Error);
						CloseWindow(3000);
						return;
					}

					int index=int.Parse(Request.QueryString["Mercenary"].ToString());
					MercenaryInfo mercenaryInfo = sector.Mercenaries[index];
					UnitInfo unitInfo = UnitInfo.Get(mercenaryInfo.Type);

					int number = int.Parse(Request.QueryString["Number"].ToString());
					number = Wc3o.Game.Min(number, mercenaryInfo.Number);

					int count = 0;
					int estimatedGold = player.Gold;
					int estimatedLumber = player.Lumber;
					int estimatedUpkeep = player.EstimatedUpkeep;
					int food = player.Food;
					while (count < number) {
						if (estimatedGold - unitInfo.Gold < 0 || estimatedLumber - unitInfo.Lumber < 0 || food < estimatedUpkeep + unitInfo.Food)
							break;
						estimatedGold -= unitInfo.Gold;
						estimatedLumber -= unitInfo.Lumber;
						estimatedUpkeep += unitInfo.Food;
						count++;
					}

					if (count > 0) {
						Unit unit = new Unit(mercenaryInfo.Type, sector, player, DateTime.Now);
						unit.Number = count;
						player.Gold = estimatedGold;
						player.Lumber = estimatedLumber;

						mercenaryInfo.Number -= count;
						if (mercenaryInfo.Number <= 0) {
							mercenaryInfo = sector.CreateNewMercenary();
							mercenaryInfo.Date = DateTime.Now.AddMinutes(UnitInfo.Get(mercenaryInfo.Type).Minutes * mercenaryInfo.Number);
							sector.Mercenaries[index] = mercenaryInfo;
						}

						Wc3o.Game.Message(this, "You recruited " + count + " " + unitInfo.Name + ". Use them wisely.", MessageType.Acknowledgement);
						lblScript.Text += "<script language='JavaScript'>window.opener.document.location='../Sector.aspx?Refresh=Mercenaries';</script>";
						CloseWindow(3000);
						return;
					}
					else {
						Wc3o.Game.Message(this, "You cannot recruit this mercenary. Make sure you have enough gold and lumber and that you produce enough food.", MessageType.Error);
						CloseWindow(3000);
						return;
					}

				}
			}
		}

		#region " Methods for javascript "
		void CloseWindow(int timeout) {
			lblScript.Text += "<script language='JavaScript'>setTimeout('close()'," + timeout + ");</script>";
		}
		#endregion

	}
}
