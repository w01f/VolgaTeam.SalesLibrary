using System;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class InfoItem : BaseBundleItem, ILinkBundleInfoItem
	{
		public const string ItemName = "Info";

		private string _header;
		public string Header
		{
			get { return _header; }
			set
			{
				if (_header != value)
					OnSettingsChanged();
				_header = value;
			}
		}

		private string _body;
		public string Body
		{
			get { return _body; }
			set
			{
				if (_body != value)
					OnSettingsChanged();
				_body = value;
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

		private Color _backColor = Color.White;
		public Color BackColor
		{
			get { return _backColor; }
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
			get { return _font; }
			set
			{
				if (_font != value)
					OnSettingsChanged();
				_font = value;
			}
		}

		[JsonIgnore]
		public override string Name
		{
			get { return ItemName; }
			set { throw new NotImplementedException(); }
		}

		public InfoItem()
		{
			ItemType = LinkBundleItemType.Info;
		}
	}
}
