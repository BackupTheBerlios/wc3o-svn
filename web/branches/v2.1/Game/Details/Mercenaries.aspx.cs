using System;

namespace Wc3o.Pages.Game.Details {
	public partial class Mercenaries_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			if (RemoteScripting.InvokeMethod(Page)) return;
		}


		public string GetMercenaries(string coordinates) {
			string result = "";

			Player player = Wc3o.Game.CurrentPlayer;
			Sector sector = Wc3o.Game.GameData.Sectors[new Coordinate(coordinates)];

			result += player.Gfx + "@"+sector.Coordinate+"@";

			foreach (MercenaryInfo mercenaryInfo in (sector as MercenarySector).Mercenaries) {
				UnitInfo unitInfo = UnitInfo.Get(mercenaryInfo.Type);
				result += mercenaryInfo.Number + "$" + Wc3o.Game.TimeSpan(mercenaryInfo.Date) + "$";
				result += unitInfo.Name + "$" + unitInfo.CreateImage + "$" + unitInfo.Gold + "$" + unitInfo.Lumber + "$" + unitInfo.Food + "$" + unitInfo.Type + "@";
			}

			return result;
		}

	}
}
