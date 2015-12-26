using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;
using Font = System.Drawing.Font;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.NavigationData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl, IGroupControl
	public partial class GroupControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<NavigationUserReportModel> Records { get; private set; }

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

		public GroupControl(IEnumerable<NavigationUserReportModel> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<NavigationUserReportModel>();
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

		public void ApplyColumns(UserFilter filter)
		{
			gridColumnUserLibrariesNumber.Visible = filter.ShowNumber;
			gridColumnUserLibrariesPercent.Visible = filter.ShowPercent;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLibrariesNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLibrariesPercent, filter.ShowPercent && !filter.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupLibrariesNumber, 1, filter.ShowPercent && !filter.ShowNumber ? 0 : 1);
			gridColumnGroupLibrariesNumber.RowCount = !filter.ShowNumber && !filter.ShowPercent ? 2 : 1;

			gridColumnUserPagesNumber.Visible = filter.ShowNumber;
			gridColumnUserPagesPercent.Visible = filter.ShowPercent;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserPagesNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserPagesPercent, filter.ShowPercent && !filter.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupPagesNumber, 1, filter.ShowPercent && !filter.ShowNumber ? 0 : 1);
			gridColumnGroupPagesNumber.RowCount = !filter.ShowNumber && !filter.ShowPercent ? 2 : 1;

			gridColumnUserTotalNumber.Visible = filter.ShowNumber;
			gridColumnUserTotalPercent.Visible = filter.ShowPercent;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalPercent, filter.ShowPercent && !filter.ShowNumber ? 0 : 1, 0);
			advBandedGridViewData.SetColumnPosition(gridColumnGroupTotalNumber, 1, filter.ShowPercent && !filter.ShowNumber ? 0 : 1);
			gridColumnGroupTotalNumber.RowCount = !filter.ShowNumber && !filter.ShowPercent ? 2 : 1;
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("User Navigation Analysis: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}