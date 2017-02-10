using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.SalesDepot.Business.Themes
{
	public class Theme
	{
		private readonly StorageDirectory _root;
		private StorageFile _themeFile;

		public string Name { get; private set; }
		public int Order { get; private set; }
		public Image Logo { get; private set; }
		public Image BrowseLogo { get; private set; }
		public Image RibbonLogo { get; private set; }
		public List<SlideType> ApprovedSlides { get; private set; }

		public Theme(StorageDirectory root)
		{
			_root = root;
		}

		public override string ToString()
		{
			return Name;
		}

		public void Load()
		{
			var files = _root.GetLocalFiles().ToList();

			var titleFile = files.First(file => file.Name == "title.txt");
			Name = File.ReadAllText(titleFile.LocalPath).Trim();

			int tempInt;
			if (Int32.TryParse(Path.GetFileName(_root.LocalPath), out tempInt))
				Order = tempInt;

			var bigLogoFile = files.FirstOrDefault(file => file.Name == "preview.png");
			if (bigLogoFile != null)
			{
				Logo = new Bitmap(bigLogoFile.LocalPath);
				BrowseLogo = Logo.GetThumbnailImage(((Logo.Width * 144) / Logo.Height) + 10, 144, null, IntPtr.Zero);
				var borderedLogo = Logo.DrawBorder(2, Color.DimGray);
				RibbonLogo = borderedLogo.GetThumbnailImage(((borderedLogo.Width * 72) / borderedLogo.Height) + 10, 72, null, IntPtr.Zero);
			}

			_themeFile = files.FirstOrDefault(file => file.Extension == ".thmx");

			ApprovedSlides = new List<SlideType>();
		}

		public string GetThemePath()
		{
			return _themeFile.LocalPath;
		}
	}
}
