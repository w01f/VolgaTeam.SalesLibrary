using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using SalesDepot.Services.StatisticService;
using SalesDepot.SiteManager.InteropClasses;

namespace SalesDepot.SiteManager.ToolClasses
{
	public class QuizStatisticExportHelper
	{
		public static void ExportQuizStatistic(string filePath,
			string header,
			string group,
			int totalUsers,
			int totalGroups,
			IEnumerable<QuizPassGroupReportModel> totalStatistic,
			IEnumerable<IEnumerable<QuizPassUserReportModel>> groupStatistic)
		{
			try
			{
				if (!ExcelHelper.Instance.Connect()) return;
				MessageFilter.Register();
				var workbook = ExcelHelper.Instance.ExcelObject.Workbooks.Add();
				Worksheet sheet = null;
				var updated = String.Format("Updated: {0}", DateTime.Now.ToString("MM/dd/yy - hmmtt"));
				sheet = workbook.Worksheets.Count < 1 ? workbook.Worksheets.Add() : workbook.Worksheets[1];
				FillSummaryPage(sheet, header, group, totalUsers, totalGroups, updated, totalStatistic);
				var currentSheetIndex = 2;
				foreach (var groupRecords in groupStatistic)
				{
					try
					{
						sheet = workbook.Worksheets[currentSheetIndex];
					}
					catch
					{
						sheet = workbook.Worksheets.Add(After: sheet ?? Type.Missing);
					}
					FillGroupPage(sheet, header, group, updated, groupRecords);
					currentSheetIndex++;
				}
				workbook.Worksheets[1].Select();
				workbook.SaveAs(filePath);
				workbook.Close();
				Utils.ReleaseComObject(workbook);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				ExcelHelper.Instance.Disconnect();
			}
		}


		private static void FillSummaryPage(Worksheet sheet, string header, string topLevelGroup, int totalUsers, int totalGroups, string updated, IEnumerable<QuizPassGroupReportModel> totalRecords)
		{
			const string takenHeader = "TAKEN:";
			const string passedHeader = "PASSED:";
			const string groupName = "RAYCOM";

			var totalTaken = totalRecords.Sum(gr => gr.Taken);
			var totalPassed = totalRecords.Sum(gr => gr.Passed);

			sheet.Name = "!Quiz Analysis Rollup";

			var rowPosition = 1;

			var wholeRange = sheet.Range["A1", "D1000"];
			wholeRange.Font.Name = "Arial";
			wholeRange.Font.Size = 12;
			wholeRange.EntireRow.AutoFit();

			sheet.Range["A:A"].ColumnWidth = 6;
			sheet.Range["A:A"].HorizontalAlignment = Constants.xlCenter;
			sheet.Range["B:B"].ColumnWidth = 70;
			sheet.Range["B:B"].HorizontalAlignment = Constants.xlLeft;
			sheet.Range["C:C"].ColumnWidth = 12;
			sheet.Range["C:C"].HorizontalAlignment = Constants.xlCenter;
			sheet.Range["D:D"].ColumnWidth = 12;
			sheet.Range["D:D"].HorizontalAlignment = Constants.xlCenter;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = header;
			sheet.Range[String.Format("B{0}", rowPosition)].Font.Bold = true;
			sheet.Range[String.Format("C{0}", rowPosition)].Value = updated;
			sheet.Range[String.Format("C{0}", rowPosition)].VerticalAlignment = XlVAlign.xlVAlignBottom;
			sheet.Range[String.Format("C{0}", rowPosition)].HorizontalAlignment = XlHAlign.xlHAlignGeneral;
			rowPosition++;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = String.Format("# of Stations Participating: {0}", totalGroups);
			rowPosition++;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = String.Format("# of Sales Reps Participating: {0}", totalUsers);
			rowPosition++;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = String.Format("# of Sales Reps Certified: {0}", 0);
			rowPosition += 2;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = String.IsNullOrEmpty(topLevelGroup) ? "Total Quizzes in RAYCOM Sales Certification Program:" : String.Format("Total Quizzes in {0}", topLevelGroup);
			sheet.Range[String.Format("B{0}", rowPosition)].Interior.Color = 12632256;
			sheet.Range[String.Format("C{0}", rowPosition)].Value = takenHeader;
			sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
			sheet.Range[String.Format("D{0}", rowPosition)].Value = passedHeader;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
			rowPosition++;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = groupName;
			sheet.Range[String.Format("C{0}", rowPosition)].Value = totalTaken;
			sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
			sheet.Range[String.Format("D{0}", rowPosition)].Value = totalPassed;
			sheet.Range[String.Format("C{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
			rowPosition++;

			var groupNames = totalRecords.GroupBy(gr => gr.group).Select(group => new
			{
				Name = group.Key,
				TotalPassed = group.Sum(g => g.Passed)
			}).OrderByDescending(item => item.TotalPassed).Select(item => item.Name);
			var groupBegin = rowPosition;
			var groupEnd = rowPosition;
			foreach (var name in groupNames)
			{
				var groupResults = totalRecords.Where(gr => gr.group.Equals(name));
				var groupTaken = groupResults.Sum(gr => gr.Taken);
				var groupPassed = groupResults.Sum(gr => gr.Passed);

				groupEnd = rowPosition;
				sheet.Range[String.Format("B{0}", rowPosition)].Value = name;
				sheet.Range[String.Format("C{0}", rowPosition)].Value = groupTaken;
				sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
				sheet.Range[String.Format("D{0}", rowPosition)].Value = groupPassed;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
				rowPosition++;
			}
			sheet.Range[String.Format("B{0}", groupBegin), String.Format("D{0}", groupEnd)].Rows.Group();

			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Interior.Color = 526344;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
			rowPosition += 2;


			var quizNames = totalRecords.OrderBy(gr => gr.Date).Select(gr => gr.quizName).Distinct();
			var quizIndex = 1;
			foreach (var quizName in quizNames)
			{
				var quizResults = totalRecords.Where(gr => gr.quizName.Equals(quizName)).OrderBy(gr => gr.group);
				var quizTaken = quizResults.Sum(gr => gr.Taken);
				var quizPassed = quizResults.Sum(gr => gr.Passed);

				sheet.Range[String.Format("A{0}", rowPosition)].Value = quizIndex;
				sheet.Range[String.Format("B{0}", rowPosition)].Value = quizName;
				sheet.Range[String.Format("B{0}", rowPosition)].Interior.Color = 12632256;
				sheet.Range[String.Format("C{0}", rowPosition)].Value = takenHeader;
				sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
				sheet.Range[String.Format("D{0}", rowPosition)].Value = passedHeader;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
				rowPosition++;

				sheet.Range[String.Format("B{0}", rowPosition)].Value = groupName;
				sheet.Range[String.Format("C{0}", rowPosition)].Value = quizTaken;
				sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
				sheet.Range[String.Format("D{0}", rowPosition)].Value = quizPassed;
				sheet.Range[String.Format("C{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
				rowPosition++;

				var quizGroupBegin = rowPosition;
				var quizGroupEnd = rowPosition;
				foreach (var quizResult in quizResults)
				{
					quizGroupEnd = rowPosition;
					sheet.Range[String.Format("B{0}", rowPosition)].Value = quizResult.group;
					sheet.Range[String.Format("C{0}", rowPosition)].Value = quizResult.Taken;
					sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
					sheet.Range[String.Format("D{0}", rowPosition)].Value = quizResult.Passed;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
					rowPosition++;
				}
				sheet.Range[String.Format("B{0}", quizGroupBegin), String.Format("D{0}", quizGroupEnd)].Rows.Group();

				quizIndex++;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Interior.Color = 526344;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
				rowPosition += 2;
			}

			wholeRange.EntireRow.AutoFit();
			sheet.Outline.ShowLevels(1);
		}

		private static void FillGroupPage(Worksheet sheet, string header, string topLevelGroup, string updated, IEnumerable<QuizPassUserReportModel> groupRecords)
		{
			const string takenHeader = "TAKEN:";
			const string passedHeader = "PASSED:";

			var groupName = groupRecords.First().GroupName;
			var groupTaken = groupRecords.Sum(gr => gr.quizTryCount);
			var groupPassed = groupRecords.Count();

			sheet.Name = groupName;

			var rowPosition = 1;

			var wholeRange = sheet.Range["A1", "D1000"];
			wholeRange.Font.Name = "Arial";
			wholeRange.Font.Size = 12;
			wholeRange.EntireRow.AutoFit();

			sheet.Range["A:A"].ColumnWidth = 6;
			sheet.Range["A:A"].HorizontalAlignment = Constants.xlCenter;
			sheet.Range["B:B"].ColumnWidth = 70;
			sheet.Range["B:B"].HorizontalAlignment = Constants.xlLeft;
			sheet.Range["C:C"].ColumnWidth = 12;
			sheet.Range["C:C"].HorizontalAlignment = Constants.xlCenter;
			sheet.Range["D:D"].ColumnWidth = 22;
			sheet.Range["D:D"].HorizontalAlignment = Constants.xlCenter;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = header;
			sheet.Range[String.Format("B{0}", rowPosition)].Font.Bold = true;
			sheet.Range[String.Format("C{0}", rowPosition)].Value = updated;
			sheet.Range[String.Format("C{0}", rowPosition)].VerticalAlignment = XlVAlign.xlVAlignBottom;
			sheet.Range[String.Format("C{0}", rowPosition)].HorizontalAlignment = XlHAlign.xlHAlignGeneral;
			rowPosition += 2;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = String.IsNullOrEmpty(topLevelGroup) ? "Total Quizzes in RAYCOM Sales Certification Program:" : String.Format("Total Quizzes in {0}", topLevelGroup); sheet.Range[String.Format("B{0}", rowPosition)].Interior.Color = 12632256;
			sheet.Range[String.Format("C{0}", rowPosition)].Value = takenHeader;
			sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
			sheet.Range[String.Format("D{0}", rowPosition)].Value = passedHeader;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
			rowPosition++;

			sheet.Range[String.Format("B{0}", rowPosition)].Value = groupName;
			sheet.Range[String.Format("C{0}", rowPosition)].Value = groupTaken;
			sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
			sheet.Range[String.Format("D{0}", rowPosition)].Value = groupPassed;
			sheet.Range[String.Format("C{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
			rowPosition++;

			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Interior.Color = 526344;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
			sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
			rowPosition += 2;


			var quizNames = groupRecords.OrderBy(gr => gr.Date).Select(gr => gr.quizName).Distinct();
			var quizIndex = 1;
			foreach (var quizName in quizNames)
			{
				var quizResults = groupRecords.Where(gr => gr.quizName.Equals(quizName)).OrderBy(gr => gr.FullName);
				var quizTaken = quizResults.Sum(gr => gr.quizTryCount);
				var quizPassed = quizResults.Count();

				sheet.Range[String.Format("A{0}", rowPosition)].Value = quizIndex;
				sheet.Range[String.Format("B{0}", rowPosition)].Value = quizName;
				sheet.Range[String.Format("B{0}", rowPosition)].Interior.Color = 12632256;
				sheet.Range[String.Format("C{0}", rowPosition)].Value = takenHeader;
				sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
				sheet.Range[String.Format("D{0}", rowPosition)].Value = passedHeader;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
				rowPosition++;

				sheet.Range[String.Format("B{0}", rowPosition)].Value = groupName;
				sheet.Range[String.Format("C{0}", rowPosition)].Value = quizTaken;
				sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
				sheet.Range[String.Format("D{0}", rowPosition)].Value = quizPassed;
				sheet.Range[String.Format("C{0}", rowPosition), String.Format("D{0}", rowPosition)].Font.Bold = true;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlMedium;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
				rowPosition++;

				var quizGroupBegin = rowPosition;
				var quizGroupEnd = rowPosition;
				foreach (var quizResult in quizResults)
				{
					quizGroupEnd = rowPosition;
					sheet.Range[String.Format("B{0}", rowPosition)].Value = quizResult.FullName;
					sheet.Range[String.Format("C{0}", rowPosition)].Value = quizResult.quizTryCount;
					sheet.Range[String.Format("C{0}", rowPosition)].Interior.Color = 12632256;
					sheet.Range[String.Format("D{0}", rowPosition)].Value = quizResult.QuizPassDate.HasValue ? quizResult.QuizPassDate.Value.ToString("MM/dd/yyyy hh:mm tt") : null;
					sheet.Range[String.Format("D{0}", rowPosition)].Font.Color = 9868950;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;
					sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlInsideVertical].Weight = XlBorderWeight.xlThin;
					rowPosition++;
				}
				sheet.Range[String.Format("B{0}", quizGroupBegin), String.Format("D{0}", quizGroupEnd)].Rows.Group();

				quizIndex++;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Interior.Color = 526344;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeLeft].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeRight].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeTop].Weight = XlBorderWeight.xlThin;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
				sheet.Range[String.Format("B{0}", rowPosition), String.Format("D{0}", rowPosition)].Borders[XlBordersIndex.xlEdgeBottom].Weight = XlBorderWeight.xlThin;
				rowPosition += 2;
			}
			wholeRange.EntireRow.AutoFit();
			sheet.Outline.ShowLevels(1);
		}
	}
}
