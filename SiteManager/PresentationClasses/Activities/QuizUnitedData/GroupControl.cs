﻿using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using DevExpress.Data;
using DevExpress.Utils;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraTab;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.ToolClasses;

namespace SalesDepot.SiteManager.PresentationClasses.Activities.QuizUnitedData
{
	[ToolboxItem(false)]
	//public partial class GroupControl : UserControl
	public partial class GroupControl : XtraTabPage
	{
		public List<QuizPassUserReportRecord> Records { get; private set; }
		private readonly int _quizCount;

		public GroupControl(IEnumerable<QuizPassUserReportRecord> records, int quizCount)
		{
			InitializeComponent();
			Records = new List<QuizPassUserReportRecord>();
			Records.AddRange(records.OrderBy(r => r.FullName));
			_quizCount = quizCount;
			gridControlData.DataSource = Records;
		}

		public void CollapseAll()
		{
			gridViewData.CollapseAllGroups();
		}

		public void ExpandAll()
		{
			gridViewData.ExpandAllGroups();
		}

		private void advBandedGridViewData_EndGrouping(object sender, System.EventArgs e)
		{
			(sender as GridView).ExpandAllGroups();
		}

		private void gridViewData_CustomDrawGroupRow(object sender, RowObjectCustomDrawEventArgs e)
		{
			var view = sender as GridView;
			var items = GroupSummaryHelper.ExtractSummaryItems(view);
			if (items.Count == 0) return;
			GroupSummaryHelper.DrawBackground(e, view);
			GroupSummaryHelper.DrawSummaryValues(e, view, items);
			e.Handled = true;
		}

		private void gridViewData_CustomColumnSort(object sender, CustomColumnSortEventArgs e)
		{
			if (e.Column != gridColumnQuiz) return;
			var record1 = e.RowObject1 as QuizPassUserReportRecord;
			var record2 = e.RowObject1 as QuizPassUserReportRecord;
			if (record1 == null || !record1.Date.HasValue || record2 == null || !record2.Date.HasValue) return;
			e.Handled = true;
			e.Result = record1.Date.Value.CompareTo(record2.Date.Value);
		}

		private void gridViewData_CustomSummaryCalculate(object sender, CustomSummaryEventArgs e)
		{
			if (!e.IsTotalSummary || e.SummaryProcess != CustomSummaryProcess.Finalize) return;
			if (e.Item == gridColumnUser.SummaryItem)
				e.TotalValue = _quizCount;
		}

		private void gridViewData_CustomDrawFooterCell(object sender, FooterCellCustomDrawEventArgs e)
		{
			if (e.Column == gridColumnUser)
				e.Appearance.TextOptions.HAlignment = HorzAlignment.Near;
		}
	}
}