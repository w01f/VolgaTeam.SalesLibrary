using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesDepot.Services.StatisticService;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.AccessData
{
	[ToolboxItem(false)]
	//public partial class TotalControl : UserControl, IGroupControl
	public partial class TotalControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<AccessReportModel> Records { get; private set; }

		public string GroupName
		{
			get { return Text; }
		}

		public TotalControl(IEnumerable<AccessReportModel> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Text = "Total Summary";
			Records = new List<AccessReportModel>();
			Records.AddRange(records);

			_startDate = startDate;
			_endDate = endDate;

			gridControlData.DataSource = Records.Take(1).ToList();
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
			printLink.CreateReportHeaderArea += OnCreateReportHeaderArea;
			return printLink;
		}

		public void ApplyColumns(TotalFilter filter)
		{
			gridColumnActiveNumber.Visible = filter.ShowNumber;
			gridColumnActivePercent.Visible = filter.ShowPercent;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnActiveNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnActivePercent, 0, 1);
			gridColumnInactiveNumber.Visible = filter.ShowNumber;
			gridColumnInactivePercent.Visible = filter.ShowPercent;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnInactiveNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnInactivePercent, 0, 1);
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("Universal Site Access Report: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}