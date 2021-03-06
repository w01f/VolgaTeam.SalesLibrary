﻿using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.NonPersistent.PreviewContainerSettings;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(WebLinkPreviewContainer))]
	[IntendForClass(typeof(QuickSitePreviewContainer))]
	class WebPreviewGenerator : IPreviewContentGenerator
	{
		public void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var webPreviewContainer = (WebLinkPreviewContainer)previewContainer;

			var logger = new UrlPreviewGenerationLogger(webPreviewContainer);
			logger.StartLogging();

			var thumbsDestination = Path.Combine(webPreviewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());

			var thumbsDatatableDestination = Path.Combine(webPreviewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());

			if (!(updateThumbs || updateThumbsDatatable)) return;

			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			var customThumbnailImage = ((HyperlinkPreviewContainerSettings)previewContainer.Settings).CustomThumbnail;
			if (customThumbnailImage != null)
				customThumbnailImage
					.Resize(new Size(RegularBrowserThumbnailGenerator.ThumbWidth, RegularBrowserThumbnailGenerator.ThumbHeight))
					.Save(Path.Combine(thumbsDestination, "thumbnail.png"), ImageFormat.Png);
			else
			{
				MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					var thumbnailGenerator = new RegularBrowserThumbnailGenerator();
					thumbnailGenerator.GenerateThumbnail(webPreviewContainer.SourcePath, thumbsDestination,
						alternativeUrl: MainController.Instance.Settings.WebServiceSite);
				}));
			}
			JpegHelper.ConvertFiles(thumbsDestination, thumbsDatatableDestination);

			logger.LogStage(PreviewFormats.Thumbnails);
			logger.LogStage(PreviewFormats.ThumbnailsForDatatable);

			webPreviewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
