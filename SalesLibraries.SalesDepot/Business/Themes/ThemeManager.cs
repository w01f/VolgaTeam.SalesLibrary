using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.RemoteStorage;

namespace SalesLibraries.SalesDepot.Business.Themes
{
	class ThemeManager
	{
		private readonly List<Theme> _themes = new List<Theme>();

		public Dictionary<SlideType, List<string>> ApprovedThemes { get; private set; }

		public event EventHandler<EventArgs> ThemesChanged;

		public ThemeManager()
		{
			ApprovedThemes = new Dictionary<SlideType, List<string>>();
		}

		private void LoadApprovedThemes(StorageDirectory root)
		{
			var contentFile = new StorageFile(root.GetParentFolder().RelativePathParts.Merge("ApprovedThemes.xml"));

			if (!contentFile.ExistsLocal()) return;

			var document = new XmlDocument();
			document.Load(contentFile.LocalPath);

			foreach (var slideNode in document.SelectNodes(@"//Root/Slide").OfType<XmlNode>())
			{
				var slideAttribute = slideNode.Attributes["Name"];
				if (slideAttribute == null) continue;
				var slideType = SlideType.None;
				switch (slideAttribute.Value)
				{
					case "SalesLibrary":
						slideType = SlideType.SalesDepot;
						break;
				}
				if (slideType == SlideType.None) continue;
				foreach (var themeNode in slideNode.SelectNodes("Theme").OfType<XmlNode>())
				{
					if (!ApprovedThemes.ContainsKey(slideType))
						ApprovedThemes.Add(slideType, new List<string>());
					ApprovedThemes[slideType].Add(themeNode.InnerText);
				}
			}
		}

		public void Load()
		{
			_themes.Clear();
			var storageDirectory = new StorageDirectory(RemoteResourceManager.Instance.ThemesFolder.RelativePathParts.Merge(PowerPointManager.Instance.SlideSettings.SlideMasterFolder));
			if (!storageDirectory.ExistsLocal()) return;

			LoadApprovedThemes(storageDirectory);

			foreach (var themeFolder in storageDirectory.GetFolders())
			{
				var theme = new Theme(themeFolder);
				theme.Load();
				foreach (var approvedTheme in ApprovedThemes.Where(approvedTheme => approvedTheme.Value.Any(t => t.Equals(theme.Name))))
					theme.ApprovedSlides.Add(approvedTheme.Key);
				_themes.Add(theme);
			}
			_themes.Sort((x, y) => x.Order.CompareTo(y.Order));

			if (ThemesChanged != null)
				ThemesChanged(this, EventArgs.Empty);
		}

		public IEnumerable<Theme> GetThemes(SlideType slideType)
		{
			return _themes.Where(t => t.ApprovedSlides.Contains(slideType) || !ApprovedThemes.ContainsKey(slideType));
		}
	}
}
