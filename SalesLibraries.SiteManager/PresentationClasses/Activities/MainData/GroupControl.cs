using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.MainData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl, IGroupControl
	public partial class GroupControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<MainUserReportModel> Records { get; private set; }

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

		public GroupControl(IEnumerable<MainUserReportModel> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<MainUserReportModel>();
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
			gridColumnName.Visible = filter.ShowUsers;
			gridColumnGroup.Visible = filter.ShowGroups;
			if (filter.ShowUsers)
			{
				gridColumnName.RowCount = !filter.ShowGroups && filter.ShowNumber && filter.ShowPercent ? 2 : 1;
			}
			if (filter.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroup, 1, filter.ShowUsers ? 2 : 1);
			}

			gridColumnUserLoginNumber.Visible = filter.ShowNumber;
			gridColumnUserLoginPercent.Visible = filter.ShowPercent;
			gridColumnGroupLoginNumber.Visible = filter.ShowGroups;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLoginNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserLoginPercent, filter.ShowPercent && !filter.ShowNumber ? 0 : 1, 0);
			if (filter.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupLoginNumber, 1, filter.ShowPercent && !filter.ShowNumber ? 0 : 1);
				gridColumnGroupLoginNumber.RowCount = !filter.ShowNumber && !filter.ShowPercent && filter.ShowUsers ? 2 : 1;
			}

			gridColumnUserDocsNumber.Visible = filter.ShowNumber;
			gridColumnUserDocsPercent.Visible = filter.ShowPercent;
			gridColumnGroupDocsNumber.Visible = filter.ShowGroups;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserDocsNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserDocsPercent, filter.ShowPercent && !filter.ShowNumber ? 0 : 1, 0);
			if (filter.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupDocsNumber, 1, filter.ShowPercent && !filter.ShowNumber ? 0 : 1);
				gridColumnGroupDocsNumber.RowCount = !filter.ShowNumber && !filter.ShowPercent && filter.ShowUsers ? 2 : 1;
			}

			gridColumnUserVideosNumber.Visible = filter.ShowNumber;
			gridColumnUserVideosPercent.Visible = filter.ShowPercent;
			gridColumnGroupVideosNumber.Visible = filter.ShowGroups;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserVideosNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserVideosPercent, filter.ShowPercent && !filter.ShowNumber ? 0 : 1, 0);
			if (filter.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupVideosNumber, 1, filter.ShowPercent && !filter.ShowNumber ? 0 : 1);
				gridColumnGroupVideosNumber.RowCount = !filter.ShowNumber && !filter.ShowPercent && filter.ShowUsers ? 2 : 1;
			}

			gridColumnUserTotalNumber.Visible = filter.ShowNumber;
			gridColumnUserTotalPercent.Visible = filter.ShowPercent;
			gridColumnGroupTotalNumber.Visible = filter.ShowGroups;
			if (filter.ShowNumber)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalNumber, 0, 0);
			if (filter.ShowPercent)
				advBandedGridViewData.SetColumnPosition(gridColumnUserTotalPercent, filter.ShowPercent && !filter.ShowNumber ? 0 : 1, 0);
			if (filter.ShowGroups)
			{
				advBandedGridViewData.SetColumnPosition(gridColumnGroupTotalNumber, 1, filter.ShowPercent && !filter.ShowNumber ? 0 : 1);
				gridColumnGroupTotalNumber.RowCount = !filter.ShowNumber && !filter.ShowPercent && filter.ShowUsers ? 2 : 1;
			}
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("General User Activity Report: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}