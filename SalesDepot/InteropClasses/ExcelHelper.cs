using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using ProgramManager.CoreObjects;
using SalesDepot.ConfigurationClasses;
using SalesDepot.CoreObjects.InteropClasses;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.InteropClasses
{
	internal class ExcelHelper
	{
		private static readonly ExcelHelper _instance = new ExcelHelper();

		private Application _excelObject;
		private bool _isOpened;

		private ExcelHelper() {}

		public static ExcelHelper Instance
		{
			get { return _instance; }
		}

		public bool IsOpened
		{
			get
			{
				Process[] proc = Process.GetProcessesByName("EXCEL");
				if (!(proc.GetLength(0) > 0))
				{
					_excelObject = null;
					_isOpened = false;
				}
				return _isOpened;
			}
		}

		public bool Connect()
		{
			bool result = false;
			try
			{
				_excelObject = new Application();
				_excelObject.Visible = false;
				_excelObject.DisplayAlerts = false;
				result = true;
			}
			catch
			{
				_excelObject = null;
			}
			return result;
		}


		public void Disconnect()
		{
			if (_excelObject != null)
			{
				foreach (Workbook workbook in _excelObject.Workbooks)
					workbook.Close();
				uint processId = 0;
				WinAPIHelper.GetWindowThreadProcessId(new IntPtr(_excelObject.Hwnd), out processId);
				Process.GetProcessById((int)processId).Kill();
			}
			AppManager.Instance.ReleaseComObject(_excelObject);
			GC.Collect();
		}


		public void Print(FileInfo file)
		{
			Workbook workBook = _excelObject.Workbooks.Open(file.FullName);
			_excelObject.Visible = true;
			IntPtr handle = IntPtr.Zero;
			Process[] processList = Process.GetProcesses();
			foreach (Process process in processList.Where(x => x.ProcessName.ToLower().Contains("excel")))
				if (process.MainWindowHandle.ToInt32() != 0)
					Utils.ActivateForm(process.MainWindowHandle, true, false);
			workBook.Application.Dialogs[XlBuiltInDialog.xlDialogPrint].Show();
		}

		public bool ConvertToPDF(string originalFileName, string pdfFileName)
		{
			bool result = false;
			try
			{
				MessageFilter.Revoke();
				Workbook workbook = _excelObject.Workbooks.Open(originalFileName);
				workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFileName, XlFixedFormatQuality.xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
				result = true;
			}
			catch {}
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void ConvertToHtml(string oldFileName, string newFileName)
		{
			try
			{
				MessageFilter.Register();
				Workbook workbook = _excelObject.Workbooks.Open(oldFileName, ReadOnly: true);
				workbook.SaveAs(newFileName, XlFileFormat.xlHtml);
				workbook.Close(false);
			}
			catch {}
			finally
			{
				MessageFilter.Revoke();
			}
		}

		public void ReportWeekSchedule(string templatePath, Day[][] days, string destinationFilePath, bool convertToPDF, bool landscape)
		{
			if (File.Exists(templatePath) && Connect())
			{
				try
				{
					Workbook destinationWorkBook = _excelObject.Workbooks.Add();

					for (int i = 1; i < 3; i++)
						try
						{
							destinationWorkBook.Worksheets[i].Delete();
						}
						catch
						{
							break;
						}

					int worksheetIndex = 1;
					DateTime sheduleGenrated = DateTime.Now;

					foreach (var weekDays in days)
					{
						Workbook sourceWorkBook = _excelObject.Workbooks.Open(templatePath);
						Worksheet workSheet = sourceWorkBook.Worksheets["Week"];

						string title = string.Format("{0} - Weekly Program Schedule", weekDays.FirstOrDefault().Station.Name);
						string dateRange = string.Format("Week of {0}", weekDays.FirstOrDefault().Date.ToString("MMMM d, yyyy"));
						workSheet.PageSetup.CenterHeader = string.Format("&\"{0}{2}\"&{1}", new[] { SettingsManager.Instance.ProgramScheduleOutputSettings.HeaderFont.Name, SettingsManager.Instance.ProgramScheduleOutputSettings.HeaderFont.Size.ToString(), SettingsManager.Instance.ProgramScheduleOutputSettings.HeaderFont.Bold ? ",bold" : string.Empty }) + title + (char)13 + dateRange;

						workSheet.PageSetup.CenterFooter = string.Format("&\"{0}{2}\"&{1}", new[] { SettingsManager.Instance.ProgramScheduleOutputSettings.FooterFont.Name, SettingsManager.Instance.ProgramScheduleOutputSettings.FooterFont.Size.ToString(), SettingsManager.Instance.ProgramScheduleOutputSettings.FooterFont.Bold ? ",bold" : string.Empty }) + "Schedule Generated" + (char)13 + sheduleGenrated.ToString("MM/dd/yy h:mm tt");

						Range range = workSheet.Range["Data"];
						range.Font.Name = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Name;
						range.Font.Size = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Size;
						range.Font.Bold = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Bold;
						range.Font.Italic = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Italic;

						if (SettingsManager.Instance.ProgramScheduleOutputSettings.UsePrimeTimeSpecialFontSize)
						{
							{
								int firstColumn = workSheet.Range["day1"].Column;
								int lastColumn = workSheet.Range["day6"].Column;

								int firstRow = workSheet.Range["day1"].Row + 1;
								var time = new DateTime(1, 1, 1, 5, 0, 0);
								while (!(time.Hour == SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeStart.Hour && time.Minute == SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeStart.Minute))
								{
									time = time.AddMinutes(30);
									firstRow++;
								}

								int lastRow = firstRow;
								while (!(time.Hour == SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeEnd.Hour && time.Minute == SettingsManager.Instance.ProgramScheduleOutputSettings.WeekPrimeTimeEnd.Minute))
								{
									time = time.AddMinutes(30);
									lastRow++;
								}

								range = workSheet.Range[GetColumnLetterByIndex(firstColumn) + firstRow + ":" + GetColumnLetterByIndex(lastColumn) + lastRow];
								range.Font.Name = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Name;
								range.Font.Size = SettingsManager.Instance.ProgramScheduleOutputSettings.PrimeTimeSpecialFontSize;
								range.Font.Bold = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Bold;
								range.Font.Italic = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Italic;
							}

							{
								int firstColumn = workSheet.Range["day7"].Column;
								int lastColumn = workSheet.Range["day7"].Column;

								int firstRow = workSheet.Range["day7"].Row + 1;
								var time = new DateTime(1, 1, 1, 5, 0, 0);
								while (!(time.Hour == SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeStart.Hour && time.Minute == SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeStart.Minute))
								{
									time = time.AddMinutes(30);
									firstRow++;
								}

								int lastRow = firstRow;
								while (!(time.Hour == SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeEnd.Hour && time.Minute == SettingsManager.Instance.ProgramScheduleOutputSettings.SundayPrimeTimeEnd.Minute))
								{
									time = time.AddMinutes(30);
									lastRow++;
								}

								range = workSheet.Range[GetColumnLetterByIndex(firstColumn) + firstRow + ":" + GetColumnLetterByIndex(lastColumn) + lastRow];
								range.Font.Name = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Name;
								range.Font.Size = SettingsManager.Instance.ProgramScheduleOutputSettings.PrimeTimeSpecialFontSize;
								range.Font.Bold = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Bold;
								range.Font.Italic = SettingsManager.Instance.ProgramScheduleOutputSettings.BodyFont.Italic;
							}
						}

						var values = new object[48, 7];
						for (int j = 0; j < 48; j++)
						{
							var cells = new List<object>();
							for (int i = 0; i < 7; i++)
								values[j, i] = weekDays[i].ProgramActivities[j].Program;
						}
						workSheet.Range["Data"].Value2 = values;

						for (int i = 0; i < 7; i++)
						{
							range = workSheet.Range["day" + (i + 1)];
							range.Formula = weekDays[i].Date.ToString(landscape ? "dddd M/d" : "ddd M/d");
							int columnIndex = range.Column;
							int rowIndex = range.Row + 1;
							string programName = string.Empty;
							int firstRow = 0;
							for (int r = 0; r < 48; r++)
							{
								object value = workSheet.Range[GetColumnLetterByIndex(columnIndex) + (rowIndex + r)].Value;
								string currentProgramName = value != null ? value.ToString() : string.Empty;
								if (!currentProgramName.Equals(programName))
								{
									if (!string.IsNullOrEmpty(programName))
										workSheet.Range[GetColumnLetterByIndex(columnIndex) + firstRow + ":" + GetColumnLetterByIndex(columnIndex) + (rowIndex + r - 1)].Merge();
									firstRow = rowIndex + r;
									programName = currentProgramName;
								}
							}
						}

						for (int i = 0; i < 7; i++)
						{
							range = workSheet.Range["day" + (i + 1)];
							int columnIndex = range.Column;
							int r = 0;
							object value = null;
							do
							{
								value = null;
								int rowIndex = 0;
								Range rowRange = range.Offset[r];
								try
								{
									value = rowRange.Formula;
									rowIndex = rowRange.Row;
								}
								catch {}
								if (value != null)
								{
									string programName = string.Empty;
									int firstColumn = 0;
									for (int j = i; j < 7; j++)
									{
										object nextValue = null;
										try
										{
											nextValue = rowRange.Offset[ColumnOffset: j].Value;
										}
										catch {}
										string currentProgramName = nextValue != null ? nextValue.ToString() : string.Empty;
										if (!currentProgramName.Equals(programName))
										{
											if (!string.IsNullOrEmpty(programName))
												workSheet.Range[GetColumnLetterByIndex(firstColumn) + rowIndex + ":" + GetColumnLetterByIndex(columnIndex + j - 1) + rowIndex].Merge();
											firstColumn = columnIndex + j;
											programName = currentProgramName;
										}
									}
								}
								r++;
							} while (value != null && r <= 48);
						}

						Range dataRange = workSheet.Range["Data"];

						dataRange.Rows.AutoFit();

						//set correct page breaks
						int fisrtColumnNumber = workSheet.Range["day1"].Column;
						int lastColumnNumber = workSheet.Range["day7"].Column;
						HPageBreaks pageBreaks = workSheet.HPageBreaks;
						for (int i = 1; i <= pageBreaks.Count; i++)
						{
							int currentBreakRow = pageBreaks[i].Location.Row;
							int newBreakRow = currentBreakRow;
							Range beforeRange = pageBreaks[i].Location;
							for (int j = fisrtColumnNumber; j <= lastColumnNumber; j++)
							{
								Range cellRange = workSheet.Cells[currentBreakRow, j];
								if (cellRange.MergeCells)
								{
									if (beforeRange.Row > cellRange.MergeArea.Row)
									{
										beforeRange = cellRange.MergeArea;
										newBreakRow = beforeRange.Row;
									}
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
				catch (Exception ex)
				{
					AppManager.Instance.Log.Records.Add(new LogRecord(ex));
					AppManager.Instance.Log.Save();
				}
				Disconnect();
			}
		}

		public void ReportActivityList(string templatePath, ProgramActivity[] activities, string destinationFilePath, bool convertToPDF)
		{
			if (File.Exists(templatePath) && Connect())
			{
				try
				{
					Workbook sourceWorkBook = _excelObject.Workbooks.Open(templatePath);
					Worksheet workSheet = sourceWorkBook.Worksheets["Program Activities"];

					DateTime start = activities.FirstOrDefault().Time;
					DateTime end = activities.LastOrDefault().Time;
					string title = string.Format("Program Activity For {0}", activities.FirstOrDefault().Day.Station.Name);
					string dateRange = string.Format("Between {0} and {1} from {2} to {3}", new[] { start.ToString("MM/dd/yyyy"), end.ToString("MM/dd/yyyy"), start.ToString("hh:mmtt"), end.ToString("hh:mmtt") });
					workSheet.PageSetup.CenterHeader = "&12&B" + title + (char)13 + dateRange;

					DateTime sheduleGenrated = DateTime.Now;
					workSheet.PageSetup.CenterFooter = "&11Generated" + (char)13 + sheduleGenrated.ToString("MM/dd/yy h:mm tt");

					Range range = workSheet.Range["Date"];
					int firstRow = range.Row + 1;
					int lastRow = firstRow;
					int firstColumn = range.Column;
					range = workSheet.Range["Type"];
					int lastColumn = range.Column;

					var rows = new List<object[]>();
					foreach (ProgramActivity activity in activities)
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
						for (int i = 0; i < rows.Count; i++)
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
				catch (Exception ex)
				{
					AppManager.Instance.Log.Records.Add(new LogRecord(ex));
					AppManager.Instance.Log.Save();
				}
				Disconnect();
			}
		}

		public static string GetColumnLetterByIndex(int index)
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