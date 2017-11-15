using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading;
using System.Windows.Forms;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SalesLibraries.Business.Entities.Interfaces;
using SalesLibraries.Business.Entities.Wallbin.Common.Constants;
using SalesLibraries.Business.Entities.Wallbin.Persistent.PreviewContainers;
using SalesLibraries.Common.Helpers;
using SalesLibraries.FileManager.Business.Services;
using SalesLibraries.FileManager.Controllers;

namespace SalesLibraries.FileManager.Business.PreviewGenerators
{
	[IntendForClass(typeof(VimeoPreviewContainer))]
	class VimeoPreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var vimeoPreviewContainer = (VimeoPreviewContainer)previewContainer;

			var logger = new UrlPreviewGenerationLogger(vimeoPreviewContainer);
			logger.StartLogging();

			var thumbsDestination = Path.Combine(vimeoPreviewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());

			var thumbsDatatableDestination = Path.Combine(vimeoPreviewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());

			if (!(updateThumbs || updateThumbsDatatable)) return;

			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			var infoUrl = vimeoPreviewContainer.VimeoInfoUrl;
			if (!String.IsNullOrEmpty(infoUrl))
			{
				var imagePath = Path.Combine(thumbsDestination, "thumbnail.png");
				try
				{
					using (var client = new WebClient())
					{
						var text = client.DownloadString(infoUrl);
						if (!String.IsNullOrEmpty(text))
						{
							dynamic vimeoInfo = JArray.Parse(text);
							var imageUrl = vimeoInfo[0].thumbnail_large?.Value;
							using (var stream = client.OpenRead(imageUrl))
							{
								using (var bitmap = new Bitmap(stream))
									bitmap.Save(imagePath, ImageFormat.Png);
								stream.Flush();
								stream.Close();
							}
						}
					}
				}
				catch { }
			}

			if (!Directory.GetFiles(thumbsDestination).Any())
			{
				MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
				{
					var thumbnailGenerator = new RegularBrowserThumbnailGenerator();
					thumbnailGenerator.GenerateThumbnail(vimeoPreviewContainer.SourcePath, thumbsDestination);
				}));
			}
			JpegHelper.ConvertFiles(thumbsDestination, thumbsDatatableDestination);

			logger.LogStage(PreviewFormats.Thumbnails);
			logger.LogStage(PreviewFormats.ThumbnailsForDatatable);

			vimeoPreviewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
