using System;
using System.Drawing;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public partial class FormProgressCommon : FormProgressBase
	{
		public override string Title
		{
			get => laProgress.Text;
			set => laProgress.Text = value;
		}
		public FormProgressCommon()
		{
			InitializeComponent();
			TopMost = true;
			if ((CreateGraphics()).DpiX > 96)
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