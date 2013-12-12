using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TamperProofQueryStrings
{
	public partial class _Default : Page
	{
		protected void Page_Load(object sender, EventArgs e)
		{
			if (!IsPostBack)
			{
				lnkAbout.NavigateUrl = string.Concat("/About.aspx?", HashingHelper.CreateTamperProofQueryString("cid=21&pid=43"));
			}
		}		
	}
}