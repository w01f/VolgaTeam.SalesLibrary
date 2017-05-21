using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;

namespace SalesLibraries.BatchTagger.PresentationLayer
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl
	public partial class TotalControl : XtraTabPage
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

		private void OnCustomDrawCell(object sender, RowCellCustomDrawEventArgs e)
		{
			if (e.RowHandle % 2 == 0)
				e.Appearance.BackColor = SystemColors.ControlLight;
		}
	}
}