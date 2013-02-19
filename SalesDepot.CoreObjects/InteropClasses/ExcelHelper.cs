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

		public void ExportBookAllFormats(string sourceFilePath, string destinationFolderPath, out bool update)
		{
			string txtDestination = Path.Combine(destinationFolderPath, "txt");
			bool updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination, "*.txt").Length > 0);
			if (!Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			update = false;
			if (updateTxt)
			{
				update = true;
				try
				{
					if (Connect())
					{
						MessageFilter.Register();
						Workbook workbook = _excelObject.Workbooks.Open(Filename: sourceFilePath, ReadOnly: true);
						string txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "txt"));
						workbook.SaveAs(Filename: txtFileName, FileFormat: XlFileFormat.xlTextWindows);
						workbook.Close();
						Utils.ReleaseComObject(workbook);
					}
				}
				catch { }
				finally
				{
					MessageFilter.Revoke();
					Disconnect();
				}
			}
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
	}
}