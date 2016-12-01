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
	public class InternalShortcutLink : InternalLink
	{
		#region Nonpersistent Properties
		private InternalShortcutLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<InternalShortcutLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as InternalShortcutLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.InternalShortcutLink;

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
					lines.Add(base.Hint);
				}
				return String.Join(Environment.NewLine, lines);
			}
		}
		#endregion

		public static InternalShortcutLink Create(InternalShortcutLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = CreateEntity<InternalShortcutLink>();
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((InternalShortcutLinkSettings)link.Settings).ShortcutId = linkInfo.ShortcutId;
			((InternalShortcutLinkSettings)link.Settings).OpenOnSamePage = linkInfo.OpenOnSamePage;
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
