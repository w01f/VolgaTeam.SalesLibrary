using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.SalesDepot
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

			var styleSettings = new SyncFormStyleConfiguration();
			styleSettings.Load(Path.Combine(RemoteResourceManager.Instance.AppRootFolderPath, "sync_color.xml"));
			BackColor = panelEx.Style.BorderColor.Color = panelExCancel.Style.BorderColor.Color = styleSettings.SyncBorderColor ?? BackColor;
			panelEx.Style.BackColor1.Color = panelEx.Style.BackColor2.Color = panelExCancel.Style.BackColor1.Color = panelExCancel.Style.BackColor2.Color = styleSettings.SyncBackColor ?? panelEx.Style.BackColor1.Color;
			laTitle.ForeColor = styleSettings.SyncTextColor ?? laTitle.ForeColor;
			circularProgress.ProgressColor = styleSettings.SyncCircleColor ?? circularProgress.ProgressColor;
			circularProgress.ProgressBarType = (DevComponents.DotNetBar.eCircularProgressType)((styleSettings.SyncCircleStyle ?? 2) - 1);

			var cancelLogoPath = Path.Combine(RemoteResourceManager.Instance.AppRootFolderPath, "ProgressCancel.png");
			if (File.Exists(cancelLogoPath))
				pbCancel.Image = Image.FromFile(cancelLogoPath);

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
			Trayed?.Invoke(this, EventArgs.Empty);
		}

		private void toolStripMenuItemShowProgress_Click(object sender, EventArgs e)
		{
			HideTrayIcon();
			Activated?.Invoke(this, EventArgs.Empty);
		}

		private void toolStripMenuItemKillApp_Click(object sender, EventArgs e)
		{
			Environment.Exit(1);
		}
	}
}