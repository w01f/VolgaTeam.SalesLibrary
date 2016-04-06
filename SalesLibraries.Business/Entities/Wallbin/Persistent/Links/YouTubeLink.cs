﻿using System;
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
	public class YouTubeLink : HyperLink
	{
		#region Nonpersistent Properties
		private YouTubeSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<YouTubeSettings>(this, SettingsEncoded)); }
			set { _settings = value as YouTubeSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string WebFormat
		{
			get { return WebFormats.YouTube; }
		}
		#endregion

		public YouTubeLink()
		{
			Type = FileTypes.YouTube;
		}

		public static YouTubeLink Create(YouTubeLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			((YouTubeSettings)link.Settings).ForcePreview = linkInfo.ForcePreview;
			if (linkInfo.FormatAsBluelink)
			{
				((YouTubeSettings)link.Settings).RegularFontStyle = FontStyle.Underline | FontStyle.Bold;
				link.Settings.ForeColor = Color.Blue;
			}
			return link;
		}

		public static YouTubeLink Create(string path)
		{
			return new YouTubeLink { RelativePath = path };
		}
	}
}
