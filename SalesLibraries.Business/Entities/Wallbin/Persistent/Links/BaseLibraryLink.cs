using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent.Links
{
	public abstract class BaseLibraryLink : WallbinCollectionEntity
	{
		#region Persistent Properties
		[Required]
		public FileTypes Type { get; set; }

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
		public virtual LibraryFolder Folder { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return Folder; }
		}

		[NotMapped, JsonIgnore]
		public virtual Library ParentLibrary
		{
			get { return Folder.Page.Library; }
		}

		[NotMapped, JsonIgnore]
		public virtual LibraryPage ParentPage
		{
			get { return Folder.Page; }
		}

		[NotMapped, JsonIgnore]
		public virtual LibraryFolder ParentFolder
		{
			get { return Folder; }
		}

		[NotMapped, JsonIgnore]
		public virtual BaseLibraryLink TopLevelLink
		{
			get { return this; }
		}

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

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get { return Order; }
			set { Order = value; }
		}

		[NotMapped, JsonIgnore]
		public virtual string WebFormat
		{
			get { return WebFormats.Other; }
		}

		[NotMapped, JsonIgnore]
		public virtual string DisplayName
		{
			get { return DisplayNameWithoutNote; }
		}

		[NotMapped, JsonIgnore]
		public virtual string DisplayNameWithoutNote
		{
			get
			{
				if (Banner.Enable)
					return Banner.ShowText ? Banner.Text : String.Empty;
				return Name;
			}
		}

		[NotMapped, JsonIgnore]
		public virtual string Hint
		{
			get
			{
				var lines = new List<string>();
				if (Tags.Categories.Any())
					lines.Add(String.Format("Category Tags: {0}", Tags.AllCategories));
				if (Tags.Keywords.Any())
					lines.Add(String.Format("Keyword Tags: {0}", Tags.AllKeywords));
				if (Security.HasSecuritySettings)
					lines.Add(String.Format("Special Security Settings: Enabled"));
				return String.Join(Environment.NewLine, lines);
			}
		}

		[NotMapped, JsonIgnore]
		public virtual Font DisplayFont
		{
			get
			{
				if (Banner.Enable && Banner.ShowText)
					return Banner.Font;
				return null;
			}
		}

		[NotMapped, JsonIgnore]
		public Color DisplayColor
		{
			get
			{
				if (Banner.Enable && Banner.ShowText)
					return Banner.ForeColor;
				if (Settings.ForeColor.HasValue)
					return Settings.ForeColor.Value;
				return Folder.Settings.ForeWindowColor;
			}
		}
		#endregion

		protected BaseLibraryLink()
		{
			Type = FileTypes.Other;
			AddDate = DateTime.Now;
		}

		public override string ToString()
		{
			return Name;
		}

		public override void BeforeSave()
		{
			SettingsEncoded = Settings.Serialize();
			SecurityEncoded = Security.Serialize();
			TagsEncoded = Tags.Serialize();
			WidgetEncoded = Widget.Serialize();
			BannerEncoded = Banner.Serialize();
		}

		public override void AfterSave()
		{
			Settings = null;
			Security = null;
			Tags = null;
			Widget = null;
			Banner = null;
		}

		public override void ResetParent()
		{
			Folder = null;
		}

		public virtual void DeleteLink(bool fullDelete = false)
		{
			if (fullDelete)
				Delete(ParentLibrary.Context);
			if (Folder != null)
				Folder.Links.RemoveItem(this);
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
			MarkAsModified();
			AfterSave();
		}

		public virtual void ResetToDefault()
		{
			SettingsEncoded = null;
			SecurityEncoded = null;
			WidgetEncoded = null;
			BannerEncoded = null;
			MarkAsModified();
			AfterSave();
		}
	}
}
