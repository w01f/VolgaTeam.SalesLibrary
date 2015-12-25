using System;
using System.Collections.Generic;
using System.Xml;

namespace SalesLibraries.Legacy.Entities
{
	public class LibraryPage
	{
		private bool _applyForAllColumnTitles;
		private bool _enableColumnTitles;
		private string _name = string.Empty;
		private int _order;

		public LibraryPage(Library parent, bool isHome = false)
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

		public Library Parent { get; set; }
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
	}
}