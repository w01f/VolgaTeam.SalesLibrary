﻿using System;
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
	public class LineBreak : BaseLibraryLink
	{
		#region Nonpersistent Properties

		private LineBreakSettings _settings;

		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<LineBreakSettings>(this, SettingsEncoded));
			set => _settings = value as LineBreakSettings;
		}

		[NotMapped, JsonIgnore]
		public override string DisplayNameWithoutNote
			=> String.IsNullOrEmpty(base.DisplayNameWithoutNote) ? Settings.Note : base.DisplayNameWithoutNote;

		[NotMapped, JsonIgnore]
		public override string LinkInfoDisplayName => Settings.TextWordWrap ? "Line break" : (!String.IsNullOrEmpty(Name) ? Name : "Line break");

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var baseHint = base.Hint;
				if (!String.IsNullOrEmpty(baseHint))
					return String.Format("{0}{2}{1}", Settings.Note, baseHint, Environment.NewLine);
				if (!String.IsNullOrEmpty(Settings.Note))
					return Settings.Note;
				return null;
			}
		}

		[NotMapped, JsonIgnore]
		public override Font DisplayFont => base.DisplayFont ?? Settings.Font;

		[NotMapped, JsonIgnore]
		public override string WebFormat => WebFormats.LineBreak;
		#endregion

		public LineBreak()
		{
			Type = LinkType.LineBreak;
		}

		public override string ToString()
		{
			return "Line Break";
		}

		public override void ResetToDefault(IList<LinkSettingsGroupType> groupsForReset = null)
		{
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.TextNote))
				Name = null;
			base.ResetToDefault(groupsForReset);
		}

		public static LineBreak Create(LibraryFolder parentFolder)
		{
			return CreateEntity<LineBreak>(lineBreak =>
			{
				lineBreak.Folder = parentFolder;
				lineBreak.AfterCreate();
			});
		}

		public static LineBreak CreateWithBanner(LibraryFolder parentFolder, Image banner)
		{
			var lineBreak = Create(parentFolder);

			lineBreak.Banner.Enable = true;
			lineBreak.Banner.Image = banner;
			lineBreak.Banner.ImagePaddingTop =
				lineBreak.Banner.ImagePaddingBottom =
					lineBreak.Banner.ImagePaddingLeft =
						lineBreak.Banner.ImagePaddingRight = 10;

			return lineBreak;
		}
	}
}
