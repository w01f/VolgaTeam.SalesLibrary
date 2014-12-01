using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraTab;
using FileManager.ConfigurationClasses;
using FileManager.Controllers;
using SalesDepot.CoreObjects.BusinessClasses;

namespace FileManager.PresentationClasses.WallBin.Decorators
{
	public class PageDecorator
	{
		private readonly List<FolderBoxControl> _boxes = new List<FolderBoxControl>();
		private readonly Timer _scrooTimer = new Timer();
		private XtraScrollableControl _container = new XtraScrollableControl();

		private Panel _headerPanel;
		private bool _isDisposed;
		private Panel _parentPanel;
		private bool _scroolDown;
		private Panel _splash = new Panel();
		private WallBinOptions _wallbinOptions;

		public PageDecorator(LibraryPage page)
		{
			Page = page;
			TabPage = new XtraTabPage();
			RegularPage = new Control { Dock = DockStyle.Fill };
			SelectedLinks = new List<LibraryLink>();
			TabPage.Tag = this;
			TabPage.Text = page.Name.Replace("&", "&&");
			_scrooTimer.Stop();
			_scrooTimer.Interval = 5;
			_scrooTimer.Tick += scrooTimer_Tick;

			BuildPage();
			BuildDisplayBoxes();
			LinkBoxesToColumns();

			Application.DoEvents();
		}

		public LibraryPage Page { get; private set; }

		public XtraTabPage TabPage { get; private set; }
		public Control RegularPage { get; private set; }
		public LibraryDecorator Parent { get; set; }
		public FolderBoxControl ActiveBox { get; set; }

		public void Dispose()
		{
			_isDisposed = true;

			foreach (FolderBoxControl box in _boxes)
			{
				box.Parent = null;
				box.Delete();
				Application.DoEvents();
			}
			_boxes.Clear();

			_headerPanel.Parent = null;
			_headerPanel.Dispose();
			_headerPanel = null;

			_parentPanel.Parent = null;
			_parentPanel.Dispose();
			_parentPanel = null;

			_container.Parent = null;
			_container.Dispose();
			_container = null;

			_splash.Dispose();
			_splash = null;

			TabPage.Parent = null;
			TabPage.Dispose();
			TabPage = null;
			RegularPage.Parent = null;
			RegularPage.Dispose();
			RegularPage = null;
			Application.DoEvents();
		}

		private void BuildDisplayBoxes()
		{
			foreach (FolderBoxControl box in _boxes)
				box.Dispose();
			_boxes.Clear();

			Page.Folders.Sort((x, y) => x.ColumnOrder.CompareTo(y.ColumnOrder) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.ColumnOrder.CompareTo(y.ColumnOrder));
			foreach (var folder in Page.Folders)
			{
				var box = new FolderBoxControl { Folder = folder, Decorator = this };
				box.Init();
				if (_wallbinOptions != null)
					box.ApplyWallBinOptions(_wallbinOptions);
				_boxes.Add(box);
				Application.DoEvents();
			}
		}

		private void BuildPage()
		{
			_container.Dock = DockStyle.Fill;
			_container.AlwaysScrollActiveControlIntoView = false;
			_splash.BorderStyle = BorderStyle.None;
			_splash.Dock = DockStyle.Fill;
			BuildColumnTitles();
			BuildColumns();
		}

		private void BuildColumnTitles()
		{
			_headerPanel = new Panel { BorderStyle = BorderStyle.None, Height = 0, Dock = DockStyle.Top };
			_container.Controls.Add(_headerPanel);
			_headerPanel.BringToFront();

			if (Page.EnableColumnTitles)
			{
				foreach (ColumnTitle columnTitle in Page.ColumnTitles)
				{
					var columnTitleControl = new ColumnTitleControl(columnTitle) { Dock = columnTitle.ColumnOrder == 2 ? DockStyle.Fill : DockStyle.Left };
					_headerPanel.Controls.Add(columnTitleControl);
					columnTitleControl.BringToFront();
					Application.DoEvents();
				}
			}
		}

		private void BuildColumns()
		{
			_parentPanel = new Panel { BorderStyle = BorderStyle.None, Height = _container.Height, Dock = DockStyle.Top };
			_container.Controls.Add(_parentPanel);
			_parentPanel.BringToFront();

			var panel = new ColumnPanel { BorderStyle = BorderStyle.None, AllowDrop = true, Dock = DockStyle.Left, Order = 0 };
			panel.MouseMove += (sender, e) => ((Control)sender).Parent.Parent.Focus();
			panel.DragEnter += ColumnDragEnter;
			panel.DragOver += ColumnDragOver;
			panel.DragLeave += ColumnDragLeave;
			panel.DragDrop += ColumnDragDrop;
			_parentPanel.Controls.Add(panel);
			panel.BringToFront();

			panel = new ColumnPanel { BorderStyle = BorderStyle.None, AllowDrop = true, Dock = DockStyle.Left, Order = 1 };
			panel.MouseMove += (sender, e) => ((Control)sender).Parent.Parent.Focus();
			panel.DragEnter += ColumnDragEnter;
			panel.DragOver += ColumnDragOver;
			panel.DragLeave += ColumnDragLeave;
			panel.DragDrop += ColumnDragDrop;
			_parentPanel.Controls.Add(panel);
			panel.BringToFront();

			panel = new ColumnPanel { BorderStyle = BorderStyle.None, AllowDrop = true, Dock = DockStyle.Fill, Order = 2 };
			panel.MouseMove += (sender, e) => ((Control)sender).Parent.Parent.Focus();
			panel.DragEnter += ColumnDragEnter;
			panel.DragOver += ColumnDragOver;
			panel.DragLeave += ColumnDragLeave;
			panel.DragDrop += ColumnDragDrop;
			_parentPanel.Controls.Add(panel);
			panel.BringToFront();

			Application.DoEvents();
		}

		private void LinkBoxesToColumns()
		{
			_boxes.Sort((x, y) => x.Column.CompareTo(y.Column) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.Column.CompareTo(y.Column));
			foreach (Control control in _parentPanel.Controls)
				control.Controls.Clear();
			foreach (var box in _boxes)
				_parentPanel.Controls[2 - box.Column].Controls.Add(box);
		}

		private void ReorderBoxes()
		{
			int boxOrder = -1;
			int currentColumn = -1;

			_boxes.Sort((x, y) => x.Column.CompareTo(y.Column) == 0 ? x.RowOrder.CompareTo(y.RowOrder) : x.Column.CompareTo(y.Column));
			foreach (var t in _boxes)
			{
				if (currentColumn != t.Column)
				{
					currentColumn = t.Column;
					boxOrder = 0;
				}
				t.RowOrder = boxOrder;
				boxOrder++;
			}
		}

		public void RefreshPanelHeight()
		{
			if (_isDisposed) return;
			int maxHeight = _container.Height - (_headerPanel != null ? _headerPanel.Height : 0);

			int realHeight = 0;
			for (int i = 0; i < 3; i++)
			{
				int columnHeight = _boxes.Where(box => box.Column == i).Sum(box => box.Height);
				if (realHeight < columnHeight)
					realHeight = columnHeight;
			}

			if (realHeight < maxHeight)
				realHeight = maxHeight;

			_parentPanel.Height = realHeight;
		}

		private void FitColumnsToPage()
		{
			int panelWidth = _headerPanel.Width / 3;
			int panelHeight = 0;
			foreach (Control panel in _headerPanel.Controls)
			{
				panel.Width = panelWidth;
				var columnTitleControl = panel as ColumnTitleControl;
				if (columnTitleControl == null) continue;
				int controlHeight = columnTitleControl.GetHeight();
				if (panelHeight < controlHeight)
					panelHeight = controlHeight;
			}
			_headerPanel.Height = panelHeight;
		}

		private void FitObjectsToPage()
		{
			foreach (var box in _boxes)
				box.SetGridFont(SettingsManager.Instance.FontSize);

			RefreshPanelHeight();

			foreach (Control panel in _parentPanel.Controls)
			{
				((ColumnPanel)panel).ResizePanel();
				if (((ColumnPanel)panel).Order < 2)
				{
					panel.Dock = DockStyle.Left;
				}
				else
				{
					panel.Dock = DockStyle.Fill;
					panel.BringToFront();
				}
			}
		}

		public void ResizePage()
		{
			if (!_isDisposed)
			{
				_splash.BringToFront();
				FitColumnsToPage();
				FitObjectsToPage();
				_container.BringToFront();
			}
		}

		public void SwitchMultitab(bool multitab)
		{
			if (!_isDisposed)
			{
				if (multitab)
				{
					if (!TabPage.Controls.Contains(_container))
					{
						_container.Parent = null;
						_splash.Parent = null;
						TabPage.Controls.Add(_container);
						TabPage.Controls.Add(_splash);
					}
				}
				else
				{
					if (!RegularPage.Controls.Contains(_container))
					{
						_container.Parent = null;
						_splash.Parent = null;
						RegularPage.Controls.Add(_container);
						RegularPage.Controls.Add(_splash);
					}
				}
			}
		}

		public void ApplyWallBinOptions(WallBinOptions options)
		{
			_wallbinOptions = options;
			if (ActiveBox != null)
				ActiveBox.MakeInactive();
			ClearSelection();
			foreach (var folderBoxControl in _boxes)
				folderBoxControl.ApplyWallBinOptions(options);
		}

		public void Save()
		{
			if (!_isDisposed)
				foreach (var box in _boxes)
					box.Save();
		}

		#region Drag&Drop Stuff
		public void ColumnDragEnter(object sender, DragEventArgs e)
		{
			if (e.Data.GetDataPresent(DataFormats.Serializable, true))
			{
				object data = e.Data.GetData(DataFormats.Serializable, true);
				if (data.GetType() == typeof(FolderBoxControl))
				{
					e.Effect = DragDropEffects.Move;
				}
			}
		}

		public void ColumnDragOver(object sender, DragEventArgs e)
		{
			Point point = _container.PointToClient(new Point(e.X, e.Y));

			if (point.Y < _container.Bottom && point.Y > _container.Bottom - 50)
			{
				_scroolDown = true;
				_scrooTimer.Start();
			}
			else if (point.Y > _container.Top && point.Y < _container.Top + 50)
			{
				_scroolDown = false;
				_scrooTimer.Start();
			}
			else
				_scrooTimer.Stop();
			if (e.Data.GetDataPresent(DataFormats.Serializable, true))
			{
				object data = e.Data.GetData(DataFormats.Serializable, true);
				if (data.GetType() == typeof(FolderBoxControl))
				{
					var panel = (ColumnPanel)sender;
					Point pt = panel.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
					int bottomBoxBorder = 0;
					foreach (Control control in panel.Controls)
					{
						if (control.Top <= pt.Y && control.Bottom >= pt.Y && control != data)
						{
							((FolderBoxControl)control).UnderlineBox = true;
							if (panel.DropHintLineHeight != control.Top)
							{
								panel.DropHintLineHeight = control.Top;
								panel.Refresh();
							}
						}
						else
							((FolderBoxControl)control).UnderlineBox = false;

						if (bottomBoxBorder < control.Bottom)
							bottomBoxBorder = control.Bottom;
					}
					if (bottomBoxBorder < pt.Y)
					{
						if (panel.Controls.Count > 0)
							if (panel.Controls[panel.Controls.Count - 1] == data)
								return;
						if (panel.DropHintLineHeight != bottomBoxBorder)
						{
							panel.DropHintLineHeight = bottomBoxBorder;
							panel.Refresh();
						}
					}
				}
			}
		}

		private void ColumnDragLeave(object sender, EventArgs e)
		{
			_scrooTimer.Stop();
			((ColumnPanel)sender).DropHintLineHeight = -1;
			foreach (Control control in ((ColumnPanel)sender).Controls)
				((FolderBoxControl)control).UnderlineBox = false;
			((ColumnPanel)sender).Refresh();
		}

		public void ColumnDragDrop(object sender, DragEventArgs e)
		{
			_scrooTimer.Stop();
			double orderInColumn = -1;
			var panel = (ColumnPanel)sender;
			panel.DropHintLineHeight = -1;
			Point pt = panel.PointToClient(new Point(e.X + 1, e.Y + 1));
			for (int i = 0; i < panel.Controls.Count; i++)
			{
				((FolderBoxControl)panel.Controls[i]).UnderlineBox = false;
				if (panel.Controls[i].Top <= pt.Y && panel.Controls[i].Bottom >= pt.Y)
					orderInColumn = i;
			}
			if (orderInColumn == -1)
				orderInColumn = panel.Controls.Count;

			if (e.Data.GetDataPresent(DataFormats.Serializable, true))
			{
				object data = e.Data.GetData(DataFormats.Serializable, true);
				if (data.GetType() == typeof(FolderBoxControl))
				{
					var box = (FolderBoxControl)data;
					box.Column = panel.Order;
					box.RowOrder = orderInColumn - 0.5;
					ReorderBoxes();
					LinkBoxesToColumns();
					FitObjectsToPage();
					panel.Refresh();
					Parent.StateChanged = true;
				}
			}
		}

		private void scrooTimer_Tick(object sender, EventArgs e)
		{
			if (_scroolDown)
			{
				if (_container.VerticalScroll.Value < _container.VerticalScroll.Maximum - 10)
					_container.VerticalScroll.Value += 10;
				else
					_container.VerticalScroll.Value = _container.VerticalScroll.Maximum;
			}
			else
			{
				if (_container.VerticalScroll.Value > _container.VerticalScroll.Minimum + 10)
					_container.VerticalScroll.Value -= 10;
				else
					_container.VerticalScroll.Value = _container.VerticalScroll.Minimum;
			}
		}
		#endregion

		#region Multi-select Stuff
		public List<LibraryLink> SelectedLinks { get; private set; }
		public event EventHandler<SelectionChangedEventArgs> SelectionChanged;

		public void ClearSelection()
		{
			if (SelectedLinks.Count <= 0) return;
			SelectedLinks.Clear();
			if (SelectionChanged != null)
				SelectionChanged(this, new SelectionChangedEventArgs(Guid.Empty));
			MainController.Instance.WallbinController.UpdateTagsEditor();
		}

		public bool IsLinkSelected(LibraryLink link)
		{
			return SelectedLinks.Contains(link);
		}

		public void SelectLink(Guid folderId, IEnumerable<LibraryLink> links, Keys modifierKeys)
		{
			var ctrlSelect = (modifierKeys & Keys.Control) == Keys.Control;
			SelectedLinks.RemoveAll(x => x.Parent.Identifier.Equals(folderId) || !ctrlSelect);
			foreach (var link in links.Where(link => link.Type != FileTypes.LineBreak && !SelectedLinks.Contains(link)))
				SelectedLinks.Add(link);
			if (SelectionChanged != null)
				SelectionChanged(this, new SelectionChangedEventArgs(folderId));
			MainController.Instance.WallbinController.UpdateTagsEditor();
		}

		public void SelectAllLinks()
		{
			SelectLink(Guid.Empty, Page.Folders.SelectMany(f => f.Files).OfType<LibraryLink>(), Keys.None);
		}

		public void RefreshSelectedLinks()
		{
			var selectedLinkIds = SelectedLinks.Select(link => link.Parent.Identifier.ToString()).ToList();
			foreach (var box in _boxes.Where(x => selectedLinkIds.Contains(x.Folder.Identifier.ToString())))
			{
				box.ApplyWallBinOptions();
				box.Refresh();
			}
		}
		#endregion

		#region Folder Operations
		public void DeleteFolder(FolderBoxControl folderBox)
		{
			if (folderBox == ActiveBox)
				ActiveBox.MakeInactive();
			Page.Folders.Remove(folderBox.Folder);
			BuildDisplayBoxes();
			LinkBoxesToColumns();
			ResizePage();
			MainController.Instance.WallbinController.UpdateLinkInfo(null);
			MainController.Instance.WallbinController.UpdateTagCountInfo();
			Parent.StateChanged = true;
		}

		public void DeleteLinks()
		{
			var existedFiles = Page.Folders.SelectMany(f => f.Files).OfType<LibraryLink>().ToList();
			foreach (var file in existedFiles)
				file.RemoveFromCollection();
			BuildDisplayBoxes();
			LinkBoxesToColumns();
			ResizePage();

			MainController.Instance.WallbinController.UpLinkButton = false;
			MainController.Instance.WallbinController.DownLinkButton = false;
			MainController.Instance.WallbinController.DeleteLinkButton = false;
			MainController.Instance.WallbinController.OpenLinkButton = false;
			MainController.Instance.WallbinController.LinkPropertiesNotesButton = false;
			MainController.Instance.WallbinController.LinkPropertiesTagsButton = false;
			MainController.Instance.WallbinController.LinkPropertiesExpirationDateButton = false;
			MainController.Instance.WallbinController.LinkPropertiesSecurityButton = false;
			MainController.Instance.WallbinController.LinkPropertiesWidgetButton = false;
			MainController.Instance.WallbinController.LinkPropertiesBannerButton = false;

			MainController.Instance.WallbinController.UpdateLinkInfo(null);
			MainController.Instance.WallbinController.UpdateTagCountInfo();

			Parent.StateChanged = true;
		}

		public void DeleteSecurity()
		{
			var existedFiles = Page.Folders.SelectMany(f => f.Files).OfType<LibraryLink>().ToList();
			foreach (var file in existedFiles)
			{
				file.ExtendedProperties.IsRestricted = false;
				file.ExtendedProperties.NoShare = false;
				file.ExtendedProperties.IsForbidden = false;
				file.ExtendedProperties.AssignedUsers = null;
				file.ExtendedProperties.DeniedUsers = null;
			}
			ResizePage();
			Parent.StateChanged = true;
		}

		public void DeleteTags()
		{
			var existedFiles = Page.Folders.SelectMany(f => f.Files).OfType<LibraryLink>().ToList();
			foreach (var file in existedFiles)
			{
				file.SearchTags.SearchGroups.Clear();
				file.SuperFilters.Clear();
				file.CustomKeywords.Tags.Clear();
			}
			ResizePage();
			MainController.Instance.WallbinController.UpdateLinkInfo(null);
			MainController.Instance.WallbinController.UpdateTagCountInfo();
			Parent.StateChanged = true;
		}

		public void DeleteWidgets()
		{
			var existedFiles = Page.Folders.SelectMany(f => f.Files).OfType<LibraryLink>().ToList();
			foreach (var file in existedFiles)
			{
				file.EnableWidget = false;
				file.Widget = null;
			}
			ResizePage();
			Parent.StateChanged = true;
		}

		public void DeleteBanners()
		{
			var existedFiles = Page.Folders.SelectMany(f => f.Files).OfType<LibraryLink>().ToList();
			foreach (var file in existedFiles)
			{
				file.BannerProperties.Enable = false;
				file.BannerProperties.Image = null;
			}
			ResizePage();
			Parent.StateChanged = true;
		}
		#endregion
	}

	public class SelectionChangedEventArgs : EventArgs
	{
		public Guid SourceFolderId { get; private set; }

		public SelectionChangedEventArgs(Guid sourceFolderId)
		{
			SourceFolderId = sourceFolderId;
		}
	}
}

