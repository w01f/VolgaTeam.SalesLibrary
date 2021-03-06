﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.Skins;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesLibraries.ServiceConnector.StatisticService;
using SalesLibraries.SiteManager.PresentationClasses.Common;

namespace SalesLibraries.SiteManager.PresentationClasses.Activities.AccessData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl, IGroupControl
	public partial class GroupControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		private bool _showActive;
		private bool _showInactive;
		public List<AccessReportModel> Records { get; private set; }

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

		public GroupControl(IEnumerable<AccessReportModel> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;
			Records = new List<AccessReportModel>();
			Records.AddRange(records);

			_startDate = startDate;
			_endDate = endDate;

			gridControlData.DataSource = Records;

			if (CreateGraphics().DpiX > 96)
			{
				gridBand1.Width =
					RectangleHelper.ScaleHorizontal(gridBand1.Width, gridControlData.ScaleFactor.Width);
				bandedGridColumnTotalUsersNumber.Width =
					RectangleHelper.ScaleHorizontal(bandedGridColumnTotalUsersNumber.Width, gridControlData.ScaleFactor.Width);
				bandedGridColumnActiveUsersPercent.Width =
					RectangleHelper.ScaleHorizontal(bandedGridColumnActiveUsersPercent.Width, gridControlData.ScaleFactor.Width);
				bandedGridColumnInactiveUsersPercent.Width =
					RectangleHelper.ScaleHorizontal(bandedGridColumnInactiveUsersPercent.Width, gridControlData.ScaleFactor.Width);
			}
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

		public void ApplyColumns(GroupFilter filter)
		{
			_showActive = filter.ShowActive;
			_showInactive = filter.ShowInactive;
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
			advBandedGridViewData.RefreshData();
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("Individual User Group Site Access Report: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}