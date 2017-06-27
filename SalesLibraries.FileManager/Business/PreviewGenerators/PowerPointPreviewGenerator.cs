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
using SalesLibraries.FileManager.Business.Services;
using Shape = Microsoft.Office.Interop.PowerPoint.Shape;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(PowerPointPreviewContainer))]
	class PowerPointPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var powerPointContainer = (PowerPointPreviewContainer)previewContainer;

			var log = new StringBuilder();
			log.AppendLine(String.Format("Process started at {0:hh:mm:ss tt zz}", DateTime.Now));

			var updated = false;
			var tryCount = 0;
			do
			{
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

				var thumbsDatatableDestination = Path.Combine(powerPointContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
				var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any()) &&
					powerPointContainer.GenerateImages;
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
						var processInteropped = false;
						if (!powerPointProcessor.Connect(true)) continue;
						MessageFilter.Register();

						Presentation presentation = null;
						processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
						{
							presentation = powerPointProcessor.PowerPointObject.Presentations.Open(previewContainer.SourcePath, WithWindow: MsoTriState.msoFalse);
							presentation.Final = false;
						});
						if (processInteropped || presentation == null) continue;

						var content = new StringBuilder();
						if (!cancellationToken.IsCancellationRequested && (updatePng || updateThumbs || updatePptx || updateTxt || updatePngPhone || updateThumbsPhone))
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
								if (cancellationToken.IsCancellationRequested) break;
								if (updatePng)
								{
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										slide.Export(Path.Combine(pngDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG");
									});
									if (processInteropped)
										break;
								}
								if (updatePngPhone)
								{
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										slide.Export(Path.Combine(pngPhoneDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG", phoneWidth, phoneHeight);
									});
									if (processInteropped)
										break;
								}
								if (updateThumbs)
								{
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										slide.Export(Path.Combine(thumbDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG", thumbWidth, thumbHeight);
									});
									if (processInteropped)
										break;
								}
								if (updateThumbsPhone)
								{
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										slide.Export(Path.Combine(thumbsPhoneDestination, String.Format("Slide{0}.{1}", i, "png")), "PNG", thumbPhoneWidth, thumbPhoneHeight);
									});
									if (processInteropped)
										break;
								}
								if (updatePptx)
								{
									processInteropped = powerPointProcessor.DoTimeLimitedAction(() =>
									{
										var singleSlidePresentation = powerPointProcessor.PowerPointObject.Presentations.Open(previewContainer.SourcePath, WithWindow: MsoTriState.msoFalse);
										var totalSlides = singleSlidePresentation.Slides.Count;
										for (int j = totalSlides; j >= 1; j--)
											if (j != i)
												singleSlidePresentation.Slides[j].Delete();
										singleSlidePresentation.SaveCopyAs(Path.Combine(pptxDestination, String.Format("Slide{0}.{1}", i, "pptx")));
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
								i++;
							}
							if (processInteropped)
							{
								tryCount++;
								continue;
							}
							if (updatePng)
								log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Png, DateTime.Now));
							if (updatePngPhone)
								log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.PngForMobile, DateTime.Now));
							if (updateThumbs)
								log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Thumbnails, DateTime.Now));
							if (updateThumbsPhone)
								log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.ThumbnailsForMobile, DateTime.Now));
							if (updatePptx)
								log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.PowerPoint, DateTime.Now));
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
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Text, DateTime.Now));
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
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.Pdf, DateTime.Now));
						}

						if (!cancellationToken.IsCancellationRequested && updateThumbsDatatable)
						{
							JpegGenerator.GenerateDatatableJpegs(pngDestination, thumbsDatatableDestination);
							log.AppendLine(String.Format("{0} generated at {1:hh:mm:ss tt}", PreviewFormats.ThumbnailsForDatatable, DateTime.Now));
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
					catch { }
					finally
					{
						tryCount++;
						MessageFilter.Revoke();
					}
				}
			} while (!updated && tryCount < 10);

			log.AppendLine(String.Format("Process finished at {0:hh:mm:ss tt zz}", DateTime.Now));
			if (Directory.Exists(previewContainer.ContainerPath))
				File.WriteAllText(Path.Combine(previewContainer.ContainerPath, String.Format("log_{0:MMddyy_hhmmsstt}.txt", DateTime.Now)), log.ToString());
		}
	}
}
