using System;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils.Menu;
using DevExpress.XtraGrid.Menu;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesLibraries.Business.Entities.Helpers;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.CloudAdmin.Controllers;
using SalesLibraries.CommonGUI.Common;

namespace SalesLibraries.CloudAdmin.PresentationLayer.Wallbin.Settings
{
	//public partial class ColumnSettings : UserControl
	public sealed partial class ColumnSettings : XtraTabPage
	{
		private readonly LibraryPage _page;

		public int ColumnOrder { get; private set; }
		public LibraryFolder FolderInBuffer { get; set; }

		public event EventHandler<EventArgs> FolderChanged;
		public event EventHandler<EventArgs> FolderMovedRight;
		public event EventHandler<EventArgs> FolderMovedLeft;
		public event EventHandler<FolderCopiedEventArgs> FolderCopied;
		public event EventHandler<FolderPastedEventArgs> FolderPasted;

		public ColumnSettings(LibraryPage page, int columnOrder)
		{
			InitializeComponent();
			_page = page;
			ColumnOrder = columnOrder;
			Text = String.Format("Column {0}", ColumnOrder + 1);
			LoadData();
			repositoryItemTextEdit.MouseDown += EditorHelper.EditorMouseDown;
			repositoryItemTextEdit.MouseUp += EditorHelper.EditorMouseUp;
			repositoryItemTextEdit.Enter += EditorHelper.EditorEnter;
		}

		public void LoadData()
		{
			var columnFolders = _page.Folders.Where(f => f.ColumnOrder == ColumnOrder).OrderBy(f => f.RowOrder).ToList();
			gridControl.DataSource = columnFolders;
			gridView.RefreshData();

			switch (ColumnOrder)
			{
				case 0:
					repositoryItemButtonEditWindowOperations.Buttons[3].Enabled = true;
					repositoryItemButtonEditWindowOperations.Buttons[2].Enabled = false;
					break;
				case 1:
					repositoryItemButtonEditWindowOperations.Buttons[3].Enabled = true;
					repositoryItemButtonEditWindowOperations.Buttons[2].Enabled = true;
					break;
				case 2:
					repositoryItemButtonEditWindowOperations.Buttons[3].Enabled = false;
					repositoryItemButtonEditWindowOperations.Buttons[2].Enabled = true;
					break;
			}
		}

		public void SaveData()
		{
			gridView.CloseEditor();
		}

		public void AddFolder()
		{
			_page.AddFolder(ColumnOrder);
			LoadData();
			gridView.Focus();
			gridView.FocusedRowHandle = gridView.RowCount - 1;
			gridView.ShowEditor();
		}

		public void CopyFolder()
		{
			var folder = gridView.GetFocusedRow() as LibraryFolder;
			if (folder == null) return;
			FolderInBuffer = null;
			FolderCopied?.Invoke(this, new FolderCopiedEventArgs { SourceFolder = folder });
		}

		public void PasteFolder()
		{
			if (FolderInBuffer == null) return;
			var oldColumnOrder = FolderInBuffer.ColumnOrder;
			_page.MoveFolderToColumn(FolderInBuffer, ColumnOrder);
			LoadData();
			FolderPasted?.Invoke(this, new FolderPastedEventArgs { OldColumnOrder = (Int32)oldColumnOrder });
		}

		public void SortByOrder()
		{
			_page.Folders.ResetItemsOrder(item => item.ColumnOrder == ColumnOrder);
			LoadData();
		}

		private void gridView_PopupMenuShowing(object sender, PopupMenuShowingEventArgs e)
		{
			if (e.Menu == null)
				e.Menu = new GridViewMenu((GridView)sender);
			if (e.HitInfo.InRowCell)
				e.Menu.Items.Add(new DXMenuItem("Cut", (o, args) => CopyFolder()));
			e.Menu.Items.Add(new DXMenuItem("Paste", (o, args) => PasteFolder()) { Enabled = FolderInBuffer != null });
		}

		private void gridView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
		{
			FolderChanged?.Invoke(this, EventArgs.Empty);
		}

		private void repositoryItemButtonEditWindowOperations_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
		{
			var folder = gridView.GetFocusedRow() as LibraryFolder;
			if (folder == null) return;
			switch (e.Button.Index)
			{
				case 0:
					{
						var newRowHandle = gridView.FocusedRowHandle - 1;
						_page.Folders.UpItem(folder, item => item.ColumnOrder == ColumnOrder);
						LoadData();
						gridView.FocusedRowHandle = newRowHandle >= 0 ? newRowHandle : 0;
						FolderChanged?.Invoke(this, EventArgs.Empty);
					}
					break;
				case 1:
					{
						var newRowHandle = gridView.FocusedRowHandle + 1;
						_page.Folders.DownItem(folder, item => item.ColumnOrder == ColumnOrder);
						LoadData();
						gridView.FocusedRowHandle = newRowHandle < gridView.RowCount ? newRowHandle : gridView.RowCount - 1;
						FolderChanged?.Invoke(this, EventArgs.Empty);
					}
					break;
				case 2:
					FolderMovedLeft?.Invoke(this, EventArgs.Empty);
					break;
				case 3:
					FolderMovedRight?.Invoke(this, EventArgs.Empty);
					break;
				case 4:
					using (var form = new FormWindow(folder, new BaseEditFormParams()))
					{
						if (form.ShowDialog() != DialogResult.OK) return;
						gridView.RefreshData();
						FolderChanged?.Invoke(this, EventArgs.Empty);
					}
					break;
				case 5:
					if (MainController.Instance.PopupMessages.ShowWarningQuestion("Are you sure want to delete selected window?") != DialogResult.Yes) return;
					_page.Folders.RemoveItem(folder, item => item.ColumnOrder == ColumnOrder);
					LoadData();
					FolderChanged?.Invoke(this, EventArgs.Empty);
					break;
			}
		}
	}

	public class FolderCopiedEventArgs : EventArgs
	{
		public LibraryFolder SourceFolder { get; set; }
	}

	public class FolderPastedEventArgs : EventArgs
	{
		public int OldColumnOrder { get; set; }
	}
}
