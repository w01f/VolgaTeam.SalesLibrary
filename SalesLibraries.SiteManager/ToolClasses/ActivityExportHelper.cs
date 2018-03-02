using System.Collections.Generic;
using System.IO;
using Microsoft.Office.Interop.Excel;
using SalesLibraries.SiteManager.InteropClasses;
using Application = System.Windows.Forms.Application;

namespace SalesLibraries.SiteManager.ToolClasses
{
	public class ActivityExportHelper
	{
		public static void ExportCommonData(string filePath, Dictionary<string, string> parts)
		{
			try
			{
				if (!ExcelHelper.Instance.Connect()) return;
				MessageFilter.Register();
				var workbook = ExcelHelper.Instance.ExcelObject.Workbooks.Add();
				var existedWorksheetsCount = workbook.Worksheets.Count;
				var currentWorsheet = existedWorksheetsCount;
				foreach (var part in parts)
				{
					if (!File.Exists(part.Value)) continue;
					var partBook = ExcelHelper.Instance.ExcelObject.Workbooks.Open(part.Value);
					Worksheet partWorksheet = partBook.Worksheets[1];
					partWorksheet.Name = part.Key.Substring(0, part.Key.Length > 30 ? 30 : part.Key.Length);
					partWorksheet.Copy(After: workbook.Worksheets[existedWorksheetsCount]);
					currentWorsheet++;
					partBook.Close();
					File.Delete(part.Value);
					Application.DoEvents();
				}
				if (currentWorsheet > existedWorksheetsCount)
					for (var i = existedWorksheetsCount; i >= 1; i--)
						((Worksheet)workbook.Worksheets[i]).Delete();
				workbook.SaveAs(filePath);
				workbook.Close();
				Utils.ReleaseComObject(workbook);
			}
			//catch { }
			finally
			{
				MessageFilter.Revoke();
				ExcelHelper.Instance.Disconnect();
			}
		}
	}
}
