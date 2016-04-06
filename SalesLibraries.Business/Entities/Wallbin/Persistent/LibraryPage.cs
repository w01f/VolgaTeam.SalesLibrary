using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Newtonsoft.Json;
using SalesLibraries.Business.Contexts.Wallbin;
using SalesLibraries.Business.Entities.Common;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;

namespace SalesLibraries.Business.Entities.Wallbin.Persistent
{
	public class LibraryPage : WallbinCollectionEntity
	{
		public const int ColumnsCount = 3;

		#region Persistent Properties
		private string _name;
		[Required]
		public string Name
		{
			get { return _name; }
			set
			{
				if (_name != value)
					MarkAsModified();
				_name = value;
			}
		}

		private int _order;
		[Required]
		public int Order
		{
			get { return _order; }
			set
			{
				if (_order != value)
					MarkAsModified();
				_order = value;
			}
		}
		public string SettingsEncoded { get; set; }
		public virtual Library Library { get; set; }
		public virtual ICollection<LibraryFolder> Folders { get; set; }
		public virtual ICollection<ColumnTitle> ColumnTitles { get; set; }
		#endregion

		#region Nonpersistent Properties
		[NotMapped, JsonIgnore]
		public override IChangable Parent
		{
			get { return Library; }
		}

		private LibraryPageSettings _settings;
		[NotMapped, JsonIgnore]
		public LibraryPageSettings Settings
		{
			get { return _settings ?? (_settings = SettingsContainer.CreateInstance<LibraryPageSettings>(this, SettingsEncoded)); }
			set { _settings = value; }
		}

		[NotMapped, JsonIgnore]
		public override int CollectionOrder
		{
			get { return Order; }
			set { Order = value; }
		}

		[NotMapped, JsonIgnore]
		public IEnumerable<BaseLibraryLink> TopLevelLinks
		{
			get { return Folders.SelectMany(f => f.Links); }
		}

		[NotMapped, JsonIgnore]
		public IEnumerable<BaseLibraryLink> AllLinks
		{
			get { return Folders.SelectMany(f => f.AllLinks); }
		}
		#endregion

		public LibraryPage()
		{
			Folders = new List<LibraryFolder>();
			ColumnTitles = new Collection<ColumnTitle>();
		}

		public override string ToString()
		{
			return Name;
		}

		public override void BeforeSave()
		{
			SettingsEncoded = Settings.Serialize();
			foreach (var libraryFolder in Folders)
				libraryFolder.BeforeSave();
			foreach (var columnTitle in ColumnTitles)
				columnTitle.BeforeSave();
		}

		public override void AfterSave()
		{
			Settings = null;
		}

		public override void Save(LibraryContext context, IDbEntity<LibraryContext> current, bool withCommit = true)
		{
			var currentPage = (LibraryPage)current;
			Folders.Save(currentPage.Folders, context);
			Folders.Sort();
			ColumnTitles.Save(currentPage.ColumnTitles, context);
			base.Save(context, current, withCommit);
		}

		public override void Delete(LibraryContext context)
		{
			foreach (var libraryFolder in Folders.ToList())
				libraryFolder.Delete(context);
			Folders.Clear();
			foreach (var columnTitle in ColumnTitles.ToList())
				columnTitle.Delete(context);
			ColumnTitles.Clear();
			base.Delete(context);
		}

		public override void ResetParent()
		{
			Library = null;
		}

		public void ApplyFolderSettings(LibraryFolder templateFolder)
		{
			foreach (var targetFolder in Folders.Where(f => f.ExtId != templateFolder.ExtId))
				ApplyFolderSettings(targetFolder, templateFolder);
		}

		public void ApplyFolderSettings(LibraryFolder targetFolder, LibraryFolder templateFolder)
		{
			if (Library.Settings.ApplyAppearanceForAllWindows)
			{
				targetFolder.Settings = templateFolder.Settings.Clone<LibraryFolder.LibraryFolderSettings>(targetFolder);
				targetFolder.MarkAsModified();
			}
			if (Library.Settings.ApplyWidgetForAllWindows)
			{
				targetFolder.Widget = templateFolder.Widget.Clone<WidgetSettings>(targetFolder);
				targetFolder.MarkAsModified();
			}
			if (Library.Settings.ApplyBannerForAllWindows)
			{
				targetFolder.Banner = templateFolder.Banner.Clone<BannerSettings>(targetFolder);
				targetFolder.MarkAsModified();
			}
		}

		public void ApplyColumnTitleSettings(ColumnTitle templateColumnTitle)
		{
			if (!Settings.ApplyForAllColumnTitles) return;
			foreach (var targetColumnTitle in ColumnTitles.Where(f => f.ExtId != templateColumnTitle.ExtId))
			{
				targetColumnTitle.Settings = templateColumnTitle.Settings.Clone<ColumnTitle.ColumnTitleSettings>(targetColumnTitle);
				targetColumnTitle.Widget = templateColumnTitle.Widget.Clone<WidgetSettings>(targetColumnTitle);
				targetColumnTitle.Banner = templateColumnTitle.Banner.Clone<BannerSettings>(targetColumnTitle);
				targetColumnTitle.MarkAsModified();
			}
		}

		public IEnumerable<ColumnTitle> GetColumnTitles()
		{
			if (!ColumnTitles.Any())
				for (int i = 0; i < ColumnsCount; i++)
				{
					var columnTitle = new ColumnTitle();
					columnTitle.ColumnOrder = i;
					columnTitle.Page = this;
					ColumnTitles.Add(columnTitle);
				}
			return ColumnTitles;
		}

		public void AddFolder(int columnOrder)
		{
			var folder = new LibraryFolder();
			folder.Name = String.Format("Window {0}", (folder.RowOrder + 1).ToString("#,##0"));
			var defaultFolder = Folders.FirstOrDefault();
			if (defaultFolder != null)
				ApplyFolderSettings(folder, defaultFolder);
			var rowOrder = Folders.Any(f => f.ColumnOrder == columnOrder) ? Folders.Where(f => f.ColumnOrder == columnOrder).Max(f => f.RowOrder) + 1 : 0;
			AddFolder(folder, columnOrder, rowOrder);
		}

		public void AddFolder(LibraryFolder folder, int columnOrder, int rowOrder)
		{
			folder.Page = this;
			folder.ColumnOrder = columnOrder;
			Folders.AddItem(folder);
		}

		public void AlignFoldersByColumns()
		{
			var rowOrder = 0;
			var columnOrder = 0;
			foreach (var folder in Folders.OrderBy(f => f.Name))
			{
				folder.RowOrder = rowOrder;
				folder.ColumnOrder = columnOrder;
				if (columnOrder == (ColumnsCount - 1))
				{
					columnOrder = 0;
					rowOrder++;
				}
				else
					columnOrder++;
			}
			MarkAsModified();
		}

		public void AlignFoldersByRows()
		{
			var rowCount = Math.Ceiling((Decimal)Folders.Count / ColumnsCount);
			var rowOrder = 0;
			var columnOrder = 0;
			foreach (var folder in Folders.OrderBy(f => f.Name))
			{
				folder.RowOrder = rowOrder;
				folder.ColumnOrder = columnOrder;
				if (rowOrder == (rowCount - 1))
				{
					rowOrder = 0;
					columnOrder++;
				}
				else
					rowOrder++;
			}
			MarkAsModified();
		}

		public void MoveFolderToColumn(LibraryFolder folder, int newColumnOrder)
		{
			MoveFolderToPosition(folder, newColumnOrder, -1);
		}

		public void MoveFolderToPosition(LibraryFolder folder, int newColumnOrder, int newRowOrder)
		{
			var rowOrder = newRowOrder == -1 ?
				Folders.Count(f => f.ColumnOrder == newColumnOrder) :
				newRowOrder;
			foreach (var libraryFolder in Folders.Where(f => f.ColumnOrder == newColumnOrder && f.RowOrder >= rowOrder))
				libraryFolder.RowOrder++;
			folder.RowOrder = rowOrder;
			folder.ColumnOrder = newColumnOrder;
			for (var i = 0; i < ColumnsCount; i++)
			{
				Folders.ResetItemsOrder(item => item.ColumnOrder == i);
			}
		}

		public void RemoveLinks()
		{
			foreach (var link in TopLevelLinks.ToList())
				link.Delete(link.ParentLibrary.Context);
			foreach (var libraryFolder in Folders)
				libraryFolder.Links.Clear();
		}

		public class LibraryPageSettings : SettingsContainer
		{
			private bool _enableColumnTitles;
			public bool EnableColumnTitles
			{
				get { return _enableColumnTitles; }
				set
				{
					if (_enableColumnTitles != value)
						OnSettingsChanged();
					_enableColumnTitles = value;
				}
			}

			private bool _applyForAllColumnTitles;
			public bool ApplyForAllColumnTitles
			{
				get { return _applyForAllColumnTitles; }
				set
				{
					if (_applyForAllColumnTitles != value)
						OnSettingsChanged();
					_applyForAllColumnTitles = value;
				}
			}
		}
	}
}
