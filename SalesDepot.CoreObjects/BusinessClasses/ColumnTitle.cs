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
		private Color _backgroundColor = Color.White;
		private int _columnOrder;
		private bool _enableText = true;
		private bool _enableWidget;
		private Color _foreColor = Color.Black;
		private Alignment _headerAlignment = Alignment.Center;
		private Font _headerFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Pixel);
		private DateTime _lastChanged = DateTime.Now;
		private string _name = string.Empty;
		private Image _widget;

		public ColumnTitle(LibraryPage parent)
		{
			Parent = parent;
			BannerProperties = new BannerProperties(this);
		}

		public LibraryPage Parent { get; set; }

		public BannerProperties BannerProperties { get; set; }

		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
					LastChanged = DateTime.Now;
				_name = value;
			}
		}

		public int ColumnOrder
		{
			get { return _columnOrder; }
			set
			{
				if (_columnOrder != value)
					LastChanged = DateTime.Now;
				_columnOrder = value;
			}
		}

		public Color BackgroundColor
		{
			get { return _backgroundColor; }
			set
			{
				if (_backgroundColor != value)
					LastChanged = DateTime.Now;
				_backgroundColor = value;
			}
		}

		public Color ForeColor
		{
			get { return _foreColor; }
			set
			{
				if (_foreColor != value)
					LastChanged = DateTime.Now;
				_foreColor = value;
			}
		}

		public Font HeaderFont
		{
			get { return _headerFont; }
			set { _headerFont = value; }
		}

		public bool EnableText
		{
			get { return _enableText; }
			set
			{
				if (_enableText != value)
					LastChanged = DateTime.Now;
				_enableText = value;
			}
		}

		public Alignment HeaderAlignment
		{
			get { return _headerAlignment; }
			set
			{
				if (!_headerAlignment.Equals(value))
					LastChanged = DateTime.Now;
				_headerAlignment = value;
			}
		}

		public bool EnableWidget
		{
			get { return _enableWidget; }
			set
			{
				if (_enableWidget != value)
					LastChanged = DateTime.Now;
				_enableWidget = value;
			}
		}

		public Image Widget
		{
			get { return _widget; }
			set { _widget = value; }
		}

		public DateTime LastChanged
		{
			get { return _lastChanged; }
			set
			{
				_lastChanged = value;
				Parent.LastChanged = _lastChanged;
			}
		}

		public ColumnTitle Clone(LibraryPage parent)
		{
			var columnTitle = new ColumnTitle(parent);
			columnTitle.Name = Name;
			columnTitle.ColumnOrder = ColumnOrder;
			columnTitle.BackgroundColor = BackgroundColor;
			columnTitle.ForeColor = ForeColor;
			columnTitle.HeaderFont = HeaderFont;
			columnTitle.EnableText = EnableText;
			columnTitle.HeaderAlignment = HeaderAlignment;
			columnTitle.EnableWidget = EnableWidget;
			columnTitle.Widget = Widget;
			columnTitle.BannerProperties = BannerProperties.Clone(columnTitle);
			return columnTitle;
		}

		public string Serialize()
		{
			var converter = new FontConverter();
			TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
			result.AppendLine(@"<ColumnOrder>" + _columnOrder + @"</ColumnOrder>");
			result.AppendLine(@"<BackgroundColor>" + _backgroundColor.ToArgb() + @"</BackgroundColor>");
			result.AppendLine(@"<ForeColor>" + _foreColor.ToArgb() + @"</ForeColor>");
			result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(_headerFont) + @"</HeaderFont>");
			result.AppendLine(@"<EnableText>" + _enableText + @"</EnableText>");
			result.AppendLine(@"<HeaderAligment>" + ((int)_headerAlignment) + @"</HeaderAligment>");
			result.AppendLine(@"<EnableWidget>" + _enableWidget + @"</EnableWidget>");
			result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
			result.AppendLine(@"<BannerProperties>" + BannerProperties.Serialize() + @"</BannerProperties>");
			result.AppendLine(@"<LastChanged>" + _lastChanged + @"</LastChanged>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			var converter = new FontConverter();

			foreach (XmlNode childNode in node.ChildNodes)
			{
				int tempInt;
				bool tempBool;
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
						catch { }
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
						BannerProperties.Deserialize(childNode);
						break;
					case "LastChanged":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							_lastChanged = tempDateTime;
						break;
				}
			}
		}
	}
}