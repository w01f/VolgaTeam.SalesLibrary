using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class WebLink : LibraryObjectLink
	{
		#region Nonpersistent Properties
		private WebLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<WebLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as WebLinkSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string FullPath
		{
			get { return RelativePath; }
		}

		[NotMapped, JsonIgnore]
		public override string WebPath
		{
			get { return RelativePath; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get { return ((WebLinkSettings)Settings).IsUrl365 ? WebFormats.Url365 : WebFormats.Url; }
		}

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				lines.Add(String.Format("Url: {0}", Url));
				lines.Add(base.Hint);
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public string Url
		{
			get { return RelativePath; }
		}
		#endregion

		public WebLink()
		{
			Type = FileTypes.Url;
		}

		public static WebLink Create(
			string name,
			string path,
			bool forcePreview,
			bool displayAsHyperlnk,
			LibraryFolder parentFolder)
		{
			var link = Create(path);
			link.Name = name;
			link.Folder = parentFolder;
			((WebLinkSettings)link.Settings).ForcePreview = forcePreview;
			if (displayAsHyperlnk)
			{
				((WebLinkSettings)link.Settings).RegularFontStyle = FontStyle.Underline | FontStyle.Bold;
				link.Settings.ForeColor = Color.Blue;
			}
			return link;
		}

		public static WebLink Create(string path)
		{
			return new WebLink { RelativePath = path };
		}
	}
}
