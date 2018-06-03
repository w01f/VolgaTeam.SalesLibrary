using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using Microsoft.Office.Core;
using Microsoft.Office.Interop.PowerPoint;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Configuration;
using SalesLibraries.Common.Helpers;
using SalesLibraries.Common.OfficeInterops;
using SalesLibraries.FileManager.Controllers;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(PowerPointPreviewContainer))]
	class PowerPointPreviewGenerator : FilePreviewGenerator
	{
		public override void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var powerPointContainer = (PowerPointPreviewContainer)previewContainer;

			var logger = new FilePreviewGenerationLogger(powerPointContainer);
			logger.StartLogging();

			var updated = false;
			var tryCount = 0;
			do
			{
				var pdfDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Pdf);
				var updatePdf = !(Directory.Exists(pdfDestination) && Directory.GetFiles(pdfDestination).Any());
				if (updatePdf && !Directory.Exists(pdfDestination))
					Directory.CreateDirectory(pdfDestination);

				var pngDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Png);
				var updatePng = !(Directory.Exists(pngDestination) && Directory.GetFiles(pngDestination).Any());
				if (updatePng && !Directory.Exists(pngDestination))
					Directory.CreateDirectory(pngDestination);

				var pngPhoneDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.PngForMobile);
				var updatePngPhone = !(Directory.Exists(pngPhoneDestination) && Directory.GetFiles(pngPhoneDestination).Any());
				if (updatePngPhone && !Directory.Exists(pngPhoneDestination))
					Directory.CreateDirectory(pngPhoneDestination);

				var thumbDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Thumbnails);
				var updateThumbs = !(Directory.Exists(thumbDestination) && Directory.GetFiles(thumbDestination).Any());
				if (updateThumbs && !Directory.Exists(thumbDestination))
					Directory.CreateDirectory(thumbDestination);

				var thumbsPhoneDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.ThumbnailsForMobile);
				var updateThumbsPhone = !(Directory.Exists(thumbsPhoneDestination) && Directory.GetFiles(thumbsPhoneDestination, "*.png").Length > 0);
				if (updateThumbsPhone && !Directory.Exists(thumbsPhoneDestination))
					Directory.CreateDirectory(thumbsPhoneDestination);

				var thumbsDatatableDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
				var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());
				if (updateThumbsDatatable && !Directory.Exists(thumbsDatatableDestination))
					Directory.CreateDirectory(thumbsDatatableDestination);

				var pptxDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.PowerPoint);
				var updatePptx = !(Directory.Exists(pptxDestination) && Directory.GetFiles(pptxDestination).Any());
				if (updatePptx && !Directory.Exists(pptxDestination))
					Directory.CreateDirectory(pptxDestination);

				var txtDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.Text);
				var updateTxt = !(Directory.Exists(txtDestination) && Directory.GetFiles(txtDestination).Any()) &&
					powerPointContainer.GenerateText;
				if (updateTxt && !Directory.Exists(txtDestination))
					Directory.CreateDirectory(txtDestination);

				var needToUpdate = updatePdf ||
					updatePng ||
					updateThumbs ||
					updatePptx ||
					updateTxt ||
					updatePngPhone ||
					updateThumbsPhone ||
					updateThumbsDatatable;

				if (!needToUpdate)
					break;

				using (var powerPointProcessor = new PowerPointHidden())
				{
					try
					{
						if (!powerPointProcessor.Connect(true)) continue;
						MessageFilter.Register();

						Presentation presentation = null;
						var processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
						{
							presentation = powerPointProcessor.PowerPointObject.Presentations.Open(previewContainer.SourcePath,
								WithWindow: MsoTriState.msoFalse);
							presentation.Final = false;
						});
						if (processInteropped || presentation == null) continue;

						var content = new StringBuilder();
						if (!cancellationToken.IsCancellationRequested &&
							(updatePng || updateThumbs || updatePptx || updateTxt || updatePngPhone || updateThumbsPhone))
						{
							var slideIndex = 1;
							var thumbHeight = (int)presentation.PageSetup.SlideHeight / 10;
							var thumbWidth = (int)presentation.PageSetup.SlideWidth / 10;
							var phoneHeight = (int)(presentation.PageSetup.SlideHeight / 1.5);
							var phoneWidth = (int)(presentation.PageSetup.SlideWidth / 1.5);
							var thumbPhoneHeight = (int)presentation.PageSetup.SlideHeight / 4;
							var thumbPhoneWidth = (int)presentation.PageSetup.SlideWidth / 4;
							foreach (Slide slide in presentation.Slides)
							{
								if (cancellationToken.IsCancellationRequested) break;
								if (updatePng)
								{
									var index = slideIndex;
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										if (powerPointContainer.GenerateFullImages)
											slide.Export(Path.Combine(pngDestination, String.Format("Slide{0}.{1}", index, "png")), "PNG");
										else if (powerPointContainer.GenerateSingleImage && index == 1)
											slide.Export(Path.Combine(pngDestination, String.Format("{0}Slide.{1}", Constants.SinglePreviewFilePrefixName, "png")), "PNG");
									});
									if (processInteropped)
										break;
								}
								if (updatePngPhone)
								{
									var index = slideIndex;
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										if (powerPointContainer.GenerateFullImages)
											slide.Export(Path.Combine(pngPhoneDestination, String.Format("Slide{0}.{1}", index, "png")), "PNG", phoneWidth,
												phoneHeight);
										else if (powerPointContainer.GenerateSingleImage && index == 1)
											slide.Export(Path.Combine(pngPhoneDestination, String.Format("{0}Slide.{1}", Constants.SinglePreviewFilePrefixName, "png")), "PNG", phoneWidth,
												phoneHeight);
									});
									if (processInteropped)
										break;
								}
								if (updateThumbs)
								{
									var index = slideIndex;
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										if (powerPointContainer.GenerateFullImages)
											slide.Export(Path.Combine(thumbDestination, String.Format("Slide{0}.{1}", index, "png")), "PNG", thumbWidth,
												thumbHeight);
										else if (powerPointContainer.GenerateSingleImage && index == 1)
											slide.Export(Path.Combine(thumbDestination, String.Format("{0}Slide.{1}", Constants.SinglePreviewFilePrefixName, "png")), "PNG", thumbWidth,
												thumbHeight);
									});
									if (processInteropped)
										break;
								}
								if (updateThumbsPhone)
								{
									var index = slideIndex;
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										if (powerPointContainer.GenerateFullImages)
											slide.Export(Path.Combine(thumbsPhoneDestination, String.Format("Slide{0}.{1}", index, "png")), "PNG",
												thumbPhoneWidth, thumbPhoneHeight);
										else if (powerPointContainer.GenerateSingleImage && index == 1)
											slide.Export(Path.Combine(thumbsPhoneDestination, String.Format("{0}Slide.{1}", Constants.SinglePreviewFilePrefixName, "png")), "PNG",
												thumbPhoneWidth, thumbPhoneHeight);
									});
									if (processInteropped)
										break;
								}
								if (updatePptx)
								{
									var index = slideIndex;
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										var singleSlidePresentation =
											powerPointProcessor.PowerPointObject.Presentations.Open(previewContainer.SourcePath,
												WithWindow: MsoTriState.msoFalse);
										var totalSlides = singleSlidePresentation.Slides.Count;
										for (int j = totalSlides; j >= 1; j--)
											if (j != index)
												singleSlidePresentation.Slides[j].Delete();
										singleSlidePresentation.SaveCopyAs(Path.Combine(pptxDestination, String.Format("Slide{0}.{1}", index, "pptx")));
										singleSlidePresentation.Close();
										Utils.ReleaseComObject(singleSlidePresentation);
									});
									if (processInteropped)
										break;
								}
								if (updateTxt)
								{
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										foreach (var shape in slide.Shapes.OfType<Shape>().Where(shape => shape.HasTextFrame == MsoTriState.msoTrue))
											content.AppendLine(shape.TextFrame.TextRange.Text.Trim());
									});
									if (processInteropped)
										break;
								}

								slideIndex++;
							}
							if (processInteropped)
							{
								tryCount++;
								continue;
							}
							if (updatePng)
								logger.LogStage(PreviewFormats.Png);
							if (updatePngPhone)
								logger.LogStage(PreviewFormats.PngForMobile);
							if (updateThumbs)
								logger.LogStage(PreviewFormats.Thumbnails);
							if (updateThumbsPhone)
								logger.LogStage(PreviewFormats.ThumbnailsForMobile);
							if (updatePptx)
								logger.LogStage(PreviewFormats.PowerPoint);
						}
						if (!cancellationToken.IsCancellationRequested && updateTxt)
						{
							using (
								var sw =
									new StreamWriter(
										Path.Combine(txtDestination, Path.ChangeExtension(Path.GetFileName(powerPointContainer.SourcePath), "txt")),
										false))
							{
								sw.Write(content.ToString());
								sw.Flush();
							}
							logger.LogStage(PreviewFormats.Text);
						}

						if (!cancellationToken.IsCancellationRequested && updatePdf)
						{
							processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
								presentation.ExportAsFixedFormat(
									Path.Combine(pdfDestination,
										Path.ChangeExtension(Path.GetFileName(powerPointContainer.SourcePath), "pdf")),
									PpFixedFormatType.ppFixedFormatTypePDF));
							if (processInteropped)
							{
								tryCount++;
								continue;
							}
							logger.LogStage(PreviewFormats.Pdf);
						}

						if (!cancellationToken.IsCancellationRequested && updateThumbsDatatable)
						{
							JpegHelper.ConvertFiles(pngDestination, thumbsDatatableDestination);
							logger.LogStage(PreviewFormats.ThumbnailsForDatatable);
						}

						presentation.Close();
						Utils.ReleaseComObject(presentation);

						if (needToUpdate)
						{
							PngHelper.ConvertFiles(powerPointContainer.ContainerPath);
							previewContainer.MarkAsModified();
						}

						updated = true;
					}
					catch (PreviewGenerationException)
					{
						throw;
					}
					catch { }
					finally
					{
						tryCount++;
						MessageFilter.Revoke();
					}
				}
			} while (!updated && tryCount < 10);

			if (MainController.Instance.Settings.OneDriveSettings.Enabled)
			{
				GenerateOneDriveContent(powerPointContainer, cancellationToken);
				logger.LogStage(PreviewFormats.OneDrive);
			}

			logger.FinishLogging();
		}
	}
}
