using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(PdfPreviewContainer))]
	class PdfPreviewGenerator : FilePreviewGenerator
	{
		public override void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var updated = false;
			var pdfContainer = (PdfPreviewContainer)previewContainer;

			var logger = new FilePreviewGenerationLogger(pdfContainer);
			logger.StartLogging();

			var pngDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.Png);
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination).Any());
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);

			var pngPhoneDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.PngForMobile);
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination).Any());
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);

			var thumbsDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());
			if (updateThumbs && !Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			var thumbsPhoneDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.ThumbnailsForMobile);
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination).Any());
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);

			var thumbsDatatableDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());
			if (updateThumbsDatatable && !Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			if (updatePng || updateThumbs || updateThumbsDatatable)
			{
				PdfHelper.ExportPdf(pdfContainer.SourcePath, pngDestination, thumbsDestination, pdfContainer.GenerateSingleImage);
				JpegHelper.ConvertFiles(pngDestination, thumbsDatatableDestination);
				logger.LogStage(PreviewFormats.Png);
				logger.LogStage(PreviewFormats.Thumbnails);
				logger.LogStage(PreviewFormats.ThumbnailsForDatatable);
			}

			if (updatePngPhone || updateThumbsPhone)
			{
				PdfHelper.ExportPdfPhone(pdfContainer.SourcePath, pngPhoneDestination, thumbsPhoneDestination, pdfContainer.GenerateSingleImage);
				logger.LogStage(PreviewFormats.PngForMobile);
				logger.LogStage(PreviewFormats.ThumbnailsForMobile);
			}

			var txtDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.Text);
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any()) &&
				pdfContainer.GenerateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);
			if (updateTxt)
			{
				PdfHelper.ExtractText(pdfContainer.SourcePath,
					Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(pdfContainer.SourcePath), "txt")));
				logger.LogStage(PreviewFormats.Text);
			}

			updated = updatePng || updateThumbs || updatePngPhone || updateThumbsPhone || updateTxt;
			if (updated)
			{
				PngHelper.ConvertFiles(pdfContainer.ContainerPath);
				previewContainer.MarkAsModified();
			}

			if (MainController.Instance.Settings.OneDriveSettings.Enabled)
			{
				GenerateOneDriveContent(pdfContainer, cancellationToken);
				logger.LogStage(PreviewFormats.OneDrive);
			}

			logger.FinishLogging();
		}
	}
}
