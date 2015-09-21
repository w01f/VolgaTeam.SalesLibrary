using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager.BusinessClasses
{
	public class BannerList
	{
		public string BannerFolder { get; set; }
		public string BannerAdditionalFolder { get; set; }
		public string BannerFavsFolder { get; set; }

		public List<LinkImageGroup> Items { get; private set; }

		public BannerList()
		{
			Items = new List<LinkImageGroup>();
		}

		public void Load()
		{
			var artworkPath = AppModeManager.Instance.AppMode == AppModeEnum.Local ?
				String.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)) :
				String.Format(@"{0}\!Artwork", SettingsManager.Instance.CloudResorcesPath);

			BannerFolder = String.Format(@"{0}\Banners", artworkPath);
			BannerAdditionalFolder = String.Format(@"{0}\Banners_2", artworkPath);
			BannerFavsFolder = AppModeManager.Instance.AppMode == AppModeEnum.Local ?
				String.Format(@"{0}\file_manager\Favorite_Banners", SettingsManager.Instance.SettingsRootPath) :
				String.Format(@"{0}\Favorite_Banners", SettingsManager.Instance.ApplicationLocalDataPath);

			Items.Clear();
			var imageGroup = new LinkImageGroup();
			imageGroup.Name = "Gallery";
			imageGroup.Order = -2;
			if (Directory.Exists(BannerFolder))
				imageGroup.LoadImages<Banner>(BannerFolder);
			Items.Add(imageGroup);
			imageGroup = new FavoriteImageGroup();
			imageGroup.Name = "My Favorites";
			imageGroup.Order = -1;
			if (Directory.Exists(BannerFavsFolder))
				imageGroup.LoadImages<Banner>(BannerFavsFolder);
			Items.Add(imageGroup);
			if (Directory.Exists(BannerAdditionalFolder))
			{
				var contentDescriptionPath = Path.Combine(BannerAdditionalFolder, "order.txt");
				if (File.Exists(contentDescriptionPath))
				{
					var groupNames = File.ReadAllLines(contentDescriptionPath);
					var groupIndex = 0;
					foreach (var groupName in groupNames)
					{
						var groupFolderPath = Path.Combine(BannerAdditionalFolder, groupName);
						if (!Directory.Exists(groupFolderPath)) continue;
						imageGroup = new LinkImageGroup();
						imageGroup.Name = groupName;
						imageGroup.Order = groupIndex;
						imageGroup.LoadImages<Banner>(groupFolderPath);
						Items.Add(imageGroup);
						groupIndex++;
					}
				}
			}
			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}

	public class Banner : LinkImageSource
	{
		public Banner(string filePath) : base(filePath) { }

		public override void CopyToFavs()
		{
			var favsFolder = ListManager.Instance.Banners.BannerFavsFolder;

			if (!File.Exists(_filePath)) return;
			if (!Directory.Exists(favsFolder))
				Directory.CreateDirectory(favsFolder);
			try
			{
				File.Copy(_filePath, Path.Combine(favsFolder, Path.GetFileName(_filePath)), true);
			}
			catch { }
			var favoritesImagesGroup = ListManager.Instance.Banners.Items.FirstOrDefault(g => g.Order == -1);
			if (favoritesImagesGroup == null) return;
			favoritesImagesGroup.LoadImages<Banner>(favsFolder);
		}
	}
}
