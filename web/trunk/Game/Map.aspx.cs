using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Game {
	public partial class Map_aspx : System.Web.UI.Page {

		protected void Page_PreInit(object sender, EventArgs e) {
			this.Theme = Wc3o.Game.Theme;
		}

		protected void Page_Load(object sender, EventArgs e) {
			if (!IsPostBack) {
				Player player = Wc3o.Game.CurrentPlayer;
				Sector sector = Wc3o.Game.CurrentSector;

				bool sectorAlreadyInDropdownBox = false;

				lblSectors.Text = "<select id='drpSectors'>";

				foreach (Wc3o.Sector s in player.Sectors) {
					lblSectors.Text += "<option value='"+s.Coordinate+"'>"+s+"</option>";
					if (s == sector)
						sectorAlreadyInDropdownBox = true;
				}

				if (!sectorAlreadyInDropdownBox) 
					lblSectors.Text += "<option value='" + sector.Coordinate + "'>&lt;&lt; " + sector + " &gt;&gt;</option>";

				lblSectors.Text += "</select>";

				lblJavaScript.Text = "<script language='JavaScript'>LoadMap('" + sector.Coordinate.ToString() + "');</script>";
			}
		}

	}
}
