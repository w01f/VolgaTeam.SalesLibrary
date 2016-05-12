using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class ColumnTitle : WallbinCollectionEntity, IBannerSettingsHolder
	{
		#region Persistent Properties
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

		public string SettingsEncoded { get; set; }
		public string WidgetEncoded { get; set; }
		public string BannerEncoded { get; set; }
		public virtual LibraryPage Page { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return Page; }
			set { Page = value as LibraryPage; }
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get { return ColumnOrder; }
			set { ColumnOrder = value; }
		}

		private ColumnTitleSettings _settings;
		[NotMapped, JsonIgnore]
		public ColumnTitleSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<ColumnTitleSettings>(this, SettingsEncoded)); }
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
		public Color BannerBackColor => Settings.BackgroundColor;
		#endregion

		public override void BeforeSave()
		{
			SettingsEncoded = Settings.Serialize();
			WidgetEncoded = Widget.Serialize();
			BannerEncoded = Banner.Serialize();
		}

		public override void AfterSave()
		{
			Settings = null;
			Widget = null;
			Banner = null;
		}

		public override void ResetParent()
		{
			Page = null;
		}

		public class ColumnTitleSettings : SettingsContainer
		{
			private bool _showText;
			public bool ShowText
			{
				get { return _showText; }
				set
				{
					if (_showText != value)
						OnSettingsChanged();
					_showText = value;
				}
			}

			private string _text;
			public string Text
			{
				get { return _text; }
				set
				{
					if (_text != value)
						OnSettingsChanged();
					_text = value;
				}
			}

			private Color _backgroundColor = Color.White;
			public Color BackgroundColor
			{
				get { return _backgroundColor; }
				set
				{
					if (_backgroundColor != value)
						OnSettingsChanged();
					_backgroundColor = value;
				}
			}

			private Color _foreColor = Color.Black;
			public Color ForeColor
			{
				get { return _foreColor; }
				set
				{
					if (_foreColor != value)
						OnSettingsChanged();
					_foreColor = value;
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
