using System.Drawing;
using System.IO;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.FileManager.Configuration
{
	class ImageResourcesManager
	{
		public Icon AppIcon { get; private set; }
		public Image AppRibbonLogo { get; private set; }

		public Image AppSplashLogo { get; private set; }
		public Image AppSplashStageWebSiteImage { get; private set; }
		public Image AppSplashStageSecurityImage { get; private set; }
		public Image AppSplashStageFilesImage { get; private set; }
		public Image AppSplashBrandImage { get; private set; }
		public Image AppSplashCancelImage { get; private set; }

		public Image BrowserSpalsh { get; private set; }
		public Image BrowserNavigationBack { get; private set; }
		public Image BrowserNavigationForward { get; private set; }
		public Image BrowserNavigationRefresh { get; private set; }
		public Image BrowserExternalChrome { get; private set; }
		public Image BrowserExternalFirefox { get; private set; }
		public Image BrowserExternalIE { get; private set; }
		public Image BrowserExternalEdge { get; private set; }
		public Image BrowserPowerPointAddSlide { get; private set; }
		public Image BrowserPowerPointAddSlides { get; private set; }
		public Image BrowserPowerPointPrint { get; private set; }
		public Image BrowserVideoAdd { get; private set; }
		public Image BrowserYoutubeAdd { get; private set; }
		public Image BrowserUrlCopy { get; private set; }
		public Image BrowserUrlEmail { get; private set; }

		public void LoadRemote()
		{
			var resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "dynamic_icon.ico");
			if (File.Exists(resourceFile))
				AppIcon = new Icon(resourceFile);

			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "dynamic_header.png");
			if (File.Exists(resourceFile))
				AppRibbonLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_splash.png");
			if (File.Exists(resourceFile))
				BrowserSpalsh = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_left.png");
			if (File.Exists(resourceFile))
				BrowserNavigationBack = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_right.png");
			if (File.Exists(resourceFile))
				BrowserNavigationForward = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_refresh.png");
			if (File.Exists(resourceFile))
				BrowserNavigationRefresh = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_chrome.png");
			if (File.Exists(resourceFile))
				BrowserExternalChrome = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_firefox.png");
			if (File.Exists(resourceFile))
				BrowserExternalFirefox = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_ie.png");
			if (File.Exists(resourceFile))
				BrowserExternalIE = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_edge.png");
			if (File.Exists(resourceFile))
				BrowserExternalEdge = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_add_slide.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointAddSlide = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_add_all.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointAddSlides = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_printer.png");
			if (File.Exists(resourceFile))
				BrowserPowerPointPrint = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_video.png");
			if (File.Exists(resourceFile))
				BrowserVideoAdd = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_youtube.png");
			if (File.Exists(resourceFile))
				BrowserYoutubeAdd = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_copy.png");
			if (File.Exists(resourceFile))
				BrowserUrlCopy = Image.FromFile(resourceFile);
			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "eo_email.png");
			if (File.Exists(resourceFile))
				BrowserUrlEmail = Image.FromFile(resourceFile);
		}

		public void LoadLocal()
		{
			var resourceFile = Path.Combine(GlobalSettings. ApplicationRootPath, "dynamic_splash.png");
			if (File.Exists(resourceFile))
				AppSplashLogo = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(GlobalSettings.ApplicationRootPath, "progress_web.png");
			if (File.Exists(resourceFile))
				AppSplashStageWebSiteImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(GlobalSettings.ApplicationRootPath, "progress_security.png");
			if (File.Exists(resourceFile))
				AppSplashStageSecurityImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(GlobalSettings.ApplicationRootPath, "progress_files.png");
			if (File.Exists(resourceFile))
				AppSplashStageFilesImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(GlobalSettings.ApplicationRootPath, "splash_tag.png");
			if (File.Exists(resourceFile))
				AppSplashBrandImage = Image.FromFile(resourceFile);

			resourceFile = Path.Combine(GlobalSettings.ApplicationRootPath, "ProgressCancel.png");
			if (File.Exists(resourceFile))
				AppSplashCancelImage = Image.FromFile(resourceFile);
		}
	}
}
