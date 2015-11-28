using System.IO;
using System.Linq;
using System.Threading;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(PdfPreviewContainer))]
	class PdfPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var updated = false;
			var pdfContainer = (PdfPreviewContainer)previewContainer;

			var pngDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.Png);
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination).Any()) &&
				pdfContainer.GenerateImages;
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);
			var pngPhoneDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.PngForMobile);
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination).Any()) &&
				pdfContainer.GenerateImages;
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);
			var jpgDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.Jpeg);
			var updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination).Any()) &&
				pdfContainer.GenerateImages;
			if (updateJpg && !Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);
			var jpgPhoneDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.JpegForMobile);
			var updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination).Any()) &&
				pdfContainer.GenerateImages;
			if (updateJpgPhone && !Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);
			var thumbsDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any()) &&
				pdfContainer.GenerateImages;
			if (updateThumbs && !Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);
			var thumbsPhoneDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.ThumbnailsForMobile);
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination).Any()) &&
				pdfContainer.GenerateImages;
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);

			if (updatePng || updateJpg || updateThumbs)
				PdfHelper.ExportPdf(pdfContainer.SourcePath, pngDestination, jpgDestination, thumbsDestination);

			if (updatePngPhone || updateJpgPhone || updateThumbsPhone)
				PdfHelper.ExportPdfPhone(pdfContainer.SourcePath, pngPhoneDestination, jpgPhoneDestination, thumbsPhoneDestination);

			var txtDestination = Path.Combine(pdfContainer.ContainerPath, PreviewFormats.Text);
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any()) &&
				pdfContainer.GenerateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);
			if (updateTxt)
				PdfHelper.ExtractText(pdfContainer.SourcePath, Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(pdfContainer.SourcePath), "txt")));

			updated = updatePng || updateJpg || updateThumbs || updatePngPhone || updateJpgPhone || updateThumbsPhone || updateTxt;
			if (updated)
			{
				PngHelper.ConvertFiles(pdfContainer.ContainerPath);
				previewContainer.MarkAsModified();
			}
		}
	}
}
