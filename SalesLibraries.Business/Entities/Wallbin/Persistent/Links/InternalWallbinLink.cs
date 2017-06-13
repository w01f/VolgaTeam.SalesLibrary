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
	public class InternalWallbinLink : InternalLink
	{
		#region Nonpersistent Properties
		private InternalWallbinLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<InternalWallbinLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as InternalWallbinLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.InternalWallbin;

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
					lines.Add(base.Hint);
				}
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string TargetLibrary => ((InternalWallbinLinkSettings)Settings).LibraryName;
		#endregion
		
		public static InternalWallbinLink Create(InternalWallbinLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = CreateEntity<InternalWallbinLink>();
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((InternalWallbinLinkSettings)link.Settings).LibraryName = linkInfo.LibraryName;
			((InternalWallbinLinkSettings)link.Settings).PageName = linkInfo.PageName;
			((InternalWallbinLinkSettings)link.Settings).ShowHeaderText = linkInfo.ShowHeaderText;
			((InternalWallbinLinkSettings)link.Settings).OpenOnSamePage = linkInfo.OpenOnSamePage;
			((InternalWallbinLinkSettings)link.Settings).StyleSettings = linkInfo.StyleSettings;
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
