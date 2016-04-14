using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class AppLink : LibraryObjectLink
	{
		#region Nonpersistent Properties
		private AppLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<AppLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as AppLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string FullPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string[] OpenPaths => new[] { FullPath, SecondPath };

		[NotMapped, JsonIgnore]
		public override string WebPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.AppLink;

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				lines.Add(String.Format("Path 1: {0}", FullPath));
				lines.Add(String.Format("Path 2: {0}", SecondPath));
				lines.Add(base.Hint);
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string SecondPath => ((AppLinkSettings)Settings).SecondPath;

		#endregion

		public AppLink()
		{
			Type = FileTypes.AppLink;
		}

		public static AppLink Create(AppLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((AppLinkSettings)link.Settings).SecondPath = linkInfo.SecondPath;
			if (linkInfo.FormatAsBluelink)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Underline;
				link.Settings.ForeColor = Color.Blue;
			}
			if (linkInfo.FormatBold)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Bold;
			}
			return link;
		}

		public static AppLink Create(string path)
		{
			return new AppLink { RelativePath = path };
		}
	}
}
