using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TamperProofQueryStrings
{
	public partial class About : Page
	{
		protected override void OnLoad(EventArgs e)
		{
			HashingHelper.ValidateQueryString();
			base.OnLoad(e);
		}

		protected void Page_Load(object sender, EventArgs e)
		{

		}
	}
}