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
	public class InternalLink : LibraryObjectLink
	{
		#region Nonpersistent Properties
		private InternalLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<InternalLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as InternalLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string FullPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string WebPath => RelativePath;

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.InternalLink;

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				lines.Add(String.Format("Target Library: {0}", TargetLibrary));
				lines.Add(String.Format("Target Page: {0}", TargetPage));
				if (!String.IsNullOrEmpty(TargetWindow))
					lines.Add(String.Format("Target Window: {0}", TargetWindow));
				if (!String.IsNullOrEmpty(TargetLink))
					lines.Add(String.Format("Target Link: {0}", TargetLink));
				lines.Add(base.Hint);
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string TargetLibrary => ((InternalLinkSettings)Settings).LibraryName;

		[NotMapped, JsonIgnore]
		public string TargetPage => ((InternalLinkSettings)Settings).PageName;

		[NotMapped, JsonIgnore]
		public string TargetWindow => ((InternalLinkSettings)Settings).WindowName;

		[NotMapped, JsonIgnore]
		public string TargetLink => ((InternalLinkSettings)Settings).LinkName;

		#endregion

		public InternalLink()
		{
			Type = FileTypes.InternalLink;
		}

		public static InternalLink Create(InternalLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = new InternalLink();
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((InternalLinkSettings)link.Settings).LibraryName = linkInfo.LibraryName;
			((InternalLinkSettings)link.Settings).PageName = linkInfo.PageName;
			((InternalLinkSettings)link.Settings).WindowName = linkInfo.WindowName;
			((InternalLinkSettings)link.Settings).LinkName = linkInfo.LinkName;
			((InternalLinkSettings)link.Settings).ForcePreview = linkInfo.ForcePreview;
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
