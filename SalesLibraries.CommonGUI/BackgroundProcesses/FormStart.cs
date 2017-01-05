using System;
using System.Drawing;
using System.Windows.Forms;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CommonGUI.BackgroundProcesses
{
	public partial class FormStart : FormProgressBase
	{
		private bool _minimized;

		public FormStart(string title)
		{
			InitializeComponent();
			if ((CreateGraphics()).DpiX > 96)
			{
				var font = new Font(styleController.Appearance.Font.FontFamily, styleController.Appearance.Font.Size - 2,
					styleController.Appearance.Font.Style);
				styleController.Appearance.Font = font;
				styleController.AppearanceDisabled.Font = font;
				styleController.AppearanceDropDown.Font = font;
				styleController.AppearanceDropDownHeader.Font = font;
				styleController.AppearanceFocused.Font = font;
				styleController.AppearanceReadOnly.Font = font;
			}

			pbCancelRegular.Buttonize();
			notifyIcon.Text = title;
			notifyIcon.BalloonTipText = title;
			toolStripMenuItemKillApp.Text = String.Format(toolStripMenuItemKillApp.Text, title);

			StartPosition = FormStartPosition.CenterScreen;
			WindowState = FormWindowState.Normal;
			notifyIcon.Visible = false;
			Opacity = 1;
			Width = 700;
			Height = 370;
			pnNormal.Visible = true;
			pnMinimized.Visible = false;

			FormClosed += FormStart_FormClosed;
		}

		public void ProcessConnectionStage()
		{
			pnProgressStages.SuspendLayout();
			pnProgressStageWebConnection.Visible = true;
			pnProgressStageWebConnection.BringToFront();
			pnProgressStages.ResumeLayout();
		}

		public void ProcessSecurityStage()
		{
			pnProgressStages.SuspendLayout();
			pnProgressStageSecurity.Visible = true;
			pnProgressStageSecurity.BringToFront();
			pnProgressStages.ResumeLayout();
		}

		public void ProcessLoadFilesStage()
		{
			pnProgressStages.SuspendLayout();
			pnProgressStageFiles.Visible = true;
			pnProgressStageFiles.BringToFront();
			pnProgressStages.ResumeLayout();
		}

		private void ShowTrayIcon()
		{
			if (!_minimized)
			{
				StartPosition = FormStartPosition.Manual;
				Width = 440;
				Height = 55;
				Left = Screen.PrimaryScreen.WorkingArea.Width - Width - 20;
				Top = Screen.PrimaryScreen.WorkingArea.Height - Height - 20;
				pnNormal.Visible = false;
				pnMinimized.Visible = true;
				_minimized = true;
			}
			else
			{
				Opacity = 0;
				notifyIcon.Visible = true;
				notifyIcon.ShowBalloonTip(1);
			}
			Application.DoEvents();
		}

		private void HideTrayIcon()
		{
			notifyIcon.Visible = false;
			Opacity = 1;
			Application.DoEvents();
		}

		private void FormProgress_Shown(object sender, EventArgs e)
		{
			circularProgressRegular.IsRunning = true;
			circularProgressMinimized.IsRunning = true;
		}

		private void FormStart_FormClosed(object sender, FormClosedEventArgs e)
		{
			Invoke(new MethodInvoker(HideTrayIcon));
		}

		private void pbCancel_Click(object sender, EventArgs e)
		{
			Invoke(new MethodInvoker(ShowTrayIcon));
		}

		private void toolStripMenuItemShowProgress_Click(object sender, EventArgs e)
		{
			Invoke(new MethodInvoker(HideTrayIcon));
		}

		private void toolStripMenuItemKillApp_Click(object sender, EventArgs e)
		{
			Environment.Exit(1);
		}
	}
}