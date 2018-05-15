using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Extensions;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(ImagePreviewContainer))]
	class ImagePreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var imagePreviewContainer = (ImagePreviewContainer)previewContainer;

			var logger = new FilePreviewGenerationLogger(imagePreviewContainer);
			logger.StartLogging();

			var thumbsDestination = Path.Combine(imagePreviewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());
			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			var thumbsDatatableDestination = Path.Combine(imagePreviewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());
			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			var oneDriveUrl = imagePreviewContainer.OneDriveUrl;
			var oneDriveUrlDestination = Path.Combine(imagePreviewContainer.ContainerPath, PreviewFormats.OneDrive);
			var updateOneDrive = !Directory.Exists(oneDriveUrlDestination) && !String.IsNullOrEmpty(oneDriveUrl);
			if (!Directory.Exists(oneDriveUrlDestination) && updateOneDrive)
				Directory.CreateDirectory(oneDriveUrlDestination);

			var needToUpdate = updateThumbs || updateThumbsDatatable;
			if (needToUpdate)
			{
				try
				{
					{
						var pngFileName = Path.Combine(thumbsDestination, "thumbnail.png");
						using (var image = Image.FromFile(imagePreviewContainer.SourcePath))
						{
							image
								.Resize(new Size(800, 600))
								.Save(pngFileName, ImageFormat.Png);
						}
						JpegHelper.ConvertFiles(thumbsDestination, thumbsDatatableDestination);
						logger.LogStage(PreviewFormats.Thumbnails);
						logger.LogStage(PreviewFormats.ThumbnailsForDatatable);
					}
				}
				catch (Exception ex)
				{
					//throw ex;
				}
			}

			if (updateOneDrive)
			{
				OneDrivePreviewHelper.GenerateShortcutFiles(
					oneDriveUrl,
					Path.GetFileName(previewContainer.SourcePath),
					oneDriveUrlDestination);
				logger.LogStage(PreviewFormats.OneDrive);
			}

			if (needToUpdate)
				previewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
