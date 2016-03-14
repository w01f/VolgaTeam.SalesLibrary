using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.PresentationClasses.Common;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.FileActivityData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl, IGroupControl
	public partial class GroupControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<FileActivityReportModel> Records { get; private set; }

		private string _groupName;
		public string GroupName
		{
			get { return _groupName; }
			set
			{
				_groupName = String.IsNullOrEmpty(value) ? "No Group" : value;
				Text = _groupName;
			}
		}

		public GroupControl(IEnumerable<FileActivityReportModel> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			Records = new List<FileActivityReportModel>();
			Records.AddRange(records);

			_startDate = startDate;
			_endDate = endDate;

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
			printLink.CreateReportHeaderArea += OnCreateReportHeaderArea;
			return printLink;
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("File Access Summary: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}