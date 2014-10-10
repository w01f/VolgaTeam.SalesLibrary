using System;
using System.IO;
using Microsoft.Office.Interop.Excel;
using SalesDepot.CoreObjects.ToolClasses;

namespace SalesDepot.CoreObjects.InteropClasses
{
	public class ExcelHelper
	{
		private static readonly ExcelHelper _instance = new ExcelHelper();

		private Application _excelObject;
		public Application ExcelObject
		{
			get { return _excelObject; }
		}

		private ExcelHelper() { }

		public static ExcelHelper Instance
		{
			get { return _instance; }
		}

		public bool Connect()
		{
			bool result = false;
			MessageFilter.Register();
			try
			{
				_excelObject = new Application();
				_excelObject.DisplayAlerts = false;
				result = true;
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
			return result;
		}

		public void Disconnect()
		{
			Utils.ReleaseComObject(_excelObject);
			GC.Collect();
			GC.WaitForPendingFinalizers();
		}

		public void ExportGroup(string filePath, string groupName, object[,] dataSource)
		{
			try
			{
				if (!Connect()) return;
				MessageFilter.Register();
				var workbook = _excelObject.Workbooks.Add();
				Worksheet sheet;
				try
				{
					sheet = workbook.Worksheets[1];
				}
				catch
				{
					sheet = workbook.Worksheets.Add();
				}

				sheet.Name = "Authorized Users";
				sheet.Range["A1"].Value = "First Name";
				sheet.Range["B1"].Value = "Last Name";
				sheet.Range["C1"].Value = "Email Address";
				sheet.Range["D1"].Value = "UserName";

				var headerRow = sheet.Range["1:1"];
				headerRow.Font.Bold = true;
				headerRow.HorizontalAlignment = -4108;

				var dataRange = sheet.Range["A2", "D" + (dataSource.GetLength(0) + 1).ToString()];
				dataRange.Value = dataSource;
				dataRange.EntireColumn.AutoFit();

				workbook.SaveAs(filePath);
				workbook.Close();
				Utils.ReleaseComObject(workbook);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				Disconnect();
			}
		}

		public void ExportTickerLinks(string filePath, object[,] dataSource)
		{
			try
			{
				if (!Connect()) return;
				MessageFilter.Register();
				var workbook = _excelObject.Workbooks.Add();
				Worksheet sheet;
				try
				{
					sheet = workbook.Worksheets[1];
				}
				catch
				{
					sheet = workbook.Worksheets.Add();
				}

				sheet.Name = "Ticker Links";
				sheet.Range["A1"].Value = "Type";
				sheet.Range["B1"].Value = "Text";
				sheet.Range["C1"].Value = "URL";
				sheet.Range["D1"].Value = "File";
				sheet.Range["E1"].Value = "Video";
				sheet.Range["F1"].Value = "Library";
				sheet.Range["G1"].Value = "Page";
				sheet.Range["H1"].Value = "Link";

				var headerRow = sheet.Range["1:1"];
				headerRow.Font.Bold = true;
				headerRow.HorizontalAlignment = -4108;

				var dataRange = sheet.Range["A2", "H" + (dataSource.GetLength(0) + 1).ToString()];
				dataRange.Value = dataSource;
				dataRange.EntireColumn.AutoFit();

				workbook.SaveAs(filePath);
				workbook.Close();
				Utils.ReleaseComObject(workbook);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				Disconnect();
			}
		}

		public void ExportQuizStatistic(string filePath, string groupName, object[,] dataSource)
		{
			try
			{
				if (!Connect()) return;
				MessageFilter.Register();
				var workbook = _excelObject.Workbooks.Add();
				Worksheet sheet;
				try
				{
					sheet = workbook.Worksheets[1];
				}
				catch
				{
					sheet = workbook.Worksheets.Add();
				}

				sheet.Name = "Authorized Users";
				sheet.Range["A1"].Value = "First Name";
				sheet.Range["B1"].Value = "Last Name";
				sheet.Range["C1"].Value = "Email Address";
				sheet.Range["D1"].Value = "UserName";

				var headerRow = sheet.Range["1:1"];
				headerRow.Font.Bold = true;
				headerRow.HorizontalAlignment = -4108;

				var dataRange = sheet.Range["A2", "D" + (dataSource.GetLength(0) + 1).ToString()];
				dataRange.Value = dataSource;
				dataRange.EntireColumn.AutoFit();

				workbook.SaveAs(filePath);
				workbook.Close();
				Utils.ReleaseComObject(workbook);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				Disconnect();
			}
		}
	}
}