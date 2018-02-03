using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager
{
	public partial class FormStart : FormProgressBase
	{
		private bool _minimized;

		public FormStart(string title)
		{
			InitializeComponent();

			pbCancelRegular.Image = MainController.Instance.ImageResources.AppSplashCancelImage ?? pbCancelRegular.Image;
			pbCancelRegular.Size = new Size(32, 32);
			pbCancelRegular.Location = new Point(Right - 32 - 2, Top + 2);
			pbCancelRegular.Buttonize();

			StartPosition = FormStartPosition.CenterScreen;
			WindowState = FormWindowState.Normal;
			notifyIcon.Visible = false;
			Opacity = 1;
			Width = (Int32)(700 * Utils.GetScaleFactor(CreateGraphics().DpiX).Width);
			Height = pnNormal.Height = (Int32)(392 * Utils.GetScaleFactor(CreateGraphics().DpiX).Height);
			pnNormal.Visible = true;
			pnMinimized.Visible = false;

			var styleSettings = new SyncFormStyleConfiguration();
			styleSettings.Load(Path.Combine(GlobalSettings.ApplicationRootPath, "sync_color.xml"), "SyncStart");
			BackColor = styleSettings.SyncBorderColor ?? BackColor;
			panelEx.Style.BackColor1.Color = panelEx.Style.BackColor2.Color = styleSettings.SyncBackColor ?? panelEx.Style.BackColor1.Color;
			labelControlDownloadInfo.ForeColor = styleSettings.SyncTextColor ?? labelControlDownloadInfo.ForeColor;
			circularProgressRegular.ProgressColor = circularProgressMinimized.ProgressColor = styleSettings.SyncCircleColor ?? circularProgressRegular.ProgressColor;
			circularProgressRegular.ProgressBarType = circularProgressMinimized.ProgressBarType = (DevComponents.DotNetBar.eCircularProgressType)((styleSettings.SyncCircleStyle ?? 2) - 1);
			circularProgressRegular.AnimationSpeed = circularProgressMinimized.AnimationSpeed = styleSettings.SyncCircleSpeed ?? 150;

			var regularHeaderImage = MainController.Instance.ImageResources.AppSplashLogo;
			if (regularHeaderImage != null)
				pictureEditHeaderRegular.Image = pictureEditHeaderRegular.Height < regularHeaderImage.Height
					? regularHeaderImage.Resize(new Size(regularHeaderImage.Width, pictureEditHeaderRegular.Height))
					: regularHeaderImage;

			var webSiteStageImage = MainController.Instance.ImageResources.AppSplashStageWebSiteImage;
			if (webSiteStageImage != null)
				pbProgressStageWebSite.Image = pbProgressStageWebSite.Height < webSiteStageImage.Height
					? webSiteStageImage.Resize(new Size(webSiteStageImage.Width, pbProgressStageWebSite.Height))
					: webSiteStageImage;

			var securityStageImage = MainController.Instance.ImageResources.AppSplashStageSecurityImage;
			if (securityStageImage != null)
				pbProgressStageSecurity.Image = pbProgressStageSecurity.Height < securityStageImage.Height
					? securityStageImage.Resize(new Size(securityStageImage.Width, pbProgressStageSecurity.Height))
					: securityStageImage;

			var filesStageImage = MainController.Instance.ImageResources.AppSplashStageFilesImage;
			if (filesStageImage != null)
				pbProgressStageFiles.Image = pbProgressStageFiles.Height < filesStageImage.Height
					? filesStageImage.Resize(new Size(filesStageImage.Width, pbProgressStageFiles.Height))
					: filesStageImage;

			pictureEditBrand.Image = MainController.Instance.ImageResources.AppSplashBrandImage ?? pictureEditBrand.Image;

			notifyIcon.Text = title;
			notifyIcon.BalloonTipText = title;
			toolStripMenuItemKillApp.Text = String.Format(toolStripMenuItemKillApp.Text, title);

			FormClosed += FormStart_FormClosed;
		}

		public void ProcessConnectionStage()
		{
			pbProgressStageWebSite.Visible = true;
		}

		public void ProcessSecurityStage()
		{
			pbProgressStageSecurity.Visible = true;
		}

		public void ProcessLoadFilesStage()
		{
			pbProgressStageFiles.Visible = true;
		}

		public void SetDownloadInfo(string info)
		{
			Invoke(new MethodInvoker(() =>
			{
				labelControlDownloadInfo.Text = String.Format("<size=+1><color=white><i>{0}</i></color></size>", info);
				Application.DoEvents();
			}));
		}

		private void ShowTrayIcon()
		{
			if (!_minimized)
			{
				StartPosition = FormStartPosition.Manual;
				Width = Math.Max(440, (Int32)(440 * Utils.GetScaleFactor(CreateGraphics().DpiX).Width * 0.75));
				Height = pnMinimized.Height = 57;
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