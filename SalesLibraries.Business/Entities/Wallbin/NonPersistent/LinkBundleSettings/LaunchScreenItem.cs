using System;
using System.Drawing;
using Newtonsoft.Json;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Enums;

namespace SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkBundleSettings
{
	public class LaunchScreenItem : BaseBundleItem, ILinkBundleInfoItem
	{
		public const string ItemName = "Launch Screen";

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

		private string _footer;
		public string Footer
		{
			get { return _footer; }
			set
			{
				if (_footer != value)
					OnSettingsChanged();
				_footer = value;
			}
		}

		protected Image _logo;
		public Image Logo
		{
			get { return _logo; }
			set
			{
				if (_logo != value)
				{
					OnSettingsChanged();
				}
				_logo = value;
			}
		}

		protected Image _banner;
		public Image Banner
		{
			get { return _banner; }
			set
			{
				if (_banner != value)
				{
					OnSettingsChanged();
				}
				_banner = value;
			}
		}

		private Color _headerForeColor = Color.Black;
		public Color HeaderForeColor
		{
			get { return _headerForeColor; }
			set
			{
				if (_headerForeColor != value)
					OnSettingsChanged();
				_headerForeColor = value;
			}
		}

		private Color _headerBackColor = Color.White;
		public Color HeaderBackColor
		{
			get { return _headerBackColor; }
			set
			{
				if (_headerBackColor != value)
					OnSettingsChanged();
				_headerBackColor = value;
			}
		}

		private Font _headerFont = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);
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

		private Color _footerForeColor = Color.Black;
		public Color FooterForeColor
		{
			get { return _footerForeColor; }
			set
			{
				if (_footerForeColor != value)
					OnSettingsChanged();
				_footerForeColor = value;
			}
		}

		private Color _footerBackColor = Color.White;
		public Color FooterBackColor
		{
			get { return _footerBackColor; }
			set
			{
				if (_footerBackColor != value)
					OnSettingsChanged();
				_footerBackColor = value;
			}
		}

		private Font _footerFont = new Font("Arial", 10, FontStyle.Regular, GraphicsUnit.Point);
		public Font FooterFont
		{
			get { return _footerFont; }
			set
			{
				if (_footerFont != value)
					OnSettingsChanged();
				_footerFont = value;
			}
		}

		[JsonIgnore]
		public override string Name
		{
			get { return ItemName; }
			set { throw new NotImplementedException(); }
		}

		public LaunchScreenItem()
		{
			ItemType = LinkBundleItemType.LaunchScreen;
		}
	}
}
