using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileManager.BusinessClasses
{
	public class WidgetList
	{
		public string WidgetFolder { get; set; }
		public string WidgetAdditionalFolder { get; set; }
		public string WidgetFavsFolder { get; set; }

		public List<LinkImageGroup> Items { get; private set; }

		public WidgetList()
		{
			Items = new List<LinkImageGroup>();
		}

		public void Load()
		{
			var artworkPath = AppModeManager.Instance.AppMode == AppModeEnum.Local ?
				String.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles)) :
				String.Format(@"{0}\!Artwork", SettingsManager.Instance.CloudResorcesPath);

			WidgetFolder = String.Format(@"{0}\Widgets", artworkPath);
			WidgetAdditionalFolder = String.Format(@"{0}\Widgets_2", artworkPath);
			WidgetFavsFolder = AppModeManager.Instance.AppMode == AppModeEnum.Local ?
				String.Format(@"{0}\file_manager\Favorite_Widgets", SettingsManager.Instance.SettingsRootPath) :
				String.Format(@"{0}\Favorite_Widgets", SettingsManager.Instance.ApplicationLocalDataPath);

			Items.Clear();
			var imageGroup = new LinkImageGroup();
			imageGroup.Name = "Gallery";
			imageGroup.Order = -2;
			if (Directory.Exists(WidgetFolder))
				imageGroup.LoadImages<Widget>(WidgetFolder);
			Items.Add(imageGroup);
			imageGroup = new FavoriteImageGroup();
			imageGroup.Name = "My Favorites";
			imageGroup.Order = -1;
			if (Directory.Exists(WidgetFavsFolder))
				imageGroup.LoadImages<Widget>(WidgetFavsFolder);
			Items.Add(imageGroup);
			if (Directory.Exists(WidgetAdditionalFolder))
			{
				var contentDescriptionPath = Path.Combine(WidgetAdditionalFolder, "order.txt");
				if (File.Exists(contentDescriptionPath))
				{
					var groupNames = File.ReadAllLines(contentDescriptionPath);
					var groupIndex = 0;
					foreach (var groupName in groupNames)
					{
						var groupFolderPath = Path.Combine(WidgetAdditionalFolder, groupName);
						if (!Directory.Exists(groupFolderPath)) continue;
						imageGroup = new LinkImageGroup();
						imageGroup.Name = groupName;
						imageGroup.Order = groupIndex;
						imageGroup.LoadImages<Widget>(groupFolderPath);
						Items.Add(imageGroup);
						groupIndex++;
					}
				}
			}
			Items.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}

	public class Widget : LinkImageSource
	{
		public Widget(string filePath) : base(filePath) { }

		public override void CopyToFavs()
		{
			var favsFolder = ListManager.Instance.Widgets.WidgetFavsFolder;
			if (!File.Exists(_filePath)) return;
			if (!Directory.Exists(favsFolder))
				Directory.CreateDirectory(favsFolder);
			File.Copy(_filePath, Path.Combine(favsFolder, Path.GetFileName(_filePath)), true);
			var favoritesImagesGroup = ListManager.Instance.Widgets.Items.FirstOrDefault(g => g.Order == -1);
			if (favoritesImagesGroup == null) return;
			favoritesImagesGroup.LoadImages<Widget>(favsFolder);
		}
	}

}
