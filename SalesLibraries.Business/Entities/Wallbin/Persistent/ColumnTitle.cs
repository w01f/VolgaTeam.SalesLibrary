﻿using System;
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
			get => _columnOrder;
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
			get => Page;
			set => Page = value as LibraryPage;
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get => ColumnOrder;
			set => ColumnOrder = value;
		}

		private ColumnTitleSettings _settings;
		[NotMapped, JsonIgnore]
		public ColumnTitleSettings Settings
		{
			get => _settings ?? (_settings = SettingsContainer.CreateInstance<ColumnTitleSettings>(this, SettingsEncoded));
			set => _settings = value;
		}

		private WidgetSettings _widget;
		[NotMapped, JsonIgnore]
		public WidgetSettings Widget
		{
			get => _widget ?? (_widget = SettingsContainer.CreateInstance<WidgetSettings>(this, WidgetEncoded));
			set => _widget = value;
		}

		private BannerSettings _banner;
		[NotMapped, JsonIgnore]
		public BannerSettings Banner
		{
			get => _banner ?? (_banner = SettingsContainer.CreateInstance<BannerSettings>(this, BannerEncoded));
			set => _banner = value;
		}

		[NotMapped, JsonIgnore]
		public Color BannerBackColor => Settings.BackgroundColor;

		[NotMapped, JsonIgnore]
		public String Name => ObjectDisplayName;

		[NotMapped, JsonIgnore]
		public string ObjectDisplayName => "Column";
		#endregion

		public override void BeforeSave()
		{
			if (NeedToSave)
			{
				SettingsEncoded = Settings.Serialize();
				WidgetEncoded = Widget.Serialize();
				BannerEncoded = Banner.Serialize();
			}
			base.BeforeSave();
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

		public ColumnTitle Copy()
		{
			NeedToSave = true;

			BeforeSave();

			return CreateEntity<ColumnTitle>(columnTitle =>
			{
				columnTitle.ColumnOrder = CollectionOrder;
				columnTitle.SettingsEncoded = SettingsEncoded;
				columnTitle.BannerEncoded = BannerEncoded;
				columnTitle.WidgetEncoded = WidgetEncoded;
			});
		}

		public class ColumnTitleSettings : SettingsContainer
		{
			private bool _showText;
			public bool ShowText
			{
				get => _showText;
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
				get => _text;
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
				get => _backgroundColor;
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
				get => _foreColor;
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
				get => _headerFont;
				set
				{
					if (_headerFont != value)
						OnSettingsChanged();
					_headerFont = value;
				}
			}

			private HorizontalAlignment _headerAlignment = HorizontalAlignment.Center;
			public HorizontalAlignment HeaderAlignment
			{
				get => _headerAlignment;
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
