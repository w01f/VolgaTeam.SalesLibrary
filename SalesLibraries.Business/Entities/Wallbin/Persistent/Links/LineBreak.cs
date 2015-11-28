﻿using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
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
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LineBreakSettings>(this, SettingsEncoded)); }
			set { _settings = value as LineBreakSettings; }
		}

		[NotMapped, JsonIgnore]
		public override string DisplayNameWithoutNote
		{
			get { return String.IsNullOrEmpty(base.DisplayNameWithoutNote) ? Settings.Note : base.DisplayNameWithoutNote; }
		}

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get { return String.Format("{0}{2}{1}", Settings.Note, base.Hint, Environment.NewLine); }
		}

		[NotMapped, JsonIgnore]
		public override Font DisplayFont
		{
			get
			{
				return base.DisplayFont ?? Settings.Font;
			}
		}

		[NotMapped, JsonIgnore]
		public override Color DisplayColor
		{
			get { return Settings.ForeColor; }
		}
		#endregion

		public LineBreak()
		{
			Type = FileTypes.LineBreak;
		}

		public override string ToString()
		{
			return "Line Break";
		}

		public static LineBreak Create(LibraryFolder parentFolder)
		{
			var link = new LineBreak();
			link.Folder = parentFolder;
			return link;
		}
	}
}
