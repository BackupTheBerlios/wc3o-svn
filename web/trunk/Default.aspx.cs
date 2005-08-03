using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

public partial class _Default : System.Web.UI.Page {

	protected void btnEmail_Click(object sender, EventArgs e) {
		if (!Page.IsValid)
			return;

		System.IO.StreamWriter writer = new System.IO.StreamWriter(Server.MapPath("App_Data/Emails.txt"));
		writer.WriteLine(txtEmail.Text);
		writer.Close();

		Response.Write("Your eMail address has been saved. Thanks.");
	}
}
