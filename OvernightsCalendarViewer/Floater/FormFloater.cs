using System;
using System.Drawing;
using System.Windows.Forms;
using OvernightsCalendarViewer.ConfigurationClasses;

namespace OvernightsCalendarViewer.Floater
{
	public partial class FormFloater : Form
	{
		public FormFloater(int x, int y, Image defaultImage, string defaultText)
		{
			InitializeComponent();
			Top = y;
			Left = x - Width;
			Text = "Overnights";
			buttonItemBack.Image = defaultImage;
			ribbonBarBack.Text = defaultText;
			if ((CreateGraphics()).DpiX > 96)
				Font = new Font(Font.FontFamily, Font.Size - 1, Font.Style);
		}

		private void buttonItemBack_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void buttonItemHide_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}
	}
}