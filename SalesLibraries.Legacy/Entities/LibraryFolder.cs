using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class LibraryFolder
	{
		private Color _backgroundHeaderColor = Color.White;
		private Color _backgroundWindowColor = Color.White;
		private Color _borderColor = Color.Black;
		private int _columnOrder;
		private bool _enableWidget;
		private Color _foreHeaderColor = Color.Black;
		private Color _foreWindowColor = Color.Black;
		private Alignment _headerAlignment = Alignment.Center;
		private Font _headerFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Point);
		private DateTime _lastChanged = DateTime.MinValue;
		private string _name = string.Empty;
		private double _rowOrder;
		private Image _widget;
		private Font _windowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point);

		public LibraryFolder(LibraryPage parent)
		{
			Identifier = Guid.NewGuid();
			Parent = parent;
			AddDate = DateTime.Now;

			BannerProperties = new BannerProperties();
			BannerProperties.Font = _headerFont;
			BannerProperties.ForeColor = _foreHeaderColor;

			Files = new List<LibraryLink>();
		}

		public Guid Identifier { get; set; }
		public LibraryPage Parent { get; set; }

		public DateTime AddDate { get; set; }
		public BannerProperties BannerProperties { get; set; }
		public List<LibraryLink> Files { get; set; }

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

		public double RowOrder
		{
			get { return _rowOrder; }
			set
			{
				if (_rowOrder != value)
					LastChanged = DateTime.Now;
				_rowOrder = value;
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

		public Color BorderColor
		{
			get { return _borderColor; }
			set
			{
				if (_borderColor != value)
					LastChanged = DateTime.Now;
				_borderColor = value;
			}
		}

		public Color BackgroundWindowColor
		{
			get { return _backgroundWindowColor; }
			set
			{
				if (_backgroundWindowColor != value)
					LastChanged = DateTime.Now;
				_backgroundWindowColor = value;
			}
		}

		public Color ForeWindowColor
		{
			get { return _foreWindowColor; }
			set
			{
				if (_foreWindowColor != value)
					LastChanged = DateTime.Now;
				_foreWindowColor = value;
			}
		}

		public Color BackgroundHeaderColor
		{
			get { return _backgroundHeaderColor; }
			set
			{
				if (_backgroundHeaderColor != value)
					LastChanged = DateTime.Now;
				_backgroundHeaderColor = value;
			}
		}

		public Color ForeHeaderColor
		{
			get { return _foreHeaderColor; }
			set
			{
				if (_foreHeaderColor != value)
					LastChanged = DateTime.Now;
				_foreHeaderColor = value;
			}
		}

		public Font WindowFont
		{
			get { return _windowFont; }
			set
			{
				if (_windowFont != value)
					LastChanged = DateTime.Now;
				_windowFont = value;
			}
		}

		public Font HeaderFont
		{
			get { return _headerFont; }
			set
			{
				if (_headerFont != value)
					LastChanged = DateTime.Now;
				_headerFont = value;
			}
		}

		public Alignment HeaderAlignment
		{
			get { return _headerAlignment; }
			set
			{
				if (_headerAlignment != value)
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
			set
			{
				if (_widget != value)
					LastChanged = DateTime.Now;
				_widget = value;
			}
		}

		public double AbsoluteRowOrder
		{
			get { return Parent.Folders.Count(x => x.ColumnOrder < ColumnOrder) + Parent.Folders.Where(x => x.ColumnOrder == ColumnOrder).ToList().IndexOf(this); }
		}

		#region ISyncObject Members
		public DateTime LastChanged
		{
			get { return _lastChanged; }
			set
			{
				_lastChanged = value;
				Parent.LastChanged = _lastChanged;
			}
		}
		#endregion

		public void Deserialize(XmlNode node)
		{
			var converter = new FontConverter();

			foreach (XmlNode childNode in node.ChildNodes)
			{
				int tempInt = 0;
				DateTime tempDateTime;
				switch (childNode.Name)
				{
					case "Name":
						_name = childNode.InnerText;
						break;
					case "Identifier":
						Guid tempGuid;
						if (Guid.TryParse(childNode.InnerText, out tempGuid))
							Identifier = tempGuid;
						break;
					case "RowOrder":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_rowOrder = tempInt;
						break;
					case "ColumnOrder":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_columnOrder = tempInt;
						break;
					case "BorderColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_borderColor = Color.FromArgb(tempInt);
						break;
					case "BackgroundWindowColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_backgroundWindowColor = Color.FromArgb(tempInt);
						break;
					case "ForeWindowColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_foreWindowColor = Color.FromArgb(tempInt);
						break;
					case "BackgroundHeaderColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_backgroundHeaderColor = Color.FromArgb(tempInt);
						break;
					case "ForeHeaderColor":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_foreHeaderColor = Color.FromArgb(tempInt);
						break;
					case "WindowFont":
						try
						{
							_windowFont = converter.ConvertFromString(childNode.InnerText) as Font;
						}
						catch { }
						break;
					case "HeaderFont":
						try
						{
							_headerFont = converter.ConvertFromString(childNode.InnerText) as Font;
						}
						catch { }
						break;
					case "HeaderAligment":
						if (int.TryParse(childNode.InnerText, out tempInt))
							_headerAlignment = (Alignment)tempInt;
						break;
					case "EnableWidget":
						bool tempBool;
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
					case "AddDate":
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							AddDate = tempDateTime;
						break;
					case "LastChanged":
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							_lastChanged = tempDateTime;
						break;
					case "Files":
						Files.Clear();
						foreach (XmlNode fileNode in childNode.ChildNodes)
						{
							LibraryLink libraryFile = null;
							var typeNode = fileNode.SelectSingleNode("Type");
							if (typeNode != null && int.TryParse(typeNode.InnerText, out tempInt))
							{
								var type = (FileTypes)tempInt;
								if (type == FileTypes.Folder)
									libraryFile = new LibraryFolderLink(this);
							}
							if (libraryFile == null)
								libraryFile = new LibraryLink(this);
							libraryFile.Deserialize(fileNode);
							Files.Add(libraryFile);
						}

						#region Order Bug Fix
						if (Files.Count > 0)
						{
							int maxOrder = Files.Select(x => x.Order).Max();
							if (maxOrder == 0)
								for (int i = 0; i < Files.Count; i++)
									if (Files[i].Order != i)
										Files[i].Order = i;
						}
						#endregion

						Files.Sort((x, y) => x.Order.CompareTo(y.Order));
						break;
				}
			}
			if (!BannerProperties.Configured)
			{
				BannerProperties.Text = _name;
				BannerProperties.Font = _headerFont;
				BannerProperties.ForeColor = _foreHeaderColor;
			}
		}
	}
}