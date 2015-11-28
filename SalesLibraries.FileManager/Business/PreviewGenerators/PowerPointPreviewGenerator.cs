using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(PowerPointPreviewContainer))]
	class PowerPointPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var powerPointContainer = (PowerPointPreviewContainer)previewContainer;

			var pdfDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Pdf);
			var updatePdf = !(Directory.Exists(pdfDestination) && Directory.GetFiles(pdfDestination).Any());
			if (updatePdf && !Directory.Exists(pdfDestination))
				Directory.CreateDirectory(pdfDestination);

			var pngDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Png);
			var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination).Any()) &&
				powerPointContainer.GenerateImages;
			if (updatePng && !Directory.Exists(pngDestination))
				Directory.CreateDirectory(pngDestination);

			var pngPhoneDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.PngForMobile);
			var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination).Any()) &&
				powerPointContainer.GenerateImages;
			if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
				Directory.CreateDirectory(pngPhoneDestination);

			var jpgDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Jpeg);
			var updateJpg = !(Directory.Exists(jpgDestination) && Directory.GetFiles(jpgDestination).Any()) &&
				powerPointContainer.GenerateImages;
			if (updateJpg && !Directory.Exists(jpgDestination))
				Directory.CreateDirectory(jpgDestination);

			var jpgPhoneDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.JpegForMobile);
			var updateJpgPhone = !(Directory.Exists(jpgPhoneDestination) && Directory.GetFiles(jpgPhoneDestination).Any()) &&
				powerPointContainer.GenerateImages;
			if (updateJpgPhone && !Directory.Exists(jpgPhoneDestination))
				Directory.CreateDirectory(jpgPhoneDestination);

			var thumbDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbDestination) && Directory.GetFiles(thumbDestination).Any()) &&
				powerPointContainer.GenerateImages;
			if (updateThumbs && !Directory.Exists(thumbDestination))
				Directory.CreateDirectory(thumbDestination);

			var thumbsPhoneDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.ThumbnailsForMobile);
			var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination, "*.png").Length > 0) &&
				powerPointContainer.GenerateImages;
			if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
				Directory.CreateDirectory(thumbsPhoneDestination);

			var pptxDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.PowerPoint);
			var updatePptx = !(Directory.Exists(pptxDestination) && Directory.GetFiles(pptxDestination).Any());
			if (updatePptx && !Directory.Exists(pptxDestination))
				Directory.CreateDirectory(pptxDestination);

			var txtDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Text);
			var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any()) &&
				powerPointContainer.GenerateText;
			if (updateTxt && !Directory.Exists(txtDestination))
				Directory.CreateDirectory(txtDestination);

			var updated = updatePdf ||
				updatePng ||
				updateJpg ||
				updateThumbs ||
				updatePptx ||
				updateTxt ||
				updatePngPhone ||
				updateJpgPhone ||
				updateThumbsPhone;

			if (!updated) return;
			try
			{
				if (PowerPointHelper.Instance.ConnectHidden())
				{
					MessageFilter.Register();
					var presentation = PowerPointHelper.Instance.PowerPointObject.Presentations.Open(previewContainer.SourcePath, WithWindow: MsoTriState.msoFalse);

					var content = new StringBuilder();

					if (updatePng || updateJpg || updateThumbs || updatePptx || updateTxt || updatePngPhone || updateJpgPhone || updateThumbsPhone)
					{
						var i = 1;
						var thumbHeight = (int)presentation.PageSetup.SlideHeight / 10;
						var thumbWidth = (int)presentation.PageSetup.SlideWidth / 10;
						var phoneHeight = (int)(presentation.PageSetup.SlideHeight / 1.5);
						var phoneWidth = (int)(presentation.PageSetup.SlideWidth / 1.5);
						var thumbPhoneHeight = (int)presentation.PageSetup.SlideHeight / 4;
						var thumbPhoneWidth = (int)presentation.PageSetup.SlideWidth / 4;
						foreach (Slide slide in presentation.Slides)
						{
							if (updatePng)
								slide.Export(Path.Combine(pngDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG");
							if (updatePngPhone)
								slide.Export(Path.Combine(pngPhoneDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG", phoneWidth, phoneHeight);
							if (updateJpg)
								slide.Export(Path.Combine(jpgDestination, String.Format("Slide{0}.{1}", i, "jpg")), "JPG");
							if (updateJpgPhone)
								slide.Export(Path.Combine(jpgPhoneDestination, String.Format("Slide{0}.{1}", i, "jpg")), "JPG", phoneWidth, phoneHeight);
							if (updateThumbs)
								slide.Export(Path.Combine(thumbDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG", thumbWidth, thumbHeight);
							if (updateThumbsPhone)
								slide.Export(Path.Combine(thumbsPhoneDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG", thumbPhoneWidth, thumbPhoneHeight);

							if (updatePptx)
							{
								var singleSlidePresentation = PowerPointHelper.Instance.PowerPointObject.Presentations.Add(MsoTriState.msoFalse);
								PowerPointHelper.Instance.CopyPasteSlide(slide, singleSlidePresentation);
								if (updatePptx)
									singleSlidePresentation.SaveCopyAs(Path.Combine(pptxDestination, String.Format("Slide{0}.{1}", i, "pptx")));
								singleSlidePresentation.Close();
								Utils.ReleaseComObject(singleSlidePresentation);
							}
							if (updateTxt)
							{
								foreach (var shape in slide.Shapes.OfType<Shape>().Where(shape => shape.HasTextFrame == MsoTriState.msoTrue))
									content.AppendLine(shape.TextFrame.TextRange.Text.Trim());
							}
							i++;
						}
					}

					if (updateTxt && content.Length > 0)
						using (var sw = new StreamWriter(Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(powerPointContainer.SourcePath), "txt")), false))
						{
							sw.Write(content.ToString());
							sw.Flush();
						}

					if (updatePdf)
						presentation.ExportAsFixedFormat(Path.Combine(pdfDestination, Path.ChangeExtension(Path.GetFileName(powerPointContainer.SourcePath), "pdf")), PpFixedFormatType.ppFixedFormatTypePDF);
					Utils.ReleaseComObject(presentation);
				}
			}
			catch { }
			finally
			{
				MessageFilter.Revoke();
				PowerPointHelper.Instance.Disconnect();
			}

			if (updated)
			{
				PngHelper.ConvertFiles(powerPointContainer.ContainerPath);
				previewContainer.MarkAsModified();
			}
		}
	}
}
