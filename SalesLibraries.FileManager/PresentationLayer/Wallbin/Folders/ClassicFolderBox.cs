using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.DataState;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.Objects.SearchTags;
using SalesLibraries.CommonGUI.Wallbin.Folders;
using SalesLibraries.CommonGUI.Wallbin.Views;
using SalesLibraries.FileManager.Controllers;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Links.SingleSettings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Settings;
using SalesLibraries.FileManager.PresentationLayer.Wallbin.Views;

namespace SalesLibraries.FileManager.PresentationLayer.Wallbin.Folders
{
	[ToolboxItem(false)]
	public partial class ClassicFolderBox : FolderBoxWithHeader
	{
		private readonly Pen _folderBoxDraggedIndicatorPen = new Pen(Color.Black, 8);
		private readonly Pen _rowDraggedIndicatorPen = new Pen(Color.Black, 2);

		#region Public Properties
		public override IWallbinViewFormat FormatState
		{
			get { return MainController.Instance.WallbinViews.FormatState; }
		}

		protected SelectionManager SelectionManager
		{
			get { return MainController.Instance.WallbinViews.Selection; }
		}

		protected PageContent FolderContainer
		{
			get { return (PageContent)Parent; }
		}

		public bool IsActive
		{
			get { return SelectionManager.SelectedFolder == this; }
		}

		public LinkRow SelectedLinkRow
		{
			get { return grFiles.SelectedRows.OfType<LinkRow>().FirstOrDefault(); }
		}

		public override Color SelectedRowBackColor
		{
			get
			{
				if (IsActive)
					return Color.Wheat;
				return base.SelectedRowBackColor;
			}
		}

		public event EventHandler<EventArgs> DataChanged;
		#endregion

		public ClassicFolderBox(LibraryFolder dataSource)
			: base(dataSource)
		{
			SelectionManager.SelectionChanged += OnSelectionChanged;
			DataStateObserver.Instance.DataChanged += OnLinksDeleted;

			// 
			// grFiles
			// 
			grFiles.CellBeginEdit += OnGridCellBeginEdit;
			grFiles.CellEndEdit += OnGridCellEndEdit;
			grFiles.CellMouseClick += OnGridCellMouseClick;
			grFiles.CellMouseDown += OnGridCellMouseDown;
			grFiles.CellMouseLeave += OnGridCellMouseLeave;
			grFiles.CellMouseMove += OnGridCellMouseMove;
			grFiles.CellMouseUp += grFiles_CellMouseUp;
			grFiles.SelectionChanged += OnGridSelectionChanged;
			grFiles.DragDrop += OnDragDrop;
			grFiles.DragOver += OnDragOver;
			grFiles.DragLeave += OnDragLeave;
			grFiles.MouseDown += OnGridMouseDown;

			// 
			// pnBorders
			// 
			pnBorders.DragDrop += OnDragDrop;
			pnBorders.DragOver += OnDragOver;
			pnBorders.DragLeave += OnDragLeave;

			// 
			// pnHeaderBorder
			// 
			pnHeaderBorder.DragDrop += OnDragDrop;
			pnHeaderBorder.DragOver += OnDragOver;
			pnHeaderBorder.DragLeave += OnDragLeave;
			// 
			// pnHeader
			// 
			pnHeader.DragDrop += OnDragDrop;
			pnHeader.DragOver += OnDragOver;
			pnHeader.DragLeave += OnDragLeave;
			// 
			// labelControlText
			// 
			labelControlText.Click += OnHeaderClick;
			labelControlText.DragDrop += OnDragDrop;
			labelControlText.DragOver += OnDragOver;
			labelControlText.DragLeave += OnDragLeave;
			labelControlText.MouseDown += OnHeaderMouseDown;
			labelControlText.MouseMove += OnHeaderMouseMove;
			// 
			// pbImage
			// 
			pbImage.Click += OnHeaderClick;
			pbImage.DragDrop += OnDragDrop;
			pbImage.DragOver += OnDragOver;
			pbImage.DragLeave += OnDragLeave;
			pbImage.MouseDown += OnHeaderMouseDown;
			pbImage.MouseMove += OnHeaderMouseMove;
		}

		#region Public Methods
		public override void ReleaseControl()
		{
			base.ReleaseControl();
			DataChanged = null;
		}
		#endregion

		#region Link Data Processing
		public void AddUrl()
		{
			using (var form = new FormAddUrl())
			{
				if (form.ShowDialog() != DialogResult.OK) return;

				int position = -1;
				LinkRow selectedLink = SelectedLinkRow;
				if (selectedLink != null)
					position = selectedLink.Index;

				_outsideChangesInProgress = true;
				WebLink newLink = WebLink.Create(form.LinkName, form.LinkPath, DataSource);
				if (position >= 0)
					((List<BaseLibraryLink>)DataSource.Links).InsertItem(position, newLink);
				else
					DataSource.Links.AddItem(newLink);
				InsertLinkRow(newLink, position);
				_outsideChangesInProgress = false;

				UpdateGridSize();
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		public void AddNetworkLink()
		{
			using (var form = new FormAddNetworkLink())
			{
				if (form.ShowDialog() != DialogResult.OK) return;

				int position = -1;
				LinkRow selectedLink = SelectedLinkRow;
				if (selectedLink != null)
					position = selectedLink.Index;

				_outsideChangesInProgress = true;
				NetworkLink newLink = NetworkLink.Create(form.LinkName, form.LinkPath, DataSource);
				if (position >= 0)
					((List<BaseLibraryLink>)DataSource.Links).InsertItem(position, newLink);
				else
					DataSource.Links.AddItem(newLink);
				InsertLinkRow(newLink, position);
				_outsideChangesInProgress = false;

				UpdateGridSize();
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		public void AddLineBreak()
		{
			int position = -1;
			LinkRow selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				position = selectedLink.Index;

			_outsideChangesInProgress = true;
			LineBreak newLink = LineBreak.Create(DataSource);
			if (position >= 0)
				((List<BaseLibraryLink>)DataSource.Links).InsertItem(position, newLink);
			else
				DataSource.Links.AddItem(newLink);
			InsertLinkRow(newLink, position);
			_outsideChangesInProgress = false;

			UpdateGridSize();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public void DownLink()
		{
			LinkRow selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			if (selectedRow.IsBottom) return;
			int currentIndex = selectedRow.Index;
			int newIndex = currentIndex + 1;
			BaseLibraryLink linkSource = selectedRow.Source;

			_outsideChangesInProgress = true;
			grFiles.ClearSelection();
			selectedRow.Delete();
			linkSource.Folder = DataSource;
			((List<BaseLibraryLink>)DataSource.Links).InsertItem(newIndex, linkSource);
			LinkRow insertedRow = InsertLinkRow(linkSource, newIndex);
			_outsideChangesInProgress = false;

			insertedRow.Selected = true;

			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public void UpLink()
		{
			LinkRow selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			if (selectedRow.IsTop) return;
			int currentIndex = selectedRow.Index;
			int newIndex = currentIndex - 1;
			BaseLibraryLink linkSource = selectedRow.Source;

			_outsideChangesInProgress = true;
			grFiles.ClearSelection();
			selectedRow.Delete();
			linkSource.Folder = DataSource;
			((List<BaseLibraryLink>)DataSource.Links).InsertItem(newIndex, linkSource);
			LinkRow insertedRow = InsertLinkRow(linkSource, newIndex);
			_outsideChangesInProgress = false;

			insertedRow.Selected = true;

			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public void EditLinkSettings(LinkSettingsType settingsType)
		{
			LinkRow selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			if (SettingsEditorFactory.Run(selectedRow.Source, settingsType) != DialogResult.OK) return;
			selectedRow.Info.Recalc();
			grFiles.Refresh();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public void OpenLink()
		{
			LinkRow selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			var sourceLink = selectedRow.Source as LibraryObjectLink;
			if (sourceLink == null) return;
			Utils.OpenFile(sourceLink.FullPath);
		}

		public void DeleteLink()
		{
			LinkRow selectedRow = SelectedLinkRow;
			if (selectedRow == null) return;
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove this link/line break?") != DialogResult.Yes) return;
			selectedRow.Delete(true);
			UpdateGridSize();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void DeleteLinks()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove links?") != DialogResult.Yes) return;
			foreach (LinkRow linkRow in grFiles.Rows.OfType<LinkRow>().ToList())
				linkRow.Delete(true);
			UpdateGridSize();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		public void SelectAll(bool handleSelection = true)
		{
			_outsideChangesInProgress = !handleSelection;
			grFiles.SelectAll();
			_outsideChangesInProgress = false;
		}

		private void ResetSecurity()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to delete security settings?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplySecurity(new SecuritySettings());
			UpdateContent(true);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ResetTags()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to wipe tags?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplyCategories(new SearchGroup[] { });
			DataSource.AllLinks.ApplyKeywords(new SearchTag[] { });
			DataSource.AllLinks.ApplySuperFilters(new string[] { });
			UpdateContent(true);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ResetWidgets()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove widgets?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplyWidgets(new LinkWidgetSettings());
			UpdateContent(true);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ResetBanners()
		{
			if (MainController.Instance.PopupMessages.ShowQuestion("Are You sure You want to remove banners?") != DialogResult.Yes) return;
			DataSource.AllLinks.ApplyBanners(new BannerSettings());
			UpdateContent(true);
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void SortLinkByName()
		{
			grFiles.ClearSelection();
			DataSource.SortLinksByDisplayName();
			List<LinkRow> rows = grFiles.Rows.OfType<LinkRow>().ToList();
			grFiles.Rows.Clear();
			grFiles.Rows.AddRange(rows.OrderBy(linkRow => linkRow.Source.Order).ToArray());
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}
		#endregion

		#region Folder Data Processing
		private void EditFolderSettings()
		{
			using (var form = new FormWindow(DataSource, WindowPropertiesType.Appearnce))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				SetupView();
				UpdateContent(true);
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void EditFolderBanner()
		{
			using (var form = new FormWindow(DataSource, WindowPropertiesType.Banner))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				SetupView();
				UpdateGridSize();
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void EditFolderWidget()
		{
			using (var form = new FormWindow(DataSource, WindowPropertiesType.Widget))
			{
				if (form.ShowDialog(MainController.Instance.MainForm) != DialogResult.OK) return;
				SetupView();
				UpdateGridSize();
				if (DataChanged != null)
					DataChanged(this, EventArgs.Empty);
			}
		}

		private void DeleteFolder()
		{
			FolderContainer.DeleteFolder(this);
		}
		#endregion

		#region Private Methods
		private BaseLibraryLink InsertFileLink(SourceLink sourceLink, int position = -1)
		{
			BaseLibraryLink link = null;
			MainController.Instance.ProcessManager.Run("Adding Link...", cancelationToken =>
			{
				link = LibraryFileLink.Create(sourceLink, DataSource);
				if (position >= 0)
					((List<BaseLibraryLink>)DataSource.Links).InsertItem(position, link);
				else
					DataSource.Links.AddItem(link);
			});
			return link;
		}

		private void InsertLinks(IEnumerable<SourceLink> sourceLinks, int position = -1)
		{
			_outsideChangesInProgress = true;
			foreach (SourceLink sourceLink in sourceLinks)
			{
				InsertLinkRow(InsertFileLink(sourceLink, position), position);
				if (position != -1)
					position++;
			}
			_outsideChangesInProgress = false;
			UpdateGridSize();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void ProcessRowMoving(LinkRow targetRow, int newPosition = -1)
		{
			int positionToInsert;
			if (targetRow.DataGridView == grFiles)
				positionToInsert = newPosition == -1 ?
					grFiles.RowCount - 1 :
					(targetRow.Index < newPosition ? newPosition - 1 : newPosition);
			else
				positionToInsert = newPosition == -1 ? grFiles.RowCount : newPosition;
			targetRow.Delete();
			grFiles.ClearSelection();
			grFiles.Rows.Insert(positionToInsert, targetRow);
			targetRow.Source.Folder = DataSource;
			((List<BaseLibraryLink>)DataSource.Links).InsertItem(positionToInsert, targetRow.Source);
			targetRow.ChangeFolder(this);
			targetRow.Info.Recalc();
			targetRow.InfoChanged += OnLinkRowInfoChanged;
			UpdateGridSize();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void OnSelectionChanged(object sender, SelectionEventArgs e)
		{
			if (IsDisposed) return;
			if (e.SelectionType == SelectionEventType.FolderSelected || e.SelectionType == SelectionEventType.SelectionReset)
			{
				if (IsActive)
				{
					ResetPadding();
					grFiles.ReadOnly = false;
					grFiles.MultiSelect = FormatState.AllowMultiSelect;
				}
				else
				{
					grFiles.ReadOnly = true;
					ResetPadding();
				}
			}

			if (!IsActive &&
				((e.SelectionType == SelectionEventType.FolderSelected && !FormatState.AllowMultiSelect) ||
				 e.SelectionType == SelectionEventType.SelectionReset)
				)
			{
				grFiles.ClearSelection();
				grFiles.CurrentCell = null;
			}

			grFiles.DefaultCellStyle.SelectionBackColor = SelectedRowBackColor;
		}

		private void ResetPadding()
		{
			Padding newPadding;
			if (IsFolderBoxDragged)
				newPadding = new Padding(0, 3, 0, 0);
			else if (IsActive && !FormatState.AllowMultiSelect)
				newPadding = new Padding(3, 3, 3, 3);
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

		private void OnLinksDeleted(object sender, DataChangeEventArgs e)
		{
			if (e.ChangeType != DataChangeType.LinksDeleted) return;
			var linksDeletedArgs = (LinksDeletedEventArgs)e;
			foreach (LinkRow linkRow in grFiles.Rows.OfType<LinkRow>().Where(row => linksDeletedArgs.LinkIds.Any(id => id.Equals(row.Source.ExtId))))
				linkRow.RemoveFromGrid();
			UpdateGridSize();
		}
		#endregion

		#region Link Row Events (Editing, Selection, Sizing)
		private void OnGridCellBeginEdit(object sender, DataGridViewCellCancelEventArgs e)
		{
			e.Cancel = true;
			if (!FormatState.AllowEdit) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			if (!linkRow.AllowEdit) return;
			grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = RegularRowFont;
			grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = linkRow.Source.DisplayNameWithoutNote;
			e.Cancel = false;
		}

		private void OnGridCellEndEdit(object sender, DataGridViewCellEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			if (!linkRow.AllowEdit) return;
			string editValue = grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Value as String ?? String.Empty;
			if (linkRow.Source.DisplayNameWithoutNote == editValue) return;
			linkRow.Source.Name = editValue;
			grFiles.Rows[e.RowIndex].Cells[e.ColumnIndex].Style.Font = linkRow.Info.Font;
			linkRow.Info.Recalc();
			if (DataChanged != null)
				DataChanged(this, EventArgs.Empty);
		}

		private void OnGridSelectionChanged(object sender, EventArgs e)
		{
			if (_outsideChangesInProgress) return;
			IEnumerable<BaseLibraryLink> selectedLinks = grFiles.SelectedRows.OfType<LinkRow>().Select(row => row.Source);
			SelectionManager.SelectLinks(selectedLinks, ModifierKeys);
		}
		#endregion

		#region Formatting Event Handlers
		private void OnBordersPaint(object sender, PaintEventArgs e)
		{
			if (IsActive)
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
				(IsSourceLinksDragged || IsLinkRowDragged))
			{
				if (_mouseDragOverHitInfo.Type == DataGridViewHitTestType.Cell && _mouseDragOverHitInfo.RowIndex == linkRow.Index)
					e.Graphics.DrawLine(_rowDraggedIndicatorPen, 0, e.CellBounds.Top + 2, grFiles.Width, e.CellBounds.Top + 2);
				else if (_mouseDragOverHitInfo.RowIndex == -1 && linkRow.Index == grFiles.RowCount - 1)
					e.Graphics.DrawLine(_rowDraggedIndicatorPen, 0, e.CellBounds.Height * grFiles.RowCount + 1, grFiles.Width, e.CellBounds.Height * grFiles.RowCount + 1);
			}
		}
		#endregion

		#region Drag&Drop Processing (Methods and Events)
		private IDataObject _dataDraggedOver;
		private Rectangle _dragStartBox;
		private DataGridView.HitTestInfo _mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		private DataGridView.HitTestInfo _mouseDragOverHitInfo = DataGridView.HitTestInfo.Nowhere;

		protected IDataObject DataDraggedOver
		{
			get { return _dataDraggedOver; }
			set
			{
				_dataDraggedOver = value;
				ResetPadding();
			}
		}

		protected bool IsDataDragged
		{
			get { return DataDraggedOver != null; }
		}

		protected bool IsSourceLinksDragged
		{
			get { return IsDataDragged && DataDraggedOver.GetDataPresent(typeof(SourceLink[])); }
		}

		protected bool IsLinkRowDragged
		{
			get { return IsDataDragged && DataDraggedOver.GetDataPresent(typeof(LinkRow)); }
		}

		protected bool IsFolderBoxDragged
		{
			get
			{
				object data = IsDataDragged ? DataDraggedOver.GetData(typeof(ClassicFolderBox)) : null;
				return data != null && data != this;
			}
		}

		private void ResetDragInfo()
		{
			bool needGridRefersh = IsSourceLinksDragged || IsLinkRowDragged;
			DataDraggedOver = null;
			_mouseDragOverHitInfo = DataGridView.HitTestInfo.Nowhere;
			if (needGridRefersh)
				grFiles.Refresh();
		}

		private void OnDragOver(object sender, DragEventArgs e)
		{
			FolderContainer.ProcessScrollOnDragOver(sender, e);
			if (!FormatState.AllowEdit) return;
			DataDraggedOver = e.Data;
			if (!IsDataDragged) return;
			if (IsSourceLinksDragged)
				e.Effect = DragDropEffects.Copy;
			else if (IsLinkRowDragged)
				e.Effect = DragDropEffects.Move;
			else if (IsFolderBoxDragged)
				e.Effect = DragDropEffects.Move;
			else
			{
				e.Effect = DragDropEffects.None;
				ResetDragInfo();
				return;
			}

			Point pt = grFiles.PointToClient(new Point(e.X, e.Y));
			DataGridView.HitTestInfo dragOverInfo = grFiles.HitTest(pt.X, pt.Y);
			if (!dragOverInfo.Equals(_mouseDragOverHitInfo))
			{
				_mouseDragOverHitInfo = dragOverInfo;
				grFiles.Refresh();
			}
		}

		private void OnDragLeave(object sender, EventArgs e)
		{
			FolderContainer.ProcessScrollOnDragLeave(sender, e);
			if (!FormatState.AllowEdit) return;
			if (DataDraggedOver == null) return;
			if (IsFolderBoxDragged && ClientRectangle.Contains(PointToClient(MousePosition)))
				return;
			ResetDragInfo();
		}

		private void OnDragDrop(object sender, DragEventArgs e)
		{
			FolderContainer.ProcessScrollOnDragOver(sender, e);
			if (!FormatState.AllowEdit) return;
			var droppedLinks = e.Data.GetData(typeof(SourceLink[])) as SourceLink[];
			if (droppedLinks != null)
			{
				SelectionManager.SelectFolder(this);
				InsertLinks(droppedLinks, _mouseDragOverHitInfo.RowIndex);
			}
			var droppedRow = e.Data.GetData(typeof(LinkRow)) as LinkRow;
			if (droppedRow != null)
			{
				SelectionManager.SelectFolder(this);
				ProcessRowMoving(droppedRow, _mouseDragOverHitInfo.RowIndex);
			}
			var droppedFolder = e.Data.GetData(typeof(ClassicFolderBox)) as ClassicFolderBox;
			if (droppedFolder != null && droppedFolder != this)
				FolderContainer.ProcessFolderMoving(droppedFolder, DataSource.ColumnOrder, DataSource.RowOrder);
			ResetDragInfo();
		}

		private void OnGridCellMouseDown(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			_dragStartBox = new Rectangle(
				new Point(
					e.X - (SystemInformation.DragSize.Width / 2),
					e.Y - (SystemInformation.DragSize.Height / 2)),
				SystemInformation.DragSize);
			_mouseDownHitInfo = grFiles.HitTest(e.X, e.Y);
		}

		private void grFiles_CellMouseUp(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			_mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
			;
		}

		private void OnGridCellMouseMove(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (DataSource.Links.Any())
				Cursor = Cursors.Hand;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left) ||
				Equals(_mouseDownHitInfo, DataGridView.HitTestInfo.Nowhere) ||
				_dragStartBox.Contains(e.X, e.Y))
				return;
			LinkRow selectedLink = SelectedLinkRow;
			if (selectedLink != null)
				grFiles.DoDragDrop(selectedLink, DragDropEffects.All);
			_mouseDownHitInfo = DataGridView.HitTestInfo.Nowhere;
		}

		private void OnHeaderMouseDown(object sender, MouseEventArgs e)
		{
			switch (e.Button)
			{
				case MouseButtons.Left:
					if (!FormatState.AllowEdit) return;
					_dragStartBox = new Rectangle(
						new Point(
							e.X - (SystemInformation.DragSize.Width / 2),
							e.Y - (SystemInformation.DragSize.Height / 2)),
						SystemInformation.DragSize);
					break;
				case MouseButtons.Right:
					if (FormatState.ShowSecurityTags)
						contextMenuStripSecurity.Show((Control)sender, e.Location);
					else
						contextMenuStripFolderProperties.Show((Control)sender, e.Location);
					break;
			}
		}

		private void OnHeaderMouseMove(object sender, MouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			Cursor = Cursors.Default;
			if (((e.Button & MouseButtons.Left) != MouseButtons.Left) || _dragStartBox.Contains(e.X, e.Y))
				return;
			grFiles.DoDragDrop(this, DragDropEffects.Move);
		}
		#endregion

		#region Other GUI Routines
		private void OnHeaderClick(object sender, EventArgs e)
		{
			if (IsActive) return;
			SelectionManager.SelectFolder(this);
			labelControlText.Focus();
		}

		private void OnGridMouseDown(object sender, MouseEventArgs e)
		{
			if (IsActive) return;
			SelectionManager.SelectFolder(this);
			DataGridView.HitTestInfo hitTest = grFiles.HitTest(e.X, e.Y);
			if (hitTest.Type != DataGridViewHitTestType.Cell)
				labelControlText.Focus();
		}

		private void OnGridCellMouseLeave(object sender, DataGridViewCellEventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void OnGridCellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
		{
			if (!FormatState.AllowEdit) return;
			if (!IsActive) return;
			if (e.Button != MouseButtons.Right) return;
			var linkRow = (LinkRow)grFiles.Rows[e.RowIndex];
			linkRow.Selected = true;
			if (linkRow.Source is LineBreak)
			{
				toolStripMenuItemLinkPropertiesOpen.Visible = false;
				toolStripMenuItemLinkPropertiesAdvanced.Visible = false;
				toolStripMenuItemLinkPropertiesTags.Visible = false;
				toolStripMenuItemLinkPropertiesExpirationDate.Visible = false;
				toolStripMenuItemLinkPropertiesDelete.Text = "Delete this Line Break";
			}
			else
			{
				toolStripMenuItemLinkPropertiesOpen.Visible = true;
				toolStripMenuItemLinkPropertiesAdvanced.Visible = linkRow.Source is LibraryFolderLink;
				toolStripMenuItemLinkPropertiesTags.Visible = true;
				toolStripMenuItemLinkPropertiesExpirationDate.Visible = true;
				toolStripMenuItemLinkPropertiesDelete.Text = "Delete this Link";
			}
			contextMenuStripLinkProperties.Show(sender as Control, grFiles.PointToClient(Cursor.Position));
		}
		#endregion

		#region Context Menu

		#region Link
		private void toolStripMenuItemLinkPropertiesOpen_Click(object sender, EventArgs e)
		{
			OpenLink();
		}

		private void toolStripMenuItemLinkPropertiesDelete_Click(object sender, EventArgs e)
		{
			DeleteLink();
		}

		private void toolStripMenuItemLinkPropertiesNotes_Click(object sender, EventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Notes);
		}

		private void toolStripMenuItemLinkPropertiesTags_Click(object sender, EventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Tags);
		}

		private void toolStripMenuItemLinkPropertiesExpirationDate_Click(object sender, EventArgs e)
		{
			EditLinkSettings(LinkSettingsType.ExpirationDate);
		}

		private void toolStripMenuItemLinkPropertiesSecurity_Click(object sender, EventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Security);
		}

		private void toolStripMenuItemLinkPropertiesWidget_Click(object sender, EventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Widget);
		}

		private void toolStripMenuItemLinkPropertiesBanner_Click(object sender, EventArgs e)
		{
			EditLinkSettings(LinkSettingsType.Banner);
		}

		private void toolStripMenuItemLinkPropertiesAdvanced_Click(object sender, EventArgs e)
		{
			EditLinkSettings(LinkSettingsType.AdvancedSettings);
		}
		#endregion

		#region Folder
		private void toolStripMenuItemFolderDeleteLinks_Click(object sender, EventArgs e)
		{
			DeleteLinks();
		}

		private void toolStripMenuItemFolderDeleteSecurity_Click(object sender, EventArgs e)
		{
			ResetSecurity();
		}

		private void toolStripMenuItemFolderDeleteTags_Click(object sender, EventArgs e)
		{
			ResetTags();
		}

		private void toolStripMenuItemFolderDeleteWidgets_Click(object sender, EventArgs e)
		{
			ResetWidgets();
		}

		private void toolStripMenuItemFolderDeleteBanners_Click(object sender, EventArgs e)
		{
			ResetBanners();
		}

		private void toolStripMenuItemFolderSettings_Click(object sender, EventArgs e)
		{
			EditFolderSettings();
		}

		private void toolStripMenuItemFolderDelete_Click(object sender, EventArgs e)
		{
			DeleteFolder();
		}

		private void toolStripMenuItemFolderWidget_Click(object sender, EventArgs e)
		{
			EditFolderWidget();
		}

		private void toolStripMenuItemFolderBanner_Click(object sender, EventArgs e)
		{
			EditFolderBanner();
		}

		private void toolStripMenuItemFolderSort_Click(object sender, EventArgs e)
		{
			SortLinkByName();
		}
		#endregion

		#region Security
		private void toolStripMenuItemSelectAll_Click(object sender, EventArgs e)
		{
			SelectAll();
		}

		private void toolStripMenuItemResetAll_Click(object sender, EventArgs e)
		{
			ResetSecurity();
		}
		#endregion

		#endregion
	}
}