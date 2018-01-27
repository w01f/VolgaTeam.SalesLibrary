using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.CommonGUI.Floater
{
	public partial class FormFloater : Form
	{
		private Rectangle _dragStartRectangle = Rectangle.Empty;

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

		private void OnBackButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.Yes;
		}

		private void OnHideButtonClick(object sender, EventArgs e)
		{
			DialogResult = DialogResult.No;
		}
		
		private void OnCaptionMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			WinAPIHelper.ReleaseCapture();
			WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
		}

		private void OnButtonMouseMove(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			if (_dragStartRectangle.IsEmpty) return;
			if (!_dragStartRectangle.Contains(e.Location))
			{
				WinAPIHelper.ReleaseCapture();
				WinAPIHelper.SendMessage(Handle, WinAPIHelper.WM_NCLBUTTONDOWN, WinAPIHelper.HTCAPTION, IntPtr.Zero);
			}
		}

		private void OnButtonMouseDown(object sender, MouseEventArgs e)
		{
			if (e.Button != MouseButtons.Left) return;
			_dragStartRectangle = new Rectangle(
				new Point(
					e.X - (SystemInformation.DragSize.Width / 2),
					e.Y - (SystemInformation.DragSize.Height / 2)),
				SystemInformation.DragSize);
		}

		private void OnButtonMouseUp(object sender, MouseEventArgs e)
		{
			_dragStartRectangle = Rectangle.Empty;
		}
	}
}