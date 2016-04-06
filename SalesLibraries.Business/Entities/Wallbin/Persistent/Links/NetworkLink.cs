﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.HyperLinkInfo;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public class NetworkLink : LibraryObjectLink
	{
		#region Nonpersistent Properties
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
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				if (!String.IsNullOrEmpty(((LibraryObjectLinkSettings)Settings).HoverNote))
					lines.Add(((LibraryObjectLinkSettings)Settings).HoverNote);
				lines.Add(String.Format("Path: {0}", FullPath));
				lines.Add(base.Hint);
				return String.Join(Environment.NewLine, lines);
			}
		}
		#endregion

		public NetworkLink()
		{
			Type = FileTypes.Network;
		}

		public static NetworkLink Create(LanLinkInfo linkInfo, LibraryFolder parentFolder)
		{
			var link = Create(linkInfo.Path);
			link.Name = linkInfo.Name;
			link.Folder = parentFolder;
			if (linkInfo.FormatAsBluelink)
			{
				((LibraryObjectLinkSettings)link.Settings).RegularFontStyle = FontStyle.Underline | FontStyle.Bold;
				link.Settings.ForeColor = Color.Blue;
			}
			return link;
		}

		public static NetworkLink Create(string path)
		{
			return new NetworkLink { RelativePath = path };
		}
	}
}
