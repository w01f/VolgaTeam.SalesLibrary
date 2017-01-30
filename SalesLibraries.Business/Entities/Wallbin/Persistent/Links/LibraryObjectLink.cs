using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class LibraryObjectLink : BaseLibraryLink
	{
		#region Persistent Properties
		public string RelativePath { get; set; }
		public string ExpirationEncoded { get; set; }
		public string QuickLinkEncoded { get; set; }
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

		private QuickLinkSettings _quickLinkSettings;
		[NotMapped, JsonIgnore]
		public QuickLinkSettings QuickLinkSettings
		{
			get { return _quickLinkSettings ?? (_quickLinkSettings = SettingsContainer.CreateInstance<QuickLinkSettings>(this, QuickLinkEncoded)); }
			set { _quickLinkSettings = value; }
		}

		[NotMapped, JsonIgnore]
		public abstract string FullPath { get; }

		[NotMapped, JsonIgnore]
		public virtual string[] OpenPaths => new[] { FullPath };

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
		public override string LinkInfoDisplayName => Settings.TextWordWrap ? RelativePath : Name;

		[NotMapped, JsonIgnore]
		public override string Hint
		{
			get
			{
				var lines = new List<string>();
				lines.Add(String.Format("Added: {0}", AddDate.ToString("M/dd/yy h:mm:ss tt")));
				lines.Add(String.Format("Updated: {0}", LastModified.ToString("M/dd/yy h:mm:ss tt")));
				if (ExpirationSettings.IsExpired)
					lines.Add(String.Format("Expires: {0}", ExpirationSettings.ExpirationDate.ToString("M/dd/yy h:mm:ss tt")));
				var baseHint = base.Hint;
				if (!String.IsNullOrEmpty(baseHint))
					lines.Add(baseHint);
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public override Font DisplayFont => base.DisplayFont ??
											(((LibraryObjectLinkSettings)Settings).IsSpecialFormat ?
												Settings.Font :
												null);

		[NotMapped, JsonIgnore]
		public bool IsCorrupted => !Banner.Enable && String.IsNullOrEmpty(DisplayName);

		[NotMapped, JsonIgnore]
		public virtual string AutoWidgetKey => String.Empty;
		#endregion

		public override void BeforeSave()
		{
			if (NeedToSave)
			{
				ExpirationEncoded = ExpirationSettings.Serialize();
				QuickLinkEncoded = QuickLinkSettings.Serialize();
			}
			base.BeforeSave();
		}

		public override void AfterSave()
		{
			ExpirationSettings = null;
			QuickLinkSettings = null;
			base.AfterSave();
		}

		public override void ApplyValues(BaseLibraryLink link)
		{
			RelativePath = ((LibraryObjectLink)link).RelativePath;
			ExpirationEncoded = ((LibraryObjectLink)link).ExpirationEncoded;
			QuickLinkEncoded = ((LibraryObjectLink)link).QuickLinkEncoded;
			base.ApplyValues(link);
		}

		public override void ResetToDefault(IList<LinkSettingsGroupType> groupsForReset = null)
		{
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.Expiration))
				ExpirationEncoded = null;
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.QuickLink))
				QuickLinkEncoded = null;
			base.ResetToDefault(groupsForReset);
		}

		public override IList<LinkSettingsGroupType> GetCustomizedSettigsGroups()
		{
			var customizedSettingsGroups = base.GetCustomizedSettigsGroups().ToList();

			if (ExpirationSettings.Enable)
				customizedSettingsGroups.Add(LinkSettingsGroupType.Expiration);

			if (QuickLinkSettings.Enable)
				customizedSettingsGroups.Add(LinkSettingsGroupType.QuickLink);

			return customizedSettingsGroups;
		}

		public override BaseLibraryLink Copy()
		{
			var link = (LibraryObjectLink)base.Copy();
			link.RelativePath = RelativePath;
			link.ExpirationEncoded = ExpirationEncoded;
			link.QuickLinkEncoded = QuickLinkEncoded;
			return link;
		}

		public IList<LibraryObjectLink> GetRelatedLinks()
		{
			return ParentLibrary.Pages
				.SelectMany(p => p.TopLevelLinks)
				.OfType<LibraryObjectLink>()
				.Where(l => String.Equals(l.RelativePath, RelativePath, StringComparison.OrdinalIgnoreCase))
				.ToList();
		}

		public void DeleteLinkAndRelatedLinks()
		{
			var relatedLinks = GetRelatedLinks();
			foreach (var libraryObjectLink in relatedLinks)
				libraryObjectLink.DeleteLink();
		}
	}
}
