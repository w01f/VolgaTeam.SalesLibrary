using System;
using System.Diagnostics;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Word;

namespace OutlookSalesDepotAddIn.BusinessClasses
{
	internal class WordHelper
	{
		private static readonly WordHelper _instance = new WordHelper();

		private bool _isFirstLaunch;
		private bool _isOpened;
		private Application _wordObject;

		private WordHelper() { }

		public static WordHelper Instance
		{
			get { return _instance; }
		}

		public bool IsOpened
		{
			get
			{
				Process[] proc = Process.GetProcessesByName("WINWORD");
				if (!(proc.GetLength(0) > 0))
				{
					_wordObject = null;
					_isOpened = false;
				}
				return _isOpened;
			}
		}

		public bool Open()
		{
			_isOpened = false;
			try
			{
				_wordObject =
					Marshal.GetActiveObject("Word.Application") as Application;
				_isFirstLaunch = false;
			}
			catch
			{
				_wordObject = null;
			}
			if (_wordObject == null)
			{
				try
				{
					_wordObject = new Application();
					_isFirstLaunch = true;
				}
				catch
				{
					return false;
				}
			}
			if (_wordObject != null)
			{
				_wordObject.Visible = false;
				_wordObject.DisplayAlerts = WdAlertLevel.wdAlertsNone;
				_isOpened = true;
				return true;
			}
			return false;
		}

		public void Close()
		{
			if (_isFirstLaunch)
			{
				Process[] proc = Process.GetProcessesByName("WINWORD");
				if (proc.GetLength(0) > 0)
					proc[0].Kill();
				_isOpened = false;
			}
			_wordObject = null;
			GC.Collect();
		}

		public bool ConvertToPDF(string originalFileName, string pdfFileName)
		{
			bool result = false;
			try
			{
				MessageFilter.Register();
				Document document = _wordObject.Documents.Open(originalFileName);
				document.ExportAsFixedFormat(pdfFileName, WdExportFormat.wdExportFormatPDF);
				document.Close(false);
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
				Document document = _wordObject.Documents.Open(oldFileName);
				document.SaveAs(newFileName, WdSaveFormat.wdFormatHTML);
				document.Close(false);
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}
}