using System.IO;
using System.Linq;
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
	[IntendForClass(typeof(Html5PreviewContainer))]
	class Html5PreviewGenerator : IPreviewContentGenerator
	{
		public void GeneratePreviewContent(BasePreviewContainer previewContainer, CancellationToken cancellationToken)
		{
			var html5PreviewContainer = (Html5PreviewContainer)previewContainer;

			var logger = new UrlPreviewGenerationLogger(html5PreviewContainer);
			logger.StartLogging();

			var thumbsDestination = Path.Combine(html5PreviewContainer.ContainerPath, PreviewFormats.Thumbnails);
			var updateThumbs = !(Directory.Exists(thumbsDestination) && Directory.GetFiles(thumbsDestination).Any());

			var thumbsDatatableDestination = Path.Combine(html5PreviewContainer.ContainerPath, PreviewFormats.ThumbnailsForDatatable);
			var updateThumbsDatatable = !(Directory.Exists(thumbsDatatableDestination) && Directory.GetFiles(thumbsDatatableDestination).Any());

			if (!(updateThumbs || updateThumbsDatatable)) return;

			if (!Directory.Exists(thumbsDestination))
				Directory.CreateDirectory(thumbsDestination);

			if (!Directory.Exists(thumbsDatatableDestination))
				Directory.CreateDirectory(thumbsDatatableDestination);

			MainController.Instance.MainForm.Invoke(new MethodInvoker(() =>
			{
				var thumbnailGenerator = new RegularBrowserThumbnailGenerator();
				thumbnailGenerator.GenerateThumbnail(html5PreviewContainer.ThumnailUrl, thumbsDestination, alternativeUrl: MainController.Instance.Settings.WebServiceSite);
			}));
			JpegHelper.ConvertFiles(thumbsDestination, thumbsDatatableDestination);

			logger.LogStage(PreviewFormats.Thumbnails);
			logger.LogStage(PreviewFormats.ThumbnailsForDatatable);

			html5PreviewContainer.MarkAsModified();

			logger.FinishLogging();
		}
	}
}
