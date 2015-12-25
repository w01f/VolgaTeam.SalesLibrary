using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.SalesDepot.Business.LinkViewers;

namespace SalesLibraries.SalesDepot.PresentationLayer.Wallbin.LinkViewers.Controls
{
	[IntendForClass(typeof(LibraryFolderLink))]
	[ToolboxItem(false)]
	public partial class FolderViewer : UserControl, ILinkViewer
	{
		#region Properties
		private List<FilePreviewShortcut> FolderContent
		{
			get { return ((LibraryFolderLink)Link).Links.Select(link => new FilePreviewShortcut(link)).ToList(); }
		}

		private FilePreviewShortcut SelectedFile
		{
			get { return gridViewFiles.GetFocusedRow() as FilePreviewShortcut; }
		}

		public LibraryObjectLink Link { get; private set; }

		public string DisplayName
		{
			get { return Link.Name; }
		}
		#endregion

		public FolderViewer(LibraryObjectLink link)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			Link = link;
			gridControlFiles.DataSource = FolderContent;
		}

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void Open()
		{
			LinkManager.OpenFolderLink((LibraryFolderLink)Link);
		}

		public void Save()
		{
		}

		public void Email()
		{
		}

		public void Print()
		{
		}
		#endregion

		private void gridViewFiles_MouseMove(object sender, MouseEventArgs e)
		{
			var hi = gridViewFiles.CalcHitInfo(e.X, e.Y);
			Cursor = hi.InRowCell ? Cursors.Hand : Cursors.Default;
		}

		private void gridViewFiles_MouseLeave(object sender, EventArgs e)
		{
			Cursor = Cursors.Default;
		}

		private void gridViewFiles_RowCellClick(object sender, RowCellClickEventArgs e)
		{
			if (SelectedFile != null)
				LinkManager.OpenLink(SelectedFile.SourceFileLink);
		}

		private void toolTipController_GetActiveObjectInfo(object sender, ToolTipControllerGetActiveObjectInfoEventArgs e)
		{
			if (e.SelectedControl != gridControlFiles)
				return;
			var info = e.Info;
			try
			{
				var view = gridControlFiles.GetViewAt(e.ControlMousePosition) as GridView;
				if (view == null)
					return;
				var hi = view.CalcHitInfo(e.ControlMousePosition);
				if (!hi.InRowCell) return;
				var file = FolderContent[gridViewFiles.GetDataSourceRowIndex(hi.RowHandle)];
				info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), file.SourceFileLink.Hint);
			}
			finally
			{
				e.Info = info;
			}
		}
	}
}