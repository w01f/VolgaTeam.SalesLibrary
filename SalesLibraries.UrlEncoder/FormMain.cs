using System;
using System.Windows.Forms;

namespace SalesLibraries.UrlEncoder
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void buttonConver_Click(object sender, EventArgs e)
		{
			var sourceUrl = textBoxSource.Text;
			if(String.IsNullOrEmpty(sourceUrl)) return;
			textBoxResult.Text = sourceUrl.Replace("!", "%21").Replace("#", "%23")
			   .Replace("$", "%24").Replace("&", "%26")
			   .Replace("'", "%27").Replace("(", "%28")
			   .Replace(")", "%29").Replace("*", "%2A");
		}
	}
}
