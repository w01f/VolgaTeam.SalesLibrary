﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Office.Interop.Excel;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(ExcelPreviewContainer))]
	class ExcelPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var excelContainer = (ExcelPreviewContainer)previewContainer;

			var log = new StringBuilder();
			log.AppendLine(String.Format("Process started at {0:hh:mm:ss tt zz}", DateTime.Now));

			var txtDestination = Path.Combine(excelContainer.ContainerPath, PreviewFormats.Text);
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any());
			if (!Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			var updated = updateTxt;
			if (updated)
			{
				try
				{
					if (ExcelHelper.Instance.Connect())
					{
						MessageFilter.Register();
						var workbook = ExcelHelper.Instance.ExcelObject.Workbooks.Open(excelContainer.SourcePath, ReadOnly: true);
						string txtFileName = Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(excelContainer.SourcePath), "txt"));
						workbook.SaveAs(txtFileName, XlFileFormat.xlTextWindows);
						log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Text, DateTime.Now));
						workbook.Close();
						Utils.ReleaseComObject(workbook);
					}
				}
				catch { }
				finally
				{
					MessageFilter.Revoke();
					ExcelHelper.Instance.Disconnect();
				}
			}

			if (updated)
				previewContainer.MarkAsModified();

			log.AppendLine(String.Format("Process finished at {0:hh:mm:ss tt zz}", DateTime.Now));
			if (Directory.Exists(previewContainer.ContainerPath))
				File.WriteAllText(Path.Combine(previewContainer.ContainerPath, String.Format("log_{0:MMddyy_hhmmsstt}.txt", DateTime.Now)), log.ToString());
		}
	}
}
