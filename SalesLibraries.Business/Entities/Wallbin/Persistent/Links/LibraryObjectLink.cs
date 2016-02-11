using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class LibraryObjectLink : BaseLibraryLink
	{
		#region Persistent Properties
		public string RelativePath { get; set; }
		public string ExpirationEncoded { get; set; }
		#endregion

		#region Nonpersistent Properties
		private LibraryObjectLinkSettings _settings;
		[NotMapped, JsonIgnore]
		public override BaseLinkSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LibraryObjectLinkSettings>(this, SettingsEncoded)); }
			set { _settings = value as LibraryObjectLinkSettings; }
		}

		private LinkExpirationSettings _expirationSettings;
		[NotMapped, JsonIgnore]
		public LinkExpirationSettings ExpirationSettings
		{
			get { return _expirationSettings ?? (_expirationSettings = SettingsContainer.CreateInstance<LinkExpirationSettings>(this, ExpirationEncoded)); }
			set { _expirationSettings = value; }
		}

		[NotMapped, JsonIgnore]
		public abstract string FullPath { get; }

		[NotMapped, JsonIgnore]
		public abstract string WebPath { get; }

		[NotMapped, JsonIgnore]
		public override string DisplayName
		{
			get
			{
				if (Banner.Enable)
					return base.DisplayNameWithoutNote;
				return String.Format("{0}{1}",
						DisplayNameWithoutNote,
						!String.IsNullOrEmpty(Settings.Note) ? String.Format(" - {0}", Settings.Note) : String.Empty);
			}
		}

		[NotMapped, JsonIgnore]
		public override string DisplayNameWithoutNote
		{
			get
			{
				if (ExpirationSettings.Enable && ExpirationSettings.MarkWhenExpired && ExpirationSettings.IsExpired)
					return String.Format("EXPIRED! {0}", base.DisplayNameWithoutNote);
				return base.DisplayNameWithoutNote;
			}
		}

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				lines.Add(String.Format("Added: {0}", AddDate.ToString("M/dd/yy h:mm:ss tt")));
				if (ExpirationSettings.IsExpired)
					lines.Add(String.Format("Expires: {0}", ExpirationSettings.ExpirationDate.ToString("M/dd/yy h:mm:ss tt")));
				var baseHint = base.Hint;
				if (!String.IsNullOrEmpty(baseHint))
					lines.Add(baseHint);
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public override Font DisplayFont
		{
			get
			{
				return base.DisplayFont ??
					(((LibraryObjectLinkSettings)Settings).IsSpecialFormat ?
						Settings.Font :
						null);
			}
		}

		[NotMapped, JsonIgnore]
		public bool IsCorrupted
		{
			get { return !Banner.Enable && String.IsNullOrEmpty(DisplayName); }
		}
		#endregion

		public override void BeforeSave()
		{
			ExpirationEncoded = ExpirationSettings.Serialize();
			base.BeforeSave();
		}

		public override void AfterSave()
		{
			ExpirationSettings = null;
			base.AfterSave();
		}

		public override void ApplyValues(BaseLibraryLink link)
		{
			RelativePath = ((LibraryObjectLink)link).RelativePath;
			ExpirationEncoded = ((LibraryObjectLink)link).ExpirationEncoded;
			base.ApplyValues(link);
		}

		public override void ResetToDefault()
		{
			ExpirationEncoded = null;
			base.ResetToDefault();
		}
	}
}
