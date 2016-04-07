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
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class LibraryFolder : WallbinCollectionEntity, IBannerSettingsHolder
	{
		#region Persistent Properties
		private string _name;
		[Required]
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

		private int _rowOrder;
		[Required]
		public int RowOrder
		{
			get { return _rowOrder; }
			set
			{
				if (_rowOrder != value)
					MarkAsModified();
				_rowOrder = value;
			}
		}

		private int _columnOrder;
		[Required]
		public int ColumnOrder
		{
			get { return _columnOrder; }
			set
			{
				if (_columnOrder != value)
					MarkAsModified();
				_columnOrder = value;
			}
		}

		[Required]
		public DateTime AddDate { get; set; }
		public string SettingsEncoded { get; set; }
		public string WidgetEncoded { get; set; }
		public string BannerEncoded { get; set; }
		public virtual LibraryPage Page { get; set; }

		[InverseProperty("Folder")]
		public virtual ICollection<BaseLibraryLink> Links { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return Page; }
		}

		private LibraryFolderSettings _settings;
		[NotMapped, JsonIgnore]
		public LibraryFolderSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LibraryFolderSettings>(this, SettingsEncoded)); }
			set { _settings = value; }
		}

		private WidgetSettings _widget;
		[NotMapped, JsonIgnore]
		public WidgetSettings Widget
		{
			get { return _widget ?? (_widget = SettingsContainer.CreateInstance<WidgetSettings>(this, WidgetEncoded)); }
			set { _widget = value; }
		}

		private BannerSettings _banner;
		[NotMapped, JsonIgnore]
		public BannerSettings Banner
		{
			get { return _banner ?? (_banner = SettingsContainer.CreateInstance<BannerSettings>(this, BannerEncoded)); }
			set { _banner = value; }
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get { return RowOrder; }
			set { RowOrder = value; }
		}

		[NotMapped, JsonIgnore]
		public IEnumerable<BaseLibraryLink> AllLinks
		{
			get { return Links.Union(Links.OfType<LibraryFolderLink>().SelectMany(lf => lf.AllLinks)); }
		}

		[NotMapped, JsonIgnore]
		public bool ContainLinkWidgets
		{
			get { return Links.Any(l => !l.Widget.Disabled && l.Widget.Image != null); }
		}

		[NotMapped, JsonIgnore]
		public Color BannerBackColor => Settings.BackgroundHeaderColor;
		#endregion

		public LibraryFolder()
		{
			AddDate = DateTime.Now;
			Links = new List<BaseLibraryLink>();
		}

		public override void BeforeSave()
		{
			SettingsEncoded = Settings.Serialize();
			WidgetEncoded = Widget.Serialize();
			BannerEncoded = Banner.Serialize();
			foreach (var link in Links)
				link.BeforeSave();
		}

		public override void AfterSave()
		{
			Settings = null;
			Widget = null;
			Banner = null;
		}

		public override void Save(LibraryContext context, IDbEntity<LibraryContext> current, bool withCommit = true)
		{
			var currentFolder = (LibraryFolder)current;
			Links.Save(currentFolder.Links, context);
			Links.Sort();
			base.Save(context, current, withCommit);
		}

		public override void Delete(LibraryContext context)
		{
			foreach (var link in Links.ToList())
				link.Delete(context);
			Links.Clear();
			base.Delete(context);
		}

		public override void ResetParent()
		{
			Page = null;
		}

		public void SortLinksByDisplayName()
		{
			var sorderLinks = Links.ToList();
			sorderLinks.Sort((x, y) => WinAPIHelper.StrCmpLogicalW(x.DisplayName, y.DisplayName));
			var order = 0;
			foreach (var libraryLink in sorderLinks)
			{
				libraryLink.Order = order;
				order++;
			}
			Links.Sort();
		}

		public LibraryFolder Copy()
		{
			BeforeSave();

			var folder = new LibraryFolder();
			folder.Name = Name;
			folder.ColumnOrder = ColumnOrder;
			folder.RowOrder = RowOrder;
			folder.SettingsEncoded = SettingsEncoded;
			folder.WidgetEncoded = WidgetEncoded;
			folder.BannerEncoded = BannerEncoded;

			foreach (var libraryLink in Links)
			{
				var newLink = libraryLink.Copy();
				newLink.Folder = folder;
				folder.Links.Add(newLink);
			}

			return folder;
		}

		public class LibraryFolderSettings : SettingsContainer
		{
			private Color _borderColor = Color.Black;
			public Color BorderColor
			{
				get { return _borderColor; }
				set
				{
					if (_borderColor != value)
						OnSettingsChanged();
					_borderColor = value;
				}
			}

			private Color _backgroundWindowColor = Color.White;
			public Color BackgroundWindowColor
			{
				get { return _backgroundWindowColor; }
				set
				{
					if (_backgroundWindowColor != value)
						OnSettingsChanged();
					_backgroundWindowColor = value;
				}
			}

			private Color _foreWindowColor = Color.Black;
			public Color ForeWindowColor
			{
				get { return _foreWindowColor; }
				set
				{
					if (_foreWindowColor != value)
						OnSettingsChanged();
					_foreWindowColor = value;
				}
			}

			private Color _backgroundHeaderColor = Color.White;
			public Color BackgroundHeaderColor
			{
				get { return _backgroundHeaderColor; }
				set
				{
					if (_backgroundHeaderColor != value)
						OnSettingsChanged();
					_backgroundHeaderColor = value;
				}
			}

			private Color _foreHeaderColor = Color.Black;
			public Color ForeHeaderColor
			{
				get { return _foreHeaderColor; }
				set
				{
					if (_foreHeaderColor != value)
						OnSettingsChanged();
					_foreHeaderColor = value;
				}
			}

			private Font _windowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point);
			public Font WindowFont
			{
				get { return _windowFont; }
				set
				{
					if (_windowFont != value)
						OnSettingsChanged();
					_windowFont = value;
				}
			}

			private Font _headerFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
			public Font HeaderFont
			{
				get { return _headerFont; }
				set
				{
					if (_headerFont != value)
						OnSettingsChanged();
					_headerFont = value;
				}
			}

			private Alignment _headerAlignment = Alignment.Center;
			public Alignment HeaderAlignment
			{
				get { return _headerAlignment; }
				set
				{
					if (_headerAlignment != value)
						OnSettingsChanged();
					_headerAlignment = value;
				}
			}
		}
	}
}
