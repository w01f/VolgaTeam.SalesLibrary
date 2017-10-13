using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SalesLibraries.Business.Contexts.Wallbin.Local;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Persistent.Links;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using Application = System.Windows.Forms.Application;

namespace SalesLibraries.WVSummary
{
	public partial class FormMain : Form
	{
		public FormMain()
		{
			InitializeComponent();
		}

		private void OnProcessClick(object sender, EventArgs e)
		{
			labelProgress.Visible = true;
			buttonProcess.Enabled = false;
			Application.DoEvents();

			var rootPath = textBoxPath.Text;
			if (String.IsNullOrEmpty(rootPath) || !Directory.Exists(rootPath))
				return;

			var wallbinManager = new LocalWallbinManager();
			wallbinManager.LoadLibrary(rootPath);

			var previewAvailableLinksInfo = new List<string[]>();
			var previewNotAvailableLinksInfo = new List<string[]>();
			var sharedPreviewContainerInfo = new List<string[]>();

			foreach (var libraryContext in wallbinManager.Libraries)
			{
				foreach (
					var libraryLink in
					libraryContext.Library.Pages.SelectMany(page => page.AllGroupLinks).OfType<LibraryObjectLink>().ToList())
				{
					string[] linkInfo;
					if (libraryLink is IPreviewableLink)
					{
						var previewContainer = ((IPreviewableLink)libraryLink).GetPreviewContainer();
						if (previewContainer != null && Directory.Exists(previewContainer.ContainerPath))
						{
							if (libraryLink is LibraryFileLink)
							{
								var fileLink = (LibraryFileLink)libraryLink;
								linkInfo = new[] { fileLink.Name, fileLink.WebFormat, fileLink.FullPath, previewContainer.ExtId.ToString() };
							}
							else
								linkInfo = new[] { libraryLink.Name, libraryLink.WebFormat, libraryLink.RelativePath, previewContainer.ExtId.ToString() };
							previewAvailableLinksInfo.Add(linkInfo);
						}
						else
						{
							if (libraryLink is LibraryFileLink)
							{
								var fileLink = (LibraryFileLink)libraryLink;
								linkInfo = new[] { fileLink.Name, fileLink.WebFormat, fileLink.FullPath };
							}
							else
								linkInfo = new[] { libraryLink.Name, libraryLink.WebFormat, libraryLink.RelativePath };
							previewNotAvailableLinksInfo.Add(linkInfo);
						}
					}
					else
					{
						if (libraryLink is LibraryFileLink)
						{
							var fileLink = (LibraryFileLink)libraryLink;
							linkInfo = new[] { fileLink.Name, fileLink.WebFormat, fileLink.FullPath };
						}
						else
							linkInfo = new[] { libraryLink.Name, libraryLink.WebFormat, libraryLink.RelativePath };
						previewNotAvailableLinksInfo.Add(linkInfo);
					}
				}
				foreach (var previewContainer in libraryContext.Library.PreviewContainers)
				{
					var associatedLinks = libraryContext.Library.GetPreviewableLinksBySourcePath(previewContainer.SourcePath).ToList();
					if (associatedLinks.Count > 1)
						sharedPreviewContainerInfo.Add(new[] { previewContainer.ExtId.ToString(), associatedLinks.Count.ToString() });
				}
			}

			var filePath = Path.ChangeExtension(Path.GetTempFileName(), "xlsx");
			if (ExcelHelper.Instance.Connect())
			{
				try
				{
					MessageFilter.Register();
					var workbook = ExcelHelper.Instance.ExcelObject.Workbooks.Add();

					Worksheet sheet;
					try
					{
						sheet = workbook.Worksheets[1];
					}
					catch
					{
						sheet = workbook.Worksheets.Add();
					}
					sheet.Name = String.Format("Links with WV - {0}", previewAvailableLinksInfo.Count);
					sheet.Range["A1"].Value = "Link Name";
					sheet.Range["B1"].Value = "Link Type";
					sheet.Range["C1"].Value = "File Path";
					sheet.Range["D1"].Value = "WV Folder";

					var dataRange = sheet.Range["A2", "D" + (previewAvailableLinksInfo.Count + 1)];
					var valueArray = new string[previewAvailableLinksInfo.Count, 4];
					for (int i = 0; i < previewAvailableLinksInfo.Count; i++)
					{
						valueArray[i, 0] = previewAvailableLinksInfo[i][0];
						valueArray[i, 1] = previewAvailableLinksInfo[i][1];
						valueArray[i, 2] = previewAvailableLinksInfo[i][2];
						valueArray[i, 3] = previewAvailableLinksInfo[i][3];
					}
					dataRange.Value = valueArray;
					dataRange.EntireColumn.AutoFit();

					try
					{
						sheet = workbook.Worksheets[2];
					}
					catch
					{
						sheet = workbook.Worksheets.Add();
						sheet.Move(After: workbook.Worksheets[2]);
					}
					sheet.Name = String.Format("Links with no WV - {0}", previewNotAvailableLinksInfo.Count);
					sheet.Range["A1"].Value = "Link Name";
					sheet.Range["B1"].Value = "Link Type";
					sheet.Range["C1"].Value = "File Path";

					dataRange = sheet.Range["A2", "C" + (previewNotAvailableLinksInfo.Count + 1)];
					valueArray = new string[previewNotAvailableLinksInfo.Count, 3];
					for (int i = 0; i < previewNotAvailableLinksInfo.Count; i++)
					{
						valueArray[i, 0] = previewNotAvailableLinksInfo[i][0];
						valueArray[i, 1] = previewNotAvailableLinksInfo[i][1];
						valueArray[i, 2] = previewNotAvailableLinksInfo[i][2];
					}
					dataRange.Value = valueArray;
					dataRange.EntireColumn.AutoFit();

					if (sharedPreviewContainerInfo.Any())
					{
						try
						{
							sheet = workbook.Worksheets[3];
						}
						catch
						{
							sheet = workbook.Worksheets.Add();
							sheet.Move(After: workbook.Worksheets[3]);
						}
						sheet.Name = "Shared WV";
						sheet.Range["A1"].Value = "WV";
						sheet.Range["B1"].Value = "Links Number";

						dataRange = sheet.Range["A2", "B" + (sharedPreviewContainerInfo.Count + 1)];
						valueArray = new string[sharedPreviewContainerInfo.Count, 2];
						for (int i = 0; i < sharedPreviewContainerInfo.Count; i++)
						{
							valueArray[i, 0] = sharedPreviewContainerInfo[i][0];
							valueArray[i, 1] = sharedPreviewContainerInfo[i][1];
						}
						dataRange.Value = valueArray;
						dataRange.EntireColumn.AutoFit();
					}

					workbook.Worksheets[1].Select();
					workbook.SaveAs(filePath);
					workbook.Close();
					Utils.ReleaseComObject(workbook);
				}
				catch (Exception ex)
				{
					throw ex;
				}
				finally
				{
					MessageFilter.Revoke();
					ExcelHelper.Instance.Disconnect();
				}

				if (File.Exists(filePath))
					Process.Start(filePath);
			}

			labelProgress.Visible = false;
			buttonProcess.Enabled = true;
			Application.DoEvents();
		}
	}
}
