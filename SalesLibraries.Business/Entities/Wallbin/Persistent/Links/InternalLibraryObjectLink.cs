using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class InternalLibraryObjectLink : InternalLink
	{
		#region Nonpersistent Properties
		private InternalLibraryObjectLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<InternalLibraryObjectLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as InternalLibraryObjectLinkSettings; }
		}

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
					lines.Add(String.Format("Target Link: {0}", TargetLink));
					lines.Add(base.Hint);
				}
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string TargetLibrary => ((InternalLibraryObjectLinkSettings)Settings).LibraryName;

		[NotMapped, JsonIgnore]
		public string TargetPage => ((InternalLibraryObjectLinkSettings)Settings).PageName;

		[NotMapped, JsonIgnore]
		public string TargetFolder => ((InternalLibraryObjectLinkSettings)Settings).WindowName;

		[NotMapped, JsonIgnore]
		public string TargetLink => ((InternalLibraryObjectLinkSettings)Settings).LinkName;
		#endregion


		public static InternalLibraryObjectLink Create(InternalLibraryObjectLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = CreateEntity<InternalLibraryObjectLink>();
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((InternalLibraryObjectLinkSettings)link.Settings).LibraryName = linkInfo.LibraryName;
			((InternalLibraryObjectLinkSettings)link.Settings).PageName = linkInfo.PageName;
			((InternalLibraryObjectLinkSettings)link.Settings).WindowName = linkInfo.WindowName;
			((InternalLibraryObjectLinkSettings)link.Settings).LinkName = linkInfo.LinkName;
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
