using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraBars;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.DataState;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Clipboard;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders.Controls
{
	[ToolboxItem(false)]
	public partial class ClassicFolderBox : FolderBoxWithHeader
	{
		public override IWallbinViewFormat FormatState => MainController.Instance.WallbinViews.FormatState;

		public SelectionManager SelectionManager => MainController.Instance.WallbinViews.Selection;

		protected PageContent FolderContainer => (PageContent)Parent;

		public bool IsActive => SelectionManager.SelectedFolder == this;

		public LinkRow SelectedLinkRow => grFiles.SelectedRows.OfType<LinkRow>().FirstOrDefault();

		public override Color SelectedRowBackColor => Color.Wheat;

		public event EventHandler<EventArgs> DataChanged;
		public event EventHandler<EventArgs> MultiLinksSettingsChanged;

		public ClassicFolderBox(LibraryFolder dataSource)
			: base(dataSource)
		{
			SelectionManager.SelectionChanged += OnSelectionChanged;
			DataStateObserver.Instance.DataChanged += OnLinksDeleted;

			barButtonItemFolderPropertiesDeleteLinkSecurity.Visibility = MainController.Instance.Settings.EditorSettings.EnableSecurityEdit ? BarItemVisibility.Always : BarItemVisibility.Never;
			barButtonItemFolderPropertiesDeleteLinkTags.Visibility = MainController.Instance.Settings.EditorSettings.EnableTagsEdit ? BarItemVisibility.Always : BarItemVisibility.Never;

			contextMenuStripSecurity.DefaultDropDownDirection = Screen.AllScreens.Length > 0 && DataSource.ColumnOrder > 1
				? ToolStripDropDownDirection.Left
				: ToolStripDropDownDirection.Default;

			_folderClipboardManager = new FolderClipboardManager(
				DataSource,
				barSubItemFolderPropertiesFolderCopy,
				barSubItemFolderPropertiesFolderMove);
			_folderClipboardManager.FolderMoved += OnFolderMoved;
			
			InitFolderLinksContextMenuEditors();
			InitSingleLinkContextMenuEditors();
			InitMultiLinksContextMenuEditors();

			// 
			// grFiles
			// 
			grFiles.CellBeginEdit += (o, e) => { e.Cancel = true; };
			grFiles.CellMouseClick += ProcessSingleLinkContextMenu;
			grFiles.CellMouseClick += ProcessMultiLinksContextMenu;
			grFiles.CellMouseDoubleClick += ProcessLinkTextEdit;
			grFiles.CellMouseDown += OnGridCellMouseDown;
			grFiles.CellMouseLeave += OnGridCellMouseLeave;
			grFiles.CellMouseMove += OnGridCellMouseMove;
			grFiles.CellMouseUp += OnGridCellMouseUp;
			grFiles.SelectionChanged += OnGridSelectionChanged;
			grFiles.DragDrop += OnDragDrop;
			grFiles.DragOver += OnDragOver;
			grFiles.DragLeave += OnDragLeave;
			grFiles.MouseDown += OnGridMouseDown;
			grFiles.MouseClick += OnGridMouseClick;
			// 
			// labelControlText
			// 
			labelControlText.Click += OnHeaderClick;
			labelControlText.DoubleClick += OnHeaderDoubleClick;
			labelControlText.DragDrop += OnDragDrop;
			labelControlText.DragOver += OnDragOver;
			labelControlText.DragLeave += OnDragLeave;
			labelControlText.MouseDown += OnHeaderMouseDown;
			labelControlText.MouseMove += OnHeaderMouseMove;
		}

		public override void ReleaseControl()
		{
			_folderClipboardManager.FolderMoved -= OnFolderMoved;
			_folderClipboardManager = null;

			base.ReleaseControl();
			DataChanged = null;
			MultiLinksSettingsChanged = null;
		}

		private void ResetPadding()
		{
			Padding newPadding;
			if (IsFolderBoxDragged)
				newPadding = new Padding(0, 2, 0, 0);
			else if (FormatState.ShowSelectedFolder && IsActive)
				newPadding = new Padding(2, 2, 2, 2);
			else
				newPadding = Padding.Empty;
			if (Padding.Left != newPadding.Left ||
				Padding.Right != newPadding.Right ||
				Padding.Top != newPadding.Top ||
				Padding.Bottom != newPadding.Bottom)
			{
				Padding = newPadding;
				UpdateGridSize();
				Refresh();
			}
		}

		private void SelectSingleRow(LinkRow row)
		{
			var oldOutsideChangesInProgress = _outsideChangesInProgress;
			_outsideChangesInProgress = false;
			grFiles.ClearSelection();
			row.Selected = true;
			_outsideChangesInProgress = oldOutsideChangesInProgress;

			if (DataSource.Links.Count<=1)
			OnGridSelectionChanged(grFiles, EventArgs.Empty);
		}

		private void OnSelectionChanged(object sender, SelectionEventArgs e)
		{
			if (IsDisposed) return;
			switch (e.SelectionType)
			{
				case SelectionEventType.SelectionReset:
					if (!IsActive)
					{
						var storedValue = _outsideChangesInProgress;
						_outsideChangesInProgress = true;
						grFiles.ClearSelection();
						grFiles.CurrentCell = null;
						_outsideChangesInProgress = storedValue;
					}
					break;
				case SelectionEventType.FolderSelected:
					ResetPadding();
					break;
			}
			grFiles.DefaultCellStyle.SelectionBackColor = SelectedRowBackColor;
		}

		private void OnGridSelectionChanged(object sender, EventArgs e)
		{
			if (_outsideChangesInProgress) return;
			var selectedLinks = grFiles.SelectedRows.OfType<LinkRow>().Select(row => row.Source);
			SelectionManager.SelectLinks(selectedLinks, ModifierKeys);
		}

		private void OnHeaderClick(object sender, EventArgs e)
		{
			if (IsActive) return;
			SelectionManager.SelectFolder(this);
			labelControlText.Focus();
		}

		private void OnHeaderDoubleClick(object sender, EventArgs e)
		{
			EditFolderSettings();
		}

		private void OnGridMouseDown(object sender, MouseEventArgs e)
		{
			if (IsActive) return;
			SelectionManager.SelectFolder(this);
			var hitTest = grFiles.HitTest(e.X, e.Y);
			if (hitTest.Type != DataGridViewHitTestType.Cell)
				labelControlText.Focus();
		}

		private void OnGridCellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void OnBordersPaint(object sender, PaintEventArgs e)
		{
			if (FormatState.ShowSelectedFolder && IsActive)
			{
				Rectangle rect = e.ClipRectangle.Top == 0 ?
					new Rectangle(e.ClipRectangle.Left, e.ClipRectangle.Top, e.ClipRectangle.Width, Height) :
					new Rectangle(e.ClipRectangle.Left, 0, e.ClipRectangle.Width, e.ClipRectangle.Bottom);
				const int borderWidth = 3;
				for (int i = 0; i < borderWidth; i++)
				{
					ControlPaint.DrawBorder(e.Graphics,
						rect,
						Color.Red,
						ButtonBorderStyle.Solid);
					rect.X++;
					rect.Y++;
					rect.Width -= 2;
					rect.Height -= 2;
				}
			}
			if (IsFolderBoxDragged)
				e.Graphics.DrawLine(_folderBoxDraggedIndicatorPen, 0, 0, Width, 0);
		}

		protected override void OnGridCellPainting(object sender, DataGridViewCellPaintingEventArgs e)
		{
			if (e.ColumnIndex != 0) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			base.OnGridCellPainting(sender, e);
			if (DataSource.Links.Any() &&
				(IsSourceLinksDragged || IsLinkRowDragged || IsLinkBundleDragged || IsNativeFilesDragged))
			{
				if (_mouseDragOverHitInfo.Type == DataGridViewHitTestType.Cell && _mouseDragOverHitInfo.RowIndex == linkRow.Index)
					e.Graphics.DrawLine(_rowDraggedIndicatorPen, 0, e.CellBounds.Top + 2, grFiles.Width, e.CellBounds.Top + 2);
				else if (_mouseDragOverHitInfo.RowIndex == -1 && linkRow.Index == grFiles.RowCount - 1)
					e.Graphics.DrawLine(_rowDraggedIndicatorPen, 0, e.CellBounds.Height * grFiles.RowCount + 1, grFiles.Width, e.CellBounds.Height * grFiles.RowCount + 1);
			}
		}
	}
}