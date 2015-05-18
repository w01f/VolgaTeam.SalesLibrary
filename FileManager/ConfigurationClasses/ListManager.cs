using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ConfigurationClasses
{
	public class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		private ListManager() { }

		public string ListsFolder { get; set; }
		public string WidgetFolder { get; set; }
		public string WidgetAdditionalFolder { get; set; }
		public string WidgetFavsFolder { get; set; }

		public string BannerFolder { get; set; }
		public string BannerAdditionalFolder { get; set; }
		public string BannerFavsFolder { get; set; }

		public SearchTags SearchTags { get; set; }
		public List<LinkImageGroup> Widgets { get; private set; }
		public List<LinkImageGroup> Banners { get; private set; }
		public List<SuperFilter> SuperFilters { get; private set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public void Init()
		{
			ListsFolder = String.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			BannerFolder = String.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\Banners", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			BannerAdditionalFolder = String.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\Banners_2", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			BannerFavsFolder = String.Format(@"{0}\newlocaldirect.com\xml\file_manager\Favorite_Banners", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			WidgetFolder = String.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\Widgets", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WidgetAdditionalFolder = String.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\Widgets_2", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WidgetFavsFolder = String.Format(@"{0}\newlocaldirect.com\xml\file_manager\Favorite_Widgets", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			SearchTags = new SearchTags();
			Widgets = new List<LinkImageGroup>();
			Banners = new List<LinkImageGroup>();
			SuperFilters = new List<SuperFilter>();
			SuperFilters.AddRange(SuperFilter.LoadSuperFilters());
			LoadWidgets();
			LoadBanners();
		}

		private void LoadWidgets()
		{
			Widgets.Clear();
			var imageGroup = new LinkImageGroup();
			imageGroup.Name = "Gallery";
			imageGroup.Order = -2;
			if (Directory.Exists(WidgetFolder))
				imageGroup.LoadImages<Widget>(WidgetFolder);
			Widgets.Add(imageGroup);
			imageGroup = new FavoriteImageGroup();
			imageGroup.Name = "My Favorites";
			imageGroup.Order = -1;
			if (Directory.Exists(WidgetFavsFolder))
				imageGroup.LoadImages<Widget>(WidgetFavsFolder);
			Widgets.Add(imageGroup);
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
						Widgets.Add(imageGroup);
						groupIndex++;
					}
				}
			}
			Widgets.Sort((x, y) => x.Order.CompareTo(y.Order));
		}

		private void LoadBanners()
		{
			Banners.Clear();
			var imageGroup = new LinkImageGroup();
			imageGroup.Name = "Gallery";
			imageGroup.Order = -2;
			if (Directory.Exists(BannerFolder))
				imageGroup.LoadImages<Banner>(BannerFolder);
			Banners.Add(imageGroup);
			imageGroup = new FavoriteImageGroup();
			imageGroup.Name = "My Favorites";
			imageGroup.Order = -1;
			if (Directory.Exists(BannerFavsFolder))
				imageGroup.LoadImages<Banner>(BannerFavsFolder);
			Banners.Add(imageGroup);
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
						Banners.Add(imageGroup);
						groupIndex++;
					}
				}
			}
			Banners.Sort((x, y) => x.Order.CompareTo(y.Order));
		}
	}

	public class LinkImageGroup
	{
		public string Name { get; set; }
		public int Order { get; set; }

		public List<LinkImageSource> Images { get; private set; }

		public EventHandler<EventArgs> OnDataChanged;

		public LinkImageGroup()
		{
			Images = new List<LinkImageSource>();
		}

		public void LoadImages<T>(string sourcePath) where T : LinkImageSource
		{
			Images.Clear();
			foreach (var filePath in Directory.GetFiles(sourcePath, "*.png"))
			{
				var linkImageSource = Activator.CreateInstance(typeof(T), filePath) as LinkImageSource;
				if (linkImageSource.Index > 0 && linkImageSource.Image != null)
					Images.Add(linkImageSource);
			}
			Images.Sort((x, y) => x.Index.CompareTo(y.Index));
			if (OnDataChanged != null)
				OnDataChanged(this, EventArgs.Empty);
		}
	}

	public class FavoriteImageGroup : LinkImageGroup { }

	public abstract class LinkImageSource
	{
		protected string _filePath;

		public int Index { get; set; }
		public string FileName { get; set; }
		public Image Image { get; set; }

		public LinkImageSource(string filePath)
		{
			_filePath = filePath;
			int index;
			if (int.TryParse(Path.GetFileName(filePath).Substring(0, Path.GetFileName(filePath).IndexOf('.')), out index))
				Index = index;
			FileName = Path.GetFileNameWithoutExtension(filePath);
			Image = new Bitmap(filePath);
		}

		public abstract void CopyToFavs();
	}

	public class Widget : LinkImageSource
	{
		public Widget(string filePath) : base(filePath) { }

		public override void CopyToFavs()
		{
			if (!File.Exists(_filePath)) return;
			if (!Directory.Exists(ListManager.Instance.WidgetFavsFolder))
				Directory.CreateDirectory(ListManager.Instance.WidgetFavsFolder);
			File.Copy(_filePath, Path.Combine(ListManager.Instance.WidgetFavsFolder, Path.GetFileName(_filePath)), true);
			var favoritesImagesGroup = ListManager.Instance.Widgets.FirstOrDefault(g => g.Order == -1);
			if (favoritesImagesGroup == null) return;
			favoritesImagesGroup.LoadImages<Widget>(ListManager.Instance.WidgetFavsFolder);

		}
	}

	public class Banner : LinkImageSource
	{
		public Banner(string filePath) : base(filePath) { }

		public override void CopyToFavs()
		{
			if (!File.Exists(_filePath)) return;
			if (!Directory.Exists(ListManager.Instance.BannerFavsFolder))
				Directory.CreateDirectory(ListManager.Instance.BannerFavsFolder);
			try
			{
				File.Copy(_filePath, Path.Combine(ListManager.Instance.BannerFavsFolder, Path.GetFileName(_filePath)), true);
			}
			catch { }
			var favoritesImagesGroup = ListManager.Instance.Banners.FirstOrDefault(g => g.Order == -1);
			if (favoritesImagesGroup == null) return;
			favoritesImagesGroup.LoadImages<Banner>(ListManager.Instance.BannerFavsFolder);
		}
	}
}