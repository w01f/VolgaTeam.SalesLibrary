using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Xml;

namespace SalesDepot.CoreObjects.BusinessClasses
{
	public class LibraryPage : ISyncObject
	{
		private bool _applyForAllColumnTitles;
		private bool _enableColumnTitles;
		private string _name = string.Empty;
		private int _order;

		public LibraryPage(ILibrary parent, bool isHome = false)
		{
			Parent = parent;
			_name = isHome ? "Page 1" : string.Format("Page {0}", Parent.Pages.Count + 1);
			Identifier = Guid.NewGuid();
			Folders = new List<LibraryFolder>();
			ColumnTitles = new List<ColumnTitle>();

			var column = new ColumnTitle(this);
			column.Name = "Column 1";
			column.ColumnOrder = 0;
			ColumnTitles.Add(column);
			column = new ColumnTitle(this);
			column.Name = "Column 2";
			column.ColumnOrder = 1;
			ColumnTitles.Add(column);
			column = new ColumnTitle(this);
			column.Name = "Column 3";
			column.ColumnOrder = 2;
			ColumnTitles.Add(column);
		}

		public ILibrary Parent { get; set; }
		public Guid Identifier { get; set; }
		public List<LibraryFolder> Folders { get; set; }
		public List<ColumnTitle> ColumnTitles { get; set; }

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

		public int Order
		{
			get { return _order; }
			set
			{
				if (_order != value)
					LastChanged = DateTime.Now;
				_order = value;
			}
		}

		public bool EnableColumnTitles
		{
			get { return _enableColumnTitles; }
			set
			{
				if (_enableColumnTitles != value)
					LastChanged = DateTime.Now;
				_enableColumnTitles = value;
			}
		}

		public bool ApplyForAllColumnTitles
		{
			get { return _applyForAllColumnTitles; }
			set
			{
				if (_applyForAllColumnTitles != value)
					LastChanged = DateTime.Now;
				_applyForAllColumnTitles = value;
			}
		}

		public int Index
		{
			get { return _order + 1; }
		}

		public DateTime LastChanged { get; set; }

		public override string ToString()
		{
			return Name;
		}

		public string Serialize()
		{
			var result = new StringBuilder();
			result.AppendLine(@"<Name>" + _name.Replace(@"&", "&#38;").Replace(@"<", "&#60;").Replace("\"", "&quot;") + @"</Name>");
			result.AppendLine(@"<Identifier>" + Identifier + @"</Identifier>");
			result.AppendLine(@"<Order>" + _order + @"</Order>");
			result.AppendLine(@"<EnableColumnTitles>" + _enableColumnTitles + @"</EnableColumnTitles>");
			result.AppendLine(@"<ApplyForAllColumnTitles>" + _applyForAllColumnTitles + @"</ApplyForAllColumnTitles>");
			result.AppendLine(@"<LastChanged>" + LastChanged + @"</LastChanged>");
			result.AppendLine("<Folders>");
			foreach (LibraryFolder folder in Folders)
				result.AppendLine(@"<Folder>" + folder.Serialize() + @"</Folder>");
			result.AppendLine("</Folders>");
			result.AppendLine("<ColumnTitles>");
			foreach (ColumnTitle columnTitle in ColumnTitles)
				result.AppendLine(@"<ColumnTitle>" + columnTitle.Serialize() + @"</ColumnTitle>");
			result.AppendLine("</ColumnTitles>");
			return result.ToString();
		}

		public void Deserialize(XmlNode node)
		{
			foreach (XmlNode childNode in node.ChildNodes)
			{
				bool tempBool;
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
					case "Order":
						int tempInt = 0;
						if (int.TryParse(childNode.InnerText, out tempInt))
							_order = tempInt;
						break;
					case "EnableColumnTitles":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_enableColumnTitles = tempBool;
						break;
					case "ApplyForAllColumnTitles":
						if (bool.TryParse(childNode.InnerText, out tempBool))
							_applyForAllColumnTitles = tempBool;
						break;
					case "LastChanged":
						DateTime tempDateTime;
						if (DateTime.TryParse(childNode.InnerText, out tempDateTime))
							LastChanged = tempDateTime;
						break;
					case "Folders":
						Folders.Clear();
						foreach (XmlNode folderNode in childNode.ChildNodes)
						{
							var folder = new LibraryFolder(this);
							folder.Deserialize(folderNode);
							Folders.Add(folder);
						}
						break;
					case "ColumnTitles":
						ColumnTitles.Clear();
						foreach (XmlNode columnTitleNode in childNode.ChildNodes)
						{
							var columnTitle = new ColumnTitle(this);
							columnTitle.Deserialize(columnTitleNode);
							ColumnTitles.Add(columnTitle);
						}
						break;
				}
			}
		}

		public LibraryPage Clone(ILibrary parent)
		{
			var page = new LibraryPage(parent);
			page.Name = Name;
			page.Order = Order;
			page.EnableColumnTitles = EnableColumnTitles;
			page.ApplyForAllColumnTitles = ApplyForAllColumnTitles;
			page.Folders.AddRange(Folders.Select(x => x.Clone(page)));
			page.ColumnTitles.AddRange(ColumnTitles.Select(x => x.Clone(page)));
			return page;
		}

		public ILibraryLink[] SearchByTags(LibraryFileSearchTags searchCriteria)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (var folder in Folders)
				searchFiles.AddRange(folder.SearchByTags(searchCriteria));
			return searchFiles.ToArray();
		}

		public ILibraryLink[] SearchByName(string template, bool fullMatchOnly, FileTypes type)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (var folder in Folders)
				searchFiles.AddRange(folder.SearchByName(template, fullMatchOnly, type));
			return searchFiles.ToArray();
		}

		public ILibraryLink[] SearchByDate(DateTime startDate, DateTime endDate)
		{
			var searchFiles = new List<ILibraryLink>();
			foreach (var folder in Folders)
				searchFiles.AddRange(folder.SearchByDate(startDate, endDate));
			return searchFiles.ToArray();
		}

		public void ReorderFolders(int columnOrder)
		{
			var order = 0;
			foreach (var folder in Folders.Where(f => f.ColumnOrder == columnOrder).OrderBy(f => f.RowOrder))
			{
				folder.RowOrder = order;
				order++;
			}
			LastChanged = DateTime.Now;
		}

		public void ApplyFolderSettings(LibraryFolder libraryFolder, LibraryFolder templateFolder)
		{
			if (templateFolder.Parent.Parent.ApplyAppearanceForAllWindows)
			{
				libraryFolder.BackgroundHeaderColor = templateFolder.BackgroundHeaderColor;
				libraryFolder.ForeHeaderColor = templateFolder.ForeHeaderColor;
				libraryFolder.BackgroundWindowColor = templateFolder.BackgroundWindowColor;
				libraryFolder.ForeWindowColor = templateFolder.ForeWindowColor;
				libraryFolder.BorderColor = templateFolder.BorderColor;
				libraryFolder.HeaderFont = (Font)templateFolder.HeaderFont.Clone();
				libraryFolder.HeaderAlignment = templateFolder.HeaderAlignment;
			}
			if (templateFolder.Parent.Parent.ApplyWidgetForAllWindows)
			{
				libraryFolder.EnableWidget = templateFolder.EnableWidget;
				if (templateFolder.EnableWidget)
					libraryFolder.BannerProperties = templateFolder.BannerProperties.Clone(libraryFolder);
				libraryFolder.Widget = templateFolder.Widget != null ? (Image)templateFolder.Widget.Clone() : null;
			}
			if (templateFolder.Parent.Parent.ApplyBannerForAllWindows)
			{
				libraryFolder.BannerProperties = templateFolder.BannerProperties.Clone(libraryFolder);
				if (templateFolder.BannerProperties.Enable)
				{
					libraryFolder.EnableWidget = templateFolder.EnableWidget;
					libraryFolder.Widget = templateFolder.Widget != null ? (Image)templateFolder.Widget.Clone() : null;
				}
			}
		}

		public void AddFolder(int columnOrder)
		{
			var folder = new LibraryFolder(this);
			folder.ColumnOrder = columnOrder;
			folder.RowOrder = Folders.Any(f => f.ColumnOrder == columnOrder) ? Folders.Where(f => f.ColumnOrder == columnOrder).Max(f => f.RowOrder) + 1 : 0;
			folder.Name = String.Format("Window {0}", folder.RowOrder + 1);
			var defaultFolder = Folders.FirstOrDefault();
			if (defaultFolder != null)
				ApplyFolderSettings(folder, defaultFolder);
			Folders.Add(folder);
			LastChanged = DateTime.Now;
		}

		public void DeleteFolder(int columnOrder, LibraryFolder folder)
		{
			Folders.Remove(folder);
			ReorderFolders(columnOrder);
			LastChanged = DateTime.Now;
		}

		public void SortFolderByName()
		{
			var rowOrder = 0;
			var columnOrder = 0;
			foreach (var folder in Folders.OrderBy(f => f.Name))
			{
				folder.RowOrder = rowOrder;
				folder.ColumnOrder = columnOrder;
				if (columnOrder > 1)
				{
					columnOrder = 0;
					rowOrder++;
				}
				else
					columnOrder++;
			}
			LastChanged = DateTime.Now;
		}

		public void UpFolder(int columnOrder, LibraryFolder folder)
		{
			folder.RowOrder -= 1.5;
			ReorderFolders(columnOrder);
		}

		public void DownFolder(int columnOrder, LibraryFolder folder)
		{
			folder.RowOrder += 1.5;
			ReorderFolders(columnOrder);
		}

		public void MoveFolderToColumn(LibraryFolder folder, int newColumnOrder)
		{
			folder.ColumnOrder = newColumnOrder;
			folder.RowOrder = Folders.Any(f => f.ColumnOrder == newColumnOrder) ? Folders.Where(f => f.ColumnOrder == newColumnOrder).Max(f => f.RowOrder) + 1 : 0;
			ReorderFolders(newColumnOrder);
		}
	}
}