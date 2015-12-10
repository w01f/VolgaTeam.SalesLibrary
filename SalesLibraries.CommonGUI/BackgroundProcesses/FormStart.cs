using System;
using System.Drawing;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public partial class FormStart : FormProgressBase
	{
		private const string GrayTextFormat = "<font color=\"#8C8C8C\">{0}</font>";

		public FormStart()
		{
			InitializeComponent();
			TopMost = true;
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
			}
		}

		public void SetTitle(string text, string description)
		{
			laTitle.Text = text;
			labelXDescription.Text = String.Format(GrayTextFormat, description);
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			BackColor = Color.Green;
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}
	}
}