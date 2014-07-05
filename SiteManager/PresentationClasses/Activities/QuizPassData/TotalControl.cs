using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesDepot.Services.StatisticService;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.QuizPassData
{
	[ToolboxItem(false)]
	//public partial class TotalControl : UserControl, IGroupControl
	public partial class TotalControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<QuizPassGroupReportModel> Records { get; private set; }
		public PrintableComponentLink PrintLink { get; private set; }

		public string GroupName
		{
			get { return Text; }
		}

		public TotalControl(IEnumerable<QuizPassGroupReportModel> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Text = "Total Summary";
			Records = new List<QuizPassGroupReportModel>();
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

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("Station Quiz Performance Report: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}