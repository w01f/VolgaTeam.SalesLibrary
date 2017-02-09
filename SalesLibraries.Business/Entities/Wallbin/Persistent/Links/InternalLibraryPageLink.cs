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
	public class InternalLibraryPageLink : InternalLink
	{
		#region Nonpersistent Properties
		private InternalLibraryPageLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<InternalLibraryPageLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as InternalLibraryPageLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.InternalLibraryPage;

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
					lines.Add(base.Hint);
				}
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string TargetLibrary => ((InternalLibraryPageLinkSettings)Settings).LibraryName;

		[NotMapped, JsonIgnore]
		public string TargetPage => ((InternalLibraryPageLinkSettings)Settings).PageName;
		#endregion

		public static InternalLibraryPageLink Create(InternalLibraryPageLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = CreateEntity<InternalLibraryPageLink>();
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((InternalLibraryPageLinkSettings)link.Settings).LibraryName = linkInfo.LibraryName;
			((InternalLibraryPageLinkSettings)link.Settings).PageName = linkInfo.PageName;
			((InternalLibraryPageLinkSettings)link.Settings).ShowHeaderText = linkInfo.ShowHeaderText;
			((InternalLibraryPageLinkSettings)link.Settings).OpenOnSamePage = linkInfo.OpenOnSamePage;
			((InternalLibraryPageLinkSettings)link.Settings).StyleSettings = linkInfo.StyleSettings;
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
	}
}
