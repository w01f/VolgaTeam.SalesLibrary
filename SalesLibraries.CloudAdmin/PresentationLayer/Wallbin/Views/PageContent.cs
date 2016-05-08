using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Folders;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Wallbin.ColumnTitles;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Views
{
	[ToolboxItem(false)]
	public partial class PageContent : XtraScrollableControl
	{
		private ColumnTitlePanel _columnTitlePanel;
		private readonly List<ClassicFolderBox> _folderBoxes = new List<ClassicFolderBox>();
		public IPageView PageContainer { get; private set; }

		private int InnerWidth
		{
			get { return Width - SystemInformation.VerticalScrollBarWidth; }
		}

		public PageContent(IPageView pageContainer)
		{
			InitializeComponent();
			PageContainer = pageContainer;

			_scrooTimer.Stop();
			_scrooTimer.Interval = 5;
			_scrooTimer.Tick += OnScrooTimerTick;
		}

		#region Public Methods

		#region Content
		public void LoadContent()
		{
			Resize -= OnResize;
			WinAPIHelper.SendMessage(Handle, 11, IntPtr.Zero, IntPtr.Zero);
			LoadColumnTitles();
			LoadFolders();
			WinAPIHelper.SendMessage(Handle, 11, new IntPtr(1), IntPtr.Zero);
			Refresh();
			Resize += OnResize;
		}

		public void DisposeContent()
		{
			Resize -= OnResize;
			DisposeFolders();
			DisposeColumnTitles();
			Resize += OnResize;
		}

		public void UpdateContent()
		{
			Resize -= OnResize;
			WinAPIHelper.SendMessage(Handle, 11, IntPtr.Zero, IntPtr.Zero);
			UpdateColumnTitlesSize();
			UpdateFolders();
			WinAPIHelper.SendMessage(Handle, 11, new IntPtr(1), IntPtr.Zero);
			Refresh();
			Resize += OnResize;
		}
		#endregion

		#region Links on the Page
		public void SelectAll()
		{
			MainController.Instance.WallbinViews.Selection.SelectLinks(PageContainer.Page.TopLevelLinks, Keys.None);
			_folderBoxes.ForEach(folderBoxControl => folderBoxControl.SelectAll(false));
		}

		public void DeleteLinks()
		{
			PageContainer.Suspend();
			MainController.Instance.WallbinViews.Selection.Reset();
			MainController.Instance.ProcessManager.Run("Deleting Links...",
			cancelationToken =>
			{
				MainController.Instance.MainForm.Invoke(new MethodInvoker(DisposeContent));
				PageContainer.Page.RemoveLinks();
			});
			MainController.Instance.ProcessManager.Run("Loading Page...",
				cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					PageContainer.LoadPage(true);
					PageContainer.ShowPage();
				})));
			PageContainer.Resume();
		}

		public void ResetExpirationDates()
		{
			PageContainer.Page.AllLinks.ResetExpirationSettings();
			MainController.Instance.WallbinViews.Selection.Reset();
			MainController.Instance.ProcessManager.Run("Updating Page...", cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateContent)));
			UpdateContent();
		}

		public void ResetSecurity()
		{
			PageContainer.Page.AllLinks.ResetSecurity();
			MainController.Instance.WallbinViews.Selection.Reset();
			MainController.Instance.ProcessManager.Run("Updating Page...", cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateContent)));
			UpdateContent();
		}

		public void ResetTags()
		{
			PageContainer.Page.AllLinks.ApplyCategories(new SearchGroup[] { });
			PageContainer.Page.AllLinks.ApplyKeywords(new SearchTag[] { });
			PageContainer.Page.AllLinks.ApplySuperFilters(new string[] { });
			MainController.Instance.WallbinViews.Selection.Reset();
			MainController.Instance.ProcessManager.Run("Updating Page...",
				cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateContent)));
		}

		public void ResetWidgets()
		{
			PageContainer.Page.AllLinks.ResetWidgets();
			MainController.Instance.WallbinViews.Selection.Reset();
			MainController.Instance.ProcessManager.Run("Updating Page...",
				cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateContent)));
		}

		public void ResetBanners()
		{
			PageContainer.Page.AllLinks.ResetBanners();
			MainController.Instance.WallbinViews.Selection.Reset();
			MainController.Instance.ProcessManager.Run("Updating Page...",
				cancelationToken => MainController.Instance.MainForm.Invoke(new MethodInvoker(UpdateContent)));
		}
		#endregion
		#endregion

		#region Private Methods
		private void OnResize(object sender, EventArgs e)
		{
			if (IsDisposed) return;
			if (!PageContainer.IsActive) return;
			PageContainer.Suspend();
			UpdateContent();
			PageContainer.Resume();
		}

		private void OnLayout(object sender, LayoutEventArgs e)
		{
			HorizontalScroll.Visible = false;
			VerticalScroll.Visible = true;
		}

		private void OnMouseMove(object sender, MouseEventArgs e)
		{
			Focus();
		}
		#endregion

		#region Folders Processing
		private void LoadFolders()
		{
			_folderBoxes.AddRange(
				PageContainer.Page.Folders
					.Select(libraryFolder =>
					{
						var folderBox = new ClassicFolderBox(libraryFolder);
						folderBox.BoxSizeChanged += OnFolderSizeChanged;
						folderBox.DataChanged += OnFolderDataChanged;
						Application.DoEvents();
						return folderBox;
					})
					.ToArray()
				);
			Controls.AddRange(_folderBoxes.OfType<Control>().ToArray());
		}

		public void DisposeFolders()
		{
			_folderBoxes.ForEach(fb =>
			{
				Controls.Remove(fb);
				fb.Dispose();
			});
			_folderBoxes.Clear();
		}

		public void UpdateFolders()
		{
			_folderBoxes.ForEach(fb => fb.UpdateContent(false));
			UpdateFoldersSize();
		}

		private void UpdateFoldersSize()
		{
			for (var i = 0; i < LibraryPage.ColumnsCount; i++)
				UpdateFoldersColumnSize(i);
		}

		private void UpdateFoldersColumnSize(int columnIndex)
		{

			var width = columnIndex == (LibraryPage.ColumnsCount - 1) ? InnerWidth - (LibraryPage.ColumnsCount - 1) * (InnerWidth / LibraryPage.ColumnsCount) : (InnerWidth / LibraryPage.ColumnsCount);
			var top = _columnTitlePanel.PanelBottom;
			var left = columnIndex * (InnerWidth / LibraryPage.ColumnsCount);
			foreach (var folderBox in _folderBoxes
				.Where(fb => fb.DataSource.ColumnOrder == columnIndex)
				.OrderBy(fb => fb.DataSource.RowOrder))
			{
				folderBox.UpdateHeaderSize();
				folderBox.Width = width;
				folderBox.Top = top;
				folderBox.Left = left;
				top += folderBox.Height;
			}
		}

		public void ProcessFolderMoving(ClassicFolderBox folderBox, int newColumnPosition, int newRowPosition)
		{
			PageContainer.Page.MoveFolderToPosition(folderBox.DataSource, newColumnPosition, newRowPosition);
			UpdateFoldersSize();
			OnFolderDataChanged(this, EventArgs.Empty);
		}

		public void DeleteFolder(ClassicFolderBox folderBox)
		{
			var libraryFolder = folderBox.DataSource;
			_folderBoxes.Remove(folderBox);
			Controls.Remove(folderBox);
			folderBox.Dispose();
			libraryFolder.Delete(PageContainer.Page.Library.Context);
			PageContainer.Page.Folders.RemoveItem(libraryFolder);
			UpdateFoldersSize();
			OnFolderDataChanged(this, EventArgs.Empty);
		}

		private void OnFolderDataChanged(object sender, EventArgs e)
		{
			MainController.Instance.WallbinViews.ActiveWallbin.IsDataChanged = true;
		}

		private void OnFolderSizeChanged(object sender, EventArgs e)
		{
			var folder = (ClassicFolderBox)sender;
			WinAPIHelper.SendMessage(Handle, 11, IntPtr.Zero, IntPtr.Zero);
			UpdateFoldersColumnSize(folder.DataSource.ColumnOrder);
			WinAPIHelper.SendMessage(Handle, 11, new IntPtr(1), IntPtr.Zero);
			Refresh();
		}
		#endregion

		#region Column Title Processing
		private void LoadColumnTitles()
		{
			_columnTitlePanel = new ColumnTitlePanel(PageContainer.Page);
			_columnTitlePanel.Dock = DockStyle.Top;
			Controls.Add(_columnTitlePanel);
			UpdateColumnTitlesSize();
		}

		private void UpdateColumnTitlesSize()
		{
			_columnTitlePanel.UpdateSize();
		}

		private void DisposeColumnTitles()
		{
			if (_columnTitlePanel == null) return;
			Controls.Remove(_columnTitlePanel);
			_columnTitlePanel.Dispose();
			_columnTitlePanel = null;
		}
		#endregion

		#region Drag&Drop Processing
		private const int DragLinePenHeigth = 4;
		private readonly Pen _pen = new Pen(Color.Black, DragLinePenHeigth);
		private ClassicFolderBox _draggedFolder;
		private int _draggedOverColumnIndex = -1;
		private bool _scroolDown;
		private readonly Timer _scrooTimer = new Timer();

		public void ProcessDragOver(object sender, DragEventArgs e)
		{
			if (!MainController.Instance.WallbinViews.FormatState.AllowEdit) return;
			var draggedFolder = _draggedFolder = e.Data != null ? e.Data.GetData(typeof(ClassicFolderBox)) as ClassicFolderBox : null;
			var draggedOverColumnIndex = -1;
			if (draggedFolder != null)
			{
				var point = PointToClient(new Point(e.X, e.Y));
				var width = InnerWidth / 3;
				if (point.X <= width)
					draggedOverColumnIndex = 0;
				else if (point.X > width && point.X <= (width * 2))
					draggedOverColumnIndex = 1;
				else if (point.X > (width * 2))
					draggedOverColumnIndex = 2;
				e.Effect = DragDropEffects.Move;
				if (_draggedFolder != draggedFolder || _draggedOverColumnIndex != draggedOverColumnIndex)
				{
					_draggedFolder = draggedFolder;
					_draggedOverColumnIndex = draggedOverColumnIndex;
					Refresh();
				}
			}
			else
			{
				e.Effect = DragDropEffects.None;
				ProcessDragLeave(sender, e);
			}

			ProcessScrollOnDragOver(sender, e);
		}

		public void ProcessScrollOnDragOver(object sender, DragEventArgs e)
		{
			if (!MainController.Instance.WallbinViews.FormatState.AllowEdit) return;
			var point = PointToClient(MousePosition);
			if (point.Y < Bottom && point.Y > Bottom - 50)
			{
				_scroolDown = true;
				_scrooTimer.Start();
			}
			else if (point.Y > Top && point.Y < Top + 50)
			{
				_scroolDown = false;
				_scrooTimer.Start();
			}
			else
				_scrooTimer.Stop();
		}

		private void ProcessDragDrop(object sender, DragEventArgs e)
		{
			if (!MainController.Instance.WallbinViews.FormatState.AllowEdit) return;
			if (_draggedFolder != null)
				ProcessFolderMoving(_draggedFolder, _draggedOverColumnIndex, -1);
			ProcessDragLeave(sender, e);
		}

		private void ProcessDragLeave(object sender, EventArgs e)
		{
			if (_draggedFolder == null && _draggedOverColumnIndex == -1) return;
			_draggedFolder = null;
			_draggedOverColumnIndex = -1;
			Refresh();
			ProcessScrollOnDragLeave(sender, e);
		}

		public void ProcessScrollOnDragLeave(object sender, EventArgs e)
		{
			_scrooTimer.Stop();
		}

		private void OnScrooTimerTick(object sender, EventArgs e)
		{
			if (_scroolDown)
			{
				if (VerticalScroll.Value < VerticalScroll.Maximum - 10)
					VerticalScroll.Value += 10;
				else
					VerticalScroll.Value = VerticalScroll.Maximum;
			}
			else
			{
				if (VerticalScroll.Value > VerticalScroll.Minimum + 10)
					VerticalScroll.Value -= 10;
				else
					VerticalScroll.Value = VerticalScroll.Minimum;
			}
		}

		protected override void OnPaint(PaintEventArgs e)
		{
			base.OnPaint(e);
			if (_draggedFolder == null) return;
			var lastFolderBox = _folderBoxes.Where(f => f.DataSource.ColumnOrder == _draggedOverColumnIndex).OrderByDescending(f => f.DataSource.RowOrder).FirstOrDefault();
			var top = lastFolderBox != null ? lastFolderBox.Bottom : _columnTitlePanel.PanelBottom;
			var width = _draggedOverColumnIndex == (LibraryPage.ColumnsCount - 1) ? InnerWidth - (LibraryPage.ColumnsCount - 1) * (InnerWidth / LibraryPage.ColumnsCount) : (InnerWidth / LibraryPage.ColumnsCount);
			var left = width * _draggedOverColumnIndex;
			e.Graphics.DrawLine(_pen, left, top, (left + width), top);
		}
		#endregion
	}
}
