using System;
using System.Drawing;
using System.IO;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class BannerProperties
	{
		private bool _enable;
		private Font _font = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
		private Color _foreColor = Color.Black;
		private Image _image;
		private Alignment _imageAlignement = Alignment.Left;
		private DateTime _lastChanged = DateTime.Now;
		private bool _showText;
		private string _text = string.Empty;

		public BannerProperties()
		{
			Identifier = Guid.NewGuid();
		}

		public bool Configured { get; set; }
		public Guid Identifier { get; set; }

		public bool Enable
		{
			get { return _enable; }
			set
			{
				if (_enable != value)
					LastChanged = DateTime.Now;
				_enable = value;
			}
		}

		public Image Image
		{
			get { return _image; }
			set
			{
				if (_image != value)
					LastChanged = DateTime.Now;
				_image = value;
			}
		}

		public bool ShowText
		{
			get { return _showText; }
			set
			{
				if (_showText != value)
					LastChanged = DateTime.Now;
				_showText = value;
			}
		}

		public Alignment ImageAlignement
		{
			get { return _imageAlignement; }
			set
			{
				if (_imageAlignement != value)
					LastChanged = DateTime.Now;
				_imageAlignement = value;
			}
		}

		public string Text
		{
			get { return _text; }
			set
			{
				if (_text != value)
					LastChanged = DateTime.Now;
				_text = value;
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

		public Font Font
		{
			get { return _font; }
			set { _font = value; }
		}

		public DateTime LastChanged
		{
			get { return _lastChanged; }
			set { _lastChanged = value; }
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
					case "Identifier":
						Guid tempGuid;
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "Enable":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_enable = tempBool;
						break;
					case "Image":
						if (string.IsNullOrEmpty(childNode.InnerText))
							_image = null;
						else
							_image = new Bitmap(new MemoryStream(Convert.FromBase64String(childNode.InnerText)));
						break;
					case "ImageAligement":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_imageAlignement = (Alignment)tempInt;
						break;
					case "ShowText":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_showText = tempBool;
						break;
					case "Text":
						_text = childNode.InnerText;
						break;
					case "Font":
						try
						{
							_font = converter.ConvertFromString(childNode.InnerText) as Font;
						}
						catch { }
						break;
					case "ForeColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_foreColor = Color.FromArgb(tempInt);
						break;
					case "LastChanged":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							_lastChanged = tempDateTime;
						break;
				}
			}
			Configured = true;
		}
	}
}