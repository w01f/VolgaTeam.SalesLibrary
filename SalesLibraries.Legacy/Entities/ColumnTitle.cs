using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class ColumnTitle
	{
		private Color _backgroundColor = Color.White;
		private int _columnOrder;
		private bool _enableText = true;
		private bool _enableWidget;
		private Color _foreColor = Color.Black;
		private Alignment _headerAlignment = Alignment.Center;
		private Font _headerFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);
		private DateTime _lastChanged = DateTime.Now;
		private string _name = string.Empty;
		private Image _widget;

		public ColumnTitle(LibraryPage parent)
		{
			Parent = parent;
			BannerProperties = new BannerProperties();
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