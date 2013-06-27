using System;
using System.Drawing;
using System.Windows.Forms;

namespace SalesDepot.ToolForms.QBuilderForms
{
	public partial class FormBrowserSelector : Form
	{
		public enum Browser
		{
			Chrome = 0,
			Firefox,
			IE,
			Opera,
		}

		public Browser SelectedBrowser { get; set; }

		public FormBrowserSelector()
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				buttonXChrome.Font = new Font(buttonXChrome.Font.FontFamily, buttonXChrome.Font.Size - 3, buttonXChrome.Font.Style);
				buttonXFirefox.Font = new Font(buttonXFirefox.Font.FontFamily, buttonXFirefox.Font.Size - 3, buttonXFirefox.Font.Style);
				buttonXIE.Font = new Font(buttonXIE.Font.FontFamily, buttonXIE.Font.Size - 3, buttonXIE.Font.Style);
				buttonXOpera.Font = new Font(buttonXOpera.Font.FontFamily, buttonXOpera.Font.Size - 3, buttonXOpera.Font.Style);
				buttonXClose.Font = new Font(buttonXClose.Font.FontFamily, buttonXClose.Font.Size - 3, buttonXClose.Font.Style);
			}
		}

		private void buttonXClose_Click(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Cancel;
			Close();
		}

		private void buttonXChrome_Click(object sender, EventArgs e)
		{
			SelectedBrowser = Browser.Chrome;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXFirefox_Click(object sender, EventArgs e)
		{
			SelectedBrowser = Browser.Firefox;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXIE_Click(object sender, EventArgs e)
		{
			SelectedBrowser = Browser.IE;
			DialogResult = DialogResult.OK;
			Close();
		}

		private void buttonXOpera_Click(object sender, EventArgs e)
		{
			SelectedBrowser = Browser.Opera;
			DialogResult = DialogResult.OK;
			Close();
		}
	}
}