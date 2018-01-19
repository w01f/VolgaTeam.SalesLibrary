using System.Drawing;
using System.IO;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.BackgroundProcesses;

namespace SalesLibraries.FileManager.PresentationLayer.Sync
{
	class FormProgressSync : FormProgressWithAbort
	{
		protected override void InitForm()
		{
			Title = "Uploading Library...";

			var styleSettings = new SyncFormStyleConfiguration();
			styleSettings.Load(Path.Combine(RemoteResourceManager.Instance.AppRootFolderPath, "sync_color.xml"), "SyncClose");
			BackColor = panelEx.Style.BorderColor.Color = panelExCancel.Style.BorderColor.Color = styleSettings.SyncBorderColor ?? BackColor;
			panelEx.Style.BackColor1.Color = panelEx.Style.BackColor2.Color = panelExCancel.Style.BackColor1.Color = panelExCancel.Style.BackColor2.Color = styleSettings.SyncBackColor ?? panelEx.Style.BackColor1.Color;
			laTitle.ForeColor = laTime.ForeColor = styleSettings.SyncTextColor ?? laTitle.ForeColor;
			circularProgress.ProgressColor = styleSettings.SyncCircleColor ?? circularProgress.ProgressColor;

			var cancelLogoPath = Path.Combine(RemoteResourceManager.Instance.AppRootFolderPath, "ProgressCancel.png");
			if (File.Exists(cancelLogoPath))
				pbCancel.Image = Image.FromFile(cancelLogoPath);
		}
	}
}