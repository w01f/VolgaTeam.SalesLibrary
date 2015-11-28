using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ProgramManager.CoreObjects;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.SalesDepot.Configuration;

namespace SalesLibraries.SalesDepot.Business.ProgramSchedule
{
	static class ProgramScheduleReportHelper
	{
		public static void GenerateWeekSchedule(
			string templatePath,
			Day[][] days,
			string destinationFilePath,
			bool convertToPDF,
			bool landscape,
			ProgramScheduleSettings outputSettings)
		{
			if (!File.Exists(templatePath) || !ExcelHelper.Instance.Connect()) return;
			try
			{
				var destinationWorkBook = ExcelHelper.Instance.ExcelObject.Workbooks.Add();

				for (var i = 1; i < 3; i++)
					try
					{
						destinationWorkBook.Worksheets[i].Delete();
					}
					catch
					{
						break;
					}

				var worksheetIndex = 1;
				var sheduleGenrated = DateTime.Now;

				foreach (var weekDays in days)
				{
					var sourceWorkBook = ExcelHelper.Instance.ExcelObject.Workbooks.Open(templatePath);
					Worksheet workSheet = sourceWorkBook.Worksheets["Week"];

					var title = string.Format("{0} - Weekly Program Schedule", weekDays.FirstOrDefault().Station.Name);
					var dateRange = string.Format("Week of {0}", weekDays.FirstOrDefault().Date.ToString("MMMM d, yyyy"));
					workSheet.PageSetup.CenterHeader = String.Format("&\"{0}{2}\"&{1}", outputSettings.HeaderFont.Name, outputSettings.HeaderFont.Size.ToString(), outputSettings.HeaderFont.Bold ? ",bold" : string.Empty) + title + (char)13 + dateRange;

					workSheet.PageSetup.CenterFooter = String.Format("&\"{0}{2}\"&{1}", outputSettings.FooterFont.Name, outputSettings.FooterFont.Size.ToString(), outputSettings.FooterFont.Bold ? ",bold" : string.Empty) + "Schedule Generated" + (char)13 + sheduleGenrated.ToString("MM/dd/yy h:mm tt");

					var range = workSheet.Range["Data"];
					range.Font.Name = outputSettings.BodyFont.Name;
					range.Font.Size = outputSettings.BodyFont.Size;
					range.Font.Bold = outputSettings.BodyFont.Bold;
					range.Font.Italic = outputSettings.BodyFont.Italic;

					if (outputSettings.UsePrimeTimeSpecialFontSize)
					{
						{
							var firstColumn = workSheet.Range["day1"].Column;
							var lastColumn = workSheet.Range["day6"].Column;

							var firstRow = workSheet.Range["day1"].Row + 1;
							var time = new DateTime(1, 1, 1, 5, 0, 0);
							while (!(time.Hour == outputSettings.WeekPrimeTimeStart.Hour && time.Minute == outputSettings.WeekPrimeTimeStart.Minute))
							{
								time = time.AddMinutes(30);
								firstRow++;
							}

							var lastRow = firstRow;
							while (!(time.Hour == outputSettings.WeekPrimeTimeEnd.Hour && time.Minute == outputSettings.WeekPrimeTimeEnd.Minute))
							{
								time = time.AddMinutes(30);
								lastRow++;
							}

							range = workSheet.Range[GetColumnLetterByIndex(firstColumn) + firstRow + ":" + GetColumnLetterByIndex(lastColumn) + lastRow];
							range.Font.Name = outputSettings.BodyFont.Name;
							range.Font.Size = outputSettings.PrimeTimeSpecialFontSize;
							range.Font.Bold = outputSettings.BodyFont.Bold;
							range.Font.Italic = outputSettings.BodyFont.Italic;
						}

						{
							var firstColumn = workSheet.Range["day7"].Column;
							var lastColumn = workSheet.Range["day7"].Column;

							var firstRow = workSheet.Range["day7"].Row + 1;
							var time = new DateTime(1, 1, 1, 5, 0, 0);
							while (!(time.Hour == outputSettings.SundayPrimeTimeStart.Hour && time.Minute == outputSettings.SundayPrimeTimeStart.Minute))
							{
								time = time.AddMinutes(30);
								firstRow++;
							}

							var lastRow = firstRow;
							while (!(time.Hour == outputSettings.SundayPrimeTimeEnd.Hour && time.Minute == outputSettings.SundayPrimeTimeEnd.Minute))
							{
								time = time.AddMinutes(30);
								lastRow++;
							}

							range = workSheet.Range[GetColumnLetterByIndex(firstColumn) + firstRow + ":" + GetColumnLetterByIndex(lastColumn) + lastRow];
							range.Font.Name = outputSettings.BodyFont.Name;
							range.Font.Size = outputSettings.PrimeTimeSpecialFontSize;
							range.Font.Bold = outputSettings.BodyFont.Bold;
							range.Font.Italic = outputSettings.BodyFont.Italic;
						}
					}

					var values = new object[48, 7];
					for (var j = 0; j < 48; j++)
						for (var i = 0; i < 7; i++)
							values[j, i] = weekDays[i].ProgramActivities[j].Program;
					workSheet.Range["Data"].Value2 = values;

					for (var i = 0; i < 7; i++)
					{
						range = workSheet.Range["day" + (i + 1)];
						range.Formula = weekDays[i].Date.ToString(landscape ? "dddd M/d" : "ddd M/d");
						var columnIndex = range.Column;
						var rowIndex = range.Row + 1;
						var programName = string.Empty;
						var firstRow = 0;
						for (var r = 0; r < 48; r++)
						{
							object value = workSheet.Range[GetColumnLetterByIndex(columnIndex) + (rowIndex + r)].Value;
							var currentProgramName = value != null ? value.ToString() : string.Empty;
							if (currentProgramName.Equals(programName)) continue;
							if (!string.IsNullOrEmpty(programName))
								workSheet.Range[GetColumnLetterByIndex(columnIndex) + firstRow + ":" + GetColumnLetterByIndex(columnIndex) + (rowIndex + r - 1)].Merge();
							firstRow = rowIndex + r;
							programName = currentProgramName;
						}
					}

					for (var i = 0; i < 7; i++)
					{
						range = workSheet.Range["day" + (i + 1)];
						var columnIndex = range.Column;
						var r = 0;
						object value;
						do
						{
							value = null;
							var rowIndex = 0;
							var rowRange = range.Offset[r];
							try
							{
								value = rowRange.Formula;
								rowIndex = rowRange.Row;
							}
							catch { }
							if (value != null)
							{
								var programName = string.Empty;
								var firstColumn = 0;
								for (var j = i; j < 7; j++)
								{
									object nextValue = null;
									try
									{
										nextValue = rowRange.Offset[ColumnOffset: j].Value;
									}
									catch { }
									var currentProgramName = nextValue != null ? nextValue.ToString() : string.Empty;
									if (currentProgramName.Equals(programName)) continue;
									if (!string.IsNullOrEmpty(programName))
										workSheet.Range[GetColumnLetterByIndex(firstColumn) + rowIndex + ":" + GetColumnLetterByIndex(columnIndex + j - 1) + rowIndex].Merge();
									firstColumn = columnIndex + j;
									programName = currentProgramName;
								}
							}
							r++;
						} while (value != null && r <= 48);
					}

					var dataRange = workSheet.Range["Data"];

					dataRange.Rows.AutoFit();

					//set correct page breaks
					var fisrtColumnNumber = workSheet.Range["day1"].Column;
					var lastColumnNumber = workSheet.Range["day7"].Column;
					var pageBreaks = workSheet.HPageBreaks;
					for (var i = 1; i <= pageBreaks.Count; i++)
					{
						var currentBreakRow = pageBreaks[i].Location.Row;
						var newBreakRow = currentBreakRow;
						var beforeRange = pageBreaks[i].Location;
						for (var j = fisrtColumnNumber; j <= lastColumnNumber; j++)
						{
							Range cellRange = workSheet.Cells[currentBreakRow, j];
							if (!cellRange.MergeCells) continue;
							if (beforeRange.Row > cellRange.MergeArea.Row)
							{
								beforeRange = cellRange.MergeArea;
								newBreakRow = beforeRange.Row;
							}
						}
						if (newBreakRow != currentBreakRow)
							workSheet.HPageBreaks.Add(beforeRange);
					}

					workSheet.Copy(After: destinationWorkBook.Worksheets[worksheetIndex]);
					worksheetIndex++;
					workSheet = destinationWorkBook.Worksheets[worksheetIndex];
					workSheet.Name = weekDays.FirstOrDefault().Date.ToString("MMddyy") + "-" + weekDays.LastOrDefault().Date.ToString("MMddyy");
					sourceWorkBook.Close();
				}

				destinationWorkBook.Worksheets[1].Delete();
				if (convertToPDF)
					destinationWorkBook.ExportAsFixedFormat(Filename: destinationFilePath, Type: XlFixedFormatType.xlTypePDF);
				else
					destinationWorkBook.SaveAs(destinationFilePath, XlFileFormat.xlWorkbookNormal);

				destinationWorkBook.Close();
			}
			catch { }
			finally
			{
				ExcelHelper.Instance.Disconnect();
			}
		}

		public static void GenerateActivityList(
			string templatePath,
			ProgramActivity[] activities,
			string destinationFilePath,
			bool convertToPDF)
		{
			if (!File.Exists(templatePath) || !ExcelHelper.Instance.Connect()) return;
			try
			{
				var sourceWorkBook = ExcelHelper.Instance.ExcelObject.Workbooks.Open(templatePath);
				Worksheet workSheet = sourceWorkBook.Worksheets["Program Activities"];

				var start = activities.First().Time;
				var end = activities.Last().Time;
				var title = string.Format("Program Activity For {0}", activities.First().Day.Station.Name);
				var dateRange = string.Format("Between {0} and {1} from {2} to {3}", start.ToString("MM/dd/yyyy"), end.ToString("MM/dd/yyyy"), start.ToString("hh:mmtt"), end.ToString("hh:mmtt"));
				workSheet.PageSetup.CenterHeader = "&12&B" + title + (char)13 + dateRange;

				var sheduleGenrated = DateTime.Now;
				workSheet.PageSetup.CenterFooter = "&11Generated" + (char)13 + sheduleGenrated.ToString("MM/dd/yy h:mm tt");

				var range = workSheet.Range["Date"];
				var firstRow = range.Row + 1;
				var lastRow = firstRow;
				var firstColumn = range.Column;
				range = workSheet.Range["Type"];
				var lastColumn = range.Column;

				var rows = new List<object[]>();
				foreach (var activity in activities)
				{
					var cells = new List<object>();
					cells.Add(activity.Date.ToString("MM/dd/yyyy"));
					cells.Add(activity.Date.ToString("ddd"));
					cells.Add(activity.Time.ToString("hh:mmtt"));
					cells.Add(activity.Program);
					cells.Add(activity.HouseNumber);
					cells.Add(activity.Episode);
					cells.Add(activity.Type);
					rows.Add(cells.ToArray());
					lastRow++;
				}
				if (lastRow > firstRow)
					lastRow--;

				if (rows.Count > 0)
				{
					var values = new object[rows.Count, rows[0].Length];
					for (var i = 0; i < rows.Count; i++)
						for (int j = 0; j < rows[0].Length; j++)
							values[i, j] = rows[i][j];

					range = workSheet.Range[GetColumnLetterByIndex(firstColumn) + firstRow + ":" + GetColumnLetterByIndex(lastColumn) + lastRow];
					range.Value2 = values;
					range.Rows.AutoFit();
					range.Borders[XlBordersIndex.xlEdgeBottom].LineStyle = XlLineStyle.xlContinuous;
					range.Borders[XlBordersIndex.xlEdgeLeft].LineStyle = XlLineStyle.xlContinuous;
					range.Borders[XlBordersIndex.xlEdgeRight].LineStyle = XlLineStyle.xlContinuous;
					range.Borders[XlBordersIndex.xlEdgeTop].LineStyle = XlLineStyle.xlContinuous;
					range.Borders[XlBordersIndex.xlInsideHorizontal].LineStyle = XlLineStyle.xlContinuous;
					range.Borders[XlBordersIndex.xlInsideVertical].LineStyle = XlLineStyle.xlContinuous;

					if (convertToPDF)
						sourceWorkBook.ExportAsFixedFormat(Filename: destinationFilePath, Type: XlFixedFormatType.xlTypePDF);
					else
						sourceWorkBook.SaveAs(destinationFilePath, XlFileFormat.xlWorkbookNormal);
				}

				sourceWorkBook.Close();
			}
			catch { }
			finally
			{
				ExcelHelper.Instance.Disconnect();
			}
		}

		private static string GetColumnLetterByIndex(int index)
		{
			switch (index)
			{
				case 1:
					return "A";
				case 2:
					return "B";
				case 3:
					return "C";
				case 4:
					return "D";
				case 5:
					return "E";
				case 6:
					return "F";
				case 7:
					return "G";
				case 8:
					return "H";
				case 9:
					return "I";
				case 10:
					return "J";
				case 11:
					return "K";
				case 12:
					return "L";
				default:
					return string.Empty;
			}
		}
	}
}
