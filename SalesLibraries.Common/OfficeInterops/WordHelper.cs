using System;
using System.Diagnostics;
using Microsoft.Office.Interop.Word;

namespace SalesLibraries.Common.OfficeInterops
{
	public class WordHelper
	{
		private static readonly WordHelper _instance = new WordHelper();

		private bool _isFirstLaunch = false;
		private bool _isOpened;

		public Application WordObject { get; private set; }

		public static WordHelper Instance
		{
			get
			{
				return _instance;
			}
		}

		public bool IsOpened
		{
			get
			{
				Process[] proc = Process.GetProcessesByName("WINWORD");
				if (!(proc.GetLength(0) > 0))
				{
					WordObject = null;
					_isOpened = false;
				}
				return _isOpened;
			}
		}

		private WordHelper(){}

		public bool Connect()
		{
			_isOpened = false;
			try
			{
				WordObject =
					System.Runtime.InteropServices.Marshal.GetActiveObject("Word.Application") as Application;
				_isFirstLaunch = false;
			}
			catch
			{
				WordObject = null;
			}
			if (WordObject == null)
			{
				try
				{

					WordObject = new Application();
					_isFirstLaunch = true;
				}
				catch
				{
					return false;
				}
			}
			if (WordObject != null)
			{
				WordObject.Visible = false;
				WordObject.DisplayAlerts = WdAlertLevel.wdAlertsNone;
				_isOpened = true;
				return true;
			}
			else
				return false;
		}

		public void Disconnect()
		{
			if (_isFirstLaunch)
			{
				var proc = Process.GetProcessesByName("WINWORD");
				if (proc.GetLength(0) > 0)
					proc[0].Kill();
				_isOpened = false;
			}
			WordObject = null;
			GC.Collect();
		}

		public bool ConvertToPDF(string originalFileName, string pdfFileName)
		{
			var result = false;
			try
			{
				MessageFilter.Register();
				var document = WordObject.Documents.Open(originalFileName);
				document.ExportAsFixedFormat(pdfFileName, WdExportFormat.wdExportFormatPDF);
				document.Close(false);
				result = true;
			}
			catch
			{
			}
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
				var document = WordObject.Documents.Open(oldFileName);
				document.SaveAs(newFileName, WdSaveFormat.wdFormatHTML);
				document.Close(false);
			}
			catch
			{
			}
			finally
			{
				MessageFilter.Revoke();
			}
		}
	}
}
