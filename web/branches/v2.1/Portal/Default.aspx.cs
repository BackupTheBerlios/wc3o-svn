using System;
using System.Collections.Generic;

namespace Wc3o.Pages.Portal {
	public partial class Default_aspx : System.Web.UI.Page {

		protected void Page_Load(object sender, EventArgs e) {
			#region " Intro "
			imgIntroLeft.ImageUrl = Game.Gfx + "/Portal/IntroLeft.jpg";
			imgIntroRight.ImageUrl = Game.Gfx + "/Portal/IntroRight.jpg";

			#endregion

			#region " News "
			List<News> l = new List<News>();
			int numberOfNews = Game.PortalData.News.Count;
			int maxNumberOfNews = Configuration.Number_Of_News;
			if (numberOfNews < maxNumberOfNews)
				maxNumberOfNews = numberOfNews;

			for (int i = numberOfNews - 1; i >= numberOfNews  - maxNumberOfNews; i--) {
				l.Add(Game.PortalData.News[i]);
			}

			rptNews.DataSource = l;
			rptNews.DataBind();
			#endregion
		}

	}
}
