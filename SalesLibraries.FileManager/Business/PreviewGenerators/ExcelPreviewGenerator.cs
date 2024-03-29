﻿using System;
using System.IO;
using System.Linq;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(ExcelPreviewContainer))]
	class ExcelPreviewGenerator : FilePreviewGenerator
	{
		public override void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var excelContainer = (ExcelPreviewContainer)previewContainer;

			var logger = new FilePreviewGenerationLogger(excelContainer);
			logger.StartLogging();

			var txtDestination = Path.Combine(excelContainer.ContainerPath, PreviewFormats.Text);
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any()) && excelContainer.GenerateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			var thumbsDestination = Path.Combine(excelContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());
			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			var thumbsDatatableDestination = Path.Combine(excelContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());
			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			var needToUpdate = updateTxt || updateThumbs || updateThumbsDatatable;
			if (needToUpdate)
			{
				try
				{
					if (ExcelHelper.Instance.Connect())
					{
						MessageFilter.Register();
						var workbook = ExcelHelper.Instance.ExcelObject.Workbooks.Open(excelContainer.SourcePath, ReadOnly: true);

						if (updateThumbs || updateThumbsDatatable)
						{
							var pdfFileName = Path.ChangeExtension(Path.GetTempFileName(), ".pdf");
							workbook.ActiveSheet.ExportAsFixedFormat(XlFixedFormatType.xlTypePDF, pdfFileName);
							PdfHelper.ExportPdf(pdfFileName, thumbsDestination, null, true);
							JpegHelper.ConvertFiles(thumbsDestination, thumbsDatatableDestination);
							logger.LogStage(PreviewFormats.Thumbnails);
							logger.LogStage(PreviewFormats.ThumbnailsForDatatable);
							if (File.Exists(pdfFileName))
								try
								{
									File.Delete(pdfFileName);
								}
								catch
								{
								}
						}

						if (updateTxt)
						{
							var txtFileName = Path.Combine(txtDestination,
								Path.ChangeExtension(Path.GetFileName(excelContainer.SourcePath), "txt"));
							workbook.SaveAs(txtFileName, XlFileFormat.xlTextWindows);
							logger.LogStage(PreviewFormats.Text);
						}

						workbook.Close();
						Utils.ReleaseComObject(workbook);
					}
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
			}

			if (needToUpdate)
				previewContainer.MarkAsModified();

			if (MainController.Instance.Settings.OneDriveSettings.Enabled)
			{
				GenerateOneDriveContent(excelContainer, cancellationToken);
				logger.LogStage(PreviewFormats.OneDrive);
			}

			logger.FinishLogging();
		}
	}
}
