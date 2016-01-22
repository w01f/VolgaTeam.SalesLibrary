using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public partial class FormStart : FormProgressBase
	{
		public FormStart()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
			}

			Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 20;
			Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 20;

			pbCancel.Buttonize();
		}

		public void SetTitle(string text)
		{
			laTitle.Text = text;
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}

		private void pbCancel_Click(object sender, EventArgs e)
		{
			Opacity = 0;
		}
	}
}