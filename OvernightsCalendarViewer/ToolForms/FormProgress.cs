using System;
using System.Drawing;
using System.Windows.Forms;

namespace OvernightsCalendarViewer.ToolForms
{
	public partial class FormProgress : Form
	{
		public FormProgress()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
			{
				laProgress.Font = new Font(laProgress.Font.FontFamily, laProgress.Font.Size - 2, laProgress.Font.Style);
			}
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laProgress.Focus();
			circularProgress.IsRunning = true;
		}
	}
}