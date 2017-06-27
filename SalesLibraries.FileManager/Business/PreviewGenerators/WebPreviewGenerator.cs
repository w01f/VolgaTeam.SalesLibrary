﻿using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(WebLinkPreviewContainer))]
	class WebPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var log = new StringBuilder();
			log.AppendLine(String.Format("Process started at {0:hh:mm:ss tt zz}", DateTime.Now));

			var thumbsDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());

			var thumbsDatatableDestination = Path.Combine(previewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());

			if (!(updateThumbs || updateThumbsDatatable)) return;

			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
			{
				var thumbnailGenerator = new WebLinkThumbnailGenerator();
				thumbnailGenerator.GenerateThumbnail(previewContainer.SourcePath, thumbsDestination);
				JpegGenerator.GenerateDatatableJpegs(thumbsDestination, thumbsDatatableDestination);
				log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Thumbnails, DateTime.Now));
				log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.ThumbnailsForDatatable, DateTime.Now));
			}));

			previewContainer.MarkAsModified();

			log.AppendLine(String.Format("Process finished at {0:hh:mm:ss tt zz}", DateTime.Now));
			if (Directory.Exists(previewContainer.ContainerPath))
				File.WriteAllText(Path.Combine(previewContainer.ContainerPath, String.Format("log_{0:MMddyy_hhmmsstt}.txt", DateTime.Now)), log.ToString());
		}
	}
}
