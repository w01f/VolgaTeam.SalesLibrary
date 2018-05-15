using System;
using System.Collections.Generic;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class RevenueItem : BaseBundleItem, ILinkBundleInfoItem, IChangable
	{
		public const string ItemName = "Revenue";

		private LinkBundleRevenueType _revenueType;
		public LinkBundleRevenueType RevenueType
		{
			get => _revenueType;
			set
			{
				if (_revenueType != value)
					OnSettingsChanged();
				_revenueType = value;
			}
		}

		private string _additionalInfo;

		public string AdditionalInfo
		{
			get => _additionalInfo;
			set
			{
				if (_additionalInfo != value)
					OnSettingsChanged();
				_additionalInfo = value;
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

		private Color _backColor = Color.White;
		public Color BackColor
		{
			get => _backColor;
			set
			{
				if (_backColor != value)
					OnSettingsChanged();
				_backColor = value;
			}
		}

		private Font _font = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);
		public Font Font
		{
			get => _font;
			set
			{
				if (_font != value)
					OnSettingsChanged();
				_font = value;
			}
		}

		public List<RevenueInfo> InfoItems { get; private set; }

		[JsonIgnore]
		public override string Name
		{
			get => ItemName;
			set => throw new NotImplementedException();
		}

		[JsonIgnore]
		public DateTime LastModified
		{
			get => Parent.LastModified;
			set => Parent.LastModified = value;
		}

		public RevenueItem()
		{
			ItemType = LinkBundleItemType.Revenue;
			RevenueType = LinkBundleRevenueType.Generated;
			InfoItems = new List<RevenueInfo>();
		}

		public override void AfterCreate()
		{
			base.AfterCreate();
			InfoItems.ForEach(item => item.ParentRevenue = this);
		}

		public bool IsModified(IChangable latest)
		{
			return latest.LastModified > LastModified;
		}

		public void MarkAsModified()
		{
			OnSettingsChanged();
		}

		public void AddInfo(string title)
		{
			InfoItems.Add(new RevenueInfo(this, title));
			OnSettingsChanged();
		}

		public void DeleteInfo(string title)
		{
			InfoItems.RemoveAll(i => i.Title == title);
			OnSettingsChanged();
		}
	}
}
