﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Windows.Forms;
using DevExpress.XtraPrinting;
using DevExpress.XtraTab;
using SalesDepot.Services.StatisticService;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.RawData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl, IGroupControl
	public partial class GroupControl : XtraTabPage, IGroupControl
	{
		private readonly DateTime _startDate;
		private readonly DateTime _endDate;
		public List<UserActivity> Records { get; private set; }
		public PrintableComponentLink PrintLink { get; private set; }

		private string _groupName;
		public string GroupName
		{
			get { return _groupName; }
			set
			{
				_groupName = value;
				Text = String.IsNullOrEmpty(_groupName) ? "No Group" : value;
			}
		}

		public GroupControl(IEnumerable<UserActivity> records, DateTime startDate, DateTime endDate)
		{
			InitializeComponent();
			Dock = DockStyle.Fill;

			Records = new List<UserActivity>();
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

		public void ApplyColumns(Filter filter)
		{
			gridColumnType.Visible = filter.ShowActionGroup;
			gridColumnSubType.Visible = filter.ShowAction;
			gridColumnFile.Visible = filter.ShowFile;
			gridViewData.OptionsView.ShowPreview = filter.ShowDetails;
		}

		private void OnCreateReportHeaderArea(object sender, CreateAreaEventArgs e)
		{
			var reportHeader = string.Format("Activities: {0} - {1}", _startDate.ToString("MM/dd/yy"), _endDate.AddDays(-1).ToString("MM/dd/yy"));
			e.Graph.StringFormat = new BrickStringFormat(StringAlignment.Center);
			e.Graph.Font = new Font("Arial", 12, FontStyle.Bold);
			var rec = new RectangleF(0, 0, e.Graph.ClientPageSize.Width, 50);
			e.Graph.DrawString(reportHeader, Color.Black, rec, BorderSide.None);
		}
	}
}