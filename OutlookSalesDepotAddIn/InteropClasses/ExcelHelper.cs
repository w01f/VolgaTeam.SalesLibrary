using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.Office.Interop.Excel;
using SalesDepot.CoreObjects.InteropClasses;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	internal class ExcelHelper
	{
		private static readonly ExcelHelper _instance = new ExcelHelper();

		private Application _excelObject;
		private bool _isOpened;

		private ExcelHelper() { }

		public static ExcelHelper Instance
		{
			get { return _instance; }
		}

		public bool IsOpened
		{
			get
			{
				var proc = Process.GetProcessesByName("EXCEL");
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
			Utils.ReleaseComObject(_excelObject);
			GC.Collect();
		}


		public void Print(FileInfo file)
		{
			var workBook = _excelObject.Workbooks.Open(file.FullName);
			_excelObject.Visible = true;
			var processList = Process.GetProcesses();
			foreach (var process in processList.Where(x => x.ProcessName.ToLower().Contains("excel")))
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
				var workbook = _excelObject.Workbooks.Open(originalFileName);
				workbook.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFileName, XlFixedFormatQuality.xlQualityStandard, true, false, Type.Missing, Type.Missing, false, Type.Missing);
				result = true;
			}
			catch { }
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
				var workbook = _excelObject.Workbooks.Open(oldFileName, ReadOnly: true);
				workbook.SaveAs(newFileName, XlFileFormat.xlHtml);
				workbook.Close(false);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
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