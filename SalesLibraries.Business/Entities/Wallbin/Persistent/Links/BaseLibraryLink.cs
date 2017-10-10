using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class BaseLibraryLink : WallbinCollectionEntity, IBannerSettingsHolder
	{
		#region Persistent Properties
		[Required]
		public LinkType Type { get; set; }

		private string _name;
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
					MarkAsModified();
				_name = value;
			}
		}

		private int _order;
		[Required]
		public int Order
		{
			get { return _order; }
			set
			{
				if (_order != value)
					MarkAsModified();
				_order = value;
			}
		}

		[Required]
		public DateTime AddDate { get; set; }
		public string SettingsEncoded { get; set; }
		public string SecurityEncoded { get; set; }
		public string TagsEncoded { get; set; }
		public string WidgetEncoded { get; set; }
		public string BannerEncoded { get; set; }
		public string ThumbnailEncoded { get; set; }
		public string ResetSettingsSchedulerEncoded { get; set; }
		public virtual LibraryFolder Folder { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return Folder; }
			set { Folder = value as LibraryFolder; }
		}

		[NotMapped, JsonIgnore]
		public virtual Library ParentLibrary => Folder?.Page?.Library;

		[NotMapped, JsonIgnore]
		public virtual LibraryPage ParentPage => Folder?.Page;

		[NotMapped, JsonIgnore]
		public virtual LibraryFolder ParentFolder => Folder;

		[NotMapped, JsonIgnore]
		public virtual BaseLibraryLink TopLevelLink => this;

		[NotMapped, JsonIgnore]
		public abstract BaseLinkSettings Settings { get; set; }

		private SecuritySettings _security;
		[NotMapped, JsonIgnore]
		public SecuritySettings Security
		{
			get { return _security ?? (_security = SettingsContainer.CreateInstance<SecuritySettings>(this, SecurityEncoded)); }
			set { _security = value; }
		}

		private TagSettings _tags;
		[NotMapped, JsonIgnore]
		public TagSettings Tags
		{
			get { return _tags ?? (_tags = SettingsContainer.CreateInstance<TagSettings>(this, TagsEncoded)); }
			set { _tags = value; }
		}

		private LinkWidgetSettings _widget;
		[NotMapped, JsonIgnore]
		public LinkWidgetSettings Widget
		{
			get { return _widget ?? (_widget = SettingsContainer.CreateInstance<LinkWidgetSettings>(this, WidgetEncoded)); }
			set { _widget = value; }
		}

		private BannerSettings _banner;
		[NotMapped, JsonIgnore]
		public BannerSettings Banner
		{
			get
			{
				if (_banner == null)
				{
					_banner = SettingsContainer.CreateInstance<BannerSettings>(this, BannerEncoded);
					if (String.IsNullOrEmpty(BannerEncoded) && Folder != null)
					{
						_banner.Font = (Font)Folder.Settings.WindowFont.Clone();
						_banner.ForeColor = Folder.Settings.ForeWindowColor;
					}
				}
				return _banner;
			}
			set { _banner = value; }
		}

		private ThumbnailSettings _thumbnail;
		[NotMapped, JsonIgnore]
		public ThumbnailSettings Thumbnail
		{
			get { return _thumbnail ?? (_thumbnail = SettingsContainer.CreateInstance<ThumbnailSettings>(this, ThumbnailEncoded)); }
			set { _thumbnail = value; }
		}

		private ResetSettingsSchedulerSettings _resetSettingsScheduler;
		[NotMapped, JsonIgnore]
		public ResetSettingsSchedulerSettings ResetSettingsScheduler
		{
			get { return _resetSettingsScheduler ?? (_resetSettingsScheduler = SettingsContainer.CreateInstance<ResetSettingsSchedulerSettings>(this, ResetSettingsSchedulerEncoded)); }
			set { _resetSettingsScheduler = value; }
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get { return Order; }
			set { Order = value; }
		}

		[NotMapped, JsonIgnore]
		public virtual string WebFormat => WebFormats.Other;

		[NotMapped, JsonIgnore]
		public virtual LinkFileType SubType => LinkFileType.NotFile;

		[NotMapped, JsonIgnore]
		public virtual string DisplayName => DisplayNameWithoutNote;

		[NotMapped, JsonIgnore]
		public virtual string DisplayNameWithoutNote
		{
			get
			{
				if (Banner.Enable)
				{
					switch (Banner.TextMode)
					{
						case BannerTextMode.LinkName:
							return Name;
						case BannerTextMode.CustomText:
							return Banner.Text;
						default:
							return String.Empty;
					}
				}
				if (Thumbnail.Enable)
				{
					switch (Thumbnail.TextMode)
					{
						case ThumbnailTextMode.LinkName:
							return Name;
						case ThumbnailTextMode.CustomText:
							return Thumbnail.Text;
						default:
							return String.Empty;
					}
				}
				return Name;
			}
			set
			{
				if (Banner.Enable && Banner.TextMode == BannerTextMode.CustomText)
					Banner.Text = value;
				else if (Thumbnail.Enable && Thumbnail.TextMode == ThumbnailTextMode.CustomText)
					Thumbnail.Text = value;
				else
					Name = value;
			}
		}

		[NotMapped, JsonIgnore]
		public abstract string LinkInfoDisplayName { get; }

		[NotMapped, JsonIgnore]
		public virtual string Hint
		{
			get
			{
				var lines = new List<string>();
				if (Tags.Keywords.Any(k => !String.IsNullOrEmpty(k.Name)))
					lines.Add(String.Format("Keyword Tags: {0}", Tags.AllKeywords));
				if (Security.HasSecuritySettings)
					lines.Add("Special Security Settings: Enabled");
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public virtual Font DisplayFont
		{
			get
			{
				if (Banner.Enable && Banner.TextEnabled)
					return Banner.Font;
				if (Thumbnail.Enable && Thumbnail.TextEnabled)
					return Thumbnail.Font;
				return null;
			}
		}

		[NotMapped, JsonIgnore]
		public virtual Color DisplayColor
		{
			get
			{
				if (Banner.Enable && Banner.TextEnabled)
					return Banner.ForeColor;
				if (Thumbnail.Enable && Thumbnail.TextEnabled)
					return Thumbnail.ForeColor;
				if (Settings.ForeColor.HasValue)
					return Settings.ForeColor.Value;
				return Folder.Settings.ForeWindowColor;
			}
		}

		[NotMapped, JsonIgnore]
		public Color BannerBackColor => Folder.Settings.BackgroundWindowColor;

		[NotMapped, JsonIgnore]
		public string ObjectDisplayName => "Link";
		#endregion

		protected BaseLibraryLink()
		{
			Type = LinkType.Other;
			AddDate = DateTime.Now;
		}

		public override void MarkAsModified()
		{
			base.MarkAsModified();
			ParentLibrary?.LogLinklAction(LinkActionType.Change, this);
		}

		public override string ToString()
		{
			return Name;
		}

		protected virtual void AfterCreate()
		{
			ParentLibrary?.LogLinklAction(LinkActionType.Add, this);
		}

		public override void BeforeSave()
		{
			if (NeedToSave)
			{
				SettingsEncoded = Settings.Serialize();
				SecurityEncoded = Security.Serialize();
				TagsEncoded = Tags.Serialize();
				WidgetEncoded = Widget.Serialize();
				BannerEncoded = Banner.Serialize();
				ThumbnailEncoded = Thumbnail.Serialize();
				ResetSettingsSchedulerEncoded = ResetSettingsScheduler.Serialize();
			}
			base.BeforeSave();
		}

		public override void AfterSave()
		{
			Settings = null;
			Security = null;
			Tags = null;
			Widget = null;
			Banner = null;
			Thumbnail = null;
			ResetSettingsScheduler = null;
		}

		public override void ResetParent()
		{
			Folder = null;
		}

		public override void Delete(LibraryContext context)
		{
			ParentLibrary?.LogLinklAction(LinkActionType.Delete, this);
			base.Delete(context);
		}

		public virtual void DeleteLink()
		{
			if (ParentLibrary != null)
			{
				foreach (var linkBundle in ParentLibrary.LinkBundles)
				{
					if (linkBundle.Settings.Items.OfType<LibraryLinkItem>().Any(linkItem => linkItem.LinkId == ExtId))
					{
						linkBundle.Settings.Items.RemoveAll(item => item is LibraryLinkItem && ((LibraryLinkItem)item).LinkId == ExtId);
						linkBundle.MarkAsModified();
						linkBundle.Save(ParentLibrary.Context, linkBundle);
					}
				}
				Delete(ParentLibrary.Context);
			}
			UnlinkLink();
		}

		public virtual void UnlinkLink()
		{
			Folder?.Links.RemoveItem(this);
			ResetParent();
		}

		public virtual void ApplyValues(BaseLibraryLink link)
		{
			Type = link.Type;
			Order = link.Order;
			Name = link.Name;
			AddDate = link.AddDate;
			SettingsEncoded = link.SettingsEncoded;
			SecurityEncoded = link.SecurityEncoded;
			TagsEncoded = link.TagsEncoded;
			WidgetEncoded = link.WidgetEncoded;
			BannerEncoded = link.BannerEncoded;
			ThumbnailEncoded = link.ThumbnailEncoded;
			ResetSettingsSchedulerEncoded = link.ResetSettingsSchedulerEncoded;
			MarkAsModified();
			AfterSave();
		}

		public virtual void ResetToDefault(IList<LinkSettingsGroupType> groupsForReset = null)
		{
			if (groupsForReset == null)
			{
				SettingsEncoded = null;
				TagsEncoded = null;
			}
			else
			{
				Settings.ResetToDefault(groupsForReset);
				SettingsEncoded = Settings.Serialize();

				Tags.ResetToDefault(groupsForReset);
				TagsEncoded = Tags.Serialize();
			}

			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.Security))
				SecurityEncoded = null;
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.Widgets))
				WidgetEncoded = null;
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.AutoWidgets))
			{
				Widget.WidgetType = WidgetType.NoWidget;
				WidgetEncoded = Widget.Serialize();
			}
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.Banners))
				BannerEncoded = null;
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.Thumbnails))
				ThumbnailEncoded = null;
			if (groupsForReset == null || groupsForReset.Contains(LinkSettingsGroupType.SearchTags))
			{
				TagsEncoded = null;
			}

			ResetSettingsSchedulerEncoded = null;

			MarkAsModified();
			AfterSave();
		}

		public virtual IList<LinkSettingsGroupType> GetCustomizedSettigsGroups()
		{
			var customizedSettingsGroups = new List<LinkSettingsGroupType>();

			customizedSettingsGroups.AddRange(Settings.GetCustomizedSettigsGroups());

			customizedSettingsGroups.AddRange(Tags.GetCustomizedSettigsGroups());

			if (Security.HasSecuritySettings)
				customizedSettingsGroups.Add(LinkSettingsGroupType.Security);

			if (Widget.WidgetType != Widget.DefaultWidgetType)
				customizedSettingsGroups.Add(LinkSettingsGroupType.Widgets);
			if (Widget.WidgetType == WidgetType.AutoWidget)
				customizedSettingsGroups.Add(LinkSettingsGroupType.AutoWidgets);
			if (Banner.Enable)
				customizedSettingsGroups.Add(LinkSettingsGroupType.Banners);
			if (Thumbnail.Enable)
				customizedSettingsGroups.Add(LinkSettingsGroupType.Thumbnails);

			return customizedSettingsGroups;
		}

		public virtual BaseLibraryLink Copy()
		{
			NeedToSave = true;

			BeforeSave();

			var link = (BaseLibraryLink)Activator.CreateInstance(GetType());
			link.Type = Type;
			link.Name = Name;
			link.Order = Order;
			link.AddDate = AddDate;
			link.SettingsEncoded = SettingsEncoded;
			link.SecurityEncoded = SecurityEncoded;
			link.TagsEncoded = TagsEncoded;
			link.WidgetEncoded = WidgetEncoded;
			link.BannerEncoded = BannerEncoded;
			link.ThumbnailEncoded = ThumbnailEncoded;
			link.ResetSettingsSchedulerEncoded = ResetSettingsSchedulerEncoded;

			return link;
		}

		public static Type GetObjectTypeByLinkType(LinkType linkType)
		{
			switch (linkType)
			{
				case LinkType.Pdf:
					return typeof(PdfLink);
				case LinkType.Excel:
					return typeof(ExcelLink);
				default:
					return typeof(BaseLibraryLink);
			}
		}
	}
}
