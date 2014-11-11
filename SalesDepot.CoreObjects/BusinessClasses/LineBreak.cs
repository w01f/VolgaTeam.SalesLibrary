using System;
using System.Drawing;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class LineBreakProperties
	{
		private bool _enableBanner;
		private Font _font;
		private Color _foreColor = Color.Black;
		private DateTime _lastChanged = DateTime.Now;
		private string _note = string.Empty;

		public LineBreakProperties(ILibraryLink parent)
		{
			Parent = parent;
			Identifier = Guid.NewGuid();
			Font = new Font(parent.Parent.WindowFont, parent.Parent.WindowFont.Style);
		}

		public ILibraryLink Parent { get; private set; }
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
			set
			{
				_lastChanged = value;
				Parent.LastChanged = _lastChanged;
			}
		}

		public LineBreakProperties Clone(ILibraryLink parent)
		{
			var lineBreak = new LineBreakProperties(parent);
			lineBreak.ForeColor = ForeColor;
			if (Font != null)
				lineBreak.Font = new Font(Font, Font.Style);
			lineBreak.Note = Note;
			return lineBreak;
		}

		public string Serialize()
		{
			var fontConverter = new FontConverter();
			var result = new StringBuilder();
			result.AppendLine(@"<Identifier>" + Identifier + @"</Identifier>");
			result.AppendLine(@"<Font>" + fontConverter.ConvertToString(_font) + @"</Font>");
			result.AppendLine(@"<ForeColor>" + _foreColor.ToArgb() + @"</ForeColor>");
			result.AppendLine(@"<Note>" + _note.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Note>");
			return result.ToString();
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