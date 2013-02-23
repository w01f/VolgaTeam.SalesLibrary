using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.ConfigurationClasses
{
	internal class ListManager
	{
		private static readonly ListManager _instance = new ListManager();
		private ListManager() { }

		public string ListsFolder { get; set; }
		public string WidgetFolder { get; set; }
		public string BannerFolder { get; set; }
		public string WidgetFavsFolder { get; set; }
		public string BannerFavsFolder { get; set; }

		public SearchTags SearchTags { get; set; }
		public List<Widget> Widgets { get; private set; }
		public List<Banner> Banners { get; private set; }
		public List<Widget> WidgetsFavs { get; private set; }
		public List<Banner> BannersFavs { get; private set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public void Init()
		{
			ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WidgetFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\Widgets", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			BannerFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\!Artwork\Banners", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WidgetFavsFolder = string.Format(@"{0}\newlocaldirect.com\xml\file_manager\Favorite_Widgets", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			BannerFavsFolder = string.Format(@"{0}\newlocaldirect.com\xml\file_manager\Favorite_Banners", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			SearchTags = new SearchTags();
			Widgets = new List<Widget>();
			Banners = new List<Banner>();
			WidgetsFavs = new List<Widget>();
			BannersFavs = new List<Banner>();
			LoadWidgets();
			LoadWidgetsFavs();
			LoadBanners();
			LoadBannersFavs();
		}

		private void LoadWidgets()
		{
			Widgets.Clear();
			if (Directory.Exists(WidgetFolder))
			{
				string[] widgetFiles = Directory.GetFiles(WidgetFolder, "*.png");
				foreach (string widgetFile in widgetFiles)
				{
					var widget = new Widget(widgetFile);
					if (widget.Index > 0 && widget.Image != null)
						Widgets.Add(widget);
				}
				Widgets.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		private void LoadBanners()
		{
			Banners.Clear();
			if (Directory.Exists(BannerFolder))
			{
				string[] bannerFiles = Directory.GetFiles(BannerFolder, "*.png");
				foreach (string bannerFile in bannerFiles)
				{
					var banner = new Banner(bannerFile);
					if (banner.Index > 0 && banner.Image != null)
						Banners.Add(banner);
				}
				Banners.Sort((x, y) => x.Index.CompareTo(y.Index));
			}
		}

		public void LoadWidgetsFavs()
		{
			WidgetsFavs.Clear();
			if (!Directory.Exists(WidgetFavsFolder)) return;
			var widgetFiles = Directory.GetFiles(WidgetFavsFolder, "*.png");
			foreach (var widgetFile in widgetFiles)
			{
				var widget = new Widget(widgetFile);
				if (widget.Index > 0 && widget.Image != null)
					WidgetsFavs.Add(widget);
			}
			WidgetsFavs.Sort((x, y) => x.Index.CompareTo(y.Index));
		}

		public void LoadBannersFavs()
		{
			BannersFavs.Clear();
			if (!Directory.Exists(BannerFavsFolder)) return;
			var bannerFiles = Directory.GetFiles(BannerFavsFolder, "*.png");
			foreach (var bannerFile in bannerFiles)
			{
				var banner = new Banner(bannerFile);
				if (banner.Index > 0 && banner.Image != null)
					BannersFavs.Add(banner);
			}
			BannersFavs.Sort((x, y) => x.Index.CompareTo(y.Index));
		}
	}

	public class Widget
	{
		private string _filePath;
		public Widget(string widgetFile)
		{
			_filePath = widgetFile;
			int index;
			if (int.TryParse(Path.GetFileName(widgetFile).Substring(0, Path.GetFileName(widgetFile).IndexOf('.')), out index))
				Index = index;
			FileName = Path.GetFileNameWithoutExtension(widgetFile);
			Image = new Bitmap(widgetFile);
		}

		public int Index { get; set; }
		public string FileName { get; set; }
		public Image Image { get; set; }

		public void CopyToFavs()
		{
			if (!File.Exists(_filePath)) return;
			if (!Directory.Exists(ListManager.Instance.WidgetFavsFolder))
				Directory.CreateDirectory(ListManager.Instance.WidgetFavsFolder);
			File.Copy(_filePath, Path.Combine(ListManager.Instance.WidgetFavsFolder, Path.GetFileName(_filePath)), true);
			ListManager.Instance.LoadWidgetsFavs();
		}
	}

	public class Banner
	{
		private string _filePath;
		public Banner(string bannerFile)
		{
			_filePath = bannerFile;
			int index;
			if (int.TryParse(Path.GetFileName(bannerFile).Substring(0, Path.GetFileName(bannerFile).IndexOf('.')), out index))
				Index = index;
			FileName = Path.GetFileNameWithoutExtension(bannerFile);
			Image = new Bitmap(bannerFile);
		}

		public int Index { get; set; }
		public string FileName { get; set; }
		public Image Image { get; set; }

		public void CopyToFavs()
		{
			if (!File.Exists(_filePath)) return;
			if (!Directory.Exists(ListManager.Instance.BannerFavsFolder))
				Directory.CreateDirectory(ListManager.Instance.BannerFavsFolder);
			try
			{
				File.Copy(_filePath, Path.Combine(ListManager.Instance.BannerFavsFolder, Path.GetFileName(_filePath)), true);
			}
			catch {}
			ListManager.Instance.LoadBannersFavs();
		}
	}
}