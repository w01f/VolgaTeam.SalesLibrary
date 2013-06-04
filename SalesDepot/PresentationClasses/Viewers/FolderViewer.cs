using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using SalesDepot.BusinessClasses;
using SalesDepot.CoreObjects.BusinessClasses;

namespace SalesDepot.PresentationClasses.Viewers
{
	[ToolboxItem(false)]
	public partial class FolderViewer : UserControl, IFileViewer
	{
		#region Properties
		private List<ILibraryLink> FolderContent
		{
			get { return (File as LibraryFolderLink).FolderContent; }
		}

		private LibraryLink SelectedFile
		{
			get { return gridViewFiles.GetFocusedRow() as LibraryLink; }
		}

		public LibraryLink File { get; private set; }

		public string DisplayName
		{
			get { return File.DisplayName; }
		}

		public string CriteriaOverlap
		{
			get { return File.CriteriaOverlap; }
		}

		public Image Widget
		{
			get { return File.Widget; }
		}
		#endregion

		public FolderViewer(LibraryLink file)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Visible = false;

			File = file;
			gridControlFiles.DataSource = FolderContent;
		}

		#region IFileViewer Methods
		public void ReleaseResources() { }

		public void Open()
		{
			LinkManager.Instance.OpenFolder(File);
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

		public void EmailLinkToQuickSite()
		{
			LinkManager.Instance.EmailLinkToQuickSite(File);
		}

		public void AddLinkToQuickSite()
		{
			LinkManager.Instance.AddLinkToQuickSite(File);
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
				LinkManager.Instance.OpenLink(SelectedFile);
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
				var toolTip = new List<string>();
				toolTip.Add(file.NameWithExtension);
				toolTip.Add("Added: " + file.AddDate.ToString("M/dd/yy h:mm:ss tt"));
				if (file.ExpirationDateOptions.EnableExpirationDate && file.ExpirationDateOptions.ExpirationDate != DateTime.MinValue)
					toolTip.Add("Expires: " + file.ExpirationDateOptions.ExpirationDate.ToString("M/dd/yy h:mm:ss tt"));
				else
					toolTip.Add("Expires: No Expiration Date");
				info = new ToolTipControlInfo(new CellToolTipInfo(hi.RowHandle, hi.Column, "cell"), string.Join(Environment.NewLine, toolTip.ToArray()));
			}
			finally
			{
				e.Info = info;
			}
		}
	}
}