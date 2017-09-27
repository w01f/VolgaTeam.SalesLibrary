using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
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
	[IntendForClass(typeof(YoutubePreviewContainer))]
	class YoutubePreviewGenerator : IPreviewGenerator
	{
		public void Generate(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var youtubePreviewContainer = (YoutubePreviewContainer)previewContainer;

			var logger = new UrlPreviewGenerationLogger(youtubePreviewContainer);
			logger.StartLogging();

			var thumbsDestination = Path.Combine(youtubePreviewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());

			var thumbsDatatableDestination = Path.Combine(youtubePreviewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());

			if (!(updateThumbs || updateThumbsDatatable)) return;

			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
			{
				var thumbnailUrl = youtubePreviewContainer.ThumnailUrl;
				var imagePath = Path.Combine(thumbsDestination, "thumbnail.png");
				try
				{
					using (var client = new WebClient())
					{
						using (var stream = client.OpenRead(thumbnailUrl))
						{
							using (var bitmap = new Bitmap(stream))
								bitmap.Save(imagePath, ImageFormat.Png);
							stream.Flush();
							stream.Close();
						}
					}
				}
				catch { }
				if (!Directory.GetFiles(thumbsDestination).Any())
				{
					var thumbnailGenerator = new RegularBrowserThumbnailGenerator();
					thumbnailGenerator.GenerateThumbnail(youtubePreviewContainer.SourcePath, thumbsDestination);
				}
				JpegGenerator.GenerateDatatableJpegs(thumbsDestination, thumbsDatatableDestination);
			}));

			logger.LogStage(PreviewFormats.Thumbnails);
			logger.LogStage(PreviewFormats.ThumbnailsForDatatable);

			youtubePreviewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
