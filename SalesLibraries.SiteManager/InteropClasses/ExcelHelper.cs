using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Office.Interop.Excel;
using SalesLibraries.SiteManager.ToolClasses;

namespace SalesLibraries.SiteManager.InteropClasses
{
	public class ExcelHelper
	{
		private static readonly ExcelHelper _instance = new ExcelHelper();

		private Application _excelObject;
		public Application ExcelObject => _excelObject;

		private ExcelHelper() { }

		public static ExcelHelper Instance => _instance;

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
						Workbook workbook = _excelObject.Workbooks.Open(sourceFilePath, ReadOnly: true);
						string txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(sourceFilePath), "txt"));
						workbook.SaveAs(txtFileName, XlFileFormat.xlTextWindows);
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

	public class MessageFilter : IOleMessageFilter
	{
		//
		// Class containing the IOleMessageFilter
		// thread error-handling functions.

		// Start the filter.

		//
		// IOleMessageFilter functions.
		// Handle incoming thread requests.

		#region IOleMessageFilter Members
		int IOleMessageFilter.HandleInComingCall(int dwCallType,
												 IntPtr hTaskCaller, int dwTickCount, IntPtr
																						  lpInterfaceInfo)
		{
			//Return the flag SERVERCALL_ISHANDLED.
			return 0;
		}

		// Thread call was rejected, so try again.
		int IOleMessageFilter.RetryRejectedCall(IntPtr
													hTaskCallee, int dwTickCount, int dwRejectType)
		{
			if (dwRejectType == 2)
			// flag = SERVERCALL_RETRYLATER.
			{
				// Retry the thread call immediately if return >=0 & 
				// <100.
				return 99;
			}
			// Too busy; cancel call.
			return -1;
		}

		int IOleMessageFilter.MessagePending(IntPtr hTaskCallee,
											 int dwTickCount, int dwPendingType)
		{
			//Return the flag PENDINGMSG_WAITDEFPROCESS.
			return 2;
		}
		#endregion

		public static void Register()
		{
			IOleMessageFilter newFilter = new MessageFilter();
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(newFilter, out oldFilter);
		}

		// Done with the filter, close it.
		public static void Revoke()
		{
			IOleMessageFilter oldFilter = null;
			CoRegisterMessageFilter(null, out oldFilter);
		}

		// Implement the IOleMessageFilter interface.
		[DllImport("Ole32.dll")]
		private static extern int
			CoRegisterMessageFilter(IOleMessageFilter newFilter, out
				                                                     IOleMessageFilter oldFilter);
	}

	[ComImport, Guid("00000016-0000-0000-C000-000000000046"),
	 InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
	internal interface IOleMessageFilter
	{
		[PreserveSig]
		int HandleInComingCall(
			int dwCallType,
			IntPtr hTaskCaller,
			int dwTickCount,
			IntPtr lpInterfaceInfo);

		[PreserveSig]
		int RetryRejectedCall(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwRejectType);

		[PreserveSig]
		int MessagePending(
			IntPtr hTaskCallee,
			int dwTickCount,
			int dwPendingType);
	}
}
