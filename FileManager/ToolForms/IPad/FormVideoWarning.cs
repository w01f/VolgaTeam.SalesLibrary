using System;
using System.Drawing;
using System.Windows.Forms;

namespace FileManager.ToolForms.IPad
{
	public partial class FormVideoWarning : Form
	{
		public FormVideoWarning()
		{
			InitializeComponent();
			if ((base.CreateGraphics()).DpiX > 96)
				laWarningText.Font = new Font(laWarningText.Font.FontFamily, laWarningText.Font.Size - 3, laWarningText.Font.Style);

			Left = FormMain.Instance.Width - Width - 20;
			Top = FormMain.Instance.Top + 72;
		}

		#region Picture Box Clicks Habdlers
		/// <summary>
		/// Buttonize the PictureBox 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void pictureBox_MouseDown(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top += 1;
		}

		private void pictureBox_MouseUp(object sender, MouseEventArgs e)
		{
			var pic = (PictureBox)(sender);
			pic.Top -= 1;
		}
		#endregion

		private void pbCancel_Click(object sender, EventArgs e)
		{
			Close();
		}

		private void laWarningText_Click(object sender, EventArgs e)
		{
			FormMain.Instance.ribbonControl.SelectedRibbonTabItem = FormMain.Instance.ribbonTabItemIPad;
			Close();
		}
	}
}