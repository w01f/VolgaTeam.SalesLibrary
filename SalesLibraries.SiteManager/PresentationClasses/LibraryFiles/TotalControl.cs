using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
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
	public partial class TotalControl : XtraTabPage, IGroupControl
	{
		public List<LibraryFilesTotalModel> Records { get; private set; }

		public string GroupName => Text;

		public TotalControl(IEnumerable<LibraryFilesTotalModel> records)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<LibraryFilesTotalModel>();
			Records.AddRange(records);
			gridControlData.DataSource = Records;
			Text = "Total Summary";
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

		private void OnCustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}
	}
}