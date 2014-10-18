using System;
using System.Drawing;
using System.Windows.Forms;
using SalesDepot.CoreObjects.InteropClasses;

namespace SalesDepot.CommonGUI.Floater
{
	public partial class FormFloater : Form
	{
		public FormFloater(int x, int y, string defaultText,Image defaultImage)
		{
			InitializeComponent();
			Top = y;
			Left = x - Width;
			labelCaption.Text = string.Format("{0} - User: {1}", defaultText, Environment.UserName);
			buttonXBack.Image = defaultImage;
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
		
		private void labelCaption_MouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			WinAPIHelper.ReleaseCapture();
			WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
		}
	}
}