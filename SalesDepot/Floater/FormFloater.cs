using System;
using System.Drawing;
using System.Windows.Forms;
using SalesDepot.ConfigurationClasses;

namespace SalesDepot.Floater
{
	public partial class FormFloater : Form
	{
		public FormFloater(int x, int y, Image defaultImage, string defaultText)
		{
			InitializeComponent();
			Top = y;
			Left = x - Width;
			Text = string.Format("{0} - User: {1}", SettingsManager.Instance.SalesDepotName, Environment.UserName);
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