using System;
using System.Linq;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Wallbin.Persistent;
using SalesLibraries.Common.Helpers;
using SalesLibraries.CommonGUI.Wallbin.ColumnTitles;
using SalesLibraries.CommonGUI.Wallbin.Folders;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.Views
{
	class ColumnsPageContent : PageContent
	{
		private ColumnTitlePanel _columnTitlePanel;

		public ColumnsPageContent(IPageView pageContainer) : base(pageContainer) { }

		#region Public Methods
		public override void DisposeContent()
		{
			base.DisposeContent();
			DisposeColumnTitles();
		}
		#endregion

		#region Protected Methods
		protected override void LoadContentParts()
		{
			LoadColumnTitles();
			LoadFolders();
		}

		protected override void UpdateContentParts()
		{
			UpdateColumnTitlesSize();
			UpdateFolders();
		}

		protected override void UpdateContentPartsSizes()
		{
			UpdateColumnTitlesSize();
			UpdateFoldersSize();
		}
		#endregion

		#region Folders Processing
		protected override void OnFolderSizeChanged(object sender, EventArgs e)
		{
			var folder = (BaseFolderBox)sender;
			WinAPIHelper.SendMessage(Handle, 11, IntPtr.Zero, IntPtr.Zero);
			UpdateFoldersColumnSize(folder.DataSource.ColumnOrder);
			WinAPIHelper.SendMessage(Handle, 11, new IntPtr(1), IntPtr.Zero);
			Refresh();
		}

		protected override void UpdateFoldersSize()
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
	}
}
