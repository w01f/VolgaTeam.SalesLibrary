using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot
{
	public partial class FormFloater : Form
	{
		public FormFloater(int inix, int iniy, int lastx, int lasty, Image defaultImage, string defaultText)
		{
			InitializeComponent();

			if (lastx == int.MinValue || lasty == int.MinValue)
			{
				this.Top = iniy;
				this.Left = inix - this.Width;
			}
			else
			{
				this.Top = lasty;
				this.Left = lastx;
			}

			if ((base.CreateGraphics()).DpiX > 96)
			{
				this.Font = new Font(this.Font.FontFamily, this.Font.Size - 1, this.Font.Style);
			}

			this.Text = string.Format("{0} - User: {1}", ConfigurationClasses.SettingsManager.Instance.SalesDepotName, Environment.UserName);
			buttonItemDashboard.Image = defaultImage;
			ribbonBarDashboard.Text = defaultText;
			ConfigurationClasses.RegistryHelper.SalesDepotHandle = this.Handle;
		}

		private void buttonItemDefaultStar_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}
	}
}