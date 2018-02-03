using System.IO;
using SalesLibraries.Common.Configuration;
using SalesLibraries.CommonGUI.BackgroundProcesses;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.PresentationLayer.Sync
{
	class FormProgressSync : FormProgressWithAbort
	{
		protected override void InitForm()
		{
			Title = "Uploading Library...";

			var styleSettings = new SyncFormStyleConfiguration();
			styleSettings.Load(Path.Combine(GlobalSettings.ApplicationRootPath, "sync_color.xml"), "SyncClose");
			BackColor = panelEx.Style.BorderColor.Color = panelExCancel.Style.BorderColor.Color = styleSettings.SyncBorderColor ?? BackColor;
			panelEx.Style.BackColor1.Color = panelEx.Style.BackColor2.Color = panelExCancel.Style.BackColor1.Color = panelExCancel.Style.BackColor2.Color = styleSettings.SyncBackColor ?? panelEx.Style.BackColor1.Color;
			laTitle.ForeColor = laTime.ForeColor = styleSettings.SyncTextColor ?? laTitle.ForeColor;
			circularProgress.ProgressColor = styleSettings.SyncCircleColor ?? circularProgress.ProgressColor;
			circularProgress.ProgressBarType = (DevComponents.DotNetBar.eCircularProgressType)((styleSettings.SyncCircleStyle ?? 2) - 1);
			circularProgress.AnimationSpeed = styleSettings.SyncCircleSpeed ?? 150;

			pbCancel.Image = MainController.Instance.ImageResources.AppSplashCancelImage ?? pbCancel.Image;
		}
	}
}