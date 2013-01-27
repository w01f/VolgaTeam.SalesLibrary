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
		private ListManager() {}

		public string ListsFolder { get; set; }
		public string WidgetFolder { get; set; }
		public string BannerFolder { get; set; }

		public SearchTags SearchTags { get; set; }
		public List<Widget> Widgets { get; private set; }
		public List<Banner> Banners { get; private set; }

		public static ListManager Instance
		{
			get { return _instance; }
		}

		public void Init()
		{
			ListsFolder = string.Format(@"{0}\newlocaldirect.com\sync\Incoming\Slides\Data\SDSearch XML", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			WidgetFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Widgets", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));
			BannerFolder = string.Format(@"{0}\newlocaldirect.com\Sales Depot\Banners", Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

			SearchTags = new SearchTags();
			Widgets = new List<Widget>();
			Banners = new List<Banner>();
			LoadWidgets();
			LoadBanners();
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
	}

	public class Widget
	{
		public Widget(string widgetFile)
		{
			int index = 0;
			int.TryParse(Path.GetFileName(widgetFile).Substring(0, Path.GetFileName(widgetFile).IndexOf('.')), out index);
			Index = index;
			FileName = Path.GetFileNameWithoutExtension(widgetFile);
			Image = new Bitmap(widgetFile);
		}

		public int Index { get; set; }
		public string FileName { get; set; }
		public Image Image { get; set; }
	}

	public class Banner
	{
		public Banner(string bannerFile)
		{
			int index = 0;
			int.TryParse(Path.GetFileName(bannerFile).Substring(0, Path.GetFileName(bannerFile).IndexOf('.')), out index);
			Index = index;
			FileName = Path.GetFileNameWithoutExtension(bannerFile);
			Image = new Bitmap(bannerFile);
		}

		public int Index { get; set; }
		public string FileName { get; set; }
		public Image Image { get; set; }
	}
}