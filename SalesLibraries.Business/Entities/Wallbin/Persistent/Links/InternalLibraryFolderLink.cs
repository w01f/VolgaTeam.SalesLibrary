using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class InternalLibraryFolderLink : InternalLink
	{
		#region Nonpersistent Properties
		private InternalLibraryFolderLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<InternalLibraryFolderLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as InternalLibraryFolderLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.InternalLibraryFolder;

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				if (!((LibraryObjectLinkSettings) Settings).ShowOnlyCustomHoverNote)
				{
					lines.Add(String.Format("Target Library: {0}", TargetLibrary));
					lines.Add(String.Format("Target Page: {0}", TargetPage));
					lines.Add(String.Format("Target Window: {0}", TargetFolder));
					lines.Add(base.Hint);
				}
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string TargetLibrary => ((InternalLibraryFolderLinkSettings)Settings).LibraryName;

		[NotMapped, JsonIgnore]
		public string TargetPage => ((InternalLibraryFolderLinkSettings)Settings).PageName;

		[NotMapped, JsonIgnore]
		public string TargetFolder => ((InternalLibraryFolderLinkSettings)Settings).WindowName;
		#endregion
		
		public static InternalLibraryFolderLink Create(InternalLibraryFolderLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = CreateEntity<InternalLibraryFolderLink>();
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((InternalLibraryFolderLinkSettings)link.Settings).LibraryName = linkInfo.LibraryName;
			((InternalLibraryFolderLinkSettings)link.Settings).PageName = linkInfo.PageName;
			((InternalLibraryFolderLinkSettings)link.Settings).WindowName = linkInfo.WindowName;
			((InternalLibraryFolderLinkSettings)link.Settings).ShowHeaderText = linkInfo.ShowHeaderText;
			((InternalLibraryFolderLinkSettings)link.Settings).OpenOnSamePage = linkInfo.OpenOnSamePage;
			((InternalLibraryFolderLinkSettings)link.Settings).StyleSettings = linkInfo.StyleSettings;
			if (linkInfo.FormatAsBluelink)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Underline;
				link.Settings.ForeColor = Color.Blue;
			}
			if (linkInfo.FormatBold)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = ((LibraryObjectLinkSettings)link.Settings).RegularFontStyle | FontStyle.Bold;
			}
			link.AfterCreate();
			return link;
		}
	}
}
