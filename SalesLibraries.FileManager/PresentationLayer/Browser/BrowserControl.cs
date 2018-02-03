using System;
using System.Windows.Controls;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using SalesLibraries.Browser.Controls.Controls;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.CommonGUI.Common;
using SalesLibraries.CommonGUI.Floater;
using SalesLibraries.FileManager.Controllers;
using Image = System.Drawing.Image;

namespace SalesLibraries.FileManager.PresentationLayer.Browser
{
	class BrowserControl : SiteBundleControl
	{
		private readonly BrowserPage _parentTapBage;

		public override PowerPointSingleton PowerPointSingleton => PowerPointSingleton.Instance;
		public override PopupMessageHelper PopupMessageHelper => MainController.Instance.PopupMessages;
		public override BackgroundProcessManager ProcessManager => MainController.Instance.ProcessManager;
		public override Form MainForm => MainController.Instance.MainForm;
		public override Image SplashLogo => MainController.Instance.ImageResources.BrowserSpalsh ?? base.SplashLogo;

		public BrowserControl(BrowserPage parentTapBage)
		{
			Dock = DockStyle.Fill;

			_parentTapBage = parentTapBage;

			ButtonNavigationBack.Image = MainController.Instance.ImageResources.BrowserNavigationBack ??
										 ButtonNavigationBack.Image;
			ButtonNavigationForward.Image = MainController.Instance.ImageResources.BrowserNavigationForward ??
											ButtonNavigationForward.Image;
			ButtonNavigationRefresh.Image = MainController.Instance.ImageResources.BrowserNavigationRefresh ??
											ButtonNavigationRefresh.Image;
			ButtonExtensionsAddSlide.Image = MainController.Instance.ImageResources.BrowserPowerPointAddSlide ??
											 ButtonExtensionsAddSlide.Image;
			ButtonExtensionsAddSlides.Image = MainController.Instance.ImageResources.BrowserPowerPointAddSlides ??
											  ButtonExtensionsAddSlides.Image;
			ButtonExtensionsPrint.Image = MainController.Instance.ImageResources.BrowserPowerPointPrint ??
										  ButtonExtensionsPrint.Image;
			ButtonExtensionsAddVideo.Image = MainController.Instance.ImageResources.BrowserVideoAdd ??
											 ButtonExtensionsAddVideo.Image;
			ButtonExtensionsDownloadYouTube.Image = MainController.Instance.ImageResources.BrowserYoutubeAdd ??
													ButtonExtensionsDownloadYouTube.Image;
		}

		public override void ShowFloater(FloaterRequestedEventArgs args)
		{
			FloaterManager.Instance.ShowFloater(
				MainForm,
				MainForm.Text,
				args.Logo ?? MainController.Instance.ImageResources.AppRibbonLogo,
				args.AfterShow);
		}

		public override Boolean CheckPowerPointRunning(Action afterRun)
		{
			if (PowerPointSingleton.Instance.Connect())
				return true;
			if (MainController.Instance.PopupMessages.ShowWarningQuestion(String.Format("PowerPoint is required to run this application.{0}Do you want to go ahead and open PowerPoint?", Environment.NewLine)) == DialogResult.Yes)
				FloaterManager.Instance.ShowFloater(
					MainForm,
					MainForm.Text,
					MainController.Instance.ImageResources.AppRibbonLogo,
					PowerPointManager.Instance.RunPowerPointLoader);
			return false;
		}

		public override void UpdateMainStatusBarInfo()
		{
			MainController.Instance.MainForm.MainInfoContainer.SubItems.Clear();

			var titleLabel = new LabelItem();
			titleLabel.Text = _parentTapBage.BrowserSettings.StatusBarTitle;
			if (MainController.Instance.Settings.MainFormStyle.StatusBarTextColor.HasValue)
				titleLabel.ForeColor = MainController.Instance.Settings.MainFormStyle.StatusBarTextColor.Value;
			MainController.Instance.MainForm.MainInfoContainer.SubItems.Add(titleLabel);

			if (SelectedSite != null)
			{
				var urlLabel = new LabelItem();
				urlLabel.Text = SelectedSite?.CurrentUrl;
				if (MainController.Instance.Settings.MainFormStyle.StatusBarTextColor.HasValue)
					urlLabel.ForeColor = MainController.Instance.Settings.MainFormStyle.StatusBarTextColor.Value;
				MainController.Instance.MainForm.MainInfoContainer.SubItems.Add(urlLabel);
			}

			MainController.Instance.MainForm.MainInfoContainer.RecalcSize();
			MainController.Instance.MainForm.StatusBar.RecalcLayout();
		}
	}
}
