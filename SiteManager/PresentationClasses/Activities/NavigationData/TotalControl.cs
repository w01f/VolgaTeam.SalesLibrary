using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.Utils;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesDepot.Services.StatisticService;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.NavigationData
{
	[ToolboxItem(false)]
	//public partial class TotalControl : UserControl, IGroupControl
	public partial class TotalControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<NavigationGroupReportRecord> Records { get; private set; }
		public PrintableComponentLink PrintLink { get; private set; }

		public string GroupName
		{
			get { return Text; }
		}

		public TotalControl(IEnumerable<NavigationGroupReportRecord> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			Text = "Total Summary";

			Records = new List<NavigationGroupReportRecord>();
			Records.AddRange(records);

			_startDate = startDate;
			_endDate = endDate;

			gridControlData.DataSource = Records;

			PrintLink = new PrintableComponentLink()
			{
				Landscape = true,
				PaperKind = PaperKind.A4,
				Component = gridControlData
			};
			PrintLink.CreateReportHeaderArea += OnCreateReportHeaderArea;
		}

		public void ApplyColumns(GroupFilter filter)
		{
			gridColumnGroupLibrariesNumber.Visible = filter.ShowNumber;
			gridColumnGroupLibrariesPercent.Visible = filter.ShowPercent;

			gridColumnGroupPagesNumber.Visible = filter.ShowNumber;
			gridColumnGroupPagesPercent.Visible = filter.ShowPercent;

			gridColumnGroupTotalNumber.Visible = filter.ShowNumber;
			gridColumnGroupTotalPercent.Visible = filter.ShowPercent;
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("Navigation Group Activity Report: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}

		private void advBandedGridViewData_CustomDrawRowFooterCell(object sender, DevExpress.XtraGrid.Views.Grid.FooterCellCustomDrawEventArgs e)
		{
			if (e.Column != gridColumnName)
				e.Appearance.TextOptions.HAlignment = HorzAlignment.Center;
		}
	}
}