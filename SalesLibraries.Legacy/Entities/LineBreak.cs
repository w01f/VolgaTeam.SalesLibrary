using System;
using System.Drawing;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class LineBreakProperties
	{
		private bool _enableBanner;
		private Font _font;
		private Color _foreColor = Color.Black;
		private DateTime _lastChanged = DateTime.Now;
		private string _note = string.Empty;

		public LineBreakProperties()
		{
			Identifier = Guid.NewGuid();
		}

		public Guid Identifier { get; set; }

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
			set
			{
				_font = value != null ?
					new Font(value, value.Style | FontStyle.Bold) :
					null;
			}
		}

		public bool EnableBanner
		{
			get { return _enableBanner; }
			set
			{
				if (_enableBanner != value)
					LastChanged = DateTime.Now;
				_enableBanner = value;
			}
		}

		public Image Banner { get; set; }

		public string Note
		{
			get { return _note; }
			set
			{
				if (_note != value)
					LastChanged = DateTime.Now;
				_note = value;
			}
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
				switch (childNode.Name)
				{
					case "Identifier":
						Guid tempGuid;
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "Font":
						try
						{
							Font = (Font)converter.ConvertFromString(childNode.InnerText);
						}
						catch { }
						break;
					case "ForeColor":
						int tempInt;
						if (int.TryParse(childNode.InnerText, out tempInt))
							_foreColor = Color.FromArgb(tempInt);
						break;
					case "Note":
						_note = childNode.InnerText;
						break;
				}
			}
		}
	}
}