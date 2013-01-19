using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class LibraryFolder : ISyncObject
	{
		private Color _backgroundHeaderColor = Color.White;
		private Color _backgroundWindowColor = Color.White;
		private Color _borderColor = Color.Black;
		private int _columnOrder;
		private bool _enableWidget;
		private Color _foreHeaderColor = Color.Black;
		private Color _foreWindowColor = Color.Black;
		private Alignment _headerAlignment = Alignment.Center;
		private Font _headerFont = new Font("Arial", 12, FontStyle.Regular, GraphicsUnit.Pixel);
		private DateTime _lastChanged = DateTime.MinValue;
		private string _name = string.Empty;
		private double _rowOrder;
		private Image _widget;
		private Font _windowFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Pixel);

		public LibraryFolder(LibraryPage parent)
		{
			Identifier = Guid.NewGuid();
			Parent = parent;
			AddDate = DateTime.Now;

			BannerProperties = new BannerProperties(this);
			BannerProperties.Font = _headerFont;
			BannerProperties.ForeColor = _foreHeaderColor;

			Files = new List<ILibraryLink>();
		}

		public Guid Identifier { get; set; }
		public LibraryPage Parent { get; set; }

		public DateTime AddDate { get; set; }
		public BannerProperties BannerProperties { get; set; }
		public List<ILibraryLink> Files { get; set; }

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
			get { return Parent.Folders.Where(x => x.ColumnOrder < ColumnOrder).Count() + Parent.Folders.Where(x => x.ColumnOrder == ColumnOrder).ToList().IndexOf(this); }
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

		public LibraryFolder Clone(LibraryPage parent)
		{
			var folder = new LibraryFolder(parent);
			folder.Name = Name;
			folder.RowOrder = RowOrder;
			folder.ColumnOrder = ColumnOrder;
			folder.BorderColor = BorderColor;
			folder.BackgroundWindowColor = BackgroundWindowColor;
			folder.ForeWindowColor = ForeWindowColor;
			folder.BackgroundHeaderColor = BackgroundHeaderColor;
			folder.ForeHeaderColor = ForeHeaderColor;
			folder.WindowFont = WindowFont;
			folder.HeaderFont = HeaderFont;
			folder.HeaderAlignment = HeaderAlignment;
			folder.EnableWidget = EnableWidget;
			folder.Widget = Widget;
			folder.AddDate = AddDate;
			folder.BannerProperties = BannerProperties.Clone(folder);
			folder.Files.AddRange(Files.Select(x => x.Clone(folder)));
			return folder;
		}

		public string Serialize()
		{
			var converter = new FontConverter();
			TypeConverter imageConverter = TypeDescriptor.GetConverter(typeof(Bitmap));
			var result = new StringBuilder();
			result.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
			result.AppendLine(@"<Identifier>" + Identifier.ToString() + @"</Identifier>");
			result.AppendLine(@"<RowOrder>" + _rowOrder + @"</RowOrder>");
			result.AppendLine(@"<ColumnOrder>" + _columnOrder + @"</ColumnOrder>");
			result.AppendLine(@"<BorderColor>" + _borderColor.ToArgb() + @"</BorderColor>");
			result.AppendLine(@"<BackgroundWindowColor>" + _backgroundWindowColor.ToArgb() + @"</BackgroundWindowColor>");
			result.AppendLine(@"<ForeWindowColor>" + _foreWindowColor.ToArgb() + @"</ForeWindowColor>");
			result.AppendLine(@"<BackgroundHeaderColor>" + _backgroundHeaderColor.ToArgb() + @"</BackgroundHeaderColor>");
			result.AppendLine(@"<ForeHeaderColor>" + _foreHeaderColor.ToArgb() + @"</ForeHeaderColor>");
			result.AppendLine(@"<WindowFont>" + converter.ConvertToString(_windowFont) + @"</WindowFont>");
			result.AppendLine(@"<HeaderFont>" + converter.ConvertToString(_headerFont) + @"</HeaderFont>");
			result.AppendLine(@"<HeaderAligment>" + ((int)_headerAlignment).ToString() + @"</HeaderAligment>");
			result.AppendLine(@"<EnableWidget>" + _enableWidget + @"</EnableWidget>");
			if (_widget != null)
				result.AppendLine(@"<Widget>" + Convert.ToBase64String((byte[])imageConverter.ConvertTo(_widget, typeof(byte[]))).Replace(@"&", "&#38;").Replace("\"", "&quot;") + @"</Widget>");
			result.AppendLine(@"<BannerProperties>" + BannerProperties.Serialize() + @"</BannerProperties>");
			result.AppendLine(@"<AddDate>" + AddDate.ToString() + @"</AddDate>");
			result.AppendLine(@"<LastChanged>" + (_lastChanged != DateTime.MinValue ? _lastChanged.ToString() : DateTime.Now.ToString()) + @"</LastChanged>");
			result.AppendLine("<Files>");
			foreach (ILibraryLink file in Files)
				result.AppendLine(@"<File>" + file.Serialize() + @"</File>");
			result.AppendLine("</Files>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			int tempInt = 0;
			bool tempBool;
			var converter = new FontConverter();
			Guid tempGuid;
			DateTime tempDateTime;

			foreach (XmlNode childNode in node.ChildNodes)
			{
				switch (childNode.Name)
				{
					case "Name":
						_name = childNode.InnerText;
						break;
					case "Identifier":
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
							var file = Parent.Parent.GetLinkInstance(this, fileNode);
							file.Deserialize(fileNode);
							Files.Add(file);
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

		public void RemoveFromParent()
		{
			Parent.Folders.Remove(this);
			Parent.LastChanged = DateTime.Now;
		}

		public ILibraryLink[] SearchByTags(LibraryFileSearchTags searchCriteria)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (ILibraryLink file in Files.Where(x => x.Type != FileTypes.LineBreak))
			{
				bool fullMatch = true;
				bool partialMatch = false;
				foreach (SearchGroup group in searchCriteria.SearchGroups)
				{
					SearchGroup fileSearchGroup = file.SearchTags.SearchGroups.Where(x => x.Name.Equals(group.Name)).FirstOrDefault();
					if (fileSearchGroup != null)
					{
						foreach (SearchTag tag in group.Tags)
							if (fileSearchGroup.Tags.Select(x => x.Name).Contains(tag.Name))
							{
								partialMatch = true;
								fullMatch = fullMatch & true;
							}
							else
							{
								fullMatch = fullMatch & false;
							}
					}
					else
						fullMatch = fullMatch & false;
				}
				if (partialMatch)
				{
					file.CriteriaOverlap = fullMatch ? "meet ALL of your Search Criteria" : "meet SOME of your Search Criteria";
					searchFiles.Add(file);
				}
				else
					file.CriteriaOverlap = string.Empty;
			}
			return searchFiles.ToArray();
		}

		public ILibraryLink[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
		{
			string[] templateParts = template.Split(' ');
			var searchFiles = new List<ILibraryLink>();
			foreach (ILibraryLink file in Files.Where(x => x.Type != FileTypes.LineBreak))
			{
				bool fullMatch = false;
				bool partialMatch = false;

				if (file.Name.ToLower().Equals(template) && !string.IsNullOrEmpty(template) && ((type == file.Type) || (type == FileTypes.Other)))
				{
					fullMatch = true;
					partialMatch = true;
				}
				else if (!string.IsNullOrEmpty(template) && ((type == file.Type) || (type == FileTypes.Other)))
				{
					if (templateParts.Length > 1)
					{
						foreach (string templatePart in templateParts)
							if (file.Name.ToLower().Contains(templatePart.Trim().ToLower()))
							{
								fullMatch = false;
								partialMatch = true;
								break;
							}
					}
					else if (file.Name.ToLower().Contains(template))
					{
						fullMatch = false;
						partialMatch = true;
					}
				}

				if ((partialMatch && !fullMatchOnly) || fullMatch)
				{
					file.CriteriaOverlap = fullMatch ? "meet ALL of your Search Criteria" : "meet SOME of your Search Criteria";
					searchFiles.Add(file);
				}
			}
			return searchFiles.ToArray();
		}

		public ILibraryLink[] SearchByDate(DateTime startDate, DateTime endDate)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (ILibraryLink file in Files.Where(x => x.Type != FileTypes.LineBreak))
			{
				bool fullMatch = false;

				if (file.AddDate >= startDate && file.AddDate <= endDate)
					fullMatch = true;

				if (fullMatch)
				{
					file.CriteriaOverlap = "meet ALL of your Search Criteria";
					searchFiles.Add(file);
				}
				else
					file.CriteriaOverlap = string.Empty;
			}
			return searchFiles.ToArray();
		}
	}
}