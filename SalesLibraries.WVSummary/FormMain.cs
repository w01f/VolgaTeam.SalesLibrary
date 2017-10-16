using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Microsoft.Office.Interop.Excel;
using SalesLibraries.Business.Contexts.Wallbin.Local;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.LinkSettings;
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
			var previewOtherNotAvailableLinksInfo = new List<string[]>();
			var previewInternalLinksInfo = new List<string[]>();
			var previewFolderlLinksInfo = new List<string[]>();
			var sharedPreviewContainerInfo = new List<string[]>();

			foreach (var libraryContext in wallbinManager.Libraries)
			{
				foreach (
					var libraryLink in
					libraryContext.Library.Pages.SelectMany(page => page.AllGroupLinks).OfType<LibraryObjectLink>().ToList())
				{
					string[] linkInfo = null;
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
							previewOtherNotAvailableLinksInfo.Add(linkInfo);
						}
					}
					else
					{
						if (libraryLink is InternalWallbinLink)
						{
							var internalLink = (InternalWallbinLink)libraryLink;
							previewInternalLinksInfo.Add(new[] { internalLink.Name, internalLink.WebFormat, internalLink.TargetLibrary, String.Empty, String.Empty, String.Empty, String.Empty });
						}
						else if (libraryLink is InternalLibraryPageLink)
						{
							var internalLink = (InternalLibraryPageLink)libraryLink;
							previewInternalLinksInfo.Add(new[] { internalLink.Name, internalLink.WebFormat, internalLink.TargetLibrary, internalLink.TargetPage, String.Empty, String.Empty, String.Empty });
						}
						else if (libraryLink is InternalLibraryFolderLink)
						{
							var internalLink = (InternalLibraryFolderLink)libraryLink;
							previewInternalLinksInfo.Add(new[] { internalLink.Name, internalLink.WebFormat, internalLink.TargetLibrary, internalLink.TargetPage, internalLink.TargetFolder, String.Empty, String.Empty });
						}
						else if (libraryLink is InternalLibraryObjectLink)
						{
							var internalLink = (InternalLibraryObjectLink)libraryLink;
							previewInternalLinksInfo.Add(new[] { internalLink.Name, internalLink.WebFormat, internalLink.TargetLibrary, internalLink.TargetPage, internalLink.TargetFolder, internalLink.TargetLink, String.Empty });
						}
						else if (libraryLink is InternalShortcutLink)
						{
							var internalLink = (InternalShortcutLink)libraryLink;
							previewInternalLinksInfo.Add(new[] { internalLink.Name, internalLink.WebFormat, String.Empty, String.Empty, String.Empty, String.Empty, ((InternalShortcutLinkSettings)internalLink.Settings).ShortcutId });
						}
						else if (libraryLink is LibraryFolderLink)
						{
							var folderLink = (LibraryFolderLink)libraryLink;
							previewFolderlLinksInfo.Add(new[] { folderLink.Name, folderLink.WebFormat, folderLink.FullPath });
						}
						else if (libraryLink is LibraryFileLink)
						{
							var fileLink = (LibraryFileLink)libraryLink;
							linkInfo = new[] { fileLink.Name, fileLink.WebFormat, fileLink.FullPath };
						}
						else
							linkInfo = new[] { libraryLink.Name, libraryLink.WebFormat, libraryLink.RelativePath };
						if (linkInfo != null)
							previewOtherNotAvailableLinksInfo.Add(linkInfo);
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
					for (var i = 0; i < previewAvailableLinksInfo.Count; i++)
					{
						valueArray[i, 0] = previewAvailableLinksInfo[i][0];
						valueArray[i, 1] = previewAvailableLinksInfo[i][1];
						valueArray[i, 2] = previewAvailableLinksInfo[i][2];
						valueArray[i, 3] = previewAvailableLinksInfo[i][3];
					}
					dataRange.Value = valueArray;
					dataRange.EntireColumn.AutoFit();

					var nextPageIndex = 2;
					if (previewFolderlLinksInfo.Any())
					{
						try
						{
							sheet = workbook.Worksheets[nextPageIndex];
						}
						catch
						{
							sheet = workbook.Worksheets.Add();
							sheet.Move(After: workbook.Worksheets[nextPageIndex]);
						}
						nextPageIndex++;

						sheet.Name = String.Format("Folder Links - {0}", previewFolderlLinksInfo.Count);
						sheet.Range["A1"].Value = "Link Name";
						sheet.Range["B1"].Value = "Link Type";
						sheet.Range["C1"].Value = "Folder Path";


						dataRange = sheet.Range["A2", "C" + (previewFolderlLinksInfo.Count + 1)];
						valueArray = new string[previewFolderlLinksInfo.Count, 3];
						for (var i = 0; i < previewFolderlLinksInfo.Count; i++)
						{
							valueArray[i, 0] = previewFolderlLinksInfo[i][0];
							valueArray[i, 1] = previewFolderlLinksInfo[i][1];
							valueArray[i, 2] = previewFolderlLinksInfo[i][2];
						}
						dataRange.Value = valueArray;
						dataRange.EntireColumn.AutoFit();
					}

					if (previewInternalLinksInfo.Any())
					{
						try
						{
							sheet = workbook.Worksheets[nextPageIndex];
						}
						catch
						{
							sheet = workbook.Worksheets.Add();
							sheet.Move(After: workbook.Worksheets[nextPageIndex]);
						}
						nextPageIndex++;

						sheet.Name = String.Format("Internal Links - {0}", previewInternalLinksInfo.Count);
						sheet.Range["A1"].Value = "Link Name";
						sheet.Range["B1"].Value = "Link Type";
						sheet.Range["C1"].Value = "Library";
						sheet.Range["D1"].Value = "Page";
						sheet.Range["E1"].Value = "Window";
						sheet.Range["F1"].Value = "Link";
						sheet.Range["G1"].Value = "ID";


						dataRange = sheet.Range["A2", "G" + (previewInternalLinksInfo.Count + 1)];
						valueArray = new string[previewInternalLinksInfo.Count, 7];
						for (var i = 0; i < previewInternalLinksInfo.Count; i++)
						{
							valueArray[i, 0] = previewInternalLinksInfo[i][0];
							valueArray[i, 1] = previewInternalLinksInfo[i][1];
							valueArray[i, 2] = previewInternalLinksInfo[i][2];
							valueArray[i, 3] = previewInternalLinksInfo[i][3];
							valueArray[i, 4] = previewInternalLinksInfo[i][4];
							valueArray[i, 5] = previewInternalLinksInfo[i][5];
							valueArray[i, 6] = previewInternalLinksInfo[i][6];
						}
						dataRange.Value = valueArray;
						dataRange.EntireColumn.AutoFit();
					}

					if (previewOtherNotAvailableLinksInfo.Any())
					{
						try
						{
							sheet = workbook.Worksheets[nextPageIndex];
						}
						catch
						{
							sheet = workbook.Worksheets.Add();
							sheet.Move(After: workbook.Worksheets[nextPageIndex]);
						}
						nextPageIndex++;

						sheet.Name = String.Format("Other Links with no WV - {0}", previewOtherNotAvailableLinksInfo.Count);
						sheet.Range["A1"].Value = "Link Name";
						sheet.Range["B1"].Value = "Link Type";
						sheet.Range["C1"].Value = "File Path";

						dataRange = sheet.Range["A2", "C" + (previewOtherNotAvailableLinksInfo.Count + 1)];
						valueArray = new string[previewOtherNotAvailableLinksInfo.Count, 3];
						for (var i = 0; i < previewOtherNotAvailableLinksInfo.Count; i++)
						{
							valueArray[i, 0] = previewOtherNotAvailableLinksInfo[i][0];
							valueArray[i, 1] = previewOtherNotAvailableLinksInfo[i][1];
							valueArray[i, 2] = previewOtherNotAvailableLinksInfo[i][2];
						}
						dataRange.Value = valueArray;
						dataRange.EntireColumn.AutoFit();
					}

					if (sharedPreviewContainerInfo.Any())
					{
						try
						{
							sheet = workbook.Worksheets[nextPageIndex];
						}
						catch
						{
							sheet = workbook.Worksheets.Add();
							sheet.Move(After: workbook.Worksheets[nextPageIndex]);
						}
						sheet.Name = "Shared WV";
						sheet.Range["A1"].Value = "WV";
						sheet.Range["B1"].Value = "Links Number";

						dataRange = sheet.Range["A2", "B" + (sharedPreviewContainerInfo.Count + 1)];
						valueArray = new string[sharedPreviewContainerInfo.Count, 2];
						for (var i = 0; i < sharedPreviewContainerInfo.Count; i++)
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
					//throw ex;
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
