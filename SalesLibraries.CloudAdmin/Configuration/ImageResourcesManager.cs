using System.Drawing;
using System.IO;
using SalesLibraries.Common.Configuration;

namespace SalesLibraries.CloudAdmin.Configuration
{
	class ImageResourcesManager
	{
		public Icon AppIcon { get; private set; }
		public Image AppRibbonLogo { get; private set; }
		public Image AppSplashLogo { get; private set; }

		public void LoadRemote()
		{
			var resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "dynamic_icon.ico");
			if (File.Exists(resourceFile))
				AppIcon = new Icon(resourceFile);

			resourceFile = Path.Combine(RemoteResourceManager.Instance.ImageResourcesFolder.LocalPath, "dynamic_header.png");
			if (File.Exists(resourceFile))
				AppRibbonLogo = Image.FromFile(resourceFile);
		}

		public void LoadLocal()
		{
			var resourceFile = Path.Combine(GlobalSettings.ApplicationRootPath, "dynamic_splash.png");
			if (File.Exists(resourceFile))
				AppSplashLogo = Image.FromFile(resourceFile);
		}
	}
}
