using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public partial class FormStart : FormProgressBase
	{
		public event EventHandler<EventArgs> Trayed;
		public event EventHandler<EventArgs> Activated;

		public FormStart(string title, bool trayed)
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				laTitle.Font = new Font(laTitle.Font.FontFamily, laTitle.Font.Size - 2, laTitle.Font.Style);
			}

			Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 20;
			Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 20;

			pbCancel.Buttonize();
			notifyIcon.Text = title;
			notifyIcon.BalloonTipText = title;
			toolStripMenuItemKillApp.Text = String.Format(toolStripMenuItemKillApp.Text, title);

			FormClosed += FormStart_FormClosed;

			if (trayed)
			{
				Opacity = 0;
				notifyIcon.Visible = true;
			}
			else
			{
				notifyIcon.Visible = false;
				Opacity = 1;
			}
		}

		public void SetText(string text)
		{
			laTitle.Text = text;
		}

		private void ShowTrayIcon()
		{
			Opacity = 0;
			notifyIcon.Visible = true;
			notifyIcon.BalloonTipText = laTitle.Text;
			notifyIcon.ShowBalloonTip(1);
		}

		private void HideTrayIcon()
		{
			notifyIcon.Visible = false;
			Opacity = 1;
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			laTitle.Focus();
			circularProgress.IsRunning = true;
		}

		private void FormStart_FormClosed(object sender, FormClosedEventArgs e)
		{
			HideTrayIcon();
		}

		private void pbCancel_Click(object sender, EventArgs e)
		{
			ShowTrayIcon();
			if (Trayed != null)
				Trayed(this, EventArgs.Empty);
		}

		private void toolStripMenuItemShowProgress_Click(object sender, EventArgs e)
		{
			HideTrayIcon();
			if (Activated != null)
				Activated(this, EventArgs.Empty);
		}

		private void toolStripMenuItemKillApp_Click(object sender, EventArgs e)
		{
			Environment.Exit(1);
		}
	}
}