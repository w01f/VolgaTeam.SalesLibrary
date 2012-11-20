using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class ColumnTitle : ISyncObject
	{
		private DateTime _lastChanged = DateTime.Now;

		public LibraryPage Parent { get; set; }
		private string _name = string.Empty;
		private int _columnOrder = 0;
		private Color _backgroundColor = Color.White;
		private Color _foreColor = Color.Black;
		private Font _headerFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
		private bool _enableText = true;
		private Alignment _headerAlignment = Alignment.Center;
		private bool _enableWidget = false;
		private Image _widget = null;
		public BannerProperties BannerProperties { get; set; }

		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				if (_name != value)
					this.LastChanged = DateTime.Now;
				_name = value;
			}
		}

		public int ColumnOrder
		{
			get
			{
				return _columnOrder;
			}
			set
			{
				if (_columnOrder != value)
					this.LastChanged = DateTime.Now;
				_columnOrder = value;
			}
		}

		public Color BackgroundColor
		{
			get
			{
				return _backgroundColor;
			}
			set
			{
				if (_backgroundColor != value)
					this.LastChanged = DateTime.Now;
				_backgroundColor = value;
			}
		}

		public Color ForeColor
		{
			get
			{
				return _foreColor;
			}
			set
			{
				if (_foreColor != value)
					this.LastChanged = DateTime.Now;
				_foreColor = value;
			}
		}

		public Font HeaderFont
		{
			get
			{
				return _headerFont;
			}
			set
			{
				_headerFont = value;
			}
		}

		public bool EnableText
		{
			get
			{
				return _enableText;
			}
			set
			{
				if (_enableText != value)
					this.LastChanged = DateTime.Now;
				_enableText = value;
			}
		}

		public Alignment HeaderAlignment
		{
			get
			{
				return _headerAlignment;
			}
			set
			{
				if (!_headerAlignment.Equals(value))
					this.LastChanged = DateTime.Now;
				_headerAlignment = value;
			}
		}

		public bool EnableWidget
		{
			get
			{
				return _enableWidget;
			}
			set
			{
				if (_enableWidget != value)
					this.LastChanged = DateTime.Now;
				_enableWidget = value;
			}
		}

		public Image Widget
		{
			get
			{
				return _widget;
			}
			set
			{
				_widget = value;
			}
		}

		public DateTime LastChanged
		{
			get
			{
				return _lastChanged;
			}
			set
			{
				_lastChanged = value;
				this.Parent.LastChanged = _lastChanged;
			}
		}

		public ColumnTitle(LibraryPage parent)
		{
			this.Parent = parent;
			this.BannerProperties = new BannerProperties(this);
		}

		public ColumnTitle Clone (LibraryPage parent)
		{
			ColumnTitle columnTitle = new ColumnTitle(parent);
			columnTitle.Name = this.Name;
			columnTitle.ColumnOrder = this.ColumnOrder;
			columnTitle.BackgroundColor = this.BackgroundColor;
			columnTitle.ForeColor = this.ForeColor;
			columnTitle.HeaderFont = this.HeaderFont;
			columnTitle.EnableText = this.EnableText;
			columnTitle.HeaderAlignment = this.HeaderAlignment;
			columnTitle.EnableWidget = this.EnableWidget;
			columnTitle.Widget = this.Widget;
			columnTitle.BannerProperties = this.BannerProperties.Clone(columnTitle);
			return columnTitle;
		}

		public string Serialize()
		{
			FontConverter converter = new FontConverter();
			TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			StringBuilder result = new StringBuilder();
			result.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
			result.AppendLine(@"<ColumnOrder>" + _columnOrder + @"</ColumnOrder>");
			result.AppendLine(@"<BackgroundColor>" + _backgroundColor.ToArgb() + @"</BackgroundColor>");
			result.AppendLine(@"<ForeColor>" + _foreColor.ToArgb() + @"</ForeColor>");
			result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(_headerFont) + @"</HeaderFont>");
			result.AppendLine(@"<EnableText>" + _enableText + @"</EnableText>");
			result.AppendLine(@"<HeaderAligment>" + ((int)_headerAlignment).ToString() + @"</HeaderAligment>");
			result.AppendLine(@"<EnableWidget>" + _enableWidget + @"</EnableWidget>");
			result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
			result.AppendLine(@"<BannerProperties>" + this.BannerProperties.Serialize() + @"</BannerProperties>");
			result.AppendLine(@"<LastChanged>" + _lastChanged.ToString() + @"</LastChanged>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt = 0;
			bool tempBool;
			DateTime tempDateTime;
			FontConverter converter = new FontConverter();

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Name":
						_name = childNode.InnerText;
						break;
					case "ColumnOrder":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_columnOrder = tempInt;
						break;
					case "BackgroundColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_backgroundColor = Color.FromArgb(tempInt);
						break;
					case "ForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_foreColor = Color.FromArgb(tempInt);
						break;
					case "HeaderFont":
						try
						{
							_headerFont = converter.ConvertFromString(childNode.InnerText) as Font;
						}
						catch
						{
						}
						break;
					case "EnableText":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_enableText = tempBool;
						break;
					case "HeaderAligment":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_headerAlignment = (Alignment)tempInt;
						break;
					case "EnableWidget":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_enableWidget = tempBool;
						break;
					case "Widget":
						if (!string.IsNullOrEmpty(childNode.InnerText))
							_widget = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "BannerProperties":
						this.BannerProperties.Deserialize(childNode);
						break;
					case "LastChanged":
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							_lastChanged = tempDateTime;
						break;
				}
			}
		}
	}
}
