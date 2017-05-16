using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.PresentationClasses.Common;

namespace SalesLibraries.SiteManager.PresentationClasses.LibraryFiles
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl
	public partial class LibraryControl : XtraTabPage, IGroupControl
	{
		public List<LibraryFilesModel> Records { get; private set; }

		private string _libraryName;
		public string GroupName
		{
			get { return _libraryName; }
			set
			{
				_libraryName = String.IsNullOrEmpty(value) ? "No Group" : value;
				Text = _libraryName;
			}
		}

		public LibraryControl(IEnumerable<LibraryFilesModel> records)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<LibraryFilesModel>();
			Records.AddRange(records);
			gridControlData.DataSource = Records;
		}

		public PrintableComponentLink GetPrintLink()
		{
			advBandedGridViewData.CheckLoaded();
			var printLink = new PrintableComponentLink()
			{
				Landscape = true,
				PaperKind = PaperKind.A4,
				Component = gridControlData
			};
			return printLink;
		}

		public void ApplyFilter(LibraryFilter filterControlLibrary)
		{
			if (!filterControlLibrary.EnableFilter || filterControlLibrary.ShowAllLinks)
				gridControlData.DataSource = Records;
			else if (filterControlLibrary.ShowUntaggedLinks)
				gridControlData.DataSource = Records.Where(r => !r.HasCategories).ToList();
			else if (filterControlLibrary.ShowNokeywordLinks)
				gridControlData.DataSource = Records.Where(r => !r.HasKeywords).ToList();
			advBandedGridViewData.RefreshData();
			gridControlData.Update();
		}

		private void OnCustomDrawRowPreview(object sender, RowObjectCustomDrawEventArgs e)
		{
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}

		private void OnCustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}
	}
}